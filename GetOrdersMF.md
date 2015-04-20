code:
```
[C#]
    int GetOrdersMF ( 
        int SessionId, 
        out System.Array vecRecords, 
        string Account, 
        string Branch, 
        ref string LastTime 
); 
```

**Parameters:**

_SessionId_
> Unique Session Identification number that identifies the session for which the function is called.

_vecRecords_
> The String array into which the maof order records will be inserted.

_Account_
> Only records pertaining to this account number will be retrieved.

_Branch_
> Only records pertaining to this branch will be retrieved.

_LastTime_
> This is the 'Retrieve & Refresh' last time parameter. Only records which have been updated past this time will be retrieved. If this parameter is omitted or if "00000000" is specified all records are retrieved regardless of update time.

**Return Value:**
> Upon success the function returns the total number of records retrieved into vecRecords.
```
-1 : General function failure. 
-2 : Orders Data source initialization failure.
-4 : Short Account not found.
-5 : Inedequate authorization.
```

**Remarks:**
  * GetOrdersMF receives Order records either directly from the Order's Server or from an AS/400 server, depending on the user's configuration.
  * To enable the records to be drawn from the server, OrdersStreamStart must first be executed.
  * Records can be parsed using the MOFINQType structure.

**See Also:**
[MOFINQType](MOFINQType.md) | OrdersStreamStart