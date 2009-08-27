using System;

/// <summary>
/// I want to run TaskLib simulation. I am going to implement API using prerecorded
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
    public enum BaseAssetTypes
    {
        BaseAssetAll = -1,
        BaseAssetBanks = 4,
        BaseAssetDollar = 2,
        BaseAssetEuro = 5,
        BaseAssetInterest = 3,
        BaseAssetMaof = 1,
        BaseAssetShacharAroch = 7,
        BaseAssetShacharBenoni = 6
    }

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
        MaofCNTStream = 50,
        MaofStream = 0x30,
        RezefCNTStream = 0x33,
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

    public enum OrderOperation
    {
        OrderOperationNewBuy,
        OrderOperationNewSell,
        OrderOperationUpdBuy,
        OrderOperationUpdSell,
        OrderOperationDelete
    }

    public enum OrdersErrorTypes
    {
        Alert = 0x34,
        Confirmation = 0x31,
        Fatal = 0x30,
        NoError = 0x35,
        PasswordReq = 0x33,
        ReEnter = 50
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
        public int month;
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
        event IK300Event_FireMaofEventHandler FireMaof;
        event IK300Event_FireMaofCNTEventHandler FireMaofCNT;
        event IK300Event_FireRezefEventHandler FireRezef;
        event IK300Event_FireRezefCNTEventHandler FireRezefCNT;
    }


    public interface IK300Events
    {
    }

    public interface K300Events : IK300Events, _IK300EventsEvents_Event
    {
    }

    public delegate void _IK300EventsEvents_OnMaofEventHandler(ref K300MaofType data);


    public interface _IK300EventsEvents_Event
    {
        // Events
        event _IK300EventsEvents_OnMaofEventHandler OnMaof;
    }

    public class K300Class : IK300, K300, IK300Event_Event
    {
        // Events
        public event IK300Event_FireMaofEventHandler FireMaof;
        public event IK300Event_FireMaofCNTEventHandler FireMaofCNT;
        public event IK300Event_FireRezefEventHandler FireRezef;
        public event IK300Event_FireRezefCNTEventHandler FireRezefCNT;

        // Methods
        public virtual int GetMAOF(ref Array vecRecords, ref string strLastTime, string strOptionNumber, MadadTypes strMadad)
        {
            return 0;
        }

        public virtual int GetMAOFRaw(ref Array vecRecords, ref string strLastTime, string strOptionNumber, MadadTypes strMadad)
        {
            return 0;
        }

        public virtual int K300StartStream(K300StreamType streamType)
        {
            return 0;
        }

        public virtual int K300StopStream(K300StreamType streamType)
        {
            return 0;
        }

        public virtual int StartStream(K300StreamType streamType, string strStockNumber, MadadTypes strMadad, int withEvents)
        {
            return 0;
        }

        public virtual void StopUpdate(int pnID)
        {
        }

    }

    public class K300EventsClass : IK300Events, K300Events, _IK300EventsEvents_Event
    {
        // Events
        public event _IK300EventsEvents_OnMaofEventHandler OnMaof;

        // Properties
    }


    public class UserClass
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
                        _loginProgress += 20;
                        _loginStarted = current;
                    }
                    if (_loginProgress >= 100)
                    {
                        _loginStatus = LoginStatus.LoginSessionActive;
                    }
                    break;
            }
            percent = _loginProgress;
            description = "No login description";
            sessionId = _sessionId;
        }


        public LoginStatus get_LoginState(ref int sessionId)
        {
            return _loginStatus;
        }

        protected int _loginProgress;
        protected int _sessionId;
        protected LoginStatus _loginStatus;
        protected DateTime _loginStarted;


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
            //fill the AS400DateTime struct with updated values
            dt.year = now.Year;
            dt.month = now.Month;
            dt.minute = now.Minute;
            dt.second = now.Second;
            dt.ms = now.Millisecond;

            //An arbitrary value for latency
            latency = 50;

            bool success = true;
            if (success) return 0;
            else return -1;
        }
    }
}
