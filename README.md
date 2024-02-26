# CilDisassembler
A rough and ready CIL disassembler to disassemble raw bytes into CIL.

This is as rough as a bag of nails. I cobbled it together quickly to help me track down bugs in security products I work on.

Yes, there are plenty of disassemblers out there, and if you're working with physical Assembly files, I strongly recommend https://github.com/dnSpy/dnSpy, or ILDASM. However, I had a niche requirement that these other tools didn't satisfy: 

The tools I write in my day job at Waratek Ltd are all based around patching the raw CIL of web applications at runtime to enhance their security and functionality. All of our patching is done in memory just before the CIL is JITted and we never emit the patched code out to physical Assembly files. We do write the raw CIL bytes, both before patching and after patching, to debug logs though. I needed a tool that would allow me to paste in those raw hex bytes from the logs, and would decode the header, disassemble the code and decode the extra data sections.


<img src="/images/CilDisassembler.png" alt="CilDisassembler"/>
