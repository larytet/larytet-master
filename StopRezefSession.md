code:
```
[C#]
int StopRezefSession ( 
    int SessionId, 
    string Account, 
    string Branch 
)
```

**Parameters:**

_SessionId_
> Unique Session Identification number that identifies the session for which the function is called.

_Account_
> The account for which the rezef session will be terminated.

_Branch_
> The branch for which the rezef session will be terminated.


**Return Value:**
```
 0 : Rezef session terminated successfully.
-1 : General function failure.
-2 : Inadequate user permissions.
```

**Remarks:**

The StartRezefSession function should be called in order to begin the session.

**See Also:**
StartRezefSession