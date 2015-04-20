
```
    public struct MOFINQType
    {
        public string ERR;          //שגוי
        public string SYS_TYPE;     //סיסטם מקור הפקודה
        public string SND_RCV; 	    // -מטרת השדר
        public string ORDR_TYPE;    //MANA/ORDR  שיטת הפקודה
        public string OPR_NAME;     // סיסמאת המשתמש
        public string SUG_INQ; 	    //0=DETAIL     סוג פירוט
        public string SEQ_PIC; 	    // FMR  -    ספרור שוטף
        public string MANA_PIC;     //מספר מנה/שעה אחרונה
        public string BNO_PIC; 	    //מספר נייר ערך
        public string BNO_NAME;     //שם נייר
        public string OP;           //קוד קניה / מכירה
        public string BRANCH_PIC;   //סניף
        public string TIK_PIC; 	    //תיק
        public string SUG_MAVR_PIC; // סוג  חשבון מפצל/מעבר
        public string ID_MAVR_PIC;  //מספר חשבון מפצל/מעבר
        public string SUG_PIC; 	    // סוג חשבון
        public string ID_PIC; 	    //חשבון
        public string ID_NAME; 	    //שם החשבון
        public string SUG_ID_PIC;   //סוג חשבון
        public string NOSTRO; 	    //קוד נוסטרו
        public string ORDR_NV_PIC;  //כמות מבוקשת
        public string ORDR_SUG;     //סוג פקודה
        public string ORDR_PRC_PIC; //מחיר הזמנה
        public string ORDR_TIME;    //זמן קליטת ההזמנה
        public string DIL_NV_PIC;   //כמות בעסקה
        public string DIL_PRC_PIC;  //מחיר עסקה
        public string DIL_TIME_PIC; //שעת העסקה
        public string MBR_SEQ_PIC;  //מספר הודעת חבר
        public string RZF_SEQ_PIC;  //מספר הודעת רצף
        public string RZF_ORD_PIC;  //מספר פקודה ברצף
        public string ORDER_NO_PIC; //HOST - ספרור מיוחד
        public string DIL_PIC; 	    //מספר אישור עסקה
        public string COD_UPD; 	    //סטטוס הפקודה
        public string STS; 	    //סטטוס הפקודה
        public string ERR_DATA;     //נתונים חסרים/שגויים
        public string ERR_INQ; 	    //שגוי
        public string ERR_UPD; 	    //שגוי
        public string SUG_INFO;     //0=ORDR  1=DIL  2=SUM  
        public string DSP_FMR; 	    //מצב הפקודה
    }
```

```
    struct tagMOFINQType {
        [helpstring("                  שגוי")]
        BSTR ERR;
        [helpstring("     סיסטם מקור הפקודה")]
        BSTR SYS_TYPE;
        [helpstring("            -מטרת השדר")]
        BSTR SND_RCV;
        [helpstring("MANA/ORDR  שיטת הפקודה")]
        BSTR ORDR_TYPE;
        [helpstring("         סיסמאת המשתמש")]
        BSTR OPR_NAME;
        [helpstring("0=DETAIL     סוג פירוט")]
        BSTR SUG_INQ;
        [helpstring("  FMR  -    ספרור שוטף")]
        BSTR SEQ_PIC;
        [helpstring("                      ")]
        BSTR MANA_PIC;
        [helpstring("         מספר נייר ערך")]
        BSTR BNO_PIC;
        [helpstring("               שם נייר")]
        BSTR BNO_NAME;
        [helpstring("      קוד קניה / מכירה")]
        BSTR OP;
        [helpstring("                  סניף")]
        BSTR BRANCH_PIC;
        [helpstring("                   תיק")]
        BSTR TIK_PIC;
        [helpstring("                      ")]
        BSTR SUG_MAVR_PIC;
        [helpstring("                      ")]
        BSTR ID_MAVR_PIC;
        [helpstring("                      ")]
        BSTR SUG_PIC;
        [helpstring("                      ")]
        BSTR ID_PIC;
        [helpstring("             שם החשבון")]
        BSTR ID_NAME;
        [helpstring("                      ")]
        BSTR SUG_ID_PIC;
        [helpstring("            קוד נוסטרו")]
        BSTR NOSTRO;
        [helpstring("           כמות מבוקשת")]
        BSTR ORDR_NV_PIC;
        [helpstring("             סוג פקודה")]
        BSTR ORDR_SUG;
        [helpstring("            מחיר הזמנה")]
        BSTR ORDR_PRC_PIC;
        [helpstring("      זמן קליטת ההזמנה")]
        BSTR ORDR_TIME;
        [helpstring("            כמות בעסקה")]
        BSTR DIL_NV_PIC;
        [helpstring("             מחיר עסקה")]
        BSTR DIL_PRC_PIC;
        [helpstring("             שעת העסקה")]
        BSTR DIL_TIME_PIC;
        [helpstring("        מספר הודעת חבר")]
        BSTR MBR_SEQ_PIC;
        [helpstring("        מספר הודעת רצף")]
        BSTR RZF_SEQ_PIC;
        [helpstring("       מספר פקודה ברצף")]
        BSTR RZF_ORD_PIC;
        [helpstring("    HOST - ספרור מיוחד")]
        BSTR ORDER_NO_PIC;
        [helpstring("       מספר אישור עסקה")]
        BSTR DIL_PIC;
        [helpstring("          סטטוס הפקודה")]
        BSTR COD_UPD;
        [helpstring("          סטטוס הפקודה")]
        BSTR STS;
        [helpstring("   נתונים חסרים/שגויים")
        BSTR ERR_DATA;
        [helpstring("                  שגוי")]
        BSTR ERR_INQ;
        [helpstring("                  שגוי")]
        BSTR ERR_UPD;
        [helpstring("0=ORDR  1=DIL  2=SUM  ")]
        BSTR SUG_INFO;
        [helpstring("            מצב הפקודה")]
        BSTR DSP_FMR;
    } MOFINQType;
```