### Introduction ###

There are 3 UserClass balance-related methods:

  * TaskBarLib.UserClass.GetHoldingsEx

  * TaskBarLib.UserClass.GetOnlineRM

  * TaskBarLib.UserClass.GetOnlineSessionBalance


### Details ###

  * GetHoldingsEx gives a detailed report for each security in portfolio. Presumably is updated once a day, so it's good for any overnight back office tasks.

  * There is a method called GetHoldings, which is an older version ('Ex' means 'extended' - hooray FMR's developers!). Always use the newer version.

  * In general FMR doesn't depreciate their old API's for backward compatibility reasons, they add new methods with similar functionality instead. Sometimes it's hard to figure out which method to use, advise FMR's helpdesk.

  * GetOnlineRM is updated as you go through the trading session, so it will give more or less RT picture of your account. There are may be some delays, so it's healthier to keep all the details locally and use this method for reconciliation purposes.

  * GetOnlineRM returns two pieces of data:
    1. RmOnlineTotalInfoType structure which contains summary data for cash balance, such as total cash at the opening, current cash, daily cash change, total PnL.
    1. An array containing objects of RmOnlineRecordType, which is contains summary data for every security held in my portfolio through the day (from opening till current moment).

  * GetOnlineRmEx is another version of GetOnlineRM. Despite the fact of it being an 'Ex' DO NOT use it, stick with GetOnlineRM  instead, the latter is more efficient.

  * GetOnlineSessionBalance is a very detailed report returning array of objects of OnlineSessionBalance type, which is very long list of fields, most of which you will never need. FMR's folks defined this method as a 'disaster', because it takes ages to get response from AS/400 server, because it collects the data from elsewhere. In general it's advised to use any other methods to get the data, unless this data is available only from OnlineSessionBalance type.

  * Any of these methods make calls to the AS/400 mainframe and are blocking. Consider using a separate thread for calling them.

  * StartOnlineUserSession and StopOnlineUserSession are two methods that start and stop a background process for getting online data. Presumably need to call them in order to use any of 'online' methods (TODO - verify that vs. FMR!) - GetOnlineRmHistory, GetOnlineRmFromMaofSession, GetOnlineRmEx, GetOnlineRm.