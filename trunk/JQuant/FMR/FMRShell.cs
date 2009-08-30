﻿//#define USEFMRSIM

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Threading;
 
using System.Reflection;
using System.ComponentModel;
using JQuant;

/// <summary>
/// depending on the compilation flags i am going to use
/// simulation or real TaskBar namespace
/// the rest of the application is using FMRShell and is not aware if 
/// this is a simulation or a real server connected
/// Actually this is not precise - Command Line interface contains some internal test commands
/// which require simulation engines
/// </summary>
#if USEFMRSIM
using TaskBarLibSim;
#else
using TaskBarLib;
#endif

/// <summary>
/// thin shell around either TaskBarLib 
/// or around FMRSym if compiler directive USEFMRSIM is defined 
/// </summary>
namespace FMRShell
{
	public enum ConnectionState
	{
        [Description("Idle")]
        Idle,                              // never opened yet
        [Description("Established")]       
        Established, 
        [Description("Trying")]
        Trying,
        [Description("Closed")]
        Closed,
        [Description("Disposed")]           
        Disposed                           // Dispose() called
	}
	
	/// <summary>
	/// this guy logins to the remote server
	/// and eventually will run state machine keeping the connection alive and kicking 
	/// and will notify other subsystems if and when the connection goes down
	/// this little guy is one of the most important. there are two sources of information
	/// related to the connection status
	/// - periodic attempts to read data stream through TaskBarLib.K300Class
	/// - TaskBarLib.UserClass 
	/// This class handles all included in the TasBarkLib.UserClass and login related
	/// When there is no real TaskBarLib the class calls TasBarkLibSim
	/// 
	/// Normally application will do something like
	/// FMRShell.Connection connection = new FMRShell.Connection("xmlfilename")
	/// bool openResult = connection.Open(errCode)
	/// do work with connection.userClass  of type TaskBarLib.UserClass
	/// connection.Dispose();
	/// </summary>
	public class Connection : IDisposable
	{
		/// <summary>
		/// use default hard coded user name and password
		/// </summary>
		public Connection()
		{
			//  set defaults
			_parameters = new ConnectionParameters(
                "aryeh",    // user name
                "abc123",   // password
                ""          // app passsword
                );
			Init();
		}

		/// <summary>
		/// create connection using provided by application connection parameters
		/// </summary>
		/// <param name="parameters">
		/// A <see cref="ConnectionParameters"/>
		/// </param>
		public Connection(ConnectionParameters parameters)
		{
			_parameters = parameters;
			Init();
		}
		
		/// <summary>
		/// open connection based on the login information stored in the specified XML file
		/// See example of the XML file in the ConnectionParameters.xml
		/// </summary>
		/// <param name="filename">
		/// A <see cref="System.String"/>
		/// Name of the XML file where the user login credentials can be found
		/// </param>
		public Connection(string filename)
		{
			xmlFileName = filename;
			useXmlFile = true;
			Init();
		}
		
		/// <summary>
		/// do some general initialization common for all constructors
		/// </summary>
		private void Init()
		{
			state = ConnectionState.Idle;
			userClass = new UserClass();
		}
		
		/// <summary>
		/// application have to call this method to get rid of the 
		/// connection (close sockets, etc.)
		/// </summary>
		public void Dispose()
		{
			// call userClass.Logout here
			
			// set userClass to null
			
			state = ConnectionState.Disposed;
		}
		
		~Connection()
		{
		}
			
		public int GetSessionId()
		{
			return sessionId;
		}
		
		public string GetErrorMsg()
		{
			return errMsg;
		}
		
		/// <summary>
		/// return false if the open connection fails 
		/// normally application will call Open() without arguments - blocking Open
		/// or Keep() - which runs a thread and attempts to keep the connection
		/// alive. 
		/// </summary>
		/// <param name="returnCode">
		/// A <see cref="System.Int32"/>
		/// </param>
		/// <returns>
		/// A <see cref="System.Boolean"/>
		/// True if Ok and returnCode is set to something meaningful
		/// Application will check that the method retruned true and only 
		/// after that analyze returnCode
		/// </returns>
        public bool Open(out int returnCode)
        {
			bool result = true;
			returnCode = 0;
			
			// should I read login credentials from XML file ?
			if (useXmlFile) {
				ReadConnectionParameters reader = new ReadConnectionParameters(xmlFileName);
				
				// let's try to read the file 
				result = reader.Parse(out _parameters);
			}
			
			if (result)
			{
	            returnCode = userClass.Login(
                    _parameters.userName, 
                    _parameters.userPassword, 
                    _parameters.appPassword, 
				    out  errMsg, out  sessionId);

				result = (returnCode >= 0);
				if (result)
				{
					// according to the documentation returnCode == sessionId
					if (sessionId != returnCode)
					{
						Console.WriteLine("Session Id="+sessionId+",returnCode="+returnCode+" are not equal after Login");
					}
				}
			}
			
            return result;
        }			
		
		/// <summary>
		/// the calling thread will be blocked by this method until Login to the
		/// remote server succeeds 
		/// </summary>
		/// <param name="printProgress">
		/// A <see cref="System.Boolean"/>
		/// if set to true will print dots as login makes progress
		/// </param>
		/// <returns>
		/// A <see cref="System.Boolean"/>
		/// True if Ok and returnCode is set to something meaningful
		/// Application will check that the method retruned true and only 
		/// after that analyze returnCode
		/// </returns>
        public bool Open(out int returnCode, bool printProgress)
        {
			state = ConnectionState.Trying;
			int percent = 0;
			
			bool openResult = Open(out returnCode);
            
            //Print title + progress bar
            Console.WriteLine(
                "\n0    10   20   30   40   50   60   70   80   90  100\n|----+----+----+----+----+----+----+----+----+----|");
            
            // loop until login succeeds
			while (openResult)
			{
				int percent_1;
				string description;
				
				userClass.GetLoginActivity(ref sessionId, out percent_1, out description);
				if ((percent_1 != percent) && (printProgress))
				{
                    //scale the dots to the progress bar
                    while (percent < percent_1)
                    {
                        Console.Write(".");
                        percent += 2;
                    }
				}
				
				loginStatus = userClass.get_LoginState(ref sessionId);
				
				if (loginStatus == LoginStatus.LoginSessionActive)
				{
					openResult = true;
					break;
				}
				else if ((loginStatus == LoginStatus.LoginSessionInProgress) || (loginStatus == LoginStatus.LoginSessionReLogin))
				{
				}
				else 
				{
					openResult = false;
					break;
				}
				
				// i need a short delay here before next attempt - 1s
				Thread.Sleep(1*1000);
			}
			
			return openResult;
		}

		
		public string GetUserName()
		{
			return _parameters.userName;
		}
		
		public LoginStatus loginStatus
		{
			get;
			protected set;
		}
		
		
		public ConnectionState state
		{
			get;
			protected set;
		}
		
        protected int sessionId;
        protected string errMsg;
		protected string xmlFileName;
		protected bool useXmlFile;
		protected ConnectionParameters _parameters;

	
		/// <value>
		/// in the real code TaskBarLib instead of TaskBarLibSim will be used
		/// </value>
        public UserClass userClass
		{
			get;
			protected set;
		}
		
	}

    /// <summary>
	/// generic class 
	/// data container for the trading/market data
	/// can hold fields like time stamp, bid/ask, last price, etc.
	/// this class is not going to be used directly but inherited
	/// </summary>
	public struct MarketData :ICloneable
	{
		public K300MaofType k300MaofType;
        
        public object Clone()
        {
            // create a new object
            MarketData md = new MarketData();

            // copy data
            md.k300MaofType.SUG_REC = this.k300MaofType.SUG_REC;

            md.k300MaofType.TRADE_METH = this.k300MaofType.TRADE_METH;
            md.k300MaofType.BNO_Num = this.k300MaofType.BNO_Num;
            md.k300MaofType.LAST_REC = this.k300MaofType.LAST_REC;
            md.k300MaofType.SIDURI_Num = this.k300MaofType.SIDURI_Num;
            md.k300MaofType.SYMBOL_E = this.k300MaofType.SYMBOL_E;
            md.k300MaofType.Symbol = this.k300MaofType.Symbol;
            md.k300MaofType.BNO_NAME_E = this.k300MaofType.BNO_NAME_E;
            md.k300MaofType.BNO_NAME = this.k300MaofType.BNO_NAME;
            md.k300MaofType.BRANCH_NO = this.k300MaofType.BRANCH_NO;
            md.k300MaofType.BRANCH_U = this.k300MaofType.BRANCH_U;
            md.k300MaofType.SUG_BNO = this.k300MaofType.SUG_BNO;
            md.k300MaofType.MIN_UNIT = this.k300MaofType.MIN_UNIT;
            md.k300MaofType.HARIG_NV = this.k300MaofType.HARIG_NV;
            md.k300MaofType.MIN_PR = this.k300MaofType.MIN_PR;
            md.k300MaofType.MAX_PR = this.k300MaofType.MAX_PR;
            md.k300MaofType.BASIS_PRC = this.k300MaofType.BASIS_PRC;
            md.k300MaofType.BASIS_COD = this.k300MaofType.BASIS_COD;
            md.k300MaofType.STATUS_COD = this.k300MaofType.STATUS_COD;
            md.k300MaofType.EX_DATE = this.k300MaofType.EX_DATE;
            md.k300MaofType.EX_PRC = this.k300MaofType.EX_PRC;
            md.k300MaofType.VL_MULT = this.k300MaofType.VL_MULT;
            md.k300MaofType.VL_COD = this.k300MaofType.VL_COD;
            md.k300MaofType.ZERO_COD = this.k300MaofType.ZERO_COD;
            md.k300MaofType.shlav = this.k300MaofType.shlav;
            md.k300MaofType.STATUS = this.k300MaofType.STATUS;
            md.k300MaofType.TRD_STP_CD = this.k300MaofType.TRD_STP_CD;
            md.k300MaofType.TRD_STP_N = this.k300MaofType.TRD_STP_N;
            md.k300MaofType.STP_OPN_TM = this.k300MaofType.STP_OPN_TM;
            md.k300MaofType.LMT_BY1 = this.k300MaofType.LMT_BY1;
            md.k300MaofType.LMT_BY2 = this.k300MaofType.LMT_BY2;
            md.k300MaofType.LMT_BY3 = this.k300MaofType.LMT_BY3;
            md.k300MaofType.LMY_BY1_NV = this.k300MaofType.LMY_BY1_NV;
            md.k300MaofType.LMY_BY2_NV = this.k300MaofType.LMY_BY2_NV;
            md.k300MaofType.LMY_BY3_NV = this.k300MaofType.LMY_BY3_NV;
            md.k300MaofType.RWR_FE = this.k300MaofType.RWR_FE;
            md.k300MaofType.LMT_SL1 = this.k300MaofType.LMT_SL1;
            md.k300MaofType.LMT_SL2 = this.k300MaofType.LMT_SL2;
            md.k300MaofType.LMT_SL3 = this.k300MaofType.LMT_SL3;
            md.k300MaofType.LMY_SL1_NV = this.k300MaofType.LMY_SL1_NV;
            md.k300MaofType.LMY_SL2_NV = this.k300MaofType.LMY_SL2_NV;
            md.k300MaofType.LMY_SL3_NV = this.k300MaofType.LMY_SL3_NV;
            md.k300MaofType.RWR_FF = this.k300MaofType.RWR_FF;
            md.k300MaofType.PRC = this.k300MaofType.PRC;
            md.k300MaofType.COD_PRC = this.k300MaofType.COD_PRC;
            md.k300MaofType.SUG_PRC = this.k300MaofType.SUG_PRC;
            md.k300MaofType.LST_DF_BS = this.k300MaofType.LST_DF_BS;
            md.k300MaofType.RWR_FG = this.k300MaofType.RWR_FG;
            md.k300MaofType.LST_DL_PR = this.k300MaofType.LST_DL_PR;
            md.k300MaofType.LST_DL_TM = this.k300MaofType.LST_DL_TM;
            md.k300MaofType.LST_DL_VL = this.k300MaofType.LST_DL_VL;
            md.k300MaofType.DAY_VL = this.k300MaofType.DAY_VL;
            md.k300MaofType.DAY_VL_NIS = this.k300MaofType.DAY_VL_NIS;
            md.k300MaofType.DAY_DIL_NO = this.k300MaofType.DAY_DIL_NO;
            md.k300MaofType.RWR_FH = this.k300MaofType.RWR_FH;
            md.k300MaofType.DAY_MAX_PR = this.k300MaofType.DAY_MAX_PR;
            md.k300MaofType.DAY_MIN_PR = this.k300MaofType.DAY_MIN_PR;
            md.k300MaofType.POS_OPN = this.k300MaofType.POS_OPN;
            md.k300MaofType.POS_OPN_DF = this.k300MaofType.POS_OPN_DF;
            md.k300MaofType.STS_NXT_DY = this.k300MaofType.STS_NXT_DY;
            md.k300MaofType.UPD_DAT = this.k300MaofType.UPD_DAT;
            md.k300MaofType.UPD_TIME = this.k300MaofType.UPD_TIME;
            md.k300MaofType.FILER = this.k300MaofType.FILER;

            return md;
        }

        public override string ToString()
        {
            return k300MaofType.SUG_REC;
        }
	}

    public class K300MaofTypeToString :StructToString<K300MaofType>
    {
        public K300MaofTypeToString(string delimiter): base(delimiter)
        {
        }
    }
    
	/// <summary>
	/// this class used by the RxDataValidator to let the application know that
	/// something wrong with the incoming data
	/// </summary>
	public class DataValidatorEvent
	{
		public MarketData sync
		{
			get;
			set;
		}
		public MarketData async
		{
			get;
			set;
		}
	}
	
	/// <summary>
	/// This is a producer (see IProducer) 
	/// Given Connection object will open data stream and notify registered 
	/// data sinks (ISink)
	/// The class operates simultaneously in asynchronous and synchronous fashion. 
	/// The class installs an event listener by calling K300Class.K300StartStream
	/// Additonally class spawns a thread which does polling of the remote server 
	/// in case notification does not work correctly and to ensure that the data is 
	/// consistent. 
	/// There is a tricky part. I want to make sure that the data which we get by 
	/// polling and via asynchronous API is the same. Collector uses for this purpose
	/// a dedicated sink - thread which polls the servers and compares the received 
	/// data with the one sent to it by the collector
	/// </summary>
	public class Collector: JQuant.IProducer<MarketData>
	{
		
		public Collector()
		{
			listeners = new List<JQuant.ISink<MarketData>>(5);

			countOnMaof = 0;
			
			marketDataOnMaof = new MarketData();
			
			// create a couple of TaskLib objects required for access
			// to the data stream 
			k300Class = new K300Class();
			k300EventsClass = new K300EventsClass();
            k300EventsClass.OnMaof += new _IK300EventsEvents_OnMaofEventHandler(OnMaof);
		}

		/// <summary>
		/// Called by TaskBarLib. This method calls registered listeners and gets out 
		/// The idea behind it to be as fast as possible
		/// this is the point where some basic processing can be done like filter obvious
		/// errors
		/// </summary>
		/// <param name="data">
		/// A <see cref="K300MaofType"/>
		/// </param>
        protected void OnMaof(ref K300MaofType data)
        {

			// no memory allocation here - i am using allready created object 
			marketDataOnMaof.k300MaofType = data;
			
			foreach (JQuant.ISink<MarketData> sink in listeners)
			{
				// sink should not modify the data
                // sink has two options
                // 1) handle the data in the context of the Collector
                // thead
                // 2) clone the data and and postopone the procesing (delegate
                // to another thread
				sink.Notify(countOnMaof, marketDataOnMaof);
			}
		}
		
		/// <summary>
		/// default start - only Maof events
		/// </summary>
		public void Start()
		{
			k300Class.K300StartStream(K300StreamType.MaofStream);
		}
		
        public void Stop()
        {
            k300Class.K300StopStream(K300StreamType.MaofStream);
        }
        
        public bool AddSink(JQuant.ISink<MarketData> sink)
		{
			listeners.Add(sink);
			return true;
		}
		
        public bool RemoveSink(JQuant.ISink<MarketData> sink)
		{
			listeners.Remove(sink);
			return true;
		}
		
		
		protected static List<JQuant.ISink<MarketData>> listeners;
		protected K300Class k300Class;
		protected K300EventsClass k300EventsClass;
		protected MarketData marketDataOnMaof;
		
		/// <summary>
		/// count calls to the notifiers
		/// </summary>
		protected int countOnMaof; 

	}
	
	/// <summary>
	/// The class also is a sink registered with the Collector. When Collector sends 
	/// a new notification validator pushes the data to FIFO 
	/// 
	/// this is also a thread which polls API and gets market data synchronously. 
	/// looks for the data in the FIFO and if found removes 
	/// the entry from the FIFO. Validator expects that order of the events is the same 
	/// and if there is no match (miss) notifies all registered listeners (sinks) about
	/// data mismatch
	/// </summary>
	public class RxDataValidator: JQuant.IProducer<DataValidatorEvent>, JQuant.ISink<MarketData>
	{
        public bool AddSink(JQuant.ISink<DataValidatorEvent> sink)
		{
			return true;
		}
		
        public bool RemoveSink(JQuant.ISink<DataValidatorEvent> sink)
		{
			return true;
		}
		
		
		/// <summary>
		/// called by Collector to notify about incoming event. Add the event to the FIFO
		/// </summary>
		/// <param name="count">
		/// A <see cref="System.Int32"/>
		/// </param>
		/// <param name="data">
		/// A <see cref="MarketData"/>
		/// </param>
		public void Notify(int count, MarketData data)
		{
		}
	}
	
	
	/// <summary>
	/// handle XML file containing connection parameters
	/// </summary>
	class ReadConnectionParameters :XmlTextReader
	{
		public ReadConnectionParameters(string filename) :base(filename)
		{
		}
		
		private enum xmlState {
	        [Description("BEGIN")]
			BEGIN,
	        [Description("PARAMS")]
			PARAMS,
	        [Description("USERNAME")]
			USERNAME,
	        [Description("PASSWORD")]
			PASSWORD,
	        [Description("APP_PASSWORD")]
			APP_PASSWORD
		}
		
		public bool Parse(out ConnectionParameters parameters)
		{
			xmlState state = xmlState.BEGIN;
			string username = "";
			string appPassword = "";
			string password = "";
			bool result = true;
			string val;
			parameters = null;
			
			while (base.Read())
			{
				switch (base.NodeType)
				{
				case XmlNodeType.Element:
					val = base.Name; 
					if ((val.Equals("connectionparameters")) && (state == xmlState.BEGIN))
					{
						state = xmlState.PARAMS;
					}
					else if ((val.Equals("username")) && (state == xmlState.PARAMS))
					{
						state = xmlState.USERNAME;
					}
					else if ((val.Equals("password")) && (state == xmlState.USERNAME))
					{
						state = xmlState.PASSWORD;
					}
					else if ((val.Equals("apppassword")) && (state == xmlState.PASSWORD))
					{
						state = xmlState.APP_PASSWORD;
					}
					else 
					{
						Console.WriteLine("Failed at element "+val+ " in state "+JQuant.EnumUtils.GetDescription(state));
						result = false;
					}
					break;
					
				case XmlNodeType.Text:
					val = base.Value; 
					if (state == xmlState.USERNAME) {
						username = val;
					}
					else if (state == xmlState.PASSWORD)
					{
						password = val;
					}
					else if (state == xmlState.APP_PASSWORD)
					{
						appPassword = val;
					}
					else
					{
						Console.WriteLine("Failed at text "+val+ " in state "+JQuant.EnumUtils.GetDescription(state));
						result = false;
					}
					break;
					
				case XmlNodeType.EndElement:
					// I will not check that endelement Name is Ok 
					val = base.Name; 
					if ((val.Equals("connectionparameters")) && (state == xmlState.APP_PASSWORD))
					{
					}
					else if ((val.Equals("username")) && (state == xmlState.USERNAME))
					{
					}
					else if ((val.Equals("password")) && (state == xmlState.PASSWORD))
					{
					}
					else if ((val.Equals("apppassword")) && (state == xmlState.APP_PASSWORD))
					{
					}
					else 
					{
						Console.WriteLine("Failed at EndElement "+val+ " in state "+JQuant.EnumUtils.GetDescription(state));
						result = false;
					}					
					break;						
				}
				
				// somethign is broken in the XML file
				if (result)
				{
					parameters = new ConnectionParameters(username, password, appPassword);
				}
				else
				{
					break;
				}
			}
			
			
			return result;
		}
			
	}

	/// <summary>
	/// User login credentials, IP address and everything else required
	/// to establish and keep connection. This is just a data holder
	/// </summary>
	/// <returns>
	/// A <see cref="System.Int32"/>
	/// </returns>
	public class ConnectionParameters
	{
        public ConnectionParameters(string name, string password)
        {
            userName = name;
            userPassword = password;
            appPassword = "";
        }
        
        public ConnectionParameters(string name, string password, string apassw)
		{
			userName = name;
			userPassword = password;
			appPassword = apassw;
		}

        public string userName
        {
            get;
            protected set;
        }
		
        public string userPassword
        {
            get;
            protected set;
        }

        public string appPassword
        {
			get;
			set;
		}				
	}

    /// <summary>
    /// Used to get latecny and synchronize local machine vs. AS400
    /// </summary>
    /// 
    public class AS400Synch
    {
        /// <summary>
        /// Returns latency in case of successful call, -1 otherwise
        /// </summary>
        /// <returns>A <see cref="System.Int32"/></returns>
        public static int GetLatency()
        {
            ConfigClass cs = new ConfigClass();
            AS400DateTime dt;
            int ltncy;
            int ret=cs.GetAS400DateTime(out dt, out ltncy);
            if (ret == 0) return ltncy;
            else return ret;
        }

        public static string GetAS400Time()
        {
            ConfigClass cs = new ConfigClass();
            AS400DateTime dt;
            int ltncy;
            int ret = cs.GetAS400DateTime(out dt, out ltncy);
            return dt.hour + ":" + dt.minute + ":" + dt.second + "." + dt.ms;
        }

        /// <summary>
        /// Computes basic statistics on latency from a number of AS400 pings
        /// </summary>
        /// <param name="numTries">no. of times to ping the server</param>
        /// <param name="timeOut">timeout between the pings</param>
        public static string Ping(int numTries, int timeOut)
        {
            ConfigClass cs = new ConfigClass();
            List<int> lst=new List<int>(30);
            int ltncy;
            int ret;
            AS400DateTime dt;
            if (numTries == 1) return "AS400Time " + GetAS400Time() + " latency " + GetLatency();
            else
            {
                for (int i = 0; i < numTries; i++)
                {
                    ret = cs.GetAS400DateTime(out dt, out ltncy);
                    lst.Add(ltncy);
                    Thread.Sleep(timeOut);
                }
                string M = Math.Round(Convert.ToDecimal(StatUtils.Mean(lst)),2).ToString();
                string SD = Math.Round(Convert.ToDecimal(StatUtils.StdDev(lst)), 2).ToString();
                string Min = StatUtils.Min(lst).ToString();
                string Max = StatUtils.Max(lst).ToString();
                string o = OutputUtils.FormatField("Mean",10)+
                    OutputUtils.FormatField("Std.Dev.",10)+
                    OutputUtils.FormatField("Min",10)+
                    OutputUtils.FormatField("Max",10)+
                    "\n----------------------------------------\n"+
                    OutputUtils.FormatField(M, 10)+
                    OutputUtils.FormatField(SD,10)+
                    OutputUtils.FormatField(Min,10)+
                    OutputUtils.FormatField(Max,10);

                return o;
            }
        }
    }
    
    /// <summary>
	/// Example of usage of the class, Main_ should be replaced by Main in the real application
	/// </summary>
    class FMRShellTest
    {
        public static void Main(string[] args)
        {
			// use default hard coded settings
            Connection newConn = new FMRShell.Connection();
			int returnCode;
            bool result  = newConn.Open(out returnCode);
            if (!result)
            {
                Console.WriteLine("Connection start failed: return code=" + returnCode +
                                  ", errorMsg=" + newConn.GetErrorMsg());
                Console.WriteLine();
                Console.ReadLine();
            }
            else Console.WriteLine("Successfull connection, SessionId=" + returnCode);
        }
    }

}
