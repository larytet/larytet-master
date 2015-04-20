###### GIT: Indefero Installation ######

Indefero is a WEB based (PHP) front end for the existing GIT repositories. Indefero also supports Subversion and Mercury repositories.

##### Links #####

  * Main page http://www.indefero.net/ (see Open Source tab at the top)
  * Downloads page http://projects.ceondo.com/p/indefero/downloads/
  * {{:howtos:git:indefero-1.0.zip|}} Version 1.0 32nd release of Indefero
  * {{:howtos:git:pluf-master.zip|}} Pluf Jan 10, 2011
  * [http://projects.ceondo.com/p/indefero/page/Installation/] Installation guide
  * [[daemon as a service](http://batterypowered.wordpress.com/2008/07/04/deploying-a-git-repository-server-in-ubuntu/|Git)]

##### Installation of required packages #####

```

sudo aptitude -o Acquire::http::Proxy={proxy_url}:80 install php-pear git-core
```

Setup PROXY for the PEAR
```

sudo pear config-set http_proxy {proxy_url}:80
```

```

sudo pear upgrade-all
sudo pear install --alldeps Mail
sudo pear install --alldeps Mail_mime
```


Create data base with name //indefero// using [[Admin](http://localhost/phpmyadmin|PHP)].

Create folder for the installation
```

sudo mkdir /home/www
sudo chown wiki:wiki /home/www

# Next two lines in case you want to keep repos or symbolic links to the repos in the $HOME directory
sudo mkdir -p /home/git/repositories
sudo chown wiki:wiki -R  /home/git/repositories

cd /home/www
unzip pluf-master.zip
unzip indefero-1.0.zip
mv pluf-master pluf
mv indefero-1.0 indefero
cp indefero/src/IDF/conf/idf.php-dist  indefero/src/IDF/conf/idf.php
cp indefero/src/IDF/conf/path.php-dist indefero/src/IDF/conf/path.php
```

##### Configuration #####

Fix constants in the file //indefero/src/IDF/conf/idf.php//. You need at least
```

$cfg['git_repositories'] = '/home/git/repositories/%s/.git';
$cfg['git_remote_url'] = '<server_url>/%s.git';
$cfg['git_write_remote_url'] = 'git@<server_url>:%s.git';
$cfg['idf_base'] = '/indefero/index.php';
$cfg['url_base'] = 'http://<server_url>';
$cfg['url_media'] = 'http://<server_url>/indefero/media';
$cfg['url_upload'] = 'http://<server_url>/media/upload';
$cfg['secret_key'] = 'a02sajhjkhuisyeuqwieunzbcskjfdwiuoweuroeu';
$cfg['db_login'] = 'root';
$cfg['db_password'] = 'password';
$cfg['db_server'] = 'localhost';
$cfg['db_version'] = '5.1'; # Only needed for MySQL
$cfg['db_engine'] = 'MySQL';
$cfg['db_database'] = 'indefero';
```

Create file //www/bootstrap.php// containing code below and do not forget change password
```

<?php
require '/home/www/indefero/src/IDF/conf/path.php';
require 'Pluf.php';
Pluf::start('/home/www/indefero/src/IDF/conf/idf.php');
Pluf_Dispatcher::loadControllers(Pluf::f('idf_views'));

$user = new Pluf_User();
$user->first_name = 'user_name';
$user->last_name = 'M'; // Required!
$user->login = 'user_name'; // must be lowercase!
$user->email = 'doe@example.com';
$user->password = 'yourpassword'; // the password is salted/hashed
// in the database, so do not worry :)
$user->administrator = true;
$user->active = true;
$user->create();
print "Bootstrap ok\n";
?>
```

Follow official instructions or do
```

cd /home/www/indefero/src
php /home/www/pluf/src/migrate.php --conf=IDF/conf/idf.php -a -i -d
php ../www/bootstrap.php
sudo mkdir /var/www/indefero
cd /var/www/indefero
sudo ln -s /home/www/indefero/www/index.php
sudo ln -s /home/www/indefero/www/media
```

This link should work [http://localhost/indefero/index.php].

##### Add projects #####

Create a new project which name is the same as an existing folder in the path specified in the file //src/IDF/conf/idf.php//. The existing folder should be GIT repository and will contain folder //.git//. For example if you did
```

ln -s /<path>/user_name /home/git/repositories/wl8
```
the new project name is going to be //wl8//.



#### Tips ####


  * See /home/www/indefero/src/IDF/conf/urls.php all enabled key words (autolinking)
  * Add file extensions to syntax highlighter in //./src/IDF/Views/Source.php//. For example, //h//.