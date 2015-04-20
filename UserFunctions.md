|	**No.**	|	**Name**	|	**Description in the manual**	|	**Helpstring in the IDL script**	|	**Comment**	|
|:--------|:---------|:------------------------------|:---------------------------------|:------------|
|	1	|	CalculateSecurities	|	Calculates securities for account, showing base asset distribution. 	|	חישוב בטחונות עבור נכסים	|		|
|	2	|	Decrypt	|	Decrypts string using a given encrypting key.	|	Decrypt Strings	|		|
|	3	|	Encrypt	|	Encrypts string using a given encrypting key.	|	Encrypt Strings	|		|
|	4	|	GetAccounts	|	Retrieves Account data for accounts associated with relevant customer.	|	Retrieves accounts based on user query	|		|
|	5	|	GetAccountYieldByRequirement	|		|	שליפת תשואה יומית: מידע ע'פ דרישה	|	Not documented in the manual	|
|	6	|	GetAccountYieldDetailed	|		|	שליפת תשואה יומית: מידע מפורט	|	Not documented in the manual	|
|	7	|	GetAccountYieldInitial	|		|	שליפת תשואה יומית: מידע ראשוני	|	Not documented in the manual	|
|	8	|	GetHoldings	|	Retrieves the holdings for the specified account and the specified stock.	|	Retrieves the holdings for the specified account and the specified stock	|		|
|	9	|	GetHoldingsEx	|	Retrieves the type of holdings (maof or all) for the specified account or the specified stock.	|	Retrieves the type of holdings (maof or all) for the specified account or the specified stock	|		|
|	10	|	GetKranotAccounts	|		|	שולף חשבונות קרנות עם אפשרות פילטור משתמש	|	Not documented in the manual	|
|	11	|	GetMaofCnt	|		|	ריכוז הוראות ונתוני שוק במעו''ף	|	Not documented in the manual	|
|	12	|	GetLoginActivity	|	Relays the percentage of the login process that has been completed, together with the stage of the login that is being carried out.	|	נותן את אחוז העבודה שהושלם בלוגין ואת תיאור העבודה הנוכחית	|		|
|	13	|	GetMargin	|	Retrieves Account margin according to index.	|	קבלת מסגרת עבור חשבון לפי מדד	|		|
|	14	|	GetOnlineRm	|	Retrieves financial summary for the specified account, and and financial details regarding a specific stock or all stock relevant to the specified account.	|	Get Online Balance	|		|
|	15	|	GetOnlineRmEx	|	Retrieves financial summary for the specified account, and and financial details regarding a specific stock or all stock relevant to the specified account.	|	Get Online Balance	|		|
|	16	|	GetOnlineRmFromMaofSession	|		|	מחזיר יתרות עבור כל אופציה	|	Not documented in the manual	|
|	17	|	GetOnlineSessionBalance	|		|	משיכת יתרות און-ליין למשתמש מהמחשב הראשי	|	Not documented in the manual	|
|	18	|	GetOrdersMaof	|	Retrieves a summary or detailed version of Maof orders and executions for the specifed branch, account and stock number .	|	Retrieves a summary or detailed Maof orders and executions for the speicifed account and the specified stock number	|		|
|	19	|	GetOrdersMF	|	Retrieves a detailed list of Maof orders and executions for the specifed branch and account.	|	משיכת הוראות מעו''ף מהמחשב הראשי או משרתים	|		|
|	20	|	GetOrdersRezef	|	Retrieves a summary or detailed version of Rezef orders and executions for the specifed branch, account and stock number.	|	Retrieves a summary or detailed Rezef orders and executions for the speicifed account and the specified stock number	|		|
|	21	|	GetOrdersRZ	|	Retrieves a detailed list of Rezef orders and executions for the specifed branch and account.	|	משיכת הוראות רצף מהמחשב הראשי או משרתים	|		|
|	22	|	GetPremium	|	Retrieves Account's daily premium limit.	|	מגבלת פרמיה יומית	|		|
|	23	|	GetRezefCnt	|		|	ריכוז הוראות ונתוני שוק ברצף	|	Not documented in the manual	|
|	24	|	GetShortAccounts	|	Retrieves short account data for accounts associated with relevant customer.	|	Retrieves ShortAccounts based on user query	|		|
|	25	|	GetStockHistory	|	Retrieves Stock History	|	Retrieves Stock History	|		|
|	26	|	GetUserApp	|	Retrieves a list of all applications for which the user has adequate authorization.	|	רשימת אפליקציות המותרות למשתמש	|		|
|	27	|	GetUserPermissions	|	Retrieves a list of the user's permissions.	|	רשימת הרשאות של המשתמש	|		|
|	28	|	[Login](Login.md)	|	Carries out the AS400 login process.	|	message כניסה למערכת עם שם וסיסמה. במקרה של כשלון תנתן הודעה בפרמטר	|		|
|	29	|	LoginAndChangePassword	|	Carries out the AS400 login process and changes the user's password.	|	message כניסה למערכת עם שם וסיסמה לשינוי. במקרה של כשלון תנתן הודעה בפרמטר	|		|
|	30	|	LoginByLevel	|		|	כניסה למערכת עם שם וסיסמה בשלבים של רמות התחברות . במקרה של כשלון תנתן הודעה בפרמטר message	|	Not documented in the manual	|
|	31	|	LoginByLevelAndChangePassword	|		|	כניסה למערכת עם שם, סיסמה לשינוי ורמת התחברות. במקרה של כשלון תנתן הודעה בפרמטר message	|	Not documented in the manual	|
|	32	|	LoginErrorDescByLevel	|		|	הודעת שגיאה כאשר הכניסה למערכת נכשלה לפי שלב רמת התחברות	|	Not documented in the manual	|
|	33	|	[Logout](Logout.md)	|	Carries out the AS400 logout process.	|	יציאה מהמערכת	|		|
|	34	|	MaofOrderParams	|	Retrieves Maof security parameters for specified asset.	|	פרמטרים לבטחונות מעו''ף	|	wrong description?	|
|	35	|	MaofSecuritiesParams	|	Retrieves Maof security parameters for specified asset.	|	נתונים לבטחונות מעו''ף	|		|
|	36	|	OrdersStreamStart	|	Begins streaming of Orders and executions into the Taskbar from the orders server or AS/400, for the specifed branch and account.	|	התחל זרימת נתוני הוראות וביצועים לתוך הטסקבר מהמחשב ראשי או מהשרתים. ערך מוחזר 0 מציין הצלחה	|		|
|	37	|	OrdersStreamStop	|	Ceases streaming of Orders and executions into the Taskbar from the orders server or AS400, for the specifed branch and account.	|	הפסק זרימת נתוני הוראות וביצועים לתוך הטסקבר מהמחשב ראשי או מהשרתים. ערך מוחזר 0 מציין הצלחה	|		|
|	38	|	PeleSecurities	|	Calculates securities for pele orders.	|	חישוב בטחונות פלא	|		|
|	39	|	SendContOrderRZ	|		|	שליחת הוראה מתמשכת	|	Not documented in the manual	|
|	40	|	SendMaofOrder	|	Sends Single Maof Order.	|	Send Maof Order	|		|
|	41	|	SendMaofAsynchSpeedOrder	|	Sends asynchronous Maof speed orders.	|	שליחת הוראת ספיד אסינכרונית, ללא המתנה לתשובת קליטה	|		|
|	42	|	SendMaofOrderPele	|	Sends single Maof Pele Order.	|	Send Maof Order	|		|
|	43	|	SendMaofSpeedOrderPele	|	Sends multiple Maof Pele Orders.	|	שליחת הוראות סל מעו''ף ללקוח פלא	|		|
|	44	|	SendOrderRZ	|	Sends Single Rezef Order. Replaces SendRezefOrder function.	|	שליחת הוראה ברצף	|	This function replaces the SendRezefOrder function providing improved interface and performance.	|
|	45	|	SendOrderSpeedRZ	|		|	שליחת הוראת רצף speed	|	Not documented in the manual	|
|	46	|	SendRezefOrder	|	Sends Single Rezef Order.	|	Send Rezef Order	|	The SendOrderRZ function is an alternative function for sending rezef orders, with improved interface and performance.	|
|	47	|	SendRezefOrderSpeed	|	Sends Multiple Rezef Orders.	|	method SendRezefOrder2	|		|
|	48	|	StartRezefSession	|	Begins Rezef trading session.	|	Starts a background process of rezef data updates for the given account	|		|
|	49	|	StopRezefSession	|	Terminates Rezef trading session.	|	Stops the background process of rezef data updates for the given account	|		|
|	50	|	StartMaofSession	|	Begins Maof trading session.	|	Starts a background process of maof data updates for the given account	|		|
|	51	|	StopMaofSession	|	Terminates Maof trading session.	|	Stops the background process of maof data updates for the given account	|		|
|	52	|	StartOnlineUserSession	|		|	איתחול תהליך רקע שליפת יתרות און-ליין למשתמש	|	Not documented in the manual	|
|	53	|	StopOnlineUserSession	|		|	סיום תהליך רקע שליפת יתרות און-ליין למשתמש	|	Not documented in the manual	|
|	54	|	UpdateUserPassword	|		|	Update a user passowrd	|	Not documented in the manual	|
|	55	|	UserAuthentication	|		|	מוודא סיסמה אפליקטיבית (סיסמה שנייה) עבור משתמש. ערך מוחזר 1 מציין הצלחה	|	Seems outdated, Not documented in the manual	|