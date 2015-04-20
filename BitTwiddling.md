# Clear least significant non-zero bit in a number #

```
x & (x-1)
```

# Get least significant non-zero bit of a number #

```
( x & ~(x-1) )
```


In GCC there are built in functions like this

```
// Returns the number of trailing 0-bits in x, starting at the least significant bit 
// position. If x is 0, the result is undefined. 
int __builtin_ctz (unsigned int x);
```