**Code**
```
[C#]
int LoginAndChangePassword ( 
    string username, 
    string oldAS400Password,
    string newAS400Password,
    string AppPassword, 
    out string message, 
    out int sessionId ); 
```

**Parameters:**
  * _username_ UserName for AS400 login procedure.
  * _oldAS400Password_ Old Password for AS400 login procedure.
  * _newAS400Password_ New Password for AS400 login procedure.
  * _AppPassword_ Application Password for AS400 login procedure. This parameter is no longer required for the login  procedure and an empty string may be passed into the function.
  * _message_ Failure to begin AS400 login process will provide an error message stating the cause of the failure.
  * _sessionId_ A unique Session Identification number that identifies the session that has been opened by the client application. The SessionId will be passed as a parameter in many other functions.

**Return Value:**
  * In the event of failure -1 is returned from the function.
  * In the event of success the unique session id is returned.

**Remarks:**
  * The login process is asynchronous, returning to the application long before the login process has succeeded.It is therefore necessary to sample the [LoginState](LoginState.md) property, giving a true picture of the current status and outcome upon completion, and an explanation into why the login may have failed.

**See Also:**
> [Login](Login.md) | [GetLoginActivity](GetLoginActivity.md) | [LoginState](LoginState.md)