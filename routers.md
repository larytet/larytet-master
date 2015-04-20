SMC2804

http://www.ubnt.com/products/rspro.php

https://dev.openwrt.org/wiki/ar71xx

I'd set up openwrt or distro-of-your-choice (m0n0wall was nice last time I looked at these things) on a small and silent PC with two network cards, mini-itx or such. That would give you the prestanda and flexibility you want.

http://www.smallnetbuilder.com/component/option,com_chart/Itemid,189/


I have a 100/10 mbit (fiber, no modems etc) line at home and use a Linksys WRT-160NL. When I do heavy file transfer (downloading, mainly from big FTPs like universities and such) the speed is around 90 mbits (~9.5 Mb/sec).
I highly recommend it. And if you're extra geeky, I know that there's a OpenWRT port being worked on, but it's not finished yet.


Catalyst 2924


Like another user stated use pfsense. We had this problem at work. We are a library and just got 100/100 fiber service. Couldnt afford to buy some $10,000 router and our $1000 router couldnt handle the speeds. Downloaded pfsense and put it on an old server and get full 100/100 speed. Its open source , has snort and everything. ITs free to use and they have a pay for support option as well.


Yet another interesting alternative is to run your router on a VM. In my case, I also needed to have a file server, an Asterisk server, a web server, virtual desktop, etc, it made sense for me to also run the router on a VM. I built an i7 box with 12GB of RAM and 2x1TB disks for about 900 bucks, installed the free ESXi 4U1 and separate NIC cards for each interface and a virtual DMZ. The box is a rocket, and I now that covers all my needs with a single computer in the house.


Wireless N Gigabit router DIR-655". Believe it or not, but I have actually seen throughput close to 150 Mbps (using NAT) on the WAN while on this network.