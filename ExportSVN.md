# Introduction #

The commands below will dump the SVN repository from Google Code to a single dump file suitable for further import.


Some options: http://rsvndump.sourceforge.net/manpage.html

# Details #

```


svnadmin create /tmp/svn

cp /tmp/svn/hooks/pre-revprop-change.tmpl /tmp/svn/hooks/pre-revprop-change

chmod +x /tmp/svn/hooks/pre-revprop-change

```

Modify the file /tmp/svn/hooks/pre-revprop-change to exit 0 in all cases

```


svnsync init file:///tmp/svn http://larytet-master.googlecode.com/svn

svnsync sync file:///tmp/svn

svnadmin dump /tmp/svn > dump_file

```