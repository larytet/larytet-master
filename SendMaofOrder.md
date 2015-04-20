code:
```
[C#]
int SendMaofOrder ( 
    int SessionId, 
    ref TaskBarLib.MaofOrderType Order, 
    ref string VBMsg, 
    ref int ErrNO, 
    out TaskBarLib.OrdersErrorTypes ErrorType, 
    ref int OrderID, 
    string AuthUserName, 
    string AuthPassword, 
    string ReEnteredValue, 
    int SPCOrder 
); 
```

**Parameters:**

_SessionId_
> Unique Session Identification number that identifies the session for which the function is called.

_Order_
> Structure of MaofOrderType containing all details regarding the order that is being sent.

_VBMsg_
> If the sending order operation is unsuccessful, a message is relayed back to the client application with the reason for failure.
> If the operation is successful, VBMsg remains empty.

_ErrNO_
> If an error occurs during the operation, an error number will be relayed back to the client application.


|	Error Number	|	Error description	|	Error Type	|
|:-------------|:------------------|:-----------|
|	-5	|	Inadequate Taskbar authorization for Maof transactions.	|	Fatal.	|
|	-4	|	Failed to retrieve previous order details from database.	|	Fatal.	|
|	-3	|	Inadequate "Generator" authorization.	|	Fatal.	|
|	-2	|	Inadequate User authorization for Maof transactions.	|	Fatal.	|
|	-1	|	Unable to start background process for maof trade.	|	Fatal.	|
|	1	|	Order value above permitted limit.	|	Fatal.	|
|	2	|	Order value above permitted limit.	|	Fatal.	|
|	3	|	Limit above last price by illegal percentage.	|	Fatal.	|
|	4	|	Limit below last price by illegal percentage.	|	Fatal.	|
|	5	|	Permitted Order loss exceeded.	|	Fatal.	|
|	6	|	Illegal change from thoretical price.	|	Fatal.	|
|	7	|	Illegal change from last price.	|	Fatal.	|
|	8	|	Illegal change from last price.	|	Fatal.	|
|	9	|	Maximum amount exceeded.	|	Fatal.	|
|	10	|	Maximum delta exceeded.	|	Fatal.	|
|	11	|	Illegal change from last rate.	|	Password required.	|
|	12	|	Order value (price X amount) exceeds permitted limit.	|	ReEnter order amount.	|
|	13	|	Order amount exceeds maximum permitted amount.	|	ReEnter order amount.	|
|	14	|	Order price exceeds permitted percentage change from last price.	|	ReEnter order price.	|
|	15	|	Permitted Order loss exceeded.	|	ReEnter order price.	|
|	16	|	Illegal change from last price.	|	ReEnter order price.	|
|	17	|	Maximum delta exceeded.	|	ReEnter order price.	|
|	18	|	Illegal change from thoretical price.	|	ReEnter order price.	|
|	19	|	Order value requires Confirmation.	|	Confirmation.	|
|	20	|	Illegal percentage change from last price.	|	Confirmation.	|
|	21	|	Permitted Order loss exceeded.	|	Confirmation.	|
|	22	|	Illegal change from last price.	|	Confirmation.	|
|	23	|	Maximum amount exceeded.	|	Confirmation.	|
|	24	|	Maximum delta exceeded.	|	Confirmation.	|
|	25	|	Maximum delta exceeded.	|	Confirmation.	|
|	26	|	Insufficient securities.	|	Fatal.	|
|	32	|	Order will cause self execution	|	Fatal.	|
|	91	|	Option not found	|	Fatal.	|
|	101	|	Confirmation timeout	|	Fatal.	|
|	912	|	Base asset not found	|	Fatal.	|




_ErrorType_
> Type of error returned by function call. Error type is defined by OrdersErrorTypes enumeration.

_OrderID_
> Unique order id returned by function for order transaction identification.

_AuthUserName_
> In the event that the operation requires password authorization, this field will contain the authorizing user name.

_AuthPassword_
> In the event that the operation requires password authorization, this field will contain the password for the authorizing user .

_ReEnteredValue_
> In the event that the operation requires confirmation, the field will contain the value reentered by the user.


**Return Value:**
> The function returns 0 upon success and -1 upon failure.

**Remarks:**
  * The function often requires confirmation from the user and it is therefore important to pay attention to the error type that is returned from the function. The function can then be called a second time with the confirmation details.
  * The function call should be preceded by maof session initialization by calling the StartMaofSession function. If the maof session is not initialized, a temporary session is created, and destroyed after sending order.



See Also:
MaofOrderType | OrdersErrorTypes | StartMaofSession | StopMaofSession
