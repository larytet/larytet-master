# Links #
  * http://statcomp.ats.ucla.edu/sas/
  * http://www.pauldickman.com/teaching/sas/index.php
  * http://www.amadeus.co.uk/sas-technical-services/tips-and-techniques/
  * http://blog.saasinct.com/
  * http://support.sas.com/events/sasglobalforum/previous/online.html
  * http://www.sascommunity.org/wiki/Main_Page
    * http://www.sascommunity.org/wiki/Supporting_a_SAS_Server_Architecture
  * http://www.softscapesolutions.com.au/tips.htm
  * http://platformadmin.com/blogs/paul/
  * http://blog.saasinct.com/
  * http://www.lexjansen.com/

# Code snippets #
## Options msglevel = N | I; ##

N - displays notes, warnings, and error messages only. This is the default.

I - displays additional notes pertaining to index usage, merge processing, and sort utilities along with standard notes, warnings, and error messages.

## Using _call execute_ in order to perform an action in a loop ##
```
%macro loadtrdta(filenm);
    ...
    ...
    ...
%mend;

data _null_;
    length stock $6.;
    do stock="bezeq","icl","leumi","poalim","teva";
        do num=13, 42, 69, 111, 113;
            name = compress(stock!!num);
            call execute('%loadtrdta('!!name!!')');
        end;
    end;
run;

```
## Interesting functions ##
**CONSTANT** - Computes machine and mathematical constants. Example:
```
    data _null_;
        pi = constant('pi');    *PI;
        e = constant('e');    *The natural base;
        big = constant('BIG');    *The largest double-precision number;
        small = constant('SMALL');    *The smallest double-precision number;
        put pi= e= big= small= ;
    run;

pi=3.1415926536 e=2.7182818285 big=1.797693E308 small=2.22507E-308
```
**COALESCE / COALESCEC** - return the first non-missing value from a list of numeric / character arguments.
## proc SQL ##
### Testing SQL code ###
```
    proc sql feedback;

    proc sql noexec;

    proc sql;
        validate
        <other sql statements>
```
### Accessing SAS System Information by Using DICTIONARY Tables ###
```
proc sql;
   describe table dictionary.tables;
```
Some tables:

|TABLES|Currently defined tables info|
|:-----|:----------------------------|
|COLUMNS|Columns in tables|
|VIEWS|  |
|TITLES|  |
|OPTIONS|Currently defined options info|
|INDEXES|  |
|LIBNAMES|  |
|FORMATS|Info on defined formats and informats|
|EXTFILES|Defined external files|

### SAS enhancements to the ANSI SQL ###
**calculated** keyword:
```
    proc sql;
        select *, sum(a, b, c) as total
        from ttt
        where calculated total>750
        ;
    quit;
```

**Re-merging summary statistics with original data:**

The following code:
```
proc sql;
	select 
            x, 
            x/sum(x) as pct format=percent8.1
	from aa
	;
quit;
```
will yield the following note:

```
NOTE: The query requires remerging summary statistics back with the original data.
```
In order to avoid it, modify the code as follows:
```
proc sql;
	select 
            x, 
            x/(select sum(x) from aa) as pct format=percent8.1
	from aa
	;
quit;
```
Note: sometimes re-merging is unavoidable and can save extra data processing.
### Combining Tables Vertically ###
**Set operators**: EXCEPT, INTERSECT, UNION, OUTER UNION
```
proc sql;
    select * from a
    _set-operator_ <ALL> <CORR>
    select * from b;
```

EXCEPT - selects _unique_ rows from the first table that are not found in the second table.

INTERSECT - selects _unique_ rows that are common to both tables.

UNION - selects _unique_ rows from one or both tables.

OUTER UNION - selects _all_ rows from both tables.

  * By default, the set operators EXCEPT, INTERSECT, and UNION overlay columns based on the relative position of the columns in the SELECT clause.

  * In order to be overlaid, columns in the same relative position in the two SELECT clauses must have _the same data type_.

ALL - makes only one pass through the data and does not remove duplicate rows. Cannot be used with OUTER UNION.

CORR - Compares and overlays columns by name instead of by position. With EXCEPT, INTERSECT, and UNION, removes any columns that do not have the same name in both tables. with OUTER UNION, overlays same-named columns and displays columns that have nonmatching names without overlaying.

## Using unnamed pipe (environment variable) to define paths in SAS ##

Example code (windows):
```
filename pipeset pipe "set JQUANT_ROOT";

data _null_;
	infile pipeset dsd;
	informat line $150.;
	format line $150.;
	input line $ ;
	call symput("JQUANT_ROOT", trim(scan(line,2,"=")));
	call symput("CleanLogsPath", trim(scan(line,2,"="))!!"DataLogs\Clean\");
run;

%put &CleanLogsPath &JQUANT_ROOT;
```

The 'set _env\_name_' command displays the environment variable _env\_name_.


## SAS Macros ##
### Macro Functions: ###
```
%STR (argument) - quote tokens during compilation in order to mask them from the macro processor.
%NRSTR (argument) - quote tokens that include macro triggers from the macro processor
%BQUOTE (argument) - quote a character string or resolved value of a text expression during execution
of a macro or macro language statement
%UPCASE (argument)
%QUPCASE (argument)
%SUBSTR (argument, position <,n>)
%QSUBSTR (argument, position <,n>)
%INDEX (source,string)
%SCAN (argument, n <,delimiters>)
%QSCAN (argument, n <,delimiters>)
%SYSFUNC (function(argument(s))<,format>)
%QSYSFUNC (function(argument(s))<,format>)
```

A %QFOO function is the same as %FOO, but masks special characters and mnemonic operators

Example:
```
%let a=begin;
%let b=%nrstr(&a);
%put UPCASE produces: %upcase(&b);
%put QUPCASE produces: %qupcase(&b);
```
LOG:
```
39   %let a=begin;
40   %let b=%nrstr(&a);
41   %put UPCASE produces: %upcase(&b);
SYMBOLGEN:  Macro variable B resolves to &a
SYMBOLGEN:  Some characters in the above value which were subject to macro quoting have been
            unquoted for printing.
SYMBOLGEN:  Macro variable A resolves to begin
UPCASE produces: begin
42   %put QUPCASE produces: %qupcase(&b);
SYMBOLGEN:  Macro variable B resolves to &a
SYMBOLGEN:  Some characters in the above value which were subject to macro quoting have been
            unquoted for printing.
QUPCASE produces: &A
```

### The %EVAL function ###
  * translates integer strings and hexadecimal strings to integers
  * translates tokens representing arithmetic, comparison, and logical operators to macro-level operators
  * performs arithmetic and logical operations.
For arithmetic expressions, if an operation results in a non-integer value, %EVAL truncates the value to an integer. Also, %EVAL returns a null value and issues an error
message when non-integer values are used in arithmetic expressions. %EVAL evaluates  logical expressions and returns a numeric value to indicate if the expression is false (0) or true (1 or any other numeric).
The %EVAL function does not convert the following to numeric values:
  * numeric strings that contain a period or E-notation
  * SAS date and time constants.
The **%SYSEVALF** function evaluates arithmetic and logical expressions using floating-point arithmetic and returns a value that is formatted using the BEST32. format. The result of the evaluation is always text.

Example:
```
%macro figureit(a,b);
	%let y=%sysevalf(&a+&b);
	%put The result with SYSEVALF is: &y;
	%put BOOLEAN conversion: %sysevalf(&a +&b, boolean);
	%put CEIL conversion: %sysevalf(&a +&b, ceil);
	%put FLOOR conversion: %sysevalf(&a +&b, floor);
	%put INTEGER conversion: %sysevalf(&a +&b, integer);
%mend figureit;
%figureit(100,1.59)
```

LOG:
```
The result with SYSEVALF is: 101.59
BOOLEAN conversion: 1
CEIL conversion: 102
FLOOR conversion: 101
INTEGER conversion: 101
```

Any macro language function or statement that requires a numeric or logical expression automatically invokes the %EVAL function (like %SUBSTR, %IF - %THEN - %ELSE or %SCAN). %SYSEVALF is the only macro function that can evaluate logical expressions that contain floating point or missing values. Specifying a conversion type can prevent problems when %SYSEVALF returns missing or floating point values to macro expressions or macro variables that are used in other macro expressions that require an integer value.

### Indirect reference to a macro variable - The Forward Re-Scan rule: ###
  * When multiple ampersands or percent signs precede a name token, the macro processor resolves two ampersands (&&) to one ampersand (&), and re-scans the
reference.
  * To re-scan a reference, the macro processor scans and resolves tokens from left to right from the point where multiple ampersands or percent signs are coded, until no more triggers can be resolved.
According to the Forward Re-Scan rule, you need to use three ampersands in front of
a macro variable name when its value matches the name of a second macro variable.

Example Code:
```
options symbolgen;
%let C005=Artificial Intelligence;
%let crsid=C005;
%put &C005;
%put &crsid;
%put &&crsid;
%put &&&crsid;
```
Log output:
```
5    options symbolgen;
6    %let C005=Artificial Intelligence;
7    %let crsid=C005;
8    %put &C005;
SYMBOLGEN:  Macro variable C005 resolves to Artificial Intelligence
Artificial Intelligence
9    %put &crsid;
SYMBOLGEN:  Macro variable CRSID resolves to C005
C005
10   %put &&crsid;
SYMBOLGEN:  && resolves to &.
SYMBOLGEN:  Macro variable CRSID resolves to C005
C005
11   %put &&&crsid;
SYMBOLGEN:  && resolves to &.
SYMBOLGEN:  Macro variable CRSID resolves to C005
SYMBOLGEN:  Macro variable C005 resolves to Artificial Intelligence
Artificial Intelligence
```

### Use SYMGET function to obtain macro variable values: ###

Code:
```
%let x=100;

data _null_;
	y = symget('x');
	put 'y value is: ' y;
run;
```
Log:
```
34
35   data _null_;
36       y = symget('x');
37       put 'y value is: ' y;
38   run;

y value is: 100
NOTE: DATA statement used (Total process time):
      real time           0.00 seconds
      cpu time            0.00 seconds
```

### Store macros permanently ###
  1. %INCLUDE
  1. autocall macro facility
  1. compiled macros

**Creating an Autocall Library**

Store all the macros in a separate directory, a single sas program file per macro, each program named the same as the macro it contains.
This directory is defined as a SAS autocall library using the SASAUTOS system option. Note that by default SAS installation there is sasautos already defined. In order to add your own autocall macros library, add to the autoexec the lines that look like:
  * OPTIONS MAUTOSOURCE SASAUTOS=_libref-specification_;
Be sure to use fileref, not libref. Although the specifying the direct path instead of fileref is possible, this is less flexible and not 'clean'.
Example:
```
filename mymacros "c:\mymacros";
options mautosource sasautos=(mymacros,sasautos);
```

**Stored Compiled Macros**
```
%macro macro-name<(parameter list)> /STORE <SOURCE> <DES=‘description’>;
    ...
%MEND <macro-name>;
```

To access stored macro:
```
options mstored sasmstore=_libname-ref_;
```

To access the source code (given the SOURCE option was specified in macro definition):
```
%COPY macro-name / SOURCE <OUTFILE=_output-file_>;
```

### To send an email from SAS ###
```
FILENAME fileref EMAIL ‘address’ <e-mail-options> EMAILSYS=<e-mail interface to use> EMAILPW=<password of user> EMAILID=<userid>;

DATA _null_;
    FILE fileref
    TO='recipient@somemail.com'
    SUBJECT= ‘Data for review’
    ATTACH= ‘Path/to/the/attachment.ext’;

    PUT ‘Good Afternoon,’;
    PUT ‘ ‘;
    PUT ‘Attached please find the updated file for your review.’;
    PUT ‘ ‘;
    PUT ‘Have a nice day,’;
RUN;
```

**Note:** Use a &syserr auto macro var to conditionally send messages.

### Encoding passwords in SAS ###
The **PWENCODE** procedure enables to encode passwords to be used to access various servers or database systems from SAS code, instead of plain text passwords.