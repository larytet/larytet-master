
using System;
using System.Threading;
using System.Timers;
using System.Collections.Generic;
using System.ComponentModel;

/// <summary>
/// The idea is taken from http://larytet.sourceforge.net/aos.shtml  (AOS Timer)
/// In the CSharp there is System.Threading.Timer. The service comes at cost - a separate thread
/// for every timer. This is proprietary implemenation for the application timers
///   Terminology:
///   Timer list - queue of the running timers with the SAME timeout. For example list of 1s timers
///   Set - one or more lists of timers and a task handling the lists.
///       For example set A containing 1s timers, 2s timers and 5s timers
///       and set B containing 100 ms timers, 50 ms timers and 200 ms timers
///   Timer task - task that handles one and only one set of lists of timers
/// 
///                      -----------   Design   ---------------
/// 
///   In the system run one or more timer tasks handling different timer sets. Every timer
///   set contains one or more timer lists.
///   Function start timer allocates free entry from the stack of free timers and places
///   the timer in the end of the timer list (FIFO). Time ~O(1)
///   Timer task waits for the expiration of the nearest timer, calls application handler
///   TimerExpired, find the next timer to be expired using sequential search in the
///   set (always the first entry in a timer list). Time ~ O(size of set)
///   Function stop timer marks a running timer as stopped. Time ~O(1)
/// 
///                      -----------   Reasons  ---------------
/// 
///   1. It is possible that every subsystem will have own timer tasks running in
///      different priorities
///   2. Set of long timers and set of short timers can be created and handled by tasks with
///      different priorities
///   3. "Timer expired"  application handlers can be called from different tasks. For high
///      priority short timers such handler should be short - release semaphore for example,
///      for low priority long timers handler can make long processing like audit in data-base
///   4. In the system can coexist 1 or 2 short timers - 50 ms - used in call process
///      and 10 long timers  - 10 s, 1 min, 1 h, etc. - used in the application
///      sanity checking or management
///   5. In the system can coexist short - 10 ms - timer that always expired and 10 long
///      protocol timers that ususally stopped by the application before expiration
///                      -----------   Usage examples  ---------------
/// </summary>
namespace JQuant
{

    /// <summary>
    /// application calls TimersInit.Init() before any other operation
    /// </summary>
    public class TimersInit
    {
        /// <summary>
        /// this methos should be called once by the application 
        /// before any of the API will be used
        /// </summary>
        public static void Init()
        {
            TimerId.Init();
        }
    }

    public enum Error
    {
        [Description("None")]
        NONE,
        
        [Description("Already stoped")]
        ALREADY_STOPED,
        
        [Description("wrong timer ID")]
        WRONG_TIMER_ID,
        
        [Description("Unknown")]
        STOP_UNKNOWN
    };
           
    
    
    public class Timer
    {
        /// <summary>
        /// this is expiration tick (milliseconds)
        /// </summary>
        public long ExpirationTime;

        /// <summary>
        /// system tick when the timer was started  (milliseconds)
        /// </summary>
        public long StartTick;

        /// <summary>
        /// application is free to use this field
        /// </summary>
        public object ApplicationHook;

        /// <summary>
        /// true if not stopped
        /// </summary>
        public bool Running;

        public long TimerId;
    }

    public delegate void TimerExpiredCallback(Timer timer);
    
    /// <summary>
    /// lits of timers - keep all timers with the same timeout
    /// </summary>
    public class TimerList : ITimerList
    {
        /// <summary>
        /// Create a timer list
        /// </summary>
        /// <param name="name">
        /// A <see cref="System.String"/>
        /// Name of the timer list
        /// </param>
        /// <param name="size">
        /// A <see cref="System.Int32"/>
        /// Maximum number of pending timers
        /// </param>
        /// <param name="timerCallback">
        /// A <see cref="TimerExpiredCallback"/>
        /// This method will be called for all expired timers
        /// There is no a callback per timer. Only a callback per timer list
        /// </param>
        public TimerList(string name, int size, TimerExpiredCallback timerCallback, TimerTask timerTask)
        {
            // add myself to the list of created timer lists
            Resources.TimerLists.Add(this);

            this.name = name;
            this.timerCallback = timerCallback;
            this.baseTick = DateTime.Now.Ticks;
            this.timerTask = timerTask;

            // create pool of free timers
            InitTimers(size);
        }

        protected void Dispose()
        {
            // remove myself from the list of created timer lists
            Resources.TimerLists.Remove(this);
        }

        /// <summary>
        /// Allocate a free timer from the stack of free timers, set expiration
        /// add the timer to end of the list of running timers
        /// </summary>
        /// <param name="timeout">
        /// A <see cref="System.Int64"/>
        /// </param>
        /// <param name="timer">
        /// A <see cref="Timer"/>
        /// Reference to the started timer. Can be used in call to Stop()
        /// </param>
        /// <param name="timerId">
        /// A <see cref="System.Int64"/>
        /// Timer list reuse refernces (objects) of type Timer. Reference to Timer is
        /// not enough to make sure that you stop the correct timer. Value timerId
        /// is a unique (system level) timer identifier
        /// </param>
        /// <param name="applicationHook">
        /// A <see cref="System.Object"/>
        /// Timer.applicationHook field will be set to this value
        /// Field applicationHook can help to identify the timer in the context of timerExpired
        /// callback
        /// </param>
        /// <returns>
        /// A <see cref="System.Boolean"/>
        /// returns true of Ok, false is failed to allocate a new timer - no free
        /// timers are available
        /// </returns>
        public bool StartTimer(long timeout, out Timer timer, out long timerId, object applicationHook)
        {
            
            // timestamp the call as soon as possible
            DateTime startTime = DateTime.Now;
            long startTick = (startTime.Ticks - baseTick) * TICKS_IN_MILLISECOND;

                
            timer = null;
            timerId = 0;
            bool result = false;

            
            do
            {
                // get new timer Id
                timerId = TimerId.GetNext();

                // allocate a timer from the stack of free timers
                lock (this)
                {
                    if (freeTimers.Count > 0)
                    {
                        timer = freeTimers.Pop();
                    }
                    else
                    {
                        break;
                    }                    
                }

                // initialize the timer
                timer.ApplicationHook = applicationHook;
                timer.StartTick = startTick;
                timer.ExpirationTime = startTick+timeout;
                timer.Running = true;
                timer.TimerId = timerId;

                // add the timer to the queue of the pending timers
                lock (this)
                {
                    pendingTimers.Add(timer);
                }

                // send wakeup call to the task handling the timers
                timerTask.WakeupCall();
                
                result = true;
            }
            while (false);

            return result;
        }

        /// <summary>
        /// returns the timeout before the nearest timer expires
        /// </summary>
        /// <returns>
        /// A <see cref="System.Int64"/>
        /// Returns true if there is a timer in the pending list
        /// </returns>
        public bool GetDelay(long currentTick, out long delay)
        {
            bool result = false;
            
            lock (this)
            {
                if (pendingTimers.Count > 0)
                {
                    Timer timer = pendingTimers[0];
                    delay = timer.ExpirationTime - currentTick;
                    result = true;
                }
                else
                {
                    delay = long.MaxValue;
                }
            }

            return result;
        }

        /// <summary>
        /// use this method if no need to call Stop() will ever arise for the timer
        /// </summary>
        public bool StartTimer(long timeout, object applicationHook)
        {
            Timer timer;
            long timerId;
            
            bool result = StartTimer(timeout, out timer, out timerId, applicationHook);
            
            return result;
        }

        /// <summary>
        /// stop previously started timer
        /// </summary>
        /// <param name="timer">
        /// A <see cref="Timer"/>
        /// </param>
        /// <param name="timerId">
        /// A <see cref="System.Int64"/>
        /// Value returned by StartTimer()
        /// Timer list reuse refernces (objects) of type Timer. Reference to Timer object 
        /// is not enough to make sure that you stop the correct timer. Value timerId
        /// is a unique (system level) timer identifier
        /// </param>
        /// <returns>
        /// A <see cref="System.Boolean"/>
        /// Returns true if the timer was running and now stopped
        /// Call to this method for already stoped timer will return false
        /// and error message will be printed on the console
        /// </returns>
        public bool StopTimer(Timer timer, long timerId)
        {
            Error error = Error.NONE;
            
            lock (this)
            {
                if ((timer.Running) && (timer.TimerId == timerId))
                {
                    timer.Running = false;
                }
                else if (!timer.Running)
                {
                    error = Error.ALREADY_STOPED;
                }
                else if (timer.TimerId != timerId)
                {
                    error = Error.STOP_UNKNOWN;
                }
            }

            if (error != Error.NONE)
            {
                PrintError("Stop failed ", error);
            }
            
            return (error == Error.NONE);
        }

        protected void PrintError(string prefix, Error error)
        {
            System.Console.WriteLine(prefix+EnumUtils.GetDescription(error));
        }

        /// <summary>
        /// free timers are stored in the stack
        /// </summary>
        /// <param name="size">
        /// A <see cref="System.Int32"/>
        /// </param>
        protected void InitTimers(int size)
        {
            freeTimers = new Stack<Timer>(size);
            for (int i = 0; i < size; i++)
            {
                Timer timer = new Timer();
                freeTimers.Push(timer);
            }

            // list of pending timers - initially empty
            pendingTimers = new List<Timer>(size);
        }

        ~TimerList()
        {
            Console.WriteLine("TimerList " + name + " destroyed");
        }
        
        public int GetPendingTimers()
        {
            return 0;
        }
        
        public int GetCountStart()
        {
            return 0;
        }
        
        public int GetCountStop()
        {
            return 0;
        }
        
        public int GetCountExpired()
        {
            return 0;
        }
        
        /// <summary>
        /// stack of free timers
        /// </summary>
        protected Stack<Timer> freeTimers;


        /// <summary>
        /// List of pending timers. TimerList contains timers with the same timeout
        /// The oldest is going to be in the tail of the list
        /// </summary>
        protected List<Timer> pendingTimers;
        
        protected string name;

        /// <summary>
        /// this method will be called for every expired timer
        /// </summary>
        protected TimerExpiredCallback timerCallback;

        /// <summary>
        /// to save tick wrap arounds all ticks are going to be calculated as 
        /// offsets to this value
        /// </summary>
        protected long baseTick;

        protected const int TICKS_IN_MILLISECOND = 10000;

        protected TimerTask timerTask;
    }
    
    /// <summary>
    /// Timer task (timer set) implementation
    /// </summary>
    public class TimerTask
    {
        
        public TimerTask(string name)
        {
            this.name = name;
            this.isAlive = false;            
        }

        protected void Dispose()
        {
            thread.Abort();
            thread = null;
        }


        /// <summary>
        /// call this method to start the thread - enters loop forever in the 
        /// method Run()
        /// </summary>
        public void Start()
        {
            this.thread = new Thread(this.Run);
            this.thread.Start();
        }

        public void Stop()
        {
            isAlive = false;
            Monitor.Pulse(this);
        }

        public void WakeupCall()
        {
        }
        
        ~TimerTask()
        {
            Console.WriteLine("TimerTask " + name + " destroyed");
        }


        protected void Run()
        {
            isAlive = true;

            // Check all timer lists, find minimum delay (timeout before the nearest timer expires)
            // call Thread.Sleep()
            while (isAlive)
            {
            }
        }
        

        protected Thread thread;
        protected bool isAlive;
        protected string name;

    }


    /// <summary>
    /// generates a unique timer identifier - simple counter
    /// </summary>
    class TimerId
    {
        protected TimerId(long initialValue)
        {
            timerId = initialValue;
        }

        public static void Init()
        {
            if (instance == default(TimerId))
            {
                instance = new TimerId(0xB5);
            }
        }

        /// <summary>
        /// Locks access to the timerId
        /// should not be called from inside locked section
        /// </summary>
        /// <returns>
        /// A <see cref="System.Int64"/>
        /// Unique on the system level timer identifier
        /// </returns>
        static public long GetNext()
        {
            long id;
            
            lock (instance)
            {
                timerId++;
                id = timerId;
            }
            
            return id;
        }
        
        static TimerId instance;
        static long timerId;
    }
}
