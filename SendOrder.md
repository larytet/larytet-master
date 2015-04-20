UserClass.SendOrderRz blocks the calling thread for about 30ms

There are two asmachtot. "Asmachta FMR" is the only thing which is important to us. We still log TASE asmachta. Return code is zero if Ok. If the fatal error return code is non zero. If error is not fatal we probably need a confirmation. We have two parameters OrderId (state of the order) which is code describing state of the order FSM on the broker server. Broker will keep such order for about 20s. The system will fix the order according OrderId and send confirmation.


Is there a situation when SendOrderRx does not return? For example, network connection drops - what is the timeout.


After we get asmachta we poll the server.

Update/cancel - another thread. Check fill after the order placed.