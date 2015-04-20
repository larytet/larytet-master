**Code**
```
[C#]
void GetLoginActivity ( 
    ref int SessionId, 
    out int Percent, 
    out string Description
    ); 
```

**Parameters:**
  * _SessionId_    Unique Session Identification number that identifies the session for which the function is called.
  * _Percent_    The percentage of the login process that has been completed.
  * _Description_    A description of the current stage that is being executed by the login process.

**Return Value:**    No value is returned by the function.

**Remarks:**
  * In order to receive a complete picture of the login's progress, the function must be called repetitively, since a single function call returns the login process' position at any moment in time.

**See Also:**
> [Login](Login.md) | [LoginState](LoginState.md)