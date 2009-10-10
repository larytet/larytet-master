
using System;
using System.Reflection;
using System.Threading;


/// <summary>
/// I want to run TaskBarLib simulation. I am going to implement API using prerecorded
/// or generated in some other way log files as a data feed
/// Only small part of the API is implemented
/// This class is used by the FMRShell and allows to simulate different scenarios
/// and play back previously recorded log files
/// In the future this class will contain engines which simulate behaviour of the
/// real system
///
/// Side note. Another approach to the simulation is to install a real server running
/// simulator and let the rest of the application including 3rd party code (DLLs from
/// FMR) use the simulation server. At this point it looks like an overkill. Thanks to
/// the time stamps i can simulate the system behaviour fairly close.
/// </summary>
namespace TaskBarLibSim
{
    public enum MonthType
    {
        AllMonths = -1,
        April = 4,
        August = 8,
        December = 12,
        February = 2,
        January = 1,
        July = 7,
        June = 6,
        March = 3,
        May = 5,
        November = 11,
        October = 10,
        September = 9
    }

    public enum MadadTypes
    {
        AllMadad = -1,
        BANK = 4,
        BINUI = 6,
        CURRENCYLNKD = 0x17,
        DOLLAR = 5,
        GOV_CHG0 = 20,
        GOV_CHG5 = 0x15,
        GOV_FIXED0 = 0x11,
        GOV_FIXED2 = 0x12,
        GOV_FIXED5 = 0x13,
        MAALE = 14,
        MAKAM = 0x16,
        TELBOND = 11,
        TELBOND40 = 15,
        TELBOND60 = 0x10,
        TELDIV20 = 10,
        TLTK = 2,
        TLV100 = 3,
        TLV25 = 0,
        TLV75 = 1,
        TLVFIN = 7,
        TLVNADLAN15 = 8,
        YETER120 = 12,
        YETER30 = 9,
        YETER50 = 13
    }

    public enum K300StreamType
    {
        IndexStream = 0x34,
        MaofCNTStream = 50,  //out of date, not used anymore
        MaofStream = 0x30,
        RezefCNTStream = 0x33,  //out of date, not used anymore
        RezefStream = 0x31
    }

    public enum StockKind
    {
        StockKindAgach = 1,
        StockKindAll = -1,
        StockKindKeren = 3,
        StockKindMakam = 2,
        StockKindMenaya = 0
    }

    public enum LoginStatus
    {
        LoginSessionActive,
        LoginSessionInProgress,
        LoginSessionInactive,
        LoginSessionDBInitFailure,
        LoginSessionAS400Failure,
        LoginSessionPasswordExpired,
        LoginSessionPasswordChangeFailure,
        LoginSessionPasswordChangedToday,
        LoginSessionWrongUserOrPassword,
        LoginSessionMaxUsersLimit,
        LoginSessionReLogin,
        LoginSessionTaskBarBlocked
    }

    public enum OnlineSessionType
    {
        OnlineSessionTypeAccounts,
        OnlineSessionTypeAll,
        OnlineSessionTypeCustodian,
        OnlineSessionTypeKranot,
        OnlineSessionTypeKupa
    }

    public enum QueryType
    {
        qtDetailed = 0x30,
        qtSummary = 0x31
    }

    public enum YieldDataType
    {
        YieldData5YearsbyYear = 4,
        YieldDataMonthbyDay = 1,
        YieldDataTwelveMonths = 3,
        YieldDataYearbyMonth = 2,
        YieldDataYearbyQuater = 5
    }

    /// <summary>
    /// Query types enumaration
    /// </summary>
    public enum ConnectionState
    {
        csOpen,
        csProcessing,
        csClosed
    }

    public enum LoginLevel
    {
        LoginLevelAccounts = 3,
        LoginLevelMax = 50,
        LoginLevelOptionsStocks = 2,
        LoginLevelPermissions = 1
    }

    /// <summary>
    /// This type is represents the timestamps from AS400 servers
    /// </summary>
    public struct AS400DateTime
    {
        public int year;
        public int Month;
        public int day;
        public int hour;
        public int minute;
        public int second;
        public int ms;
    }

    public struct K300MaofType
    {
        public string SUG_REC;
        public string TRADE_METH;
        public string BNO_Num;
        public string LAST_REC;
        public string SIDURI_Num;
        public string SYMBOL_E;
        public string Symbol;
        public string BNO_NAME_E;
        public string BNO_NAME;
        public string BRANCH_NO;
        public string BRANCH_U;
        public string SUG_BNO;
        public string MIN_UNIT;
        public string HARIG_NV;
        public string MIN_PR;
        public string MAX_PR;
        public string BASIS_PRC;
        public string BASIS_COD;
        public string STATUS_COD;
        public string EX_DATE;
        public string EX_PRC;
        public string VL_MULT;
        public string VL_COD;
        public string ZERO_COD;
        public string shlav;
        public string STATUS;
        public string TRD_STP_CD;
        public string TRD_STP_N;
        public string STP_OPN_TM;
        public string LMT_BY1;
        public string LMT_BY2;
        public string LMT_BY3;
        public string LMY_BY1_NV;
        public string LMY_BY2_NV;
        public string LMY_BY3_NV;
        public string RWR_FE;
        public string LMT_SL1;
        public string LMT_SL2;
        public string LMT_SL3;
        public string LMY_SL1_NV;
        public string LMY_SL2_NV;
        public string LMY_SL3_NV;
        public string RWR_FF;
        public string PRC;
        public string COD_PRC;
        public string SUG_PRC;
        public string LST_DF_BS;
        public string RWR_FG;
        public string LST_DL_PR;
        public string LST_DL_TM;
        public string LST_DL_VL;
        public string DAY_VL;
        public string DAY_VL_NIS;
        public string DAY_DIL_NO;
        public string RWR_FH;
        public string DAY_MAX_PR;
        public string DAY_MIN_PR;
        public string POS_OPN;
        public string POS_OPN_DF;
        public string STS_NXT_DY;
        public string UPD_DAT;
        public string UPD_TIME;
        public string FILER;
    }

    public struct K300RzfType
    {
        public string SUG_REC;
        public string BNO_Num;
        public string BNO_NAME;
        public string Symbol;
        public string TRADE_METH;
        public string SIDURI_Num;
        public string RWR_VA;
        public string MIN_UNIT;
        public string HARIG_NV;
        public string MIN_PR_OPN;
        public string MAX_PR_OPN;
        public string MIN_PR_CNT;
        public string MAX_PR_CNT;
        public string BASIS_PRC;
        public string STATUS;
        public string EX_COD;
        public string EX_DETAIL;
        public string RWR_VB;
        public string shlav;
        public string LAST_PRC;
        public string TRD_STP_N;
        public string STP_OPN_TM;
        public string RWR_VD;
        public string LMT_BY1;
        public string LMT_BY2;
        public string LMT_BY3;
        public string LMY_BY1_NV;
        public string LMY_BY2_NV;
        public string LMY_BY3_NV;
        public string MKT_NV_BY;
        public string MKT_NV_BY_NUM;
        public string RWR_VE;
        public string LMT_SL1;
        public string LMT_SL2;
        public string LMT_SL3;
        public string LMY_SL1_NV;
        public string LMY_SL2_NV;
        public string LMY_SL3_NV;
        public string MKT_NV_SL;
        public string MKT_NV_SL_NUM;
        public string RWR_VF;
        public string THEOR_PR;
        public string THEOR_VL;
        public string RWR_VG;
        public string LST_DL_PR;
        public string LST_DL_TM;
        public string LST_DF_BS;
        public string LST_DF_OPN;
        public string LST_DL_VL;
        public string DAY_VL;
        public string DAY_VL_NIS;
        public string DAY_DIL_NO;
        public string DAY_MAX_PR;
        public string DAY_MIN_PR;
        public string BNO_NAME_E;
        public string SYMBOL_E;
        public string STP_COD;
        public string COD_SHAAR;
        public string UPD_DAT;
        public string UPD_TIME;
    }

    public struct K300MadadType
    {
        public string SUG_RC;
        public string BNO_N;
        public string FIL1_VK;
        public string MDD_COD;
        public string MDD_SUG;
        public string MDD_N;
        public string FIL2_VK;
        public string MDD_NAME;
        public string Madad;
        public string FIL3_VK;
        public string MDD_DF;
        public string CALC_TIME;
        public string FIL6_VK;
        public string UPD_DAT;
        public string UPD_TIME;
    }

    public delegate void IK300Event_FireMaofCNTEventHandler(ref Array psaStrRecords, ref int nRecords);
    public delegate void IK300Event_FireMaofEventHandler(ref Array psaStrRecords, ref int nRecords);
    public delegate void IK300Event_FireRezefCNTEventHandler(ref Array psaStrRecords, ref int nRecords);
    public delegate void IK300Event_FireRezefEventHandler(ref Array psaStrRecords, ref int nRecords);

    public interface IK300
    {
        int GetMAOF(ref Array vecRecords, ref string strLastTime, string strOptionNumber, MadadTypes strMadad);
        int GetMAOFRaw(ref Array vecRecords, ref string strLastTime, string strOptionNumber, MadadTypes strMadad);
        void StopUpdate(int pnID);
        int StartStream(K300StreamType streamType, string strStockNumber, MadadTypes strMadad, int withEvents);
        int K300StartStream(K300StreamType streamType);
        int K300StopStream(K300StreamType streamType);
    }

    public interface K300 : IK300, IK300Event_Event
    {
    }

    public interface IK300Event_Event
    {
        // Events
        /*event IK300Event_FireMaofEventHandler FireMaof;
        event IK300Event_FireMaofCNTEventHandler FireMaofCNT;
        event IK300Event_FireRezefEventHandler FireRezef;
        event IK300Event_FireRezefCNTEventHandler FireRezefCNT;*/
    }


    public interface IK300Events
    {
    }

    public interface K300Events : IK300Events, _IK300EventsEvents_Event
    {
    }

    public delegate void _IK300EventsEvents_OnMaofEventHandler(ref K300MaofType data);
    public delegate void _IK300EventsEvents_OnRezefEventHandler(ref K300RzfType data);
    public delegate void _IK300EventsEvents_OnMadadEventHandler(ref K300MadadType data);

    public interface _IK300EventsEvents_Event
    {
        // Events
        event _IK300EventsEvents_OnMaofEventHandler OnMaof;
        event _IK300EventsEvents_OnRezefEventHandler OnRezef;
    }

    public class K300Class : IK300, K300, IK300Event_Event
    {
        // Events - even though they appear here,
        // these events are out of date, use K300EventsClass onMaof / on Rezef / onMadad instead
        // we'll need to clean up some time
        /*public event IK300Event_FireMaofEventHandler FireMaof;
        public event IK300Event_FireMaofCNTEventHandler FireMaofCNT;
        public event IK300Event_FireRezefEventHandler FireRezef;
        public event IK300Event_FireRezefCNTEventHandler FireRezefCNT;*/

        public K300Class()
        {
            maofStreamStarted = false;
            rezefStreamStarted = false;
            SimulationTop.k300Class = this;
        }

        // Methods
        public virtual int GetMAOF(ref Array vecRecords, ref string strLastTime, string strOptionNumber, MadadTypes strMadad)
        {
            return 0;
        }

        public virtual int GetMAOFRaw(ref Array vecRecords, ref string strLastTime, string strOptionNumber, MadadTypes strMadad)
        {
            return 0;
        }

        public virtual int GetBaseAssets2(out System.Array psaRecords, int BaseAssetCode)
        {
            psaRecords = null;
            return -1;
        }

        public virtual int GetBaseAssets(out System.Array psaRecords, int BaseAssetCode)
        {
            psaRecords = null;
            return -1;
        }

        public virtual int K300StartStream(K300StreamType streamType)
        {
            switch (streamType)
            {
                case K300StreamType.MaofStream:
                    {
                        // start data generation thread
                        maofGenerator.Start();

                        // set flag to keep track of the started streams
                        maofStreamStarted = true;
                        break;
                    }

                case K300StreamType.RezefStream:
                    {
                        rezefGenerator.Start();
                        rezefStreamStarted = true;
                        break;
                    }

                case K300StreamType.IndexStream:
                    {
                        madadGenerator.Start();
                        madadStreamStarted = true;
                        break;
                    }
                
                default:
                    break;
            }
            return 0;
        }

        /// <summary>
        /// initializes the simulation
        /// actual simulation stream will start by the K300StartStream
        /// </summary>
        public static void InitStreamSimulation(ISimulationDataGenerator<K300MaofType> maofGenerator)
        {
            K300Class.maofGenerator = maofGenerator;
        }

        public static void InitStreamSimulation(ISimulationDataGenerator<K300RzfType> rzfGenerator)
        {
            K300Class.rezefGenerator = rzfGenerator;
        }

        public static void InitStreamSimulation(ISimulationDataGenerator<K300MadadType> mddGenerator)
        {
            K300Class.madadGenerator = mddGenerator;
        }

        public virtual int K300StopStream(K300StreamType streamType)
        {
            switch (streamType)
            {
                case K300StreamType.MaofStream:
                    maofGenerator.Stop();
                    break;

                case K300StreamType.RezefStream:
                    rezefGenerator.Stop();
                    break;

                case K300StreamType.IndexStream:
                    madadGenerator.Stop();
                    break;

                default:
                    break;
            }
            return 0;
        }

        public virtual int StartStream(K300StreamType streamType, string strStockNumber, MadadTypes strMadad, int withEvents)
        {
            maofGenerator.Start();
            return 0;
        }

        public virtual void StopUpdate(int pnID)
        {
        }

        public int K300SessionId
        {
            set;
            get;
        }

        protected bool maofStreamStarted;
        protected bool rezefStreamStarted;
        protected bool madadStreamStarted;
        protected static ISimulationDataGenerator<K300MaofType> maofGenerator;
        protected static ISimulationDataGenerator<K300RzfType> rezefGenerator;
        protected static ISimulationDataGenerator<K300MadadType> madadGenerator;
    }

    public class K300EventsClass : IK300Events, K300Events, _IK300EventsEvents_Event
    {

        public K300EventsClass()
        {
            SimulationTop.k300EventsClass = this;
        }

        // Events
        public event _IK300EventsEvents_OnMaofEventHandler OnMaof;
        public event _IK300EventsEvents_OnRezefEventHandler OnRezef;
        public event _IK300EventsEvents_OnMadadEventHandler OnMadad;

        /// <summary>
        /// part of the simulation
        /// send event to all registered users 
        /// </summary>
        public void SendEventMaof(ref K300MaofType data)
        {
            OnMaof(ref data);
        }

        /// <summary>
        /// Send simulated Rezef data to all registered users
        /// </summary>
        public void SendEventRzf(ref K300RzfType data)
        {
            OnRezef(ref data);
        }

        /// <summary>
        /// Send simulated Rezef data to all registered users
        /// </summary>
        public void SendEventMdd(ref K300MadadType data)
        {
            OnMadad(ref data);
        }

        // Properties - are used to filter the events data 
        public BaseAssetTypes EventsFilterBaseAsset { set; get; }
        public int EventsFilterBno { set; get; }
        public int EventsFilterMadad { set; get; }
        public int EventsFilterMaof { set; get; }
        public MonthType EventsFilterMonth { set; get; }
        public int EventsFilterRezef { set; get; }
        public StockKind EventsFilterStockKind { set; get; }
        public MadadTypes EventsFilterStockMadad { set; get; }
    }


    public partial class UserClass
    {
        public UserClass()
        {
            _loginProgress = 0;
            _loginStatus = LoginStatus.LoginSessionInactive;
        }

        public int Login(string username, string AS400Password, string AppPassword, out string message, out int sessionId)
        {
            message = "";
            _sessionId = 1;
            sessionId = _sessionId;
            _loginStatus = LoginStatus.LoginSessionInProgress;
            _loginStarted = DateTime.Now;

            return _sessionId;
        }

        public void GetLoginActivity(ref int sessionId, out int percent, out string description)
        {
            // simulation - if in progress move things                      
            switch (_loginStatus)
            {
                case LoginStatus.LoginSessionInactive:
                    // do nothing until Login() is not being called
                    break;
                case LoginStatus.LoginSessionActive:
                    // login done - nothing more is required
                    break;

                default:
                    TimeSpan ts = TimeSpan.FromSeconds(1);
                    DateTime current = DateTime.Now;
                    if ((_loginStarted + ts) <= current)
                    {
                        _loginProgress += 50;
                        _loginStarted = current;
                    }
                    if (_loginProgress >= 100)
                    {
                        _loginStatus = LoginStatus.LoginSessionActive;
                    }
                    break;
            }
            percent = _loginProgress;
            description = "";
            sessionId = _sessionId;
        }


        public LoginStatus get_LoginState(ref int sessionId)
        {
            return _loginStatus;
        }

        public string get_LoginErrorDesc(ref int sessionId)
        {
            return _loginErrorDesc;
        }

        /// <summary>
        /// Carries out the (simulated) AS400 logout process.
        /// </summary>
        /// <param name="SessionId">A <see cref="System.Int32"/>
        /// The logout process requires a 
        /// unique Session Identification number that identifies 
        /// the session to be closed.</param>
        /// <returns>A <see cref="System.Int32"/>
        /// In the event of failure -1 is returned from the function.
        /// In the event of success 0 is returned from the function.</returns>
        public int Logout(int SessionId)
        {
            bool success=true;
            if (success) return 0;
            else return -1;
        }

        //public properties:
        /// <summary>
        /// Relays the customer number for the current taskbar configuration.
        /// </summary>
        public virtual string Cust { get {return this._cust;}  }
        
        /// <summary>
        /// 
        /// </summary>
        public virtual ConnectionState State { get { return this._cs; } }

        public virtual string System { get {return this._system; }}

        public virtual string SystemName { get { return this._sysName;} }


        protected int _loginProgress;
        protected int _sessionId;
        protected string _loginErrorDesc;
        protected LoginStatus _loginStatus;
        protected DateTime _loginStarted;

        protected string _cust = "aryeh";
        protected ConnectionState _cs;
        protected string _system="TBSim";
        protected string _sysName="TaskBarLibSim";

    }

    public class ConfigClass
    {
        /// <summary>
        /// This function is used to get timestamp from AS400 server in order to compute
        /// roundrip times and latencies
        /// </summary>
        /// <param name="dt">A <see cref="TaskBarLibSim.AS400DateTime"/></param>
        /// <param name="latency">A <see cref="System.Int32"/></param>
        /// <returns>0 if success, -1 if failure</returns>
        public int GetAS400DateTime(out AS400DateTime dt, out int latency)
        {
            DateTime now = DateTime.Now;

            dt = new AS400DateTime();

            //fill the AS400DateTime struct with updated values
            dt.year = now.Year;
            dt.Month = now.Month;
            dt.day = now.Day;
            dt.hour = now.Hour;
            dt.minute = now.Minute;
            dt.second = now.Second;
            dt.ms = now.Millisecond;

            //An arbitrary value for latency
            latency = new Random().Next(15, 250);

            bool success = true;
            if (success) return 0;
            else return -1;
        }
    }



    // From this line down - simulation relates classes which are part of the TaskBarLib API

    /// <summary>
    /// this is not part of the FMR's TaskBarLib. Part of the simulation engine
    /// </summary>
    /// <param name="generator">
    /// A <see cref="ISimulationStreamGenerator"/>
    /// </param>
    public interface ISimulationDataGenerator<DataType>
    {
        void Start();

        void Stop();
    }

    /// <summary>
    /// a thread firing specified event
    /// </summary>
    public abstract class EventGenerator<DataType> : ISimulationDataGenerator<DataType>
    {
        public EventGenerator()
        {
            notStopped = true;
        }

        public virtual void Start()
        {
            notStopped = true;
            new Thread(Run).Start();
            return;
        }

        public virtual void Stop()
        {
            notStopped = false;
            return;
        }

        /// <summary>
        /// thread main loop 
        /// </summary>
        protected void Run()
        {
            while (notStopped)
            {
                DataType data;
                bool result = GetData(out data);

                if (!result)
                {
                    break;
                }
                SendEvents(ref data);
            }
        }


        /// <summary>
        /// Returns next chunk of data of type. If Ok return true 
        /// The method should block the calling thread until the next chunk
        /// of data is available or there is no more data will ever be available
        /// in the later case the method will return false
        /// </summary>
        /// <returns>
        /// A <see cref="System.Object"/>
        /// Data will be set to the next generated chunk
        /// </returns>
        protected abstract bool GetData(out DataType data);

        protected abstract void SendEvents(ref DataType data);

        private bool notStopped;
    }


    /// <summary>
    /// this is a thread generating 
    /// very siimple all fields are random Maof data generator
    /// objects of this type used as an argument to the InitStreamSimulation
    /// </summary>
    /// <param name="maofGenerator">
    /// A <see cref="ISimulationStreamGenerator"/>
    /// </param>
    public class MaofDataGeneratorRandom : EventGenerator<K300MaofType>, ISimulationDataGenerator<K300MaofType>, JQuant.IDataGenerator
    {
        public MaofDataGeneratorRandom()
        {
            randomString = new JQuant.RandomNumericalString(21, 80155);

            Type t = typeof(K300MaofType);
            fields = t.GetFields();
            count = 0;

            return;
        }

        protected override bool GetData(out K300MaofType data)
        {
            // delay - usually delay will be in the GetData
            // GetData reads log, pulls the time stamps and simulates
            // timing of the real data stream
            Thread.Sleep(50);

            // create a new object
            data = new K300MaofType();

            // set all fields in the object
            data.SUG_REC = randomString.Next();
            data.TRADE_METH = randomString.Next();
            data.BNO_Num = randomString.Next();
            data.LAST_REC = randomString.Next();
            data.SIDURI_Num = randomString.Next();
            data.SYMBOL_E = randomString.Next();
            data.Symbol = randomString.Next();
            data.BNO_NAME_E = randomString.Next();
            data.BNO_NAME = randomString.Next();
            data.BRANCH_NO = randomString.Next();
            data.BRANCH_U = randomString.Next();
            data.SUG_BNO = randomString.Next();
            data.MIN_UNIT = randomString.Next();
            data.HARIG_NV = randomString.Next();
            data.MIN_PR = randomString.Next();
            data.MAX_PR = randomString.Next();
            data.BASIS_PRC = randomString.Next();
            data.BASIS_COD = randomString.Next();
            data.STATUS_COD = randomString.Next();
            data.EX_DATE = randomString.Next();
            data.EX_PRC = randomString.Next();
            data.VL_MULT = randomString.Next();
            data.VL_COD = randomString.Next();
            data.ZERO_COD = randomString.Next();
            data.shlav = randomString.Next();
            data.STATUS = randomString.Next();
            data.TRD_STP_CD = randomString.Next();
            data.TRD_STP_N = randomString.Next();
            data.STP_OPN_TM = randomString.Next();
            data.LMT_BY1 = randomString.Next();
            data.LMT_BY2 = randomString.Next();
            data.LMT_BY3 = randomString.Next();
            data.LMY_BY1_NV = randomString.Next();
            data.LMY_BY2_NV = randomString.Next();
            data.LMY_BY3_NV = randomString.Next();
            data.RWR_FE = randomString.Next();
            data.LMT_SL1 = randomString.Next();
            data.LMT_SL2 = randomString.Next();
            data.LMT_SL3 = randomString.Next();
            data.LMY_SL1_NV = randomString.Next();
            data.LMY_SL2_NV = randomString.Next();
            data.LMY_SL3_NV = randomString.Next();
            data.RWR_FF = randomString.Next();
            data.PRC = randomString.Next();
            data.COD_PRC = randomString.Next();
            data.SUG_PRC = randomString.Next();
            data.LST_DF_BS = randomString.Next();
            data.RWR_FG = randomString.Next();
            data.LST_DL_PR = randomString.Next();
            data.LST_DL_TM = randomString.Next();
            data.LST_DL_VL = randomString.Next();
            data.DAY_VL = randomString.Next();
            data.DAY_VL_NIS = randomString.Next();
            data.DAY_DIL_NO = randomString.Next();
            data.RWR_FH = randomString.Next();
            data.DAY_MAX_PR = randomString.Next();
            data.DAY_MIN_PR = randomString.Next();
            data.POS_OPN = randomString.Next();
            data.POS_OPN_DF = randomString.Next();
            data.STS_NXT_DY = randomString.Next();
            data.UPD_DAT = randomString.Next();
            data.UPD_TIME = randomString.Next();
            data.FILER = randomString.Next();

            count += 1;


            return true;
        }

        protected override void SendEvents(ref K300MaofType data)
        {
            SimulationTop.k300EventsClass.SendEventMaof(ref data);
            // avoid tight loops in the system
            Thread.Sleep(50);
        }

        public int GetCount()
        {
            return count;
        }

        public string GetName()
        {
            return "Maof data random generator";
        }


        JQuant.IRandomString randomString;
        protected FieldInfo[] fields;
        int count;
    }



    /// <summary>
    /// this is a thread generating 
    /// very siimple all fields are random Rezef data generator
    /// objects of this type used as an argument to the InitStreamSimulation
    /// </summary>
    /// <param name="maofGenerator">
    /// A <see cref="ISimulationStreamGenerator"/>
    /// </param>
    public class RezefDataGeneratorRandom : EventGenerator<K300RzfType>, ISimulationDataGenerator<K300RzfType>, JQuant.IDataGenerator
    {
        public RezefDataGeneratorRandom()
        {
            randomString = new JQuant.RandomNumericalString(21, 80155);

            Type t = typeof(K300RzfType);
            fields = t.GetFields();
            count = 0;

            return;
        }

        protected override bool GetData(out K300RzfType data)
        {
            // delay - usually delay will be in the GetData
            // GetData reads log, pulls the time stamps and simulates
            // timing of the real data stream
            Thread.Sleep(50);

            // create a new object
            data = new K300RzfType();

            // set all fields in the object
            data.SUG_REC = randomString.Next();
            data.BNO_Num = randomString.Next();
            data.BNO_NAME = randomString.Next();
            data.Symbol = randomString.Next();
            data.TRADE_METH = randomString.Next();
            data.SIDURI_Num = randomString.Next();
            data.RWR_VA = randomString.Next();
            data.MIN_UNIT = randomString.Next();
            data.HARIG_NV = randomString.Next();
            data.MIN_PR_OPN = randomString.Next();
            data.MAX_PR_OPN = randomString.Next();
            data.MIN_PR_CNT = randomString.Next();
            data.MAX_PR_CNT = randomString.Next();
            data.BASIS_PRC = randomString.Next();
            data.STATUS = randomString.Next();
            data.EX_COD = randomString.Next();
            data.EX_DETAIL = randomString.Next();
            data.RWR_VB = randomString.Next();
            data.shlav = randomString.Next();
            data.LAST_PRC = randomString.Next();
            data.TRD_STP_N = randomString.Next();
            data.STP_OPN_TM = randomString.Next();
            data.RWR_VD = randomString.Next();
            data.LMT_BY1 = randomString.Next();
            data.LMT_BY2 = randomString.Next();
            data.LMT_BY3 = randomString.Next();
            data.LMY_BY1_NV = randomString.Next();
            data.LMY_BY2_NV = randomString.Next();
            data.LMY_BY3_NV = randomString.Next();
            data.MKT_NV_BY = randomString.Next();
            data.MKT_NV_BY_NUM = randomString.Next();
            data.RWR_VE = randomString.Next();
            data.LMT_SL1 = randomString.Next();
            data.LMT_SL2 = randomString.Next();
            data.LMT_SL3 = randomString.Next();
            data.LMY_SL1_NV = randomString.Next();
            data.LMY_SL2_NV = randomString.Next();
            data.LMY_SL3_NV = randomString.Next();
            data.MKT_NV_SL = randomString.Next();
            data.MKT_NV_SL_NUM = randomString.Next();
            data.RWR_VF = randomString.Next();
            data.THEOR_PR = randomString.Next();
            data.THEOR_VL = randomString.Next();
            data.RWR_VG = randomString.Next();
            data.LST_DL_PR = randomString.Next();
            data.LST_DL_TM = randomString.Next();
            data.LST_DF_BS = randomString.Next();
            data.LST_DF_OPN = randomString.Next();
            data.LST_DL_VL = randomString.Next();
            data.DAY_VL = randomString.Next();
            data.DAY_VL_NIS = randomString.Next();
            data.DAY_DIL_NO = randomString.Next();
            data.DAY_MAX_PR = randomString.Next();
            data.DAY_MIN_PR = randomString.Next();
            data.BNO_NAME_E = randomString.Next();
            data.SYMBOL_E = randomString.Next();
            data.STP_COD = randomString.Next();
            data.COD_SHAAR = randomString.Next();
            data.UPD_DAT = randomString.Next();
            data.UPD_TIME = randomString.Next();

            count += 1;


            return true;
        }

        protected override void SendEvents(ref K300RzfType data)
        {
            SimulationTop.k300EventsClass.SendEventRzf(ref data);
            // avoid tight loops in the system
            Thread.Sleep(50);
        }

        public int GetCount()
        {
            return count;
        }

        public string GetName()
        {
            return "Rezef data random generator";
        }


        JQuant.IRandomString randomString;
        protected FieldInfo[] fields;
        int count;
    }


    /// <summary>
    /// this is a thread generating 
    /// very siimple all fields are random Madad data generator
    /// objects of this type used as an argument to the InitStreamSimulation
    /// </summary>
    /// <param name="maofGenerator">
    /// A <see cref="ISimulationStreamGenerator"/>
    /// </param>
    public class MadadDataGeneratorRandom : EventGenerator<K300MadadType>, ISimulationDataGenerator<K300MadadType>, JQuant.IDataGenerator
    {
        public MadadDataGeneratorRandom()
        {
            randomString = new JQuant.RandomNumericalString(21, 80155);

            Type t = typeof(K300MadadType);
            fields = t.GetFields();
            count = 0;

            return;
        }

        protected override bool GetData(out K300MadadType data)
        {
            // delay - usually delay will be in the GetData
            // GetData reads log, pulls the time stamps and simulates
            // timing of the real data stream
            Thread.Sleep(50);

            // create a new object
            data = new K300MadadType();

            // set all fields in the object
            data.SUG_RC = randomString.Next();
            data.BNO_N = randomString.Next();
            data.FIL1_VK = randomString.Next();	
            data.MDD_COD = randomString.Next();	
            data.MDD_SUG = randomString.Next();
            data.MDD_N = randomString.Next();
            data.FIL2_VK = randomString.Next();
            data.MDD_NAME = randomString.Next();
            data.Madad = randomString.Next();	
            data.FIL3_VK = randomString.Next();	
            data.MDD_DF = randomString.Next();
            data.CALC_TIME = randomString.Next();	
            data.FIL6_VK = randomString.Next();
            data.UPD_DAT = randomString.Next();
            data.UPD_TIME = randomString.Next();
            count += 1;
            
            return true;
        }

        protected override void SendEvents(ref K300MadadType data)
        {
            SimulationTop.k300EventsClass.SendEventMdd(ref data);
            // avoid tight loops in the system
            Thread.Sleep(50);
        }

        public int GetCount()
        {
            return count;
        }

        public string GetName()
        {
            return "Madad data random generator";
        }


        JQuant.IRandomString randomString;
        protected FieldInfo[] fields;
        int count;
    }



    /// <summary>
    /// I need some class where all apparently disconnected classes are connected
    /// For example K300Class and K300EventsClass
    /// User application registers event listeners in the EventClass and calls StartStream
    /// in the K300Class
    /// </summary>
    class SimulationTop
    {
        public static K300EventsClass k300EventsClass;
        public static K300Class k300Class;
    }

}
