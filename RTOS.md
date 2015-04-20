No need for RTOS - coroutines are just fine in many cases One of the ways to implement in C (10 minutes Intro and well worth the time spent)http://www.chiark.greenend.org.uk/~sgtatham/coroutines.html

And another tutorial http://home.swbell.net/mck9/ct/


Another engine (tested for ARM) http://swtch.com/libtask/

Protothreads (ARM support) http://www.sics.se/~adam/pt/ Last, but not least FreeRTOS (ARM support) provides coroutines

Back to the RTOS kernels - FreeRTOS kernel does the trick of threads with separate stacks staring from 4K of binary code. Frequently the core is used in devices powered by 16-bits processors.



Pros of code running at user level (thread, task) instead of interrupt level (SWI, etc)

  * unlimited number of tasks. I can keep a separate task for every system resource or data unit and avoid disable interrupts in many cases
  * disable thread scheduler is a cheaper operation than disable an interrupt or a group of interrupts. No interrupt latency impact.
  * threads are cheap to create and invoke and interrupts are expensive
  * thread invocation can be done faster than interrupt invocation, because in the simplest case we do not need to switch stack
  * unlimited number of priorities and not depending on the hardware
  * round robin, priority strict, cooperative scheduler are possible at the system level and between groups of tasks


Books
  * "MicroC OS II: The Real Time Kernel (With CD-ROM)"
  * Concurrent Euclid, the Unix System, and Tunis (Addison-Wesley series in computer science) [Paperback](Paperback.md) R.C. Holt
  * Structured Concurrent Programming With Operating Systems Applications (Addison-Wesley series in computer science) [Paperback](Paperback.md) R. C. Holt
  * Power consumption and RTOS rtos.com/PDFs/PDCE2008.pdf slide 20-21