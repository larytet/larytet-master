|	**No.**	|	**Name**	|	**Description in the manual**	|	**Helpstring in the IDL script**	|	**Comment**	|
|:--------|:---------|:------------------------------|:---------------------------------|:------------|
|	1	|	[CalculateScenarios](CalculateScenarios.md)	|	Calculates Maof scenarios for securities calculations. 	|	Calculates scenarios for all options based on current madad and IV	|	-	|
|	2	|	[GetK300Madad](GetK300Madad.md)	|	Retrieves all Index market data from taskbar.	|	משיכת מדדים מקו 300. ניתן לסנן לפי מספר המדד. נתוני שוק מתעדכנים מהמחשב הראשי או משרתי ההפצה, בהתאם לתצורת ההתקנה	|	-	|
|	3	|	[GetK300MF](GetK300MF.md)	|	Retrieves all maof market data from taskbar.	|	משיכת נתוני שוק של אופציות ומדדים מקו 300. ניתן לסנן לפי סוג המדד או מספר אופציה. אם שניהם ניתנים, הסינון יתבצע לפי האופציה. נתוני שוק מתעדכנים מהמחשב  הראשי או משרתי ההפצה, בהתאם לתצורת ההתקנה	|	-	|
|	4	|	[GetK300RZ](GetK300RZ.md)	|	Retrieves all maof market data from taskbar.	|	משיכת נתוני שוק של מניות מקו 300. ניתן לסנן לפי שייכות למדד, סוג המנייה או מספר המניה, לפי סדר קדימויות זה. נתוני שוק מתעדכנים מהמחשב  הראשי או משרתי ההפצה, בהתאם לתצורת ההתקנה	|	-	|
|	5	|	[GetMadadHistory](GetMadadHistory.md)	|	 Retrieves index history. 	|	Retrieves Statistics information	|	-	|
|	6	|	[GetSH161](GetSH161.md)	|	 Retrieves weight of securities in indexes. 	|	סוג חומר 161	|	ככל הנראה מדובר בטבלה 161 של הבורסה, פרטים כאן: https://www.tase.co.il/TASE/TypeDesc/tase0161.def	|
|	7	|	[K300StartStream](K300StartStream.md)	|	 Begins streaming of Market data into the Taskbar from the K300 server or AS/400. 	|	התחל זרימת נתוני שוק לתוך הטסקבר מהמחשב הראשי או מהשרתים. ערך מוחזר 0 מציין הצלחה	|	-	|
|	8	|	[K300StopStream](K300StopStream.md)	|	 Ceases streaming of Market data into the Taskbar from the K300 server or AS/400. 	|	הפסק זרימת נתוני שוק לתוך הטסקבר מהמחשב הראשי או מהשרתים. ערך מוחזר 0 מציין הצלחה	|	-	|
|	9	|	[CreateK300EventsFile](CreateK300EventsFile.md)	|	-	|	שומר את כל אירועי השוק בקובץ. מקבל מחיצה, בה יישמרו הקבצים, שם קובץ, מספר אירועים לכל קובץ.	|	Not documented in the manual	|
|	10	|	[GetBaseAssets](GetBaseAssets.md)	|	-	|	מחזיר רשימת נתוני נכסי בסיס. ניתן לסנן לפי קוד נכס בסיס	|	Not documented in the manual	|
|	11	|	[GetBaseAssets2](GetBaseAssets2.md)	|	-	|	Retrieves the base assets info	|	Not documented in the manual	|
|	12	|	[GetConstStock](GetConstStock.md)	|	-	|	Retrieves Rezef Const Stock	|	Not documented in the manual	|
|	13	|	[GetIndexes](GetIndexes.md)	|	-	|	Retrieves the list of indexes and their details	|	Not documented in the manual	|
|	14	|	[GetIndexStructure](GetIndexStructure.md)	|	-	|	Retrieves the list of indexes structures	|	Not documented in the manual	|
|	15	|	[GetMAOF](GetMAOF.md)	|	-	|	Retrieves the MAOF records according to the last time and option number. If no last time is specified the updated records are retrieved. If no option number is specified all options are retrieved	|	Not documented in the manual	|
|	16	|	[GetMAOFByBaseAsset](GetMAOFByBaseAsset.md)	|	-	|	Retrieves the MAOF records according to the base asset number	|	Not documented in the manual	|
|	17	|	[GetMaofCnt](GetMaofCnt.md)	|	-	|	Retrieves Maof CNT	|	Outdated? Not documented in the manual	|
|	18	|	[GetMAOFRaw](GetMAOFRaw.md)	|	-	|	Retrieves the MAOF records (as a flat string) according to the last time and option number. If no last time is specified the updated records are retrieved. If no option number is specified all options are retrieved	|	Not documented in the manual	|
|	19	|	[GetMaofScenarios](GetMaofScenarios.md)	|	-	|	Retrieves the Option records according to the last time and option number. If no last time is specified the updated records are retrieved. If no option number is specified all options are retrieved	|	Not documented in the manual	|
|	20	|	[GetMaofStocks](GetMaofStocks.md)	|	-	|	Retrieves Bank Stocks	|	Wrong helpstring? Not documented in the manual	|
|	21	|	[GetRezef](GetRezef.md)	|	-	|	Retrieves Rezef records from the specified time for the speicifed stock. If zero strings are passed, all records are returned	|	Not documented in the manual	|
|	22	|	[GetRezefByIndex](GetRezefByIndex.md)	|	-	|	-	|	Not documented in the manual	|
|	23	|	[GetRezefRaw](GetRezefRaw.md)	|	-	|	Retrieves Rezef records (as a falt string) from the specified time for the speicifed stock. If zero strings are passed, all records are returned	|	Not documented in the manual	|
|	24	|	[GetRzfCNT](GetRzfCNT.md)	|	-	|	Retrieves Rezef CNT	|	Not documented in the manual	|
|	25	|	[GetSH500](GetSH500.md)	|	-	|	רשימת נתוני סוג חומר חמש-מאות	|	Not documented in the manual	|
|	26	|	[GetShacharBondsInFuture](GetShacharBondsInFuture.md)	|	-	|	רשימת אגחי שחר המרכיבים חוזה שחר	|	Not documented in the manual	|
|	27	|	[GetShortTradeOptions](GetShortTradeOptions.md)	|	-	|	Retrieves a list of options and their details	|	Not documented in the manual	|
|	28	|	[GetStatistics](GetStatistics.md)	|	-	|	Retrieves Statistics information	|	Not documented in the manual	|
|	29	|	[GetStockHistory](GetStockHistory.md)	|	-	|	Retrieves Stock History	|	Not documented in the manual	|
|	30	|	[GetStocksRZF](GetStocksRZF.md)	|	-	|	Retrieves the list of Rezef stocks	|	Not documented in the manual	|
|	31	|	[GetStockStage](GetStockStage.md)	|	-	|	Retrieves Stock Stage	|	Not documented in the manual	|
|	32	|	[GetStockValue](GetStockValue.md)	|	-	|	Retrieves Stock Values	|	Not documented in the manual	|
|	33	|	[GetTradeOptions](GetTradeOptions.md)	|	-	|	Retrieves a list of options and their details	|	Not documented in the manual	|
|	34	|	[StartStream](StartStream.md)	|	-	|	Initiate a connection point to VB	|	Not documented in the manual	|
|	35	|	[StopUpdate](StopUpdate.md)	|	-	|	Stops the push updator	|	Not documented in the manual	|