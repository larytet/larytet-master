Code:
```
[C#]
int LoginByLevel ( 
    string username,
    string AS400Password,
    string systemName, 
    int customer, 
    LoginLevel level,
    out string message,
    out int sessionId 
); 
```

**Parameters:**
_username_    UserName for AS400 login proccess.

_AS400Password_    Password for AS400 login proccess.

_systemName_    System name required for future development of multiple customer login. You must insert here the system name of the AS400 to which you are connecting.

_customer_    Customer number required for future development of multiple customer login. You must insert here the required customer number for connection.

_level_    The [LoginLevel](LoginLevel.md) parameter represents the required level of login that the user wishes to execute. The levels vary from the most basic level for basic use of the taskbar to the complete login for advanced taskbar use.

_message_    Failure to begin AS400 login process will provide an error message stating the cause of the failure.

_sessionId_    A unique Session Identification number that identifies the session that has been opened by the client application. The SessionId will be passed as a parameter in many other functions.

**Return Value:**
Upon success the function returns the positive unique session Id as is received in sessionId parameter.

In the event of failure the following values can be returned by the function:
> -1 : illegal user name or password.
> -2 : Required login level not recognized.
> -3 : System currently executing login for lower ranked login level. Please try again later.
> -4 : Maximum number of users currently logged in to system.
> -5 : Unrecognized systemName or customer number.


**Remarks:**
  * The LoginByLevel function initializes the login process and upon return does not by any means determine success of login process.
  * The login process is asynchronous, and can continue for a short period of time during which time connections are established and data is retrieved.
  * API functions must not be called until entire login process has completed successfully. It is therefore necessary to sample the LoginStateByLevel function, giving a true picture of the current login status and outcome upon completion.
  * An explanation into why the login may have failed can be retrieved using the [LoginErrorDescByLevel](LoginErrorDescByLevel.md) function.


See Also:
[LoginByLevelAndChangePassword](LoginByLevelAndChangePassword.md) | [GetLoginActivity](GetLoginActivity.md) | [LoginStateByLevel](LoginStateByLevel.md) | [Login](Login.md) | [LoginLevel](LoginLevel.md) | [LoginErrorDescByLevel](LoginErrorDescByLevel.md)