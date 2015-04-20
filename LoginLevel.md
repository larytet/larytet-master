Code:
```
[C#]
public enum LoginLevel { 
    LoginLevelPermissions,
    LoginLevelOptionsStocks,
    LoginLevelAccounts,
    LoginLevelMax 
}; 
```

**Remarks:**
  * The enum structure numerically defines the required level for the user login procedure. There are currently 4 possible levels with which the user can logon:

  1. LoginLevelPermissions is the most basic login level - connects to the AS400 and retrieves user permissions.
  1. LoginLevelOptionsStocks connects to AS400 and enables market data functionality.
  1. LoginLevelAccounts connects to AS400 and initalizes account data.
  1. LoginLevelMax is the highest logging in level and enables full use of taskbar.

  * Both LoginLevelOptionsStocks and LoginLevelAccounts contain all features of LoginLevelPermissions level.
  * Use of all API functions is subject to user's authorizations.


**See Also:**
[LoginByLevel](LoginByLevel.md) | [LoginByLevelAndChangePassword](LoginByLevelAndChangePassword.md) | [LoginStateByLevel](LoginStateByLevel.md)