# Source Tree #

To get the source code you need
<pre>
svn checkout http://larytet-master.googlecode.com/svn/trunk/JQuant JQuantGoogleCode<br>
</pre>
where JQuantGoogleCode is a name of the folder in the local file system.

  * In the root [JQuant/](http://code.google.com/p/larytet-master/source/browse/#svn/trunk/JQuant) you will find code responsible for the infrastructure - [Mailbox](http://code.google.com/p/larytet-master/source/browse/trunk/JQuant/Mailbox.cs), Pool, [CyclicBuffer](http://code.google.com/p/larytet-master/source/browse/trunk/JQuant/CyclicBuffer.cs), HTTP server support.
  * In the folder [JQuant/FMR](http://code.google.com/p/larytet-master/source/browse/trunk/JQuant/#JQuant/FMR) there is a thin shell above TaskBarLib. Most of the calls to the FRM API are done here.
  * Folder [JQuant/FMRSim](http://code.google.com/p/larytet-master/source/browse/trunk/JQuant/#JQuant/FMRSim) is an application which contains all CLI commands, class Main. Also TaskBarLib simulation is in this folder.

# Playback #

Playback is done by MaofDataGeneratorLogFile in the file [TaskBalibSim](http://code.google.com/p/larytet-master/source/browse/trunk/JQuant/FMRSim/TaskBarLibSim.cs). The object opens specified log file (currently only CSV (comma-separated values) format is supported), parses the fields, feeds the TaskBarSim. TaskBarSim generates the data feed and forwards to the application via FMR Stream API.

The playback follows carefully the timestamps in the log file. Playback API supports arbitrary acceleration. Playback of one trading day takes about 10 minutes on the low end Windows machine.

# WEB front end #

The application contains embedded HTTP server (class [JQuantHttp.Http](http://code.google.com/p/larytet-master/source/browse/trunk/JQuant/Http.cs)). The HTTP index files points to file [www/index\_local.html](http://code.google.com/p/larytet-master/source/browse/trunk/JQuant/www/index_local.html). Start of the HTTP server is conditional - compilation flag [WITHHTTP](http://code.google.com/p/larytet-master/source/browse/trunk/JQuant/FMRSim/FMRSim.csproj#19).

[Dojo JavaScript](http://www.dojotoolkit.org/)  framework is employed to display dynamic content in the browser. The protocol works like this
  * Client (browser) periodically (approximately every 1s) sends _getTime?_ request to the server.
  * Server responds with the current time and what-is-changed-flags. Example of such flag is _optionsChanged_ in the object HttpRegGetTimeData.
  * Client compares what-is-changed-flags with the previous value and if any changes does subsequent calls to get updates. Example of such call is _getOptions?_

All responses are JSON formated strings.
# Example of the CLI session #
<pre>
$ FMRSim/bin/Debug/FMRSim.exe<br>
Running under Unix 2.6.31.21<br>
JQuant - Main menu<br>
=====================================<br>
help, exit, one level up - .., main menu - ~<br>
<br>
oper - Operations<br>
dbg - System debug info<br>
tst - Short tests<br>
SIM:$ oper<br>
JQuant - oper<br>
=====================================<br>
help, exit, one level up - .., main menu - ~<br>
<br>
Login - Login to the remote server<br>
Logout - Perform the logout process<br>
StartLog - Log data stream - choose MF|RZ|MDD.<br>
StopLog - Stop log - MF|MDD|RZ, to stop stream type Y<br>
StopStream - Stop the data stream - MF | MDD | RZ<br>
ShowLog - Show existing loggers<br>
AS400TimeTest - Ping the server<br>
<br>
SIM:$ login<br>
SessionId is 1<br>
<br>
<br>
0    10   20   30   40   50   60   70   80   90  100<br>
|----+----+----+----+----+----+----+----+----+----|<br>
..................................................<br>
Connection opened for aryeh<br>
sessionId=1<br>
Login status is LoginSessionActive<br>
<br>
SIM:$ startlog mf<br>
Maof log file JQuantGoogle/DataLogs/MaofLog_9_5_2010_6_26_41_PM.csv<br>
DT= Maof (0)<br>
Name Triggered    Logged   Dropped  Log type                  Latest                  Oldest   Stamped<br>
-----------------------------------------------------------------------------------------------------------------------<br>
<br>
MaofLogger         0         0         0       CSV    1/1/0001 12:00:00 AM    1/1/0001 12:00:00 AM     False<br>
<br>
SIM:$ showlog<br>
Name Triggered    Logged   Dropped  Log type                  Latest                  Oldest   Stamped<br>
-----------------------------------------------------------------------------------------------------------------------<br>
<br>
MaofLogger     88129     88128         0       CSV     9/5/2010 6:26:47 PM     9/5/2010 6:26:41 PM     False<br>
<br>
SIM:$<br>
</pre>

# Example of test #
<pre>
SIM:$ tst<br>
JQuant - tst<br>
=====================================<br>
help, exit, one level up - .., main menu - ~<br>
<br>
GC - Run garbage collector<br>
mbxTest - Run simple mailbox tests<br>
mbxShow - Show mailboxes<br>
threadTest - Run simple thread<br>
threadShow - Show threads<br>
poolTest - Run simple pool tests<br>
poolShow - Show pools<br>
timerTest - Run simple timer tests<br>
timerShow - Show timers<br>
threadPoolTest - Run simple thread pool tests<br>
threadPoolShow - Show thread pools<br>
cbtest - Cyclic buffer class test<br>
rtclock - RT clock test<br>
rtclock_1 - RT clock test<br>
rtclock_2 - RT clock test<br>
logTest - File logger test<br>
<br>
SIM:$ mbxTest<br>
TestMbx created<br>
Mailbox.Send message sent<br>
Mailbox.Send message received<br>
<br>
Name    Size MaxCoun Pending Timeout Receive    Sent Dropped<br>
t                       d<br>
----------------------------------------------------------------<br>
<br>
TestMbx       2       1       0       0       1       1       0<br>
<br>
SIM:$ threadPoolTest<br>
<br>
Name Threads    Jobs MinThre MaxJobs   Start    Done Running PlacedJ Pending Running FailedP FailedR<br>
adsFree                         Threads     obs    Jobs    Jobs laceJob efreshQ<br>
ueue<br>
--------------------------------------------------------------------------------------------------------<br>
<br>
test       1       5       0       2       5       5       0       5       0       0       0       4<br>
ThreadPoolJob done  idx =0, time = 15 micros<br>
ThreadPoolJob done  idx =1, time = 17 micros<br>
ThreadPoolJob done  idx =2, time = 1074 micros<br>
ThreadPoolJob done  idx =3, time = 2133 micros<br>
ThreadPoolJob done  idx =4, time = 3192 micros<br>
<br>
SIM:$ ThreadPool test destroyed<br>
<br>
SIM:$ ..<br>
JQuant - Main menu<br>
=====================================<br>
help, exit, one level up - .., main menu - ~<br>
<br>
oper - Operations<br>
dbg - System debug info<br>
tst - Short tests<br>
<br>
SIM:$ exit<br>
<br>
</pre>

# Example of HTTP #
<pre>
$ FMRSim/bin/Debug/FMRSim.exe<br>
Running under Unix 2.6.31.21<br>
JQuant - Main menu<br>
=====================================<br>
help, exit, one level up - .., main menu - ~<br>
<br>
oper - Operations<br>
dbg - System debug info<br>
box - Box arb algo<br>
tst - Short tests<br>
SIM:$ dbg<br>
JQuant - dbg<br>
=====================================<br>
help, exit, one level up - .., main menu - ~<br>
<br>
OFSM - Test RZ Orders FSM<br>
dbgUF - Test vatious UserClass functions<br>
OrderStream - Test orders Stream<br>
http - HTTP server<br>
sh161 - Get TA25 Index weights<br>
AS400TimeTest - ping the server<br>
fmrPing - Start FMR ping thread<br>
threadPoolShow - Show thread pools<br>
timerShow - Show timers<br>
threadShow - Show threads<br>
mbxShow - Show mailboxes<br>
poolShow - Show pools<br>
loggerTest - Run simple test of the logger<br>
loggerShow - Show existing loggers<br>
prodShow - Show producers<br>
veriShow - Show data verifiers<br>
<br>
SIM:$ http<br>
JQuant - http<br>
=====================================<br>
help, exit, one level up - .., main menu - ~<br>
<br>
start - Start HTTP server<br>
stop - Stop HTTP server<br>
stat - Show HTTP server traffic counters<br>
<br>
SIM:$ start<br>
HTTP server started on port 8183<br>
</pre>
You can access HTTP interface (the system GUI so to say) http://localhost:8183/

# Q&A #
**Q. What StartLog command does?**<br>
A. In the simple case start log just starts log - opens a file and logs the specified data feed into the log (see <a href='http://code.google.com/p/larytet-master/source/browse/trunk/JQuant/FMRSim/SetupCommandLine_oper.cs#101'>http://code.google.com/p/larytet-master/source/browse/trunk/JQuant/FMRSim/SetupCommandLine_oper.cs#101</a>).<br>
<br>
<b>Q. How do I run the playback?</b><br>
A. See method OpenStreamAndLog (<a href='http://code.google.com/p/larytet-master/source/browse/trunk/JQuant/FMRSim/SetupCommandLine_oper.cs#319'>http://code.google.com/p/larytet-master/source/browse/trunk/JQuant/FMRSim/SetupCommandLine_oper.cs#319</a>) - this is a  method which initiates playback, connects to the (simulated) data stream and logs the data. Spend some time reading the comments and code carefully, check classes DataCollector and TaskBarLibSim. This is one of the core elements of the system. Everything else is built around DataCollectors and Producer/Consumer pattern.<br>
<br>
In the help you see<br>
<pre>
StartLog - Log data stream - choose MF|RZ|MDD.<br>
Start market data stream and run logger. In sim mode playback file can<br>
be specified<br>
</pre>
See also, <a href='http://code.google.com/p/larytet-master/source/browse/trunk/JQuant/FMRSim/SetupCommandLine_oper.cs#417'>http://code.google.com/p/larytet-master/source/browse/trunk/JQuant/FMRSim/SetupCommandLine_oper.cs#417</a>
"Sim" mode means simulation mode. In the simulation mode you proved name of the log file for playback OpenStreamAndLog() will initiate playback.<br>
<br>
<b>Q. Why there is a missing BoxArb implementation?</b><br>
A. File BoxArb is a working algo and is not a part of open source repository. Only a stub provided to allow to compile the project.