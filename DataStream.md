Start Maof data stream
  * Create object of type TaskBarLib.[K300](K300.md)
  * Set session ID TaskBarLib.K300.K300SessionId = FMRLibTester.SessionId (??)
  * Call method TaskBarLib.K300.K300StartStream(K300StreamType) with stream type MaofStream (48). Returns zero if Ok and stream is started

Stop Maof stream
  * Call K300Obj.K300StopStream(K300StreamType type) for MaofStream stream type

Get events
  * Subclass TaskBarLib.K300Events, implement OnMaof, OnMadad, OnRezef event handlers
  * In the constructor set filters
```
K300EventsObj.EventsFilterBno = OptionNumber
K300EventsObj.EventsFilterMaof = True
K300EventsObj.EventsFilterBaseAsset = BaseAsset
K300EventsObj.EventsFilterMonth = mon
K300EventsObj.EventsFilterMadad = True
```
  * Create object of the new type

Even handler gets a single argument K300MaofType

Log file
```
HRESULT CreateK300EventsFile([in] BSTR CacheFolder, [in] BSTR DataFileName, 
[in] long EventsPerFile, [in] K300StreamType streamType, [out] BSTR* ErrorMessage, 
[out, retval] long* retVal);
```