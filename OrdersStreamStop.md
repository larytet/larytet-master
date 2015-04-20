code:
```
[C#]
    int OrdersStreamStop ( 
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
> Streaming will only be ceased for records pertaining to this account number.

_Branch_
> Streaming will only be ceased for records pertaining to this branch.

_streamType_
> Streaming can be ceased for all orders or for either rezef orders alone or maof orders alone. The 'AllTradeType' variable will stop streaming for all trade, 'MaofTradeType' for Maof orders alone and 'RezefTradeType' for Rezef orders alone.

**Return Value:**
```
 0 : Orders Streaming stopped successfully.
-1 : General function failure.
-2 : Orders Data source initialization failure.
-3 : Unable to stop Streaming.
-5 : Inedequate authorization.
```

**Remarks:**
  * OrdersStreamStop should be called after calls to functions [GetOrdersRZ](GetOrdersRZ.md) or [GetOrdersMF](GetOrdersMF.md) in order to stop the streaming.
  * The OrdersStreamStart function should be called in order to initiate the stream.

**See Also:**
> OrdersStreamStart | TradeType