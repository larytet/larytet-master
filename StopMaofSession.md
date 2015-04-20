code:
```
[C#]
int StopMaofSession ( 
    int SessionId, 
    string Account, 
    string Branch 
)
```

**Parameters:**
_SessionId_
> Unique Session Identification number that identifies the session for which the function is called.

_Account_
> The account for which the maof session will be terminated.

_Branch_
> The branch for which the maof session will be terminated.


**Return Value:**
```
 1 : Unable to terminate Maof session.
 0 : Maof session terminated successfully.
-1 : General function failure.
-5 : Inadequate user permissions.
```

**Remarks:**
The StartMaofSession function should be called in order to begin the session.

**See Also:**
StartMaofSession