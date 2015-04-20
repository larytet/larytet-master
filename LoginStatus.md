**Code**
```
public enum LoginStatus {
    LoginSessionActive,
    LoginSessionInProgress,
    LoginSessionInactive,
    LoginSessionDBInitFailure,
    LoginSessionAS400Failure,
    LoginSessionPasswordExpired,
    LoginSessionPasswordChangeFailure,
    LoginSessionPasswordChangedToday,
    LoginSessionWrongUserOrPassword,
    LoginSessionMaxUsersLimit }; 
```

**Remarks:**
  * The enum structure numerically defines the current status of the user login procedure. 
  * Client applications should ensure that the local definition of the LoginStatus type should be equivalent to the declaration in the taskbar.
  * he numerical status values range from 0 to 9 inclusive.

**See Also:**
> [LoginState](LoginState.md)