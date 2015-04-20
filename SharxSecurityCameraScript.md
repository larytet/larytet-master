# Introduction #

Script which modifies settings in Sharx camera


# Details #

```
curl -u $username:$password http://$CAMERA_IP/default.asp

curl -u $username:$password --data-urlencode "MODE%3A=software" --data-urlencode "NVCTL_EXPOSURE%3A=40" --data-urlencode "NVCTL_GAIN%3A=40" --data-urlencode "NVCTL_U%3A=40" --data-urlencode "IRLED%3A=off" --data-urlencode "BWMODE%3A=off" --data-urlencode "IRCUT%3A=off" -d "Submit=Submit" http://$CAMERA_IP/en/nvctl.asp

```


Foscam camera

```
#!/bin/bash

cleanup ()
{
	kill  $!
	exit 0
}

trap cleanup SIGINT SIGTERM

cd $1
while [ 1 ] ;do
	export DATE=`date`
	/usr/bin/cvlc --run-time $2  --sout="#transcode{vcodec=theo,vb=256}:standard{access=file,mux=ogg,dst=$DATE.ogg}"  http://admin:admin@192.168.18.15/videostream.cgi  vlc://quit
done

```