# DEUCE
The main goals are converting the assembly code to machine code and simulating the program execution phase

In this project, you are expected to design simulator for a basic computer called DEUCE (DEU Electronic Universal Computing Engine) which is based on RISC. The project should have the following specifications: 


- Reading and parsing the assembly code.  
- Running the program step by step while showing the phases of instruction cycle (fetching, decoding, execution). 

- Generating label table and memory content table. 

- Showing contents of the registers and memory segments. 

- Showing computer operations and their micro operations. 

- Supporting the instruction set shown in Table 1.

- Switching between binary / decimal numbers.   

- Exporting hex or mif file of the machine code( hex code or binary code). 

- User friendly, efficient Graphical User Interface(GUI). 
 **********
![ScreenShot](http://imageshack.com/a/img924/8162/4JshoO.png)
The computer DEUCE has 10 registers and 3 memory segments  ( Figure 1). Registers are Address Register, Program Counter, Stack Pointer, Input Register, Output Register, Instruction Register and 4 general purpose registers. The memory segments are instruction, data and stack memory. Also relevant flags should be considered.  
The input files (asm or basm file) include assembly (symbolic) codes.  The assembly language of the basic computer is defined by a set of rules. An example for assembly code is given in the Code 1.  Each line of the language has three columns called fields. 
![ScreenShot](http://imageshack.com/a/img923/9345/6FVCAl.png)
1. The label field may be empty or it may specify a symbolic address. It is followed by a colon(:).  2. The instruction field specifies a machine instruction (Table 1) or a pseudo-instruction ( ORG, END, DEC N, HEX N). 3. The comment field may be empty or it may include a comment after a sharp sign (#). 


The format of a line is as follow:   [label]: instruction [#comment]
******
![ScreenShot](http://imageshack.com/a/img923/7277/nApwxL.png)
*********
Project's Trailer: https://youtu.be/wtgxpYM9V2M

