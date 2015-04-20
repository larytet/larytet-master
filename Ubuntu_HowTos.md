# An Internet connection is very slow on 10.10 64-bit #
Solution - disable IPV6 at boot level:
```
gksu gedit /etc/default/grub
```
Change:
```
GRUB_CMDLINE_LINUX_DEFAULT="quiet splash"
```
To:
```
GRUB_CMDLINE_LINUX_DEFAULT="quiet splash ipv6.disable=1"
```
```
sudo update-grub
```
Reboot.

# How to remove old kernels #
```
dpkg --get-selections | grep linux-headers
```
to list all the kernels currently installed
```
uname -r 
```
will display the current kernel used
```
sudo apt-get remove --purge 2.6.XX-X-*
```
to remove the obsolete kernel, where XX-X is the exact version, for example:
```
sudo apt-get remove --purge 2.6.27-7-*
```

Another way:
```
$ aptitude purge $(aptitude search ~ilinuximage -F %p|egrep -v "$(uname -r)|linux-imagegeneric")
```

# Multi-OS system #
## Restoring Grub 2.0 ##
  * boot with Ubuntu Live CD, mount HDD Ubuntu partition
```
    sudo grub-setup -d /media/xxxx/boot/grub -m /media/yyyy/boot/grub/device.map /dev/sda
```
> > where
```
    xxxx = UUID of the Ubuntu install partition (sudo blkid)
    yyyy = xxxxx
```
  * reboot
```
    sudo update-grub
```

## Change the default boot order in GRUB ##

edit ```
/etc/default/grub```

# Monodevelop #

  * Add ```
deb http://badgerports.org lucid main``` to the repositories
  * Add http://badgerports.org/directhex.ppa.asc to the Authorization keys and you are set
See also http://badgerports.org/help.html


# Command line Tips #

Check remaining disk space ```
df -ah```

# Scientific calculators #

  * numberempire.com - lot of calculators and LaTeX editor
  * codecogs.com/components/eqneditor/editor.php - open source math library and LaTeX editor