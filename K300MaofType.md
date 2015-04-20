Maof market data records structure, used to parse the raw records.

**K300 MAOF structure**

|	No.	|	 Record Name	| Description	|	   Comments	|
|:----|:-------------|:------------|:------------|
|	1	|	 SUG\_REC	|	סוג רשומה	|	   	|
|	2	|	 TRADE\_METH	|	שיטת מסחר	|	   	|
|	3	|	 BNO\_Num	|	מס בורסה	|	מי מאלה 	|
|	4	|	 LAST\_REC	|	סימן לרשומה אחרונה	|		|
|	5	|	 SIDURI\_Num	|	מס סדורי	|	 הוא מס' ני"ע ?	|
|	6	|	 SYMBOL\_E	|	סימבול אנגלי	|	   	|
|	7	|	 Symbol	|	 סימבול עברי	|	   	|
|	8	|	 BNO\_NAME\_E	|	שם אנגלי	|	   	|
|	9	|	 BNO\_NAME	|	שם עברי	|	   	|
|	10	|	 BRANCH\_NO	|	מספר ענף	|	   	|
|	11	|	 BRANCH\_U	|	מספר תת ענף	|	   	|
|	12	|	 SUG\_BNO	|	סוג חוזה	|	   	|
|	13	|	 MIN\_UNIT	|	MIN גודל פקודה	|	   	|
|	14	|	 HARIG\_NV	|	גבול פקודה חריגה	|	   	|
|	15	|	 MIN\_PR	|	שער רצפה	|	   	|
|	16	|	 MAX\_PR	|	שער תקרה	|	   	|
|	17	|	 BASIS\_PRC	|	שער בסיס	|	   	|
|	18	|	 BASIS\_COD	|	קוד סוג שער בסיס	|	   	|
|	19	|	 STATUS\_COD	|	קוד מצב חוזה	|	   	|
|	20	|	 EX\_DATE	|	תאריך פקיעה	|	   	|
|	21	|	 EX\_PRC	|	מחיר מימוש	|	   	|
|	22	|	 VL\_MULT	|	מכפיל לחישוב נפח	|	   	|
|	23	|	 VL\_COD	|	קוד לחישוב נפח	|	   	|
|	24	|	 ZERO\_COD	|	קוד איפוס	|	   	|
|	25	|	 shlav	|	שלב מסחר	|	   	|
|	26	|	 STATUS	|	מצב ניע	|	   	|
|	27	|	 TRD\_STP\_CD	|	קוד הפסקת מסחר	|	   	|
|	28	|	 TRD\_STP\_N	|	סיבת הפסקת מסחר	|	   	|
|	29	|	 STP\_OPN\_TM	|	שעת פתיחת הפסקה	|	   	|
|	30	|	 LMT\_BY1	|	1 לימיט קניה	|	   	|
|	31	|	 LMT\_BY2	|	2 לימיט קניה	|	   	|
|	32	|	 LMT\_BY3	|	3 לימיט קניה	|	   	|
|	33	|	 LMY\_BY1\_NV	|	כמות לימיט קניה 1	|	   	|
|	34	|	 LMY\_BY2\_NV	|	כמות לימיט קניה 2	|	   	|
|	35	|	 LMY\_BY3\_NV	|	כמות לימיט קניה 3	|	   	|
|	36	|	 RWR\_FE	|	שידור חוזר/תיקון	|	מה זה?	|
|	37	|	 LMT\_SL1	|	1 לימיט מכירה	|	   	|
|	38	|	 LMT\_SL2	|	2 לימיט מכירה	|	   	|
|	39	|	 LMT\_SL3	|	3 לימיט מכירה	|	   	|
|	40	|	 LMY\_SL1\_NV	|	כמות לימיט מכירה 1	|	   	|
|	41	|	 LMY\_SL2\_NV	|	כמות לימיט מכירה 2	|	   	|
|	42	|	 LMY\_SL3\_NV	|	כמות לימיט מכירה 3	|	   	|
|	43	|	 RWR\_FF	|	שידור חוזר/תיקון	|	מה זה?	|
|	44	|	 PRC	|	שער	|	   	|
|	45	|	 COD\_PRC	|	קוד שער	|	   	|
|	46	|	 SUG\_PRC	|	קוד סוג שער	|	   	|
|	47	|	 LST\_DF\_BS	|	שינוי  מהבסיס %	|	   	|
|	48	|	 RWR\_FG	|	שידור חוזר/תיקון	|	מה זה?	|
|	49	|	 LST\_DL\_PR	|	שער עסקה אחרונה	|	   	|
|	50	|	 LST\_DL\_TM	|	שעת עסקה אחרונה	|	   	|
|	51	|	 LST\_DL\_VL	|	מחזור עסקה אחרונה	|	   	|
|	52	|	 DAY\_VL	|	מחזור מתחילת היום	|	   	|
|	53	|	 DAY\_VL\_NIS	|	תמורה בשח היום	|	   	|
|	54	|	 DAY\_DIL\_NO	|	מספר עסקאות היום	|	   	|
|	55	|	 RWR\_FH	|	שידור חוזר/תיקון	|	מה זה?	|
|	56	|	 DAY\_MAX\_PR	|	שער יומי גבוה	|	   	|
|	57	|	 DAY\_MIN\_PR	|	שער יומי נמוך	|	   	|
|	58	|	 POS\_OPN	|	פוזיציות פתוחות	|	   	|
|	59	|	 POS\_OPN\_DF	|	אחוז שינוי פוזיציות	|	   	|
|	60	|	 STS\_NXT\_DY	|	מצב ליום הבא	|	   	|
|	61	|	 UPD\_DAT	|	תאריך	|	   	|
|	62	|	 UPD\_TIME	|	שעה	|	   	|
|	63	|	 FILER	|	  	|	   	|


**Code**
```
    struct tagK300MaofType {
        [helpstring("סוג רשומה")]
        BSTR SUG_REC;
        [helpstring("שיטת מסחר")]
        BSTR TRADE_METH;
        [helpstring("מס בורסה")]
        BSTR BNO_Num;
        [helpstring("סימן לרשומה אחרונה")]
        BSTR LAST_REC;
        [helpstring("מס סדורי")]
        BSTR SIDURI_Num;
        [helpstring("סימבול אנגלי")]
        BSTR SYMBOL_E;
        [helpstring(" סימבול עברי")]
        BSTR Symbol;
        [helpstring("שם אנגלי")]
        BSTR BNO_NAME_E;
        [helpstring("שם עברי")]
        BSTR BNO_NAME;
        [helpstring("מספר ענף")]
        BSTR BRANCH_NO;
        [helpstring("מספר תת ענף")]
        BSTR BRANCH_U;
        [helpstring("סוג חוזה")]
        BSTR SUG_BNO;
        [helpstring("MIN גודל פקודה")]
        BSTR MIN_UNIT;
        [helpstring("גבול פקודה חריגה")]
        BSTR HARIG_NV;
        [helpstring("שער רצפה")]
        BSTR MIN_PR;
        [helpstring("שער תקרה")]
        BSTR MAX_PR;
        [helpstring("שער בסיס")]
        BSTR BASIS_PRC;
        [helpstring("קוד סוג שער בסיס")]
        BSTR BASIS_COD;
        [helpstring("קוד מצב חוזה")]
        BSTR STATUS_COD;
        [helpstring("תאריך פקיעה")]
        BSTR EX_DATE;
        [helpstring("מחיר מימוש")]
        BSTR EX_PRC;
        [helpstring("מכפיל לחישוב נפח")]
        BSTR VL_MULT;
        [helpstring("קוד לחישוב נפח")]
        BSTR VL_COD;
        [helpstring("קוד איפוס")]
        BSTR ZERO_COD;
        [helpstring("שלב מסחר")]
        BSTR shlav;
        [helpstring("מצב ניע")]
        BSTR STATUS;
        [helpstring("קוד הפסקת מסחר")]
        BSTR TRD_STP_CD;
        [helpstring("סיבת הפסקת מסחר")]
        BSTR TRD_STP_N;
        [helpstring("שעת פתיחת הפסקה")]
        BSTR STP_OPN_TM;
        [helpstring("1 לימיט קניה")]
        BSTR LMT_BY1;
        [helpstring("2 לימיט קניה")]
        BSTR LMT_BY2;
        [helpstring("3 לימיט קניה")]
        BSTR LMT_BY3;
        [helpstring("1כמות לימיט קניה")]
        BSTR LMY_BY1_NV;
        [helpstring("2כמות לימיט קניה")]
        BSTR LMY_BY2_NV;
        [helpstring("3כמות לימיט קניה")]
        BSTR LMY_BY3_NV;
        [helpstring("שידור חוזר/תיקון")]
        BSTR RWR_FE;
        [helpstring("1 לימיט מכירה")]
        BSTR LMT_SL1;
        [helpstring("2 לימיט מכירה")]
        BSTR LMT_SL2;
        [helpstring("3 לימיט מכירה")]
        BSTR LMT_SL3;
        [helpstring("כמות לימיט מכירה 1")]
        BSTR LMY_SL1_NV;
        [helpstring("כמות לימיט מכירה 2")]
        BSTR LMY_SL2_NV;
        [helpstring("כמות לימיט מכירה 3")]
        BSTR LMY_SL3_NV;
        [helpstring("שידור חוזר/תיקון")]
        BSTR RWR_FF;
        [helpstring("שער")]
        BSTR PRC;
        [helpstring("קוד שער")]
        BSTR COD_PRC;
        [helpstring("קוד סוג שער")]
        BSTR SUG_PRC;
        [helpstring("שינוי  מהבסיס %")]
        BSTR LST_DF_BS;
        [helpstring("שידור חוזר/תיקון")]
        BSTR RWR_FG;
        [helpstring("שער עסקה אחרונה")]
        BSTR LST_DL_PR;
        [helpstring("שעת עסקה אחרונה")]
        BSTR LST_DL_TM;
        [helpstring("מחזור עסקה אחרונה")]
        BSTR LST_DL_VL;
        [helpstring("מחזור מתחילת היום")]
        BSTR DAY_VL;
        [helpstring("תמורה בשח היום")]
        BSTR DAY_VL_NIS;
        [helpstring("מספר עסקאות היום")]
        BSTR DAY_DIL_NO;
        [helpstring("שידור חוזר/תיקון")]
        BSTR RWR_FH;
        [helpstring("שער יומי גבוה")]
        BSTR DAY_MAX_PR;
        [helpstring("שער יומי נמוך")]
        BSTR DAY_MIN_PR;
        [helpstring("פוזיציות פתוחות")]
        BSTR POS_OPN;
        [helpstring("אחוז שינוי פוזיציות")]
        BSTR POS_OPN_DF;
        [helpstring("מצב ליום הבא")]
        BSTR STS_NXT_DY;
        [helpstring("תאריך")]
        BSTR UPD_DAT;
        [helpstring("שעה")]
        BSTR UPD_TIME;
        [helpstring("  ")]
        BSTR FILER;
    } K300MaofType;
```