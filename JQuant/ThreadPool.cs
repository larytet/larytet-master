
using System;
using System.Threading;
using System.Collections.Generic;

namespace JQuant
{

    /// <summary>
    /// this method will be called from a context of dynamically allocated
    /// thread
    /// </summary>
    public delegate void Job(object argument);

    /// <summary>
    /// this method will be called when job just before thread exists
    /// </summary>
    public delegate void JobDone(object argument);
    
    /// <summary>
    /// Similar to the System.Threading.ThreadPool
    /// Every subsystem can create it's own pool of threads with specified number of
    /// threads. Pay attention, that there can be OS related limitations for the total  
    /// number of threads coexisting in the system.
    /// </summary>
    public class ThreadPool : IResourceThreadPool
    {
        
        public ThreadPool(string name, int size)
        {
            this.Name = name;
            this.Size = size;
            MinThreadsFree = size;
            countStart = 0;
            countDone = 0;
            
            jobThreads = new Stack<JobThread>(size);
            for (int i = 0;i < size;i++)
            {
                JobThread jobThread = new JobThread(this);
                
                jobThreads.Push(jobThread);
            }
        }

        /// <summary>
        /// Executes job in a thread, calls jobDone after the job done
        /// </summary>
        /// <param name="job">
        /// A <see cref="Job"/>
        /// </param>
        /// <param name="jobDone">
        /// A <see cref="JobDone"/>
        /// </param>
        /// <param name="jobArgument">
        /// A <see cref="System.Object"/>
        /// Arbitrary data specified by the application. Can be null
        /// </param>
        /// <returns>
        /// A <see cref="System.Boolean"/>
        /// </returns>
        public bool DoJob(Job job, JobDone jobDone, object jobArgument)
        {
            bool result = false;
            JobThread jobThread = default(JobThread);
            
            do
            {
                // allocate a free thread
                lock (jobThreads)
                {
                    if (jobThreads.Count > 0)
                    {
                        jobThread = jobThreads.Pop();
                        if (MinThreadsFree > jobThreads.Count)
                        {
                            MinThreadsFree = jobThreads.Count;
                        }
                        countStart++;
                    }
                    else // no job threads available
                    {
                        break;
                    }
                }

                jobThread.Start(job, jobDone, jobArgument);

                    
                result = true;
            }
            while (false);
            
            return result;
        }

        protected void JobDone(JobThread jobThread)
        {
            lock (jobThreads)
            {
                countDone++;
                jobThreads.Push(jobThread);
            }
        }

        public int GetSize()
        {
            return Size;
        }
        
        public string GetName()
        {
            return Name;
        }
        
        public int GetMaxCount()
        {
            return (Size-MinThreadsFree);
        }
        
        public int GetCountStart()
        {
            return countStart;
        }
        
        public int GetCountDone()
        {
            return countDone;
        }
        
        protected string Name;
        protected int Size;
        protected int MinThreadsFree;
        protected int countStart;
        protected int countDone;

        Stack<JobThread> jobThreads;

        protected class JobThread
        {
            public JobThread(ThreadPool threadPool)
            {
                thread = new Thread(Run);
                thread.IsBackground = true;
                IsRunning = false;
                this.threadPool = threadPool;
            }

            public void Start(Job job, JobDone jobDone, object jobArgument)
            {
                this.job = job;
                this.jobDone = jobDone;
                this.jobArgument = jobArgument;

                lock (this)
                {
                    Monitor.Pulse(this);
                }
            }

            public void Run()
            {
                while (true)
                {
                    lock (this)
                    {
                        Monitor.Wait(this);
                    }
                    
                    IsRunning = true;

                    // execute the job and notify the application
                    job(jobArgument);
                    jobDone(jobArgument);

                    // back to the ThreadPool
                    IsRunning = false;
                    threadPool.JobDone(this);
                }
            }

            public Job job
            {
                get;
                set;
            }
            
            public JobDone jobDone
            {
                get;
                set;
            }
            
            public object jobArgument
            {
                get;
                set;
            }

            public bool IsRunning
            {
                get;
                set;
            }

            protected ThreadPool threadPool;
            
            Thread thread;
        }

    }
}
