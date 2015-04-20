code:
```
[C#]
int GetPremium ( 
    int SessionId, 
    string Account, 
    string Branch, 
    TaskBarLib.BaseAssetTypes asset, 
    out double premia 
); 
```

**Parameters:**

_SessionId_
> Unique Session Identification number that identifies the session for which the function is called.

_Account_
> The Account for which the margin will be retrieved.

_Branch_
> The Branch for which the margin will be retrieved.

_asset_
> The Base asset for which the margin is retrieved.

_Margin_
The margin retrieved.


**Return Value:**
The function returns 0 upon success and -1 upon failure.

**See Also:**
BaseAssetTypes