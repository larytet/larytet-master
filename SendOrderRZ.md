code:
```
[C#]
int SendOrderRZ ( 
    int SessionId, 
    ref TaskBarLib.RezefSimpleOrder Order, 
    ref int AsmactaFmr, 
    int AsmactaRezef, 
    out string VBMsg, 
    out int ErrNO, 
    out TaskBarLib.OrdersErrorTypes ErrorType, 
    ref int OrderID, 
    string AuthUserName, 
    string AuthPassword, 
    string ReEnteredValue 
)
```

**Parameters:**

_SessionId_
> Unique Session Identification number that identifies the session for which the function is called.

_Order_
> Structure of RezefBasicOrder Type containing all details regarding the order that is being sent.

_AsmactaFmr_
> Unique order reference number provided by FMR systems. This reference is returned as an out parameter, and should be provided when updating an order.

_AsmactaRezef_
> Unique order reference number provided by stock exchange. The reference should be retrieved using order retrieval function. Must be provided when updating an order.

_VBMsg_
> If the sending order operation is unsuccessful, a message is relayed back to the client application with the reason for failure.
If the operation is successful, VBMsg remains empty.

_ErrNO_
> If an error occurs during the operation, an error number will be relayed back to the client application.


|	Error Number	|	Error description	|	Error Type	|
|:-------------|:------------------|:-----------|
|	-100	|	General failure	|	Fatal.	|
|	-2	|	Sending failure	|	Fatal.	|
|	-1	|	Order blocked by AS400. See error message for more details.	|	Fatal.	|
|	-1	|	Non existant stock number	|	Fatal.	|
|	-1	|	No authorization for short transaction.	|	Fatal.	|
|	1	|	Stock does not have last rate.	|	Confirmation.	|
|	2	|	LMT Order value above permitted limit.	|	Fatal.	|
|	3	|	LMT Order value above permitted limit.	|	Password required.	|
|	4	|	LMT Order value above permitted limit.	|	ReEnter value.	|
|	5	|	LMT Order value above permitted limit.	|	Confirmation.	|
|	6	|	MKT Order value above permitted limit.	|	Fatal error.	|
|	7	|	MKT Order value above permitted limit.	|	Password required or ReEnter	|
|	8	|	MKT Order value above permitted limit.	|	Confirmation.	|
|	9	|	Stock missing base rate.	|	Confirmation.	|
|	10	|	Illegal change from base rate.	|	Fatal.	|
|	11	|	Illegal change from base rate.	|	Password required.	|
|	12	|	Illegal change from base rate.	|	ReEnter value.	|
|	13	|	Illegal change from base rate.	|	Confirmation.	|
|	14	|	Stock missing last price.	|	Confirmation	|
|	15	|	Illegal change from last price.	|	Fatal Error	|
|	16	|	Illegal change from last price.	|	Password required	|
|	17	|	Illegal change from last price.	|	Reenter value.	|
|	18	|	Illegal change from last price.	|	Confirmation.	|
|	19	|	Illegal difference between Closing price and Base price.	|	Fatal.	|
|	20	|	Illegal difference between Closing price and Base price.	|	Password required.	|
|	21	|	Illegal difference between Closing price and Base price.	|	ReEnter value.	|
|	22	|	Illegal difference between Closing price and Base price.	|	Warning.	|
|	23	|	Stock missing last price.	|	Confirmation value	|
|	24	|	Illegal change from base rate.	|	Fatal error.	|
|	25	|	Illegal change from base rate.	|	Password required.	|
|	26	|	Illegal change from base rate.	|	ReEnter value.	|
|	27	|	Illegal change from base rate.	|	Confirmation.	|
|	28	|	Illegal delta from last transaction.	|	Fatal.	|
|	29	|	Illegal delta from last transaction.	|	Password required.	|
|	30	|	Illegal delta from last transaction.	|	ReEnter value.	|
|	31	|	Illegal delta from last transaction.	|	Confirmation.	|
|	33	|	Maof trade error from AS400.	|	Fatal.	|
|	34	|	No authorization for short transaction.	|	Fatal.	|
|	35	|	No authorization for short transaction.	|	Password required.	|
|	36	|	No authorization for short transaction.	|	Confirmation.	|
|	37	|	No authorization for short transaction.	|	Confirmation.	|
|	38	|	AS400 requires confirmation for transaction.	|	Confirmation.	|
|	40	|	Background Rezef Session thread not activated.	|	Confirmation.	|
|	41	|	Sending order will cause self execution.	|	Confirmation.	|
|	43	|	Insufficient authorization for rezef transactions	|	Fatal	|
|	91	|	Missing parameters in RezefBasicOrder Type	|	Fatal.	|
|	912	|	Illegal parameters in RezefBasicOrder Type	|	Fatal.	|


_ErrorType_
> Type of error returned by function call. Error type is defined by OrdersErrorTypes enumeration.

_OrderID_
> Unique order id allocated by taskbar returned by function for order transaction identification. Must be provided when resending order due to user confirmation requirement.

_AuthUserName_
> In the event that the operation requires password authorization, this field will contain the authorizing user name.

_AuthPassword_
> In the event that the operation requires password authorization, this field will contain the password for the authorizing user.

_ReEnteredValue_
> In the event that the operation requires confirmation, the field will contain the value reentered by the user.

**Return Value:**
> The function returns 0 upon success and -1 upon failure.


**Remarks:**
  * This function replaces the SendRezefOrder function providing improved interface and performance.
  * The function requires the Rezef session functionality to be activated - a background thread containing the user's up-to-date market position.
  * The function often requires confirmation from the user and it is therefore important to pay attention to the error type that is returned from the function. The function can then be called a second time with the confirmation details.


**See Also:**
> RezefSimpleOrder | OrdersErrorTypes | StartRezefSession | StopRezefSession