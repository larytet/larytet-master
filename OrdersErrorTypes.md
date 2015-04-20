code:
```
[C#]
    public enum OrdersErrorTypes 
    {
        Fatal,
        Confirmation,
        ReEnter,
        PasswordReq,
        Alert,
        NoError 
    }
```

**Remarks:**
> The enum structure numerically defines the type of error returned by the SendRezefOrder and SendMaofOrder functions. Client applications should ensure that the local definition of the  OrdersErrorTypes type should be equivalent to the declaration in the taskbar. The numerical status values range from 0 to 5 inclusive.


**See Also:**
> SendRezefOrder | SendMaofOrder