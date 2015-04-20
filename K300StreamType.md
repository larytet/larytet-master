**Numerical definition of the type of market data to be imported into the taskbar.**
```
    enum {
        MaofStream = 48,
        RezefStream = 49,
        MaofCNTStream = 50,
        RezefCNTStream = 51,
        IndexStream = 52
    } K300StreamType;
```

**Remarks:**
  * [MaofStream](MaofStream.md) is Maof options trading data stream. Its value is required to receive market data on derivatives (maof) and indexes (madad).
  * [RezefStream](RezefStream.md) is rezef (stocks) trading data stream. Its value is required in order to receive market data on stocks (shares).
  * MaofCNTStream and RezefCNTStream are obsolete and not in use anymore