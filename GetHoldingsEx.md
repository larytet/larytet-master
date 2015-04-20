Code:
```
int GetHoldingsEx ( 
    int sessionId, 
    string strTik, 
    string strAccount, 
    ref System.Array vecStrRecords, 
    TaskBarLib.TradeType type, 
    string strBranch, 
    string strStock, 
    string strMefazel 
); 
```

**Parameters:**

> _sessionId:_ Unique Session Identification number that identifies the session for which the function is called.

> _strTik_ Only records regarding stocks and shares belonging to this portfolio will be retrieved. Portfolioes for which the user does not have authorization will not be shown.

> _strAccount_ Only records pertaining to this account will be retreived. If strAccount is "000000", records will be retrieved for all stocks regardless of the account.

> _vecStrRecords_ String array containing the holding details for each and every share and option. The array can be parsed with the RMType structure.

> _type_ Defines the trading type for which the holdings will be retrieved, as defined by the TradeType structure. It is possible to receive holdings for either Maof or Rezef or both Maof and Rezef.

> _strBranch_ Only records pertaining to this branch will be retrieved. If strBranch is "000" (default value), records will be retrieved for all stocks regardless of the branch.

> _strStock_ Only records pertaining to this stock number will be retrieved. If strStock is "00000000" (default value), records for all stocks will be inserted into the vector.

> _strMefazel_ All records pertaining to this joint account will be retrieved. If strMefazel is "000000" (default value), records will be retrieved for all stocks regardless of the joint account.

**Return Value:**
> Upon success the function returns the total number of RMType records retrieved into vecStrRecords or
> 0 : No records found.
> -1 : Inedequate authorization.

**Remarks:**
> The GetHoldings function carries out the same task as GetHoldingEx without the Trading type filter, and without the joint account functionality.

**See Also:**
> RMType | TradeType | GetHoldings