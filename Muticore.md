http://www.mono-project.com/Release_Notes_Mono_2.6

This release includes some components of the ParallelFx framework (http://msdn.microsoft.com/en-us/library/dd460693%28VS.100%29.aspx) that were developed as part of Google Summer Of Code 2008 & 2009. More precisely, it contains the Task Parallel Library (http://msdn.microsoft.com/en-us/library/dd460717%28VS.100%29.aspx) and Data Structures For Coordination (http://msdn.microsoft.com/en-us/library/dd460718%28VS.100%29.aspx).

Using ParallelFx, you can easily develop software that can automatically take advantage of the parallel potential of today multicore machines. For that purpose, several new constructs like futures, parallel loops or concurrent collections are now available.

To use this code you have to manually enable the .NET 4 profile using the --with-profile4=yes switch at configure stage.


  * Intel® Threading Building Blocks (TBB) http://www.threadingbuildingblocks.org/
  * Intel Parallel Studio
  * Kai's C++ (owned by Intel?)

Appro announced a pair of 1U servers aimed at small- and medium-sized HPC (high performance computing) deployments. The Tetra 1326G4 and 1426G4 combine Intel or AMD processors with Nvidia Tesla M2050 GPU (graphics processing unit) cards, offering up to 24 CPU cores and 1,792 GPU cores plus as much as 3TB of storage

FPGA

  * http://www.wallstreetandtech.com/financial-risk-management/showArticle.jhtml?articleID=223100577&pgno=2

  * http://www.concurrenteda.com   x86 assembly
  * http://www.impulseaccelerated.com C/C++ to HDL
  * http://www.mathworks.com/fpga-design : Matlab to HDL
  * http://www.maxeler.com/content/frontpage : Java
  * http://www.xilinx.com/prs_rls/ip/0535xlnx_accelchip.htm : Matlab to HDL
  * The Convey systems use FPGA's in a different way than "traditional" approaches (one of the reasons we were funded by both Xilinx and Intel). The Convey coprocessor—where the FPGAs reside—hosts extensions to the x86\_64 ISA (called "personalities"). As you might expect, these extended instructions can be anything from classic vector processing (our roots: Convey <== Convex+1) to instructions that are very application-specific (i.e. an instruction that implements the Mersenne twister).
  * Exergy, Active Financials, Solace, Tervela and the TIBCO Messaging appliance
  * Celoxica GMAC
  * Alma can encode your C++ directly into VHDL