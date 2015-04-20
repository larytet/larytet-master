# General #

  * http://www.dinosaurtech.com/utilities/
  * http://ibcsharp.codeplex.com/
  * Java http://www.interactivebrokers.com/en/p.php?f=programInterface
  * http://www.interactivebrokers.com/download/JavaAPIGettingStarted.pdf Java getting started
  * http://www.interactivebrokers.com/download/newMark/PDFs/APIprintable.pdf  Reference guide
  * http://www.bearcave.com/software/market_trading/resources_and_notes/ - documentation

# Matlab #

  * http://mathworks.com/matlabcentral/fileexchange/29434
  * http://matlab-trading.blogspot.com/2010/11/howto-wrap-interactive-brokers-tws-api.html
  * http://www.maxdama.com/?p=127
  * http://www.tradingwithmatlab.com/home
  * http://www.maxdama.com/


TWS (default port 7496)
```

java -cp jts.jar:hsqldb.jar:jcommon-1.0.12.jar:jfreechart-1.0.9.jar:jhall.jar:other.jar:rss.jar -Xmx512M jclient.LoginFrame .
```

<br>
<br>

Gateway (default port 4001)<br>
<pre><code><br>
java -cp jts.jar:hsqldb.jar:jcommon-1.0.12.jar:jhall.jar:other.jar:rss.jar -Dsun.java2d.noddraw=true -Xmx512M ibgateway.GWClient .<br>
</code></pre>

<br>
<br>

Demo client<br>
<br>
<pre><code><br>
java -jar jtsclient.jar<br>
</code></pre>


<h1>Protocol</h1>

<h2>Where to start</h2>

Protocol:<br>
<ul><li>Similar to FIX field delimiter is a zero byte.<br>
</li><li>All arguments are ASCII strings<br>
</li><li>First argument is always message identifier, for example REQ_MKT_DATA (1)<br>
</li><li>Second argument is version (always 1?)<br>
</li><li>Rest of the arguments is message specific - tickerId, etc</li></ul>

In the file EReader.java read looks like this<br>
<pre><code><br>
protected String readStr() throws IOException {<br>
StringBuffer buf = new StringBuffer();<br>
while( true) {<br>
byte c = m_dis.readByte();<br>
if( c == 0) {<br>
break;<br>
}<br>
buf.append( (char)c);<br>
}<br>
<br>
String str = buf.toString();<br>
return str.length() == 0 ? null : str;<br>
}<br>
</code></pre>

Send looks like this<br>
<pre><code><br>
protected void send( String str) throws IOException {<br>
// write string to data buffer; writer thread will<br>
// write it to socket<br>
if( !IsEmpty(str)) {<br>
m_dos.write( str.getBytes() );<br>
}<br>
sendEOL();<br>
}<br>
<br>
private void sendEOL() throws IOException {<br>
m_dos.write( EOL);<br>
}<br>
</code></pre>

There is apparently a limitation of ~50 message/seconds. There is no immediate reason to make the code more efficient. However we have this rather inefficient implementation.<br>
<br>
<br>
API is asynchronous. See interface EWrapper for required methods. See EClientSocket for outgoing message types.<br>
<br>
<h2>Initial handshake</h2>
<ul><li>Open client socket and connect to the server (file EClientSocket, method eConnect)<br>
</li><li>Send clientVersion - send string representing integer (46d) in decimal form terminated by zero.<br>
</li><li>Read integer - this is going to be ServerVersion (38d)<br>
</li><li>Send clientId. clientId starts from zero</li></ul>

<h2>Request/Response model</h2>

<ul><li>Enter loop which reads the incoming data (EReader.run()).<br>
</li><li>In the loop read first integer first, switch by message type<br>
</li><li>Client's requests contain an unique request ID (tickerId).<br>
</li><li>Server response contains version of the server software, tickerId and optional data.