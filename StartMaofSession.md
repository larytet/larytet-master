code:
```
[C#]
int StartMaofSession ( 
    int SessionId, 
    string Account, 
    string Branch, 
    TaskBarLib.SecurityCalcType calcType 
)
```

**Parameters:**

_SessionId_
> Unique Session Identification number that identifies the session for which the function is called.

_Account_
> The account for which the maof session will be initialized.

_Branch_
> The branch for which the maof session will be initialized.

_calcType_
> Type of calculation type for calculating account securities. Is ususally set to RM\_Dill\_ShortOrders.


**Return Value:**
```
    1 : Unable to begin Maof session.
    0 : Maof session started successfully.
   -1 : General function failure.
   -2 : Account or branch parameters not relayed to function.
   -3 : DataBase failure.
   -4 : Account not found.
   -5 : Short Account not found.
   -6 : Inadequate "Generator" authorization.
   -7 : Inadequate User authorization.
```

**Remarks:**
> StartMaofSession should be called before calls to maof trading functions in order to initiate the streaming.
> If a maof session is not opened prior to trading, a temporary session will be opened upon calling the trading function, and closed upon completion.
> The StopMaofSession function should be called in order to close the session.


**See Also:**
> StopMaofSession

