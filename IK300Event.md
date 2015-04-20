
```
    interface IK300Event : IDispatch {
        HRESULT _stdcall FireMaof(
                        SAFEARRAY(K300MaofType)* psaStrRecords, 
                        long* nRecords);
        HRESULT _stdcall FireRezef(
                        SAFEARRAY(K300RzfType)* psaStrRecords, 
                        long* nRecords);
        HRESULT _stdcall FireMaofCNT(
                        SAFEARRAY(BSTR)* psaStrRecords, 
                        long* nRecords);
        HRESULT _stdcall FireRezefCNT(
                        SAFEARRAY(BSTR)* psaStrRecords, 
                        long* nRecords);
    };
```