Code:
```
[C#]
public virtual int CreateK300EventsFile(
    string CacheFolder, 
    string DataFileName, 
    int EventsPerFile, 
    TaskBarLib.K300StreamType streamType, 
    out string ErrorMessage
);
```

**Parameters:**
  * CacheFolder_A folder/path to which the file(s) will be saved
  * DataFileName_ Data file(s) name.
  * EventsPerFile_The maximal number of events per file.
  * streamType_ which streams events to keep in the file (Maof/Rezef)
  * ErrorMessage_???_

**Return Value:**
int, 0 if success (and probably -1 if failure, like all the functions here? any value other than 0 indicates failure - see VB example)

**Remarks:**

**See Also:**
VB example - in TaskBarServiceVBTester2.frm, see Sub mnuK300FileCacheMaof\_Click()