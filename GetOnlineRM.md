Code:
```
[C#]
int GetOnlineRm ( 
    int sessionId, 
    ref TaskBarLib.RMOnlineTotalInfoType RmOnlineTotalInfo, 
    ref System.Array psaStrRecords, 
    string bstrBranch, 
    string bstrAccount, 
    string bstrStockNumber 
)
```

**Parameters:**
> _sessionId_ Unique Session Identification number that identifies the session for which the function is called.

> _RmOnlineTotalInfo_ RmOnlineTotalInfoType structure into which the morning's online balances are inserted. See the RmOnlineTotalInfoType structure.

> _psaStrRecords_ String array containing the financial details for each and every share and option. The array can be parsed with the RmOnlineRecordType structure.

> _bstrBranch_ Only records pertaining to this branch will be retrieved.

> _bstrAccount_ Only records pertaining to this account will be retreived. If strAccount is "000000", records will be retrieved for all stocks regardless of the account.

> _bstrStockNumber_ Only records pertaining to this stock number will be retrieved. If strStockNumber is "00000000" , records for all stocks will be inserted into the vector.

**Return Value:**
The function returns the size of the array containing the RmOnlineRecordType records.

**See Also:**
> RmOnlineRecordType Object | RmOnlineTotalInfoType Object