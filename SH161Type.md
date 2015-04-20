Code:
```
C#
public struct SH161Type
{
    long BNO;            // מס' נייר ערך
    string BNO_NAME;     // שם נייר ערך
    double PRC;          // מחיר
    double HON_RASHUM;   // הון רשום
    double PCNT;         // אחוז
    long MIN_NV;         // כמות מינימלית
    double BNO_IN_MDD;   // מספר מניות במדד = מממ
    double PUBLIC_PRCNT; // אחוז אחזקות בציבור
    double NV_TZAFA;     // כמות צפה = מממ*אחוז אחזקות בציבור
}

```

**See also**
> [GetSH161](GetSH161.md)