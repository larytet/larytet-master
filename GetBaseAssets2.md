Code:
```
public virtual int GetBaseAssets2(out System.Array psaRecords, int BaseAssetCode)
```

> Parse psaRecords using BaseAssetType struct.

> BaseAssetCode - default value is -1 (all base assets). Looks like it goes after MadadTypes values, that is TLV25 = 0.

> Returns the length of the array or -1 in case of failure.