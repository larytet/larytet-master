code:
```
[C#]
    public struct MaofOrderType
    {
        public string Branch;       //סניף
        public string Account;      //חשבון
        public string Option;       //מספר אופציה
        public string operation;    //פעולה
        public string ammount;      //כמות
        public string price;        //מחיר
        public string Sug_Pkuda;    //סוג פקודה
        public string Asmachta;     //אסמכתא
        public string AsmachtaFmr;  //אסמכתא פ.מ.ר
        public int Pass;            //נקלט
        public int OrderID;         //זיהוי פקודה פנימי
    }
```

**Parameters:**

_Branch_
> The branch number, as string.

_Account_
> The account number, as string.

_Option_
> Option id no. on TASE

_operation_
> Either:
```
   "N" = new order
   "U" = update an order that already exists (received and approved by TASE)
   "D" = delete (cancel) existing order
```

_ammount_
> (they mean 'amount') - integer number of securities (options) to buy (thus positive) or to sell (thus negative). E.g., to sell 3 units, use "-3", to buy 2 units, use "2".

_price_
> limit price, e.g. "1970" or "25"

_Sug\_Pkuda_
> For options it's either "LMT", "FOK" or "IOC"

_Asmachta_
> TASE reference number, an unique id for the order. Used for updating of canceling the specific order (need to supply both Asmachta and AsmachtaFMR for these purposes) and for identifying the orders data in the orders stream (see [OrdersStreamStart](OrdersStreamStart.md) method)

_AsmachtaFmr_
> FMR reference number, id for the order on the FMR server. Usually is received first (immediately with the SendMaofOrder return).

_Pass_
> need to aks the helpdesk, not sure it's important - there are other ways to check

_OrderID_
> internal order ID no., used for identification of order in case it returns with an error code, and needs to be resent.