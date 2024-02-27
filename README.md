# CilDisassembler
A rough and ready CIL disassembler to disassemble raw bytes into CIL.

This is as rough as a bag of nails and could no doubt do with some improvement. I cobbled it together quickly to help me track down bugs in security products I work on.

Yes, there are plenty of disassemblers out there, and if you're working with physical Assembly files, I strongly recommend https://github.com/dnSpy/dnSpy, or ILDASM. However, I had a niche requirement that these other tools didn't satisfy: 

The tools I write in my day job at Waratek Ltd are all based around patching the raw CIL of web applications at runtime to enhance their security and functionality. All of our patching is done in memory just before the CIL is JITted and we never emit the patched code out to physical Assembly files. We do write the raw CIL bytes, both before patching and after patching, to debug logs though. I needed a tool that would allow me to paste in those raw hex bytes from the logs, and would decode the header, disassemble the code and decode the extra data sections.

Internally, the parser works on a string of contiguous hex bytes, as in `000102030405060708090A0B0C0D0E0F10` (non-case-sensitive), but the tool preprocesses the pasted text to remove:
* Whitespace (spaces and tabs)
* Commas
* 0x (non-case-sensitive)
* Newline characters (\n and \r)

Therefore, the following, in lowercase, uppercase or mixed case, with or without embedded newline characters, are all valid and equivalent input to the tool:
* `000102030405060708090a0b0c0d0e0f10`
* `00 01 02 03 04 05 06 07 08 09 0a 0b 0c 0d 0e 0f 10`
* `0x000x010x020x030x040x050x060x070x080x090x0a0x0b0x0c0x0d0x0e0x0f0x10`
* `0x00 0x01 0x02 0x03 0x04 0x05 0x06 0x07 0x08 0x09 0x0a 0x0b 0x0c 0x0d 0x0e 0x0f 0x10`
* `0x00,0x01,0x02,0x03,0x04,0x05,0x06,0x07,0x08,0x09,0x0a,0x0b,0x0c,0x0d,0x0e,0x0f,0x10`
* `0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0a, 0x0b, 0x0c, 0x0d, 0x0e, 0x0f, 0x10`

<img src="/images/CilDisassembler.png" alt="CilDisassembler"/>
