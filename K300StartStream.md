**Begins streaming of Market data into the Taskbar from the AS/400 server.**
```
HRESULT K300StartStream([in] K300StreamType streamType, [out, retval] long* retVal);
```

```
[C#]
    int K300StartStream ( 
        TaskBarLib.K300StreamType streamType );
```

**Parameters:**
> streamType
> > Streaming can be enabled for either rezef or maof market data. Use either MaofStream or RezefStream as required.

**Return Value:**
|0 | K300 data streaming started successfully.|
|:-|:-----------------------------------------|
|-1| General function failure.|
|-2| K300 Data source initialization failure.|
|-3| K300 Data stream initialization failure|
|-4| Inedequate authorization.|

**Remarks:**
  * Retrieval of market data is a two-stage operation :
    1. Initialization of data stream to the taskbar from either K300 Server or AS400 using this method.
    1. Retrieval of market data from taskbar to client application using GetK300RZ | GetK300MF | GetK300Madad methods. Market data can also be relayed to the client application by registering to the K300 event mechanism.
  * The K300StartStream and K300StopStream functions are reference counted : ensure that both functions are called an equal number of times.
  * K300StartStream should therefore be called prior to the data retrieval calls.
  * The K300StopStream function should be called in order to close the stream.

**See Also:**

> K300StopStream | K300StreamType