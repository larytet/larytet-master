
```
dispinterface _IK300EventsEvents {
    properties:
    methods:
       [id(0x00000001), helpstring("method OnMaof")]
       HRESULT OnMaof([in] K300MaofType* data);
       [id(0x00000002), helpstring("method OnRezef")]
       HRESULT OnRezef([in] K300RzfType* data);
       [id(0x00000003), helpstring("method OnMadad")]
       HRESULT OnMadad([in] K300MadadType* data);
};
```