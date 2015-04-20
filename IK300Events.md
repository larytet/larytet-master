
```
    interface IK300Events : IDispatch {
        [id(0x00000001), propget]
        HRESULT EventsFilterBaseAsset([out, retval] BaseAssetTypes* pVal);
        [id(0x00000001), propput]
        HRESULT EventsFilterBaseAsset([in] BaseAssetTypes pVal);
        [id(0x00000002), propget]
        HRESULT EventsFilterMonth([out, retval] MonthType* pVal);
        [id(0x00000002), propput]
        HRESULT EventsFilterMonth([in] MonthType pVal);
        [id(0x00000003), propget]
        HRESULT EventsFilterBno([out, retval] long* pVal);
        [id(0x00000003), propput]
        HRESULT EventsFilterBno([in] long pVal);
        [id(0x00000004), propget]
        HRESULT EventsFilterMaof([out, retval] long* pVal);
        [id(0x00000004), propput]
        HRESULT EventsFilterMaof([in] long pVal);
        [id(0x00000005), propget]
        HRESULT EventsFilterRezef([out, retval] long* pVal);
        [id(0x00000005), propput]
        HRESULT EventsFilterRezef([in] long pVal);
        [id(0x00000006), propget]
        HRESULT EventsFilterMadad([out, retval] long* pVal);
        [id(0x00000006), propput]
        HRESULT EventsFilterMadad([in] long pVal);
        [id(0x00000007), propget]
        HRESULT EventsFilterStockKind([out, retval] StockKind* pVal);
        [id(0x00000007), propput]
        HRESULT EventsFilterStockKind([in] StockKind pVal);
        [id(0x00000008), propget]
        HRESULT EventsFilterStockMadad([out, retval] MadadTypes* pVal);
        [id(0x00000008), propput]
        HRESULT EventsFilterStockMadad([in] MadadTypes pVal);
    };
```