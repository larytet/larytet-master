code:
```
[C#]
int StartRezefSession ( 
    int SessionId, 
    string Account, 
    string Branch 
); 
```

**Parameters:**

_SessionId_
> Unique Session Identification number that identifies the session for which the function is called.

_Account_
> The account for which the rezef session will be initialized.

_Branch_
> The branch for which the rezef session will be initialized.

**Return Value:**
```
 0 : Rezef session started successfully.
-1 : General function failure.
-2 : Account or branch parameters not relayed to function.
-3 : DataBase failure.
-4 : Account not found.
-5 : Short Account not found.
-6 : Inadequate User authorization.
```

**Remarks:**
  * StartRezefSession should be called before calls to rezef trading functions in order to initiate the streaming.
  * StartRezefSession initiates both the K300 and orders streaming for the specified account.
  * The StopRezefSession function should be called in order to close the session.

**See Also:**
StopRezefSession