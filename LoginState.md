**Code**
```
[C#]
TaskbarLib.LoginStatus get_LoginState ( 
    ref int SessionId
    ); 
```

**Parameters:**
  * _SessionId_ Unique Session Identification number that identifies the session for which the function is called.
  * _Return Value_:  enum [LoginStatus](LoginStatus.md), parameter that defines the status of the login process.

**Remarks:**
  * In order to receive a complete picture of the login's progress, the function must continuously sampled, until a final status has been retrieved, since a single function call returns the login process' current status.
  * Apart from LoginSessionActive and LoginSessionInProgress, all other return values symbolize login failure.

**See Also:**
> [Login](Login.md) | [LoginStatus](LoginStatus.md) | [LoginAndChangePassword](LoginAndChangePassword.md)