code:
```
[C#]
    int OrdersStreamStart ( 
        int SessionId, 
        string Account, 
        string Branch, 
        TaskBarLib.TradeType streamType 
        )
```

**Parameters:**

_SessionId_
> Unique Session Identification number that identifies the session for which the function is called.

_Account_
> Streaming will only take place for records pertaining to this account number.

_Branch_
> Streaming will only take place for records pertaining to this branch.

_streamType_
> Streaming can be enabled for all orders or for either rezef orders alone or maof orders alone. The 'AllTradeType' variable will enable streaming for all trade, 'MaofTradeType' for Maof orders alone and 'RezefTradeType' for Rezef orders alone.

**Return Value:**
```
 0 : Orders Streaming started successfully.
-1 : General function failure.
-2 : Orders Data source initialization failure.
-3 : Illegal streamType parameter.
-4 : Short Account not found.
-5 : Inedequate authorization.
```

**Remarks:**
  * OrdersStreamStart should be called before calls to functions [GetOrdersRZ](GetOrdersRZ.md) or [GetOrdersMF](GetOrdersMF.md) in order to initiate the streaming.
  * The OrdersStreamStop function should be called in order to close the stream.

**See Also:**
[MOFINQType](MOFINQType.md) | [RZFINQType](RZFINQType.md) | [OrdersStreamStop](OrdersStreamStop.md) | [TradeType](TradeType.md)