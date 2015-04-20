### Ceases streaming of Market data into the Taskbar from the K300 server or AS/400. ###


```
HRESULT K300StopStream([in] K300StreamType streamType, [out, retval] long* retVal);
```

```
[C#]
int K300StopStream ( 
    TaskBarLib.K300StreamType streamType ); 
```

**Parameters:**
> streamType
> > Streaming can be ceased for either rezef or maof market data. Use either MaofStream or RezefStream as required.

**Return Value:**
|0 |: K300 data streaming ceased successfully.|
|:-|:-----------------------------------------|
|-1|  : General function failure. |
| -2| : K300 Data source retrieval failure.|
| -3|  : K300 Data stream stop failure.|
|  -4|  : Inedequate authorization.|

Remarks:
  * K300StopStream should be called upon completion of data retrieval calls. It appears from the code the StartStream is called when opening a trading session (day) and StopStream after all operation are done.
  * The K300StartStream and K300StopStream functions are reference counted : ensure that both functions are called an equal number of times. In the VB samples the guys simply keep a global flag - if true the stream is opened. I think that better solution here is state machine. We want to poll the status and if any error repeat stop/start sequence. While in the stop-start cycle no trading is possible and all relevant sub-programs should be paused.
  * The K300StartStream function should be called in order to start the stream.