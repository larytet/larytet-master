[Redmine](http://www.redmine.org) is a flexible project management web application written using [Ruby on Rails](http://rubyonrails.org) framework.

1-minute installation guide - http://www.richardnichols.net/2009/09/1-minute-guide-installing-redmine-on-windows/.

```
>gem install rails
Fetching: activesupport-3.0.9.gem (100%)
Fetching: builder-2.1.2.gem (100%)
Fetching: i18n-0.5.0.gem (100%)
Fetching: activemodel-3.0.9.gem (100%)
Fetching: rack-1.2.3.gem (100%)
Fetching: rack-test-0.5.7.gem (100%)
Fetching: rack-mount-0.6.14.gem (100%)
Fetching: tzinfo-0.3.28.gem (100%)
Fetching: abstract-1.0.0.gem (100%)
Fetching: erubis-2.6.6.gem (100%)
Fetching: actionpack-3.0.9.gem (100%)
Fetching: arel-2.0.10.gem (100%)
Fetching: activerecord-3.0.9.gem (100%)
Fetching: activeresource-3.0.9.gem (100%)
Fetching: mime-types-1.16.gem (100%)
Fetching: polyglot-0.3.1.gem (100%)
Fetching: treetop-1.4.9.gem (100%)
Fetching: mail-2.2.19.gem (100%)
Fetching: actionmailer-3.0.9.gem (100%)
Fetching: rake-0.9.2.gem (100%)
Fetching: thor-0.14.6.gem (100%)
Fetching: rdoc-3.6.1.gem (100%)
Fetching: railties-3.0.9.gem (100%)
Fetching: bundler-1.0.15.gem (100%)
Fetching: rails-3.0.9.gem (100%)
Successfully installed activesupport-3.0.9
Successfully installed builder-2.1.2
Successfully installed i18n-0.5.0
Successfully installed activemodel-3.0.9
Successfully installed rack-1.2.3
Successfully installed rack-test-0.5.7
Successfully installed rack-mount-0.6.14
Successfully installed tzinfo-0.3.28
Successfully installed abstract-1.0.0
Successfully installed erubis-2.6.6
Successfully installed actionpack-3.0.9
Successfully installed arel-2.0.10
Successfully installed activerecord-3.0.9
Successfully installed activeresource-3.0.9
Successfully installed mime-types-1.16
Successfully installed polyglot-0.3.1
Successfully installed treetop-1.4.9
Successfully installed mail-2.2.19
Successfully installed actionmailer-3.0.9
Successfully installed rake-0.9.2
Successfully installed thor-0.14.6
Successfully installed rdoc-3.6.1
Successfully installed railties-3.0.9
Successfully installed bundler-1.0.15
Successfully installed rails-3.0.9
25 gems installed
... //ommited docs install...
```

```
>gem install mongrel
Fetching: gem_plugin-0.2.3.gem (100%)
Fetching: cgi_multipart_eof_fix-2.5.0.gem (100%)
Fetching: mongrel-1.1.5-x86-mingw32.gem (100%)
Successfully installed gem_plugin-0.2.3
Successfully installed cgi_multipart_eof_fix-2.5.0
Successfully installed mongrel-1.1.5-x86-mingw32
3 gems installed
Installing ri documentation for gem_plugin-0.2.3...
Installing ri documentation for cgi_multipart_eof_fix-2.5.0...
Installing ri documentation for mongrel-1.1.5-x86-mingw32...
Installing RDoc documentation for gem_plugin-0.2.3...
Installing RDoc documentation for cgi_multipart_eof_fix-2.5.0...
Installing RDoc documentation for mongrel-1.1.5-x86-mingw32...
```

### Tips ###
  1. Skip RDoc and ri documentation installation while installing gem:
```
gem install rails --no-ri --no-rdoc 
```