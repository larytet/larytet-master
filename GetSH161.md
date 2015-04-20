Code:
```
[C#]
int GetSH161 
( 
    ref System.Array vecRecords, 
    MadadTypes madadSymbol 
)
```

**Parameters:**
> _vecRecords_

Array of type SH161Type containing all records for the index supplied in parameter madadSymbol

> _madadSymbol_

Only stocks included in this MadadType index will be retrieved.


**Return Value:**

Upon success the function returns the total number of SH161Type records retrieved into the vecRecords array. 0 : No records found.

**See Also:**
> [SH161Type](SH161Type.md) | [MadadTypes](MadadTypes.md)