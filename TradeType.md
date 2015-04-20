code:
```
[C#]
    public enum TradeType 
    { 
        ALLTradeType = -1,
        MF = 0,
        RF = 1 
    }
```

**Remarks:**
> The enum structure numerically defines the filter applied in the OrdersStreamStart and OrdersStreamStop functions. Data can be received for Rezef or Maof or both.

**See Also:**
> OrdersStreamStart | OrdersStreamStop