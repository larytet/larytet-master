
using System;
using System.Threading;
using System.IO;
using System.Collections.Generic;

namespace JQuant
{


    partial class Program
    {

        #region Oper Callbacks
        // Operations are intended to run in the real TaskBar environment - no simulated API

        protected void operLoginCallback(IWrite iWrite, string cmdName, object[] cmdArguments)
        {
            //This is shortcut - the path works for VS only!  
            string ConnFile = @"C:\Documents and Settings\Aryeh\My Documents\SVN\JQuant\ConnectionParameters.xml";
            this.MyConn = new FMRShell.Connection(ConnFile);

            bool openResult;
            int errResult;
            openResult = this.MyConn.Open(iWrite, out errResult, true);

            iWrite.WriteLine("");
            if (openResult)
            {
                iWrite.WriteLine("Connection opened for " + this.MyConn.GetUserName());
                iWrite.WriteLine("sessionId=" + errResult);
            }
            else
            {
                iWrite.WriteLine("Connection failed errResult=" + errResult);
                iWrite.WriteLine("Error description: " + this.MyConn.LoginErrorDesc());
            }

            iWrite.WriteLine("Login status is " + this.MyConn.loginStatus.ToString());
        }

        protected void operLogoutCallback(IWrite iWrite, string cmdName, object[] cmdArguments)
        {
            if (MyConn != null)
            {
                int s = this.MyConn.GetSessionId();
                this.MyConn.Dispose();
                this.MyConn = null;  //set connection to null
                Console.WriteLine("Session with id " + s + " was terminated.");
            }
            else
            {
                Console.WriteLine("There is no active connection - you're not logged in.");
            }
        }

        protected string DateNowToFilename()
        {
            string s = DateTime.Now.ToString();
            s = s.Replace('/', '_');
            s = s.Replace(':', '_');
            s = s.Replace(' ', '_');

            return s;
        }
        
        protected void operLogMaofCallback(IWrite iWrite, string cmdName, object[] cmdArguments)
        {
            // generate filename, display it
            string filename = "maofLog_" + DateNowToFilename() + ".txt";
            iWrite.WriteLine("Maof log file " + filename);

            OpenStreamAndLog(iWrite, false, FMRShell.DataType.Maof, filename, "MaofLogger");
        }

        protected void operLogMadadCallBack(IWrite iWrite, string cmdName, object[] cmdArguments)
        {
            // generate filename
            string filename = "MadadLog_" + DateNowToFilename() + ".txt";
            iWrite.WriteLine("Madad log file " + filename);

            OpenStreamAndLog(iWrite, false, FMRShell.DataType.Madad, filename, "MadadLogger");
        }

        protected void operLogRezefCallBack(IWrite iWrite, string cmdName, object[] cmdArguments)
        {
            // generate filename
            string filename = "RezefLog_" + DateNowToFilename() + ".txt";
            iWrite.WriteLine("Rezef log file " + filename);
            
            OpenStreamAndLog(iWrite, false, FMRShell.DataType.Rezef, filename, "RezefLogger");
        }

        protected void operStopLogCallback(IWrite iWrite, string cmdName, object[] cmdArguments)
        {
            bool _stopStream = false;
            if (cmdArguments.Length == 1) iWrite.WriteLine("Please supply data type: MF | RZ | MDD");
            else if (cmdArguments.Length > 3) iWrite.WriteLine("Too many arguments");
            else
            {
                if (cmdArguments.Length == 3)
                {
                    _stopStream = (cmdArguments[2].ToString().ToLower() == "y");
                    Console.WriteLine(_stopStream);
                }
                switch (cmdArguments[1].ToString().ToLower())
                {
                    case "maof":
                        CloseLog(iWrite, FMRShell.DataType.Maof, _stopStream);
                        break;
                    case "mf":
                        CloseLog(iWrite, FMRShell.DataType.Maof, _stopStream);
                        break;
                    case "rezef":
                        CloseLog(iWrite, FMRShell.DataType.Rezef, _stopStream);
                        break;
                    case "rz":
                        CloseLog(iWrite, FMRShell.DataType.Rezef, _stopStream);
                        break;
                    case "madad":
                        CloseLog(iWrite, FMRShell.DataType.Madad, _stopStream);
                        break;
                    case "mdd":
                        CloseLog(iWrite, FMRShell.DataType.Madad, _stopStream);
                        break;
                    default:
                        iWrite.WriteLine("Invalid data type parameter: " + cmdArguments[1].ToString());
                        break;
                }
            }
        }
        
        #endregion;

        #region Debug Callbacks

        protected void debugPrintResourcesNameAndStats(IWrite iWrite, System.Collections.ArrayList list)
        {
            int entry = 0;
            int columnSize = 8;

            bool isEmpty = true;

            iWrite.WriteLine();

            foreach (INamedResource resNamed in list)
            {
                isEmpty = false;

                IResourceStatistics resStat = (IResourceStatistics)resNamed;

                System.Collections.ArrayList names;
                System.Collections.ArrayList values;
                resStat.GetEventCounters(out names, out values);

                if (entry == 0)
                {
                    names.Insert(0, "Name");
                    CommandLineInterface.printTableHeader(iWrite, names, columnSize);
                }
                values.Insert(0, OutputUtils.FormatField(resNamed.Name, columnSize));
                CommandLineInterface.printValues(iWrite, values, columnSize);

                entry++;

            }
            if (isEmpty)
            {
                System.Console.WriteLine("Table is empty - no resources registered");
            }
        }

        protected void debugMbxShowCallback(IWrite iWrite, string cmdName, object[] cmdArguments)
        {
            debugPrintResourcesNameAndStats(iWrite, Resources.Mailboxes);
        }

        protected void debugMbxTestCallback(IWrite iWrite, string cmdName, object[] cmdArguments)
        {
            Mailbox<bool> mbx = new Mailbox<bool>("TestMbx", 2);

            iWrite.WriteLine("TestMbx created");
            bool message = true;
            bool result = mbx.Send(message);
            if (!result)
            {
                iWrite.WriteLine("Mailbox.Send returned false");
            }
            else
            {
                iWrite.WriteLine("Mailbox.Send message sent");
            }
            result = mbx.Receive(out message);
            if (!result)
            {
                iWrite.WriteLine("Mailbox.Receive returned false");
            }
            else
            {
                iWrite.WriteLine("Mailbox.Send message received");
            }
            if (!message)
            {
                iWrite.WriteLine("I did not get what i sent");
            }
            debugMbxShowCallback(iWrite, cmdName, cmdArguments);

            mbx.Dispose();

            System.GC.Collect();
        }

        protected void debugThreadTestCallback(IWrite iWrite, string cmdName, object[] cmdArguments)
        {
            debugThreadShowCallback(iWrite, cmdName, cmdArguments);
            MailboxThread<bool> thr = new MailboxThread<bool>("TestMbx", 2);
            debugThreadShowCallback(iWrite, cmdName, cmdArguments);
            thr.Start();
            debugThreadShowCallback(iWrite, cmdName, cmdArguments);
            bool message = true;
            bool result = thr.Send(message);
            if (!result)
            {
                iWrite.WriteLine("Thread.Send returned false");
            }
            thr.Stop();
            debugThreadShowCallback(iWrite, cmdName, cmdArguments);

            System.GC.Collect();
        }

        protected void debugThreadShowCallback(IWrite iWrite, string cmdName, object[] cmdArguments)
        {
            iWrite.WriteLine();
            iWrite.WriteLine(
                OutputUtils.FormatField("Name", 10) +
                OutputUtils.FormatField("State", 14) +
                OutputUtils.FormatField("Ticks", 10)
            );
            iWrite.WriteLine("-------------------------------------------");
            bool isEmpty = true;
            foreach (IThread iThread in Resources.Threads)
            {
                isEmpty = false;
                iWrite.WriteLine(
                    OutputUtils.FormatField(iThread.Name, 10) +
                    OutputUtils.FormatField(EnumUtils.GetDescription(iThread.GetState()), 14) +
                    OutputUtils.FormatField(iThread.GetLongestJob(), 10)
                );

            }
            if (isEmpty)
            {
                iWrite.WriteLine("No threads");
            }
            int workerThreads;
            int completionPortThreads;
            System.Threading.ThreadPool.GetAvailableThreads(out workerThreads, out completionPortThreads);
            iWrite.WriteLine("workerThreads=" + workerThreads + ",completionPortThreads=" + completionPortThreads);

        }

        protected void debugGcCallback(IWrite iWrite, string cmdName, object[] cmdArguments)
        {
            System.GC.Collect();
            System.GC.WaitForPendingFinalizers();
            iWrite.WriteLine("Garbage collection done");
        }

        protected void debugPoolShowCallback(IWrite iWrite, string cmdName, object[] cmdArguments)
        {
            debugPrintResourcesNameAndStats(iWrite, Resources.Pools);
        }

        protected void debugLoginCallback(IWrite iWrite, string cmdName, object[] cmdArguments)
        {
            //This solution is only temporary -  we need to add a method setting and changing path variables
            string path;
            //Depending on the environment, set the path:
#if USEVS
            path = @"C:\Documents and Settings\Aryeh\My Documents\SVN\JQuant\";
#else
            path="";
#endif
            string ConnFile = path + "ConnectionParameters.xml";
            this.MyConn = new FMRShell.Connection(ConnFile);

            bool openResult;
            int errResult;
            openResult = this.MyConn.Open(iWrite, out errResult, true);

            iWrite.WriteLine("");
            if (openResult)
            {
                iWrite.WriteLine("Connection opened for " + this.MyConn.GetUserName());
                iWrite.WriteLine("sessionId=" + errResult);
            }
            else
            {
                iWrite.WriteLine("Connection failed errResult=" + errResult);
                iWrite.WriteLine("Error description: " + this.MyConn.LoginErrorDesc());
            }

            iWrite.WriteLine("Login status is " + this.MyConn.loginStatus.ToString());
        }

        protected void debugLogoutCallback(IWrite iWrite, string cmdName, object[] cmdArguments)
        {
            if (MyConn != null)
            {
                int s = MyConn.GetSessionId();
                this.MyConn.Dispose();
                MyConn = null;  //set connection to null
                Console.WriteLine("Session with id " + s + " was terminated.");
            }
            else
            {
                Console.WriteLine("There is no active connection - you're not logged in.");
            }
        }

        protected void debugPoolTestCallback(IWrite iWrite, string cmdName, object[] cmdArguments)
        {
            Pool<bool> pool = new Pool<bool>("TestPool", 2);

            bool message1 = true;
            bool message2 = false;
            pool.Fill(message1); pool.Fill(message2);

            bool result = pool.Get(out message1);
            if (!result)
            {
                iWrite.WriteLine("Pool.Get returned false");
            }
            if (message1)
            {
                iWrite.WriteLine("I did not get what i stored");
            }
            pool.Free(message1);
            debugPoolShowCallback(iWrite, cmdName, cmdArguments);

            pool.Dispose();

            System.GC.Collect();
        }

        protected void debugGetAS400DTCallback(IWrite iWrite, string cmdName, object[] cmdArguments)
        {
            int ltncy;
            DateTime dt;

            //ping every 2 seconds, 60 times, write the output to the console
            //TODO - write to a file instead of console, make it a separate thread
            for (int i = 0; i < 60; i++)
            {
                FMRShell.AS400Synch.Ping(out dt, out ltncy);
                Console.Write(FMRShell.AS400Synch.ToShortCSVString(dt, ltncy));
                Thread.Sleep(2000);
            }
        }

        protected void debugLoggerTestCallback(IWrite iWrite, string cmdName, object[] cmdArguments)
        {
            OpenStreamAndLog(iWrite, true, FMRShell.DataType.Maof, "simLog.txt", "simLogger");
        }

        protected void debugOperationsLogMaofCallback(IWrite iWrite, string cmdName, object[] cmdArguments)
        {
            // generate filename
            string filename = "maofLog." + DateTime.Now.ToString() + ".txt";
            filename = filename.Replace('/', ' ');
            filename = filename.Replace(' ', '_');
            iWrite.WriteLine("Log file " + filename);
            OpenStreamAndLog(iWrite, false, FMRShell.DataType.Maof, filename, "MaofLogger");
        }

        protected FMRShell.Collector[] DataCollector = new FMRShell.Collector[(int)FMRShell.DataType.Last];
        protected TradingDataLogger[] DataLogger = new TradingDataLogger[(int)FMRShell.DataType.Last];

        protected void debugOperatonsStopLogCallback(IWrite iWrite, string cmdName, object[] cmdArguments)
        {
            //CloseDataStreamAndLog(iWrite, (FMRShell.DataType)cmdArguments[0]);    //an error here - invalid cast exception
            CloseLog(iWrite, FMRShell.DataType.Maof, false);
        }

        protected void CloseLog(IWrite iWrite, FMRShell.DataType dt, bool stopStream)
        {
            TradingDataLogger dataLogger = DataLogger[(int)dt];
            dataLogger.Stop();
            dataLogger.Dispose();
            DataLogger[(int)dt] = null;
            if (stopStream)
            {
                StopStream(iWrite, dt);
            }
        }

        protected void StopStream(IWrite iWrite, FMRShell.DataType dt)
        {
            FMRShell.Collector tradingDataCollector = DataCollector[(int)dt];
            tradingDataCollector.Stop(dt);
        }

        protected void OpenStreamAndLog(IWrite iWrite, bool test, FMRShell.DataType dt, string filename, string loggerName)
        {
            // create Collector (producer) - will do it only once
            FMRShell.Collector dataCollector = new FMRShell.Collector(this.MyConn.GetSessionId());
            DataCollector[(int)dt] = dataCollector;

            // create logger which will register itself (AddSink) in the collector
            TradingDataLogger dataLogger = new TradingDataLogger(loggerName, filename, false, dataCollector, dt);
            DataLogger[(int)dt] = dataLogger;

            // start logger
            dataLogger.Start();

            // start collector, which will start the stream in K300Class
            dataCollector.Start(dt);

            Thread.Sleep(100);
            debugLoggerShowCallback(iWrite, "", null);

            if (test)
            {
                Thread.Sleep(1000);
                CloseLog(iWrite, dt, true);
            }
        }

        protected void debugLoggerShowCallback(IWrite iWrite, string cmdName, object[] cmdArguments)
        {
            iWrite.WriteLine(
                OutputUtils.FormatField("Name", 10) +
                OutputUtils.FormatField("Triggered", 10) +
                OutputUtils.FormatField("Logged", 10) +
                OutputUtils.FormatField("Dropped", 10) +
                OutputUtils.FormatField("Log type", 10) +
                OutputUtils.FormatField("Latest", 24) +
                OutputUtils.FormatField("Oldest", 24) +
                OutputUtils.FormatField("Stamped", 10)
            );
            iWrite.WriteLine("-----------------------------------------------------------------------------------------------------------------------");
            bool isEmpty = true;
            foreach (ILogger logger in Resources.Loggers)
            {
                isEmpty = false;
                iWrite.WriteLine(
                    OutputUtils.FormatField(logger.GetName(), 10) +
                    OutputUtils.FormatField(logger.GetCountTrigger(), 10) +
                    OutputUtils.FormatField(logger.GetCountLog(), 10) +
                    OutputUtils.FormatField(logger.GetCountDropped(), 10) +
                    OutputUtils.FormatField(logger.GetLogType().ToString(), 10) +
                    OutputUtils.FormatField(logger.GetLatest().ToString(), 24) +
                    OutputUtils.FormatField(logger.GetOldest().ToString(), 24) +
                    OutputUtils.FormatField(logger.TimeStamped().ToString(), 10)
                );

            }
            if (isEmpty)
            {
                iWrite.WriteLine("No loggers");
            }
        }

        protected void Timer5sHandler(ITimer timer)
        {
            Console.WriteLine("5s timer expired " + DateTime.Now);
        }

        protected void Timer30sHandler(ITimer timer)
        {
            Console.WriteLine("30s timer expired " + DateTime.Now);
        }

        protected void debugTimerTestThread()
        {
            // create set (timer task). initially empty
            TimerTask timerTask = new TimerTask("ShortTimers");

            Console.WriteLine("Start timers " + DateTime.Now);

            // create two types of timers
            TimerList timers_5sec = new TimerList("5sec", 5 * 1000, 100, this.Timer5sHandler, timerTask);
            TimerList timers_30sec = new TimerList("30sec", 30 * 1000, 100, this.Timer30sHandler, timerTask);

            timerTask.Start();

            // start some timers
            timers_5sec.Start();
            timers_5sec.Start();
            timers_5sec.Start();
            Thread.Sleep(1 * 1000);
            timers_5sec.Start();

            ITimer timer;
            long timerId;
            timers_30sec.Start(out timer, out timerId, null, false);
            timers_5sec.Start();

            debugTimerShowCallback(null, null, null);

            // wait for the first timer to expire
            Thread.Sleep(10 * 1000);
            timers_30sec.Stop(timer, timerId);

            Thread.Sleep(30 * 1000);
            debugTimerShowCallback(null, null, null);

            // clean up
            timers_5sec.Dispose();
            timers_30sec.Dispose();
            timerTask.Dispose();
        }

        protected void debugTimerTestCallback(IWrite iWrite, string cmdName, object[] cmdArguments)
        {
            // call once - init timers subsystem
            Timers.Init();

            // timer test contains delauys. run the test from a separate thread and release
            // user input context
            System.Threading.Thread thread = new System.Threading.Thread(debugTimerTestThread);
            thread.Start();
        }

        protected void debugTimerShowCallback(IWrite iWrite, string cmdName, object[] cmdArguments)
        {
            System.Collections.ArrayList names;
            System.Collections.ArrayList values;
            int entry = 0;
            int columnSize = 12;

            bool isEmpty = true;

            ((JQuant.IWrite)this).WriteLine();

            foreach (IResourceTimerList timerList in Resources.TimerLists)
            {
                timerList.GetEventCounters(out names, out values);
                isEmpty = false;

                if (entry == 0)
                {
                    names.Insert(0, "TimerListName");
                    names.Insert(0, "TimerTaskName");
                    CommandLineInterface.printTableHeader((JQuant.IWrite)this, names, columnSize);
                }
                values.Insert(0, OutputUtils.FormatField(timerList.Name, columnSize));
                values.Insert(0, OutputUtils.FormatField(timerList.GetTaskName(), columnSize));
                CommandLineInterface.printValues((JQuant.IWrite)this, values, columnSize);

                entry++;

            }
            if (isEmpty)
            {
                System.Console.WriteLine("No timers");
            }
        }

        protected long[] threadpoolTestTicks;

        protected void ThreadPoolJobEnter(object argument)
        {
        }

        protected void ThreadPoolJobDone(object argument)
        {
            int c = (int)argument;
            long tick = DateTime.Now.Ticks;
            threadpoolTestTicks[c] = (tick - threadpoolTestTicks[c]);
        }

        protected void debugThreadPoolTestCallback(IWrite iWrite, string cmdName, object[] cmdArguments)
        {
            int maxJobs = 5;
            JQuant.ThreadPool threadPool = new JQuant.ThreadPool("test", 1, maxJobs, ThreadPriority.Lowest);

            threadpoolTestTicks = new long[maxJobs];
            long tick = DateTime.Now.Ticks;
            for (int i = 0; i < maxJobs; i++)
            {
                threadpoolTestTicks[i] = tick;
            }

            for (int i = 0; i < maxJobs; i++)
            {
                threadPool.PlaceJob(ThreadPoolJobEnter, ThreadPoolJobDone, i);
            }
            Thread.Sleep(500);

            debugThreadPoolShowCallback(iWrite, cmdName, cmdArguments);
            threadPool.Dispose();

            for (int i = 0; i < threadpoolTestTicks.Length; i++)
            {
                iWrite.WriteLine("ThreadPoolJob done  idx =" + i + ", time = " + (double)threadpoolTestTicks[i] / (double)(10 * 1) + " micros");
            }

        }


        protected void feedGetToFileCallback(IWrite iWrite, string cmdName, object[] cmdArguments)
        {
            feedGetSeriesCallback(iWrite, cmdName, cmdArguments, true);
        }

        protected void feedGetSeriesCallback(IWrite iWrite, string cmdName, object[] cmdArguments)
        {
            feedGetSeriesCallback(iWrite, cmdName, cmdArguments, false);
        }

        protected void feedGetSeriesCallback(IWrite iWrite, string cmdName, object[] cmdArguments, bool outputToFile)
        {
            int argsNum = cmdArguments.Length;
            string symbol = null;
            DateTime from = DateTime.Today - TimeSpan.FromDays(30);
            DateTime to = DateTime.Now;
            DateTime tmp;
            bool result = true;
            string[] args = (string[])cmdArguments;
            string filename = null;

            switch (argsNum)
            {
                case 1:
                    result = false;
                    break;
                case 2:
                    if (outputToFile)
                    {
                        result = false;
                    }
                    else
                    {
                        symbol = args[1];
                    }
                    break;
                case 3:
                    if (outputToFile)
                    {
                        symbol = args[1];
                        filename = args[2];
                    }
                    else
                    {
                        symbol = args[1];
                        result = DateTime.TryParse(args[2], out tmp);
                        if (result) from = tmp;
                    }
                    break;
                case 4:
                    if (outputToFile)
                    {
                        symbol = args[1];
                        filename = args[2];
                        result = DateTime.TryParse(args[3], out tmp);
                        if (result) from = tmp;
                    }
                    else
                    {
                        symbol = args[1];
                        result = DateTime.TryParse(args[2], out tmp);
                        if (result) from = tmp;
                        result = DateTime.TryParse(args[3], out tmp);
                        if (result) to = tmp;
                    }
                    break;
                case 5:
                default:
                    if (outputToFile)
                    {
                        symbol = args[1];
                        filename = args[2];
                        result = DateTime.TryParse(args[3], out tmp);
                        if (result) from = tmp;
                        result = DateTime.TryParse(args[4], out tmp);
                        if (result) to = tmp;
                    }
                    else
                    {
                        symbol = args[1];
                        result = DateTime.TryParse(args[2], out tmp);
                        if (result) from = tmp;
                        result = DateTime.TryParse(args[3], out tmp);
                        if (result) to = tmp;
                    }
                    break;
            }

            if (!result)
            {
                iWrite.WriteLine("Please, specify symbol, from and to date");
                return;
            }

            IDataFeed dataFeed = new FeedYahoo();
            TA.PriceVolumeSeries series;
            result = dataFeed.GetSeries(from, to, new Equity(symbol), DataFeed.DataType.Daily, out series);
            if (result)
            {
                System.IO.FileStream fileStream = null;
                iWrite.WriteLine("Parsed " + series.Data.Count + " entries");
                if (outputToFile)
                {
                    bool shouldClose = false;
                    try
                    {
                        fileStream = new System.IO.FileStream(filename, FileMode.CreateNew, FileAccess.Write, FileShare.Read);
                        shouldClose = true;
                        StreamWriter streamWriter = new StreamWriter(fileStream);
                        streamWriter.Write(series.ToString(TA.PriceVolumeSeries.Format.Table));
                        streamWriter.Flush();
                        fileStream.Close();
                        shouldClose = false;
                    }
                    catch (IOException e)
                    {
                        iWrite.WriteLine(e.ToString());
                    }
                    if (shouldClose)
                    {
                        fileStream.Close();
                    }
                }
                else
                {
                    iWrite.WriteLine(series.ToString(TA.PriceVolumeSeries.Format.Table));
                }
            }
            else
            {
                iWrite.WriteLine("Failed to read data from server");
            }

        }

        protected void feedGetSeriesFromFileCallback(IWrite iWrite, string cmdName, object[] cmdArguments)
        {
            string filename = "yahoo_feed_data.csv";

            IDataFeed dataFeed = new FeedYahoo();
            TA.PriceVolumeSeries series;
            bool result = dataFeed.GetSeries(filename, out series);
            if (result)
            {
                iWrite.WriteLine("Parsed " + series.Data.Count + " entries");
            }
            else
            {
                iWrite.WriteLine("Failed to read data from server");
            }
        }


        protected void debugThreadPoolShowCallback(IWrite iWrite, string cmdName, object[] cmdArguments)
        {
            debugPrintResourcesNameAndStats(iWrite, Resources.ThreadPools);
        }


        protected void debugOrderShowCallback()
        {
            OrderType orderType = (OrderType.LMT | OrderType.FOK | OrderType.IOC);
        }

        #endregion;

        #region Load Commands

        protected void LoadCommandLineInterface()
        {

            cli.SystemMenu.AddCommand("exit", "Exit from the program",
                "Cleanup and exit", this.CleanupAndExit);


            Menu menuOperations = cli.RootMenu.AddMenu("Oper", "Operations",
                                   " Login, start stream&log");

            menuOperations.AddCommand("Login", "Login to the remote server",
                                  " The call will block until login succeeds", operLoginCallback);
            menuOperations.AddCommand("Logout", "Perform the logout process",
                                  " The call will block until logout succeeds", operLogoutCallback);

            menuOperations.AddCommand("StartLogMF", "Log Maof stream",
                                  " Start Maof stream and run logger", operLogMaofCallback);
            menuOperations.AddCommand("StartLogRZ", "Log Rezef stream",
                                  " Start Rezef stream and run logger", operLogRezefCallBack);
            menuOperations.AddCommand("StartLogMDD", "Log Madad stream",
                                  " Start Index (Maof) stream and run logger", operLogMadadCallBack);

            menuOperations.AddCommand("StopLog", "Stop previosly started Maof Log - MF | MDD | RZ, to stop stream type Y",
                                  " Stop logger - Maof(MF) | Madad (MDD) | Rezef (RZ) and stream (Y/N). ", operStopLogCallback);
            menuOperations.AddCommand("StopStream", "Stop previosly started data stream - MF | MDD | RZ",
                                  " Stop data stream - Maof(MF) | Madad (MDD) | Rezef (RZ) ", operStopLogCallback);


            menuOperations.AddCommand("ShowLog", "Show existing loggers",
                                  " List of created loggers with the statistics", debugLoggerShowCallback);
            menuOperations.AddCommand("AS400TimeTest", "ping the server",
                                  "ping AS400 server in order to get latency and synchronize local amachine time with server's",
                                  debugGetAS400DTCallback);

            // Menu menuFMRLib = 
            cli.RootMenu.AddMenu("FMRLib", "Access to  FMRLib API",
                          " Allows to access the FMRLib API directly");
            // Menu menuFMRLibSim = 
            cli.RootMenu.AddMenu("FMRLibSim", "Configure FMR simulation",
                           " Condiguration and debug of the FMR simulatoion");


            Menu menuFeed = cli.RootMenu.AddMenu("Feed", "Trading data feeds",
                                   " Get data from the data feeds, TA screens");

            menuFeed.AddCommand("getseries", "Get price/volume series",
                                  " Get price/volume daily series for the specified stock symbol. Args: symbol [fromDate[toDate]]", feedGetSeriesCallback);
            menuFeed.AddCommand("gettofile", "Write price/volume series to file",
                                  " Get price/volume daily series for the specified stock symbol and write to file. Args: symbol filename [fromDate[toDate]]", feedGetToFileCallback);
            menuFeed.AddCommand("readfile", "Get price/volume series from file",
                                  " Get price/volume daily series for the specified file. Args: filename", feedGetSeriesFromFileCallback);



            Menu menuDebug = cli.RootMenu.AddMenu("Dbg", "System debug info",
                                   " Created objetcs, access to the system statistics");

            menuDebug.AddCommand("GC", "Run garbage collector",
                                  " Forces garnage collection", debugGcCallback);
            menuDebug.AddCommand("mbxTest", "Run simple mailbox tests",
                                  " Create a mailbox, send a message, receive a message, print debug info", debugMbxTestCallback);
            menuDebug.AddCommand("mbxShow", "Show mailboxes",
                                  " List of created mailboxes with the current status and statistics", debugMbxShowCallback);
            menuDebug.AddCommand("threadTest", "Run simple thread",
                                  " Create a mailbox thread, send a message, print debug info", debugThreadTestCallback);
            menuDebug.AddCommand("threadShow", "Show threads",
                                  " List of created threads and thread states", debugThreadShowCallback);
            menuDebug.AddCommand("poolTest", "Run simple pool tests",
                                  " Create a pool, add object, allocate object, free object", debugPoolTestCallback);
            menuDebug.AddCommand("poolShow", "Show pools",
                                  " List of created pools with the current status and statistics", debugPoolShowCallback);
            menuDebug.AddCommand("loginTest", "Run simple test of the login",
                                  " Create a FMRShell.Connection(xmlfile) and call Open()", debugLoginCallback);
            menuDebug.AddCommand("AS400TimeTest", "ping the server",
                                  "ping AS400 server in order to get latency and synchronize local amachine time with server's",
                                  debugGetAS400DTCallback);
            menuDebug.AddCommand("timerTest", "Run simple timer tests",
                                  " Create a timer task, two timer lists, start two timers, clean up", debugTimerTestCallback);
            menuDebug.AddCommand("timerShow", "Show timers",
                                  " List of created timers and timer tasks", debugTimerShowCallback);

            menuDebug.AddCommand("threadPoolTest", "Run simple thread pool tests",
                                  " Create a thread pool, start a couple of jobs, destroy the pool", debugThreadPoolTestCallback);
            menuDebug.AddCommand("threadPoolShow", "Show thread pools",
                                  " List of created thread pools", debugThreadPoolShowCallback);

#if USEFMRSIM
            menuDebug.AddCommand("loggerTest", "Run simple test of the logger",
                                  " Create a Collector and start a random data simulator", debugLoggerTestCallback);
#endif
            menuDebug.AddCommand("loggerShow", "Show existing loggers",
                                  " List of created loggers with the statistics", debugLoggerShowCallback);
        }

        #endregion
    }
}//namespace
