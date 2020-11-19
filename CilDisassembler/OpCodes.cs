using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.Layout;

namespace CilDisassembler
{
    public class OpCodes
    {
        private static Dictionary<string, OpCodeData> OpCodeDb;
        private static Dictionary<string, OpCodeData> OpCodeDb2;

        static OpCodes()
        {
            OpCodeDb = new Dictionary<string, OpCodeData>();
            OpCodeDb2 = new Dictionary<string, OpCodeData>();
            InitializeOpCodeDb();
        }

        public static string Disassemble(string byteString, int codeSize)
        {
            var bytes = FormatHelper.PurgeSeparators(byteString);
            var sb = new StringBuilder();
            int i = 0;
            do
            {
                if (i >= bytes.Length) break;

                if (codeSize != 0 && i >= codeSize * 2) break;

                var byt = GetByteAtOffset(bytes, i);

                var opCodeData = OpCodeDb[byt];
                if (opCodeData.IsTwoByteOpCode)
                {
                    i += 2;
                    byt = GetByteAtOffset(bytes, i);
                    opCodeData = OpCodeDb2[byt];
                    sb.Append($"IL_{(i / 2) - 1,4:X4},");
                }
                else
                {
                    sb.Append($"IL_{i / 2,4:X4},");

                }

                sb.Append(opCodeData.Name);
                sb.Append(',');
                i += 2;

                sb.Append(ParseOperands(opCodeData, ref i, bytes));
                sb.Append("\n");
            } while (true);

            return sb.ToString();
        }

        private static string ParseOperands(OpCodeData opCodeData, ref int i, string bytes)
        {
            if (opCodeData.OperandCount == 0)
            {
                return string.Empty;
            }

            var opCodeOffset = i / 2;

            var sb = new StringBuilder();

            var argLength = opCodeData.OperandCount * 2;
            var bigEndian = bytes.Substring(i, argLength);
            i += argLength;

            var littleEndian = ToLittleEndian(bigEndian);
            sb.Append(littleEndian).Append(" ");

            if (opCodeData.IsBranch)
            {
                uint offset = uint.Parse(littleEndian.Substring(2), NumberStyles.AllowHexSpecifier);

                // Handle twos-complement negative numbers
                if (opCodeData.OperandCount == 1 && offset >= sbyte.MaxValue)
                {
                    byte byteOffset = (byte)offset;
                    sbyte offsetNeg = (sbyte)~(byteOffset - 1);
                    long branchLocation = (i + (offsetNeg * -2)) / 2;
                    sb.Append($" ( IL_{branchLocation,4:X4} )");
                }
                else if (opCodeData.OperandCount == 4 && offset > int.MaxValue)
                {
                    int intOffset = (int)offset;
                    int offsetNeg = (int)~(intOffset - 1);
                    var branchLocation = (i + (offsetNeg * -2)) / 2;
                    sb.Append($" ( IL_{branchLocation,4:X4} )");
                }
                else
                {
                    var branchLocation = (i + (offset * 2)) / 2;
                    sb.Append($" ( IL_{branchLocation,4:X4} )");
                }
            }

            if (opCodeData.Name == "switch")
            {
                var caseCount = int.Parse(littleEndian.Substring(2), NumberStyles.AllowHexSpecifier);

                var nextOpCodeAddress = opCodeOffset
                                        + 4 // Size of 'case count' argument in bytes
                                        + (caseCount * 4);  // number of case arguments time the size of an Int32 in bytes

                for (var j = 0; j < caseCount; j++)
                {
                    argLength = 8;
                    bigEndian = bytes.Substring(i, argLength);
                    i += argLength;

                    littleEndian = ToLittleEndian(bigEndian);

                    var jumpSize = int.Parse(littleEndian.Substring(2), NumberStyles.AllowHexSpecifier);
                    sb.Append(littleEndian).Append($"( IL_{nextOpCodeAddress + jumpSize,4:X4} ) ");
                }
            }
            return sb.ToString();
        }

        private static string ToLittleEndian(string bigEndian)
        {
            var sb = new StringBuilder("0x");
            for (int i = bigEndian.Length - 2; i >= 0; i -= 2)
            {
                sb.Append(bigEndian.Substring(i, 2));
            }

            return sb.ToString();
        }

        private static string GetByteAtOffset(string bytes, int i)
        {
            return bytes.Substring(i, 2);
        }

        private static void InitializeOpCodeDb()
        {
            OpCodeDb.Add("00", new OpCodeData { Name = "nop", OperandCount = 0 });
            OpCodeDb.Add("01", new OpCodeData { Name = "break", OperandCount = 0 });
            OpCodeDb.Add("02", new OpCodeData { Name = "ldarg.0", OperandCount = 0 });
            OpCodeDb.Add("03", new OpCodeData { Name = "ldarg.1", OperandCount = 0 });
            OpCodeDb.Add("04", new OpCodeData { Name = "ldarg.2", OperandCount = 0 });
            OpCodeDb.Add("05", new OpCodeData { Name = "ldarg.3", OperandCount = 0 });
            OpCodeDb.Add("06", new OpCodeData { Name = "ldloc.0", OperandCount = 0 });
            OpCodeDb.Add("07", new OpCodeData { Name = "ldloc.1", OperandCount = 0 });
            OpCodeDb.Add("08", new OpCodeData { Name = "ldloc.2", OperandCount = 0 });
            OpCodeDb.Add("09", new OpCodeData { Name = "ldloc.3", OperandCount = 0 });
            OpCodeDb.Add("0A", new OpCodeData { Name = "stloc.0", OperandCount = 0 });
            OpCodeDb.Add("0B", new OpCodeData { Name = "stloc.1", OperandCount = 0 });
            OpCodeDb.Add("0C", new OpCodeData { Name = "stloc.2", OperandCount = 0 });
            OpCodeDb.Add("0D", new OpCodeData { Name = "stloc.3", OperandCount = 0 });
            OpCodeDb.Add("0E", new OpCodeData { Name = "ldarg.s", OperandCount = 1 });
            OpCodeDb.Add("0F", new OpCodeData { Name = "ldarga.s", OperandCount = 1 });
            OpCodeDb.Add("10", new OpCodeData { Name = "starg.s", OperandCount = 1 });
            OpCodeDb.Add("11", new OpCodeData { Name = "ldloc.s", OperandCount = 1 });
            OpCodeDb.Add("12", new OpCodeData { Name = "ldloca.s", OperandCount = 1 });
            OpCodeDb.Add("13", new OpCodeData { Name = "stloc.s", OperandCount = 1 });
            OpCodeDb.Add("14", new OpCodeData { Name = "ldnull", OperandCount = 0 });
            OpCodeDb.Add("15", new OpCodeData { Name = "ldc.i4.m1", OperandCount = 0 });
            OpCodeDb.Add("16", new OpCodeData { Name = "ldc.i4.0", OperandCount = 0 });
            OpCodeDb.Add("17", new OpCodeData { Name = "ldc.i4.1", OperandCount = 0 });
            OpCodeDb.Add("18", new OpCodeData { Name = "ldc.i4.2", OperandCount = 0 });
            OpCodeDb.Add("19", new OpCodeData { Name = "ldc.i4.3", OperandCount = 0 });
            OpCodeDb.Add("1A", new OpCodeData { Name = "ldc.i4.4", OperandCount = 0 });
            OpCodeDb.Add("1B", new OpCodeData { Name = "ldc.i4.5", OperandCount = 0 });
            OpCodeDb.Add("1C", new OpCodeData { Name = "ldc.i4.6", OperandCount = 0 });
            OpCodeDb.Add("1D", new OpCodeData { Name = "ldc.i4.7", OperandCount = 0 });
            OpCodeDb.Add("1E", new OpCodeData { Name = "ldc.i4.8", OperandCount = 0 });
            OpCodeDb.Add("1F", new OpCodeData { Name = "ldc.i4.s", OperandCount = 1 });
            OpCodeDb.Add("20", new OpCodeData { Name = "ldc.i4", OperandCount = 4 });
            OpCodeDb.Add("21", new OpCodeData { Name = "ldc.i8", OperandCount = 8 });
            OpCodeDb.Add("22", new OpCodeData { Name = "ldc.r4", OperandCount = 4 });
            OpCodeDb.Add("23", new OpCodeData { Name = "ldc.r8", OperandCount = 8 });
            OpCodeDb.Add("25", new OpCodeData { Name = "dup", OperandCount = 0 });
            OpCodeDb.Add("26", new OpCodeData { Name = "pop", OperandCount = 0 });
            OpCodeDb.Add("27", new OpCodeData { Name = "jmp", OperandCount = 4 });
            OpCodeDb.Add("28", new OpCodeData { Name = "call", OperandCount = 4 });
            OpCodeDb.Add("29", new OpCodeData { Name = "calli", OperandCount = 4 });
            OpCodeDb.Add("2A", new OpCodeData { Name = "ret", OperandCount = 0 });
            OpCodeDb.Add("2B", new OpCodeData { Name = "br.s", OperandCount = 1, IsBranch = true });
            OpCodeDb.Add("2C", new OpCodeData { Name = "brfalse.s", OperandCount = 1, IsBranch = true });
            OpCodeDb.Add("2D", new OpCodeData { Name = "brtrue.s", OperandCount = 1, IsBranch = true });
            OpCodeDb.Add("2E", new OpCodeData { Name = "beq.s", OperandCount = 1, IsBranch = true });
            OpCodeDb.Add("2F", new OpCodeData { Name = "bge.s", OperandCount = 1, IsBranch = true });
            OpCodeDb.Add("30", new OpCodeData { Name = "bgt.s", OperandCount = 1, IsBranch = true });
            OpCodeDb.Add("31", new OpCodeData { Name = "ble.s", OperandCount = 1, IsBranch = true });
            OpCodeDb.Add("32", new OpCodeData { Name = "blt.s", OperandCount = 1, IsBranch = true });
            OpCodeDb.Add("33", new OpCodeData { Name = "bne.un.s", OperandCount = 1, IsBranch = true });
            OpCodeDb.Add("34", new OpCodeData { Name = "bge.un.s", OperandCount = 1, IsBranch = true });
            OpCodeDb.Add("35", new OpCodeData { Name = "bgt.un.s", OperandCount = 1, IsBranch = true });
            OpCodeDb.Add("36", new OpCodeData { Name = "ble.un.s", OperandCount = 1, IsBranch = true });
            OpCodeDb.Add("37", new OpCodeData { Name = "blt.un.s", OperandCount = 1, IsBranch = true });
            OpCodeDb.Add("38", new OpCodeData { Name = "br", OperandCount = 4, IsBranch = true });
            OpCodeDb.Add("39", new OpCodeData { Name = "brfalse", OperandCount = 4, IsBranch = true });
            OpCodeDb.Add("3A", new OpCodeData { Name = "brtrue", OperandCount = 4, IsBranch = true });
            OpCodeDb.Add("3B", new OpCodeData { Name = "beq", OperandCount = 4, IsBranch = true });
            OpCodeDb.Add("3C", new OpCodeData { Name = "bge", OperandCount = 4, IsBranch = true });
            OpCodeDb.Add("3D", new OpCodeData { Name = "bgt", OperandCount = 4, IsBranch = true });
            OpCodeDb.Add("3E", new OpCodeData { Name = "ble", OperandCount = 4, IsBranch = true });
            OpCodeDb.Add("3F", new OpCodeData { Name = "blt", OperandCount = 4, IsBranch = true });
            OpCodeDb.Add("40", new OpCodeData { Name = "bne.un", OperandCount = 4, IsBranch = true });
            OpCodeDb.Add("41", new OpCodeData { Name = "bge.un", OperandCount = 4, IsBranch = true });
            OpCodeDb.Add("42", new OpCodeData { Name = "bgt.un", OperandCount = 4, IsBranch = true });
            OpCodeDb.Add("43", new OpCodeData { Name = "ble.un", OperandCount = 4, IsBranch = true });
            OpCodeDb.Add("44", new OpCodeData { Name = "blt.un", OperandCount = 4, IsBranch = true });
            OpCodeDb.Add("45", new OpCodeData { Name = "switch", OperandCount = 4, HasArgList = true });
            OpCodeDb.Add("46", new OpCodeData { Name = "ldind.i1", OperandCount = 0 });
            OpCodeDb.Add("47", new OpCodeData { Name = "ldind.u1", OperandCount = 0 });
            OpCodeDb.Add("48", new OpCodeData { Name = "ldind.i2", OperandCount = 0 });
            OpCodeDb.Add("49", new OpCodeData { Name = "ldind.u2", OperandCount = 0 });
            OpCodeDb.Add("4A", new OpCodeData { Name = "ldind.i4", OperandCount = 0 });
            OpCodeDb.Add("4B", new OpCodeData { Name = "ldind.u4", OperandCount = 0 });
            OpCodeDb.Add("4C", new OpCodeData { Name = "ldind.i8", OperandCount = 0 });
            OpCodeDb.Add("4D", new OpCodeData { Name = "ldind.i", OperandCount = 0 });
            OpCodeDb.Add("4E", new OpCodeData { Name = "ldind.r4", OperandCount = 0 });
            OpCodeDb.Add("4F", new OpCodeData { Name = "ldind.r8", OperandCount = 0 });
            OpCodeDb.Add("50", new OpCodeData { Name = "ldind.ref", OperandCount = 0 });
            OpCodeDb.Add("51", new OpCodeData { Name = "stind.ref", OperandCount = 0 });
            OpCodeDb.Add("52", new OpCodeData { Name = "stind.i1", OperandCount = 0 });
            OpCodeDb.Add("53", new OpCodeData { Name = "stind.i2", OperandCount = 0 });
            OpCodeDb.Add("54", new OpCodeData { Name = "stind.i4", OperandCount = 0 });
            OpCodeDb.Add("55", new OpCodeData { Name = "stind.i8", OperandCount = 0 });
            OpCodeDb.Add("56", new OpCodeData { Name = "stind.r4", OperandCount = 0 });
            OpCodeDb.Add("57", new OpCodeData { Name = "stind.r8", OperandCount = 0 });
            OpCodeDb.Add("58", new OpCodeData { Name = "add", OperandCount = 0 });
            OpCodeDb.Add("59", new OpCodeData { Name = "sub", OperandCount = 0 });
            OpCodeDb.Add("5A", new OpCodeData { Name = "mul", OperandCount = 0 });
            OpCodeDb.Add("5B", new OpCodeData { Name = "div", OperandCount = 0 });
            OpCodeDb.Add("5C", new OpCodeData { Name = "div.un", OperandCount = 0 });
            OpCodeDb.Add("5D", new OpCodeData { Name = "rem", OperandCount = 0 });
            OpCodeDb.Add("5E", new OpCodeData { Name = "rem.un", OperandCount = 0 });
            OpCodeDb.Add("5F", new OpCodeData { Name = "and", OperandCount = 0 });
            OpCodeDb.Add("60", new OpCodeData { Name = "or", OperandCount = 0 });
            OpCodeDb.Add("61", new OpCodeData { Name = "xor", OperandCount = 0 });
            OpCodeDb.Add("62", new OpCodeData { Name = "shl", OperandCount = 0 });
            OpCodeDb.Add("63", new OpCodeData { Name = "shr", OperandCount = 0 });
            OpCodeDb.Add("64", new OpCodeData { Name = "shr.un", OperandCount = 0 });
            OpCodeDb.Add("65", new OpCodeData { Name = "neg", OperandCount = 0 });
            OpCodeDb.Add("66", new OpCodeData { Name = "not", OperandCount = 0 });
            OpCodeDb.Add("67", new OpCodeData { Name = "conv.i1", OperandCount = 0 });
            OpCodeDb.Add("68", new OpCodeData { Name = "conv.i2", OperandCount = 0 });
            OpCodeDb.Add("69", new OpCodeData { Name = "conv.i4", OperandCount = 0 });
            OpCodeDb.Add("6A", new OpCodeData { Name = "conv.i8", OperandCount = 0 });
            OpCodeDb.Add("6B", new OpCodeData { Name = "conv.r4", OperandCount = 0 });
            OpCodeDb.Add("6C", new OpCodeData { Name = "conv.r8", OperandCount = 0 });
            OpCodeDb.Add("6D", new OpCodeData { Name = "conv.u4", OperandCount = 0 });
            OpCodeDb.Add("6E", new OpCodeData { Name = "conv.u8", OperandCount = 0 });
            OpCodeDb.Add("6F", new OpCodeData { Name = "callvirt", OperandCount = 4 });
            OpCodeDb.Add("70", new OpCodeData { Name = "cpobj", OperandCount = 4 });
            OpCodeDb.Add("71", new OpCodeData { Name = "ldobj", OperandCount = 4 });
            OpCodeDb.Add("72", new OpCodeData { Name = "ldstr", OperandCount = 4 });
            OpCodeDb.Add("73", new OpCodeData { Name = "newobj", OperandCount = 4 });
            OpCodeDb.Add("74", new OpCodeData { Name = "castclass", OperandCount = 4 });
            OpCodeDb.Add("75", new OpCodeData { Name = "isinst", OperandCount = 4 });
            OpCodeDb.Add("76", new OpCodeData { Name = "conv.r.un", OperandCount = 0 });
            OpCodeDb.Add("79", new OpCodeData { Name = "unbox", OperandCount = 4 });
            OpCodeDb.Add("7A", new OpCodeData { Name = "throw", OperandCount = 0 });
            OpCodeDb.Add("7B", new OpCodeData { Name = "ldfld", OperandCount = 4 });
            OpCodeDb.Add("7C", new OpCodeData { Name = "ldflda", OperandCount = 4 });
            OpCodeDb.Add("7D", new OpCodeData { Name = "stfld", OperandCount = 4 });
            OpCodeDb.Add("7E", new OpCodeData { Name = "ldsfld", OperandCount = 4 });
            OpCodeDb.Add("7F", new OpCodeData { Name = "ldsflda", OperandCount = 4 });
            OpCodeDb.Add("80", new OpCodeData { Name = "stsfld", OperandCount = 4 });
            OpCodeDb.Add("81", new OpCodeData { Name = "stobj", OperandCount = 4 });
            OpCodeDb.Add("82", new OpCodeData { Name = "conv.ovf.i1.un", OperandCount = 0 });
            OpCodeDb.Add("83", new OpCodeData { Name = "conv.ovf.i2.un", OperandCount = 0 });
            OpCodeDb.Add("84", new OpCodeData { Name = "conv.ovf.i4.un", OperandCount = 0 });
            OpCodeDb.Add("85", new OpCodeData { Name = "conv.ovf.i8.un", OperandCount = 0 });
            OpCodeDb.Add("86", new OpCodeData { Name = "conv.ovf.u1.un", OperandCount = 0 });
            OpCodeDb.Add("87", new OpCodeData { Name = "conv.ovf.u2.un", OperandCount = 0 });
            OpCodeDb.Add("88", new OpCodeData { Name = "conv.ovf.u4.un", OperandCount = 0 });
            OpCodeDb.Add("89", new OpCodeData { Name = "conv.ovf.u8.un", OperandCount = 0 });
            OpCodeDb.Add("8A", new OpCodeData { Name = "conv.ovf.i.un", OperandCount = 0 });
            OpCodeDb.Add("8B", new OpCodeData { Name = "conv.ovf.u.un", OperandCount = 0 });
            OpCodeDb.Add("8C", new OpCodeData { Name = "box", OperandCount = 4 });
            OpCodeDb.Add("8D", new OpCodeData { Name = "newarr", OperandCount = 4 });
            OpCodeDb.Add("8E", new OpCodeData { Name = "ldlen", OperandCount = 0 });
            OpCodeDb.Add("8F", new OpCodeData { Name = "ldelema", OperandCount = 4 });
            OpCodeDb.Add("90", new OpCodeData { Name = "ldelem.i1", OperandCount = 0 });
            OpCodeDb.Add("91", new OpCodeData { Name = "ldelem.u1", OperandCount = 0 });
            OpCodeDb.Add("92", new OpCodeData { Name = "ldelem.i2", OperandCount = 0 });
            OpCodeDb.Add("93", new OpCodeData { Name = "ldelem.u2", OperandCount = 0 });
            OpCodeDb.Add("94", new OpCodeData { Name = "ldelem.i4", OperandCount = 0 });
            OpCodeDb.Add("95", new OpCodeData { Name = "ldelem.u4", OperandCount = 0 });
            OpCodeDb.Add("96", new OpCodeData { Name = "ldelem.i8", OperandCount = 0 });
            OpCodeDb.Add("97", new OpCodeData { Name = "ldelem.i", OperandCount = 0 });
            OpCodeDb.Add("98", new OpCodeData { Name = "ldelem.r4", OperandCount = 0 });
            OpCodeDb.Add("99", new OpCodeData { Name = "ldelem.r8", OperandCount = 0 });
            OpCodeDb.Add("9A", new OpCodeData { Name = "ldelem.ref", OperandCount = 0 });
            OpCodeDb.Add("9B", new OpCodeData { Name = "stelem.i", OperandCount = 0 });
            OpCodeDb.Add("9C", new OpCodeData { Name = "stelem.i1", OperandCount = 0 });
            OpCodeDb.Add("9D", new OpCodeData { Name = "stelem.i2", OperandCount = 0 });
            OpCodeDb.Add("9E", new OpCodeData { Name = "stelem.i4", OperandCount = 0 });
            OpCodeDb.Add("9F", new OpCodeData { Name = "stelem.i8", OperandCount = 0 });
            OpCodeDb.Add("A0", new OpCodeData { Name = "stelem.r4", OperandCount = 0 });
            OpCodeDb.Add("A1", new OpCodeData { Name = "stelem.r8", OperandCount = 0 });
            OpCodeDb.Add("A2", new OpCodeData { Name = "stelem.ref", OperandCount = 0 });
            OpCodeDb.Add("A3", new OpCodeData { Name = "ldelem", OperandCount = 4 });
            OpCodeDb.Add("A4", new OpCodeData { Name = "stelem", OperandCount = 4 });
            OpCodeDb.Add("A5", new OpCodeData { Name = "unbox.any", OperandCount = 4 });
            OpCodeDb.Add("B3", new OpCodeData { Name = "conv.ovf.i1", OperandCount = 0 });
            OpCodeDb.Add("B4", new OpCodeData { Name = "conv.ovf.u1", OperandCount = 0 });
            OpCodeDb.Add("B5", new OpCodeData { Name = "conv.ovf.i2", OperandCount = 0 });
            OpCodeDb.Add("B6", new OpCodeData { Name = "conv.ovf.u2", OperandCount = 0 });
            OpCodeDb.Add("B7", new OpCodeData { Name = "conv.ovf.i4", OperandCount = 0 });
            OpCodeDb.Add("B8", new OpCodeData { Name = "conv.ovf.u4", OperandCount = 0 });
            OpCodeDb.Add("B9", new OpCodeData { Name = "conv.ovf.i8", OperandCount = 0 });
            OpCodeDb.Add("BA", new OpCodeData { Name = "conv.ovf.u8", OperandCount = 0 });
            OpCodeDb.Add("C2", new OpCodeData { Name = "refanyval", OperandCount = 4 });
            OpCodeDb.Add("C3", new OpCodeData { Name = "ckfinite", OperandCount = 0 });
            OpCodeDb.Add("C6", new OpCodeData { Name = "mkrefany", OperandCount = 4 });
            OpCodeDb.Add("D0", new OpCodeData { Name = "ldtoken", OperandCount = 4 });
            OpCodeDb.Add("D1", new OpCodeData { Name = "conv.u2", OperandCount = 0 });
            OpCodeDb.Add("D2", new OpCodeData { Name = "conv.u1", OperandCount = 0 });
            OpCodeDb.Add("D3", new OpCodeData { Name = "conv.i", OperandCount = 0 });
            OpCodeDb.Add("D4", new OpCodeData { Name = "conv.ovf.i", OperandCount = 0 });
            OpCodeDb.Add("D5", new OpCodeData { Name = "conv.ovf.u", OperandCount = 0 });
            OpCodeDb.Add("D6", new OpCodeData { Name = "add.ovf", OperandCount = 0 });
            OpCodeDb.Add("D7", new OpCodeData { Name = "add.ovf.un", OperandCount = 0 });
            OpCodeDb.Add("D8", new OpCodeData { Name = "mul.ovf", OperandCount = 0 });
            OpCodeDb.Add("D9", new OpCodeData { Name = "mul.ovf.un", OperandCount = 0 });
            OpCodeDb.Add("DA", new OpCodeData { Name = "sub.ovf", OperandCount = 0 });
            OpCodeDb.Add("DB", new OpCodeData { Name = "sub.ovf.un", OperandCount = 0 });
            OpCodeDb.Add("DC", new OpCodeData { Name = "endfinally", OperandCount = 0 });
            OpCodeDb.Add("DD", new OpCodeData { Name = "leave", OperandCount = 4, IsBranch = true });
            OpCodeDb.Add("DE", new OpCodeData { Name = "leave.s", OperandCount = 1, IsBranch = true });
            OpCodeDb.Add("DF", new OpCodeData { Name = "stind.i", OperandCount = 0 });
            OpCodeDb.Add("E0", new OpCodeData { Name = "conv.u", OperandCount = 0 });
            OpCodeDb.Add("FE", new OpCodeData { Name = "TwoByteOpcode", IsTwoByteOpCode = true });

            OpCodeDb2.Add("00", new OpCodeData { Name = "arglist", OperandCount = 0 });
            OpCodeDb2.Add("01", new OpCodeData { Name = "ceq", OperandCount = 0 });
            OpCodeDb2.Add("02", new OpCodeData { Name = "cgt", OperandCount = 0 });
            OpCodeDb2.Add("03", new OpCodeData { Name = "cgt.un", OperandCount = 0 });
            OpCodeDb2.Add("04", new OpCodeData { Name = "clt", OperandCount = 0 });
            OpCodeDb2.Add("05", new OpCodeData { Name = "clt.un", OperandCount = 0 });
            OpCodeDb2.Add("16", new OpCodeData { Name = "constrained.", OperandCount = 4 });
            OpCodeDb2.Add("17", new OpCodeData { Name = "cpblk", OperandCount = 0 });
            OpCodeDb2.Add("11", new OpCodeData { Name = "endfilter", OperandCount = 0 });
            OpCodeDb2.Add("18", new OpCodeData { Name = "initblk", OperandCount = 0 });
            OpCodeDb2.Add("15", new OpCodeData { Name = "Initobj", OperandCount = 4 });
            OpCodeDb2.Add("09", new OpCodeData { Name = "ldarg", OperandCount = 2 });
            OpCodeDb2.Add("0A", new OpCodeData { Name = "ldarga", OperandCount = 2 });
            OpCodeDb2.Add("06", new OpCodeData { Name = "ldftn", OperandCount = 4 });
            OpCodeDb2.Add("0C", new OpCodeData { Name = "ldloc", OperandCount = 2 });
            OpCodeDb2.Add("0D", new OpCodeData { Name = "ldloca", OperandCount = 2 });
            OpCodeDb2.Add("07", new OpCodeData { Name = "ldvirtftn", OperandCount = 4 });
            OpCodeDb2.Add("0F", new OpCodeData { Name = "localloc", OperandCount = 0 });
            OpCodeDb2.Add("19", new OpCodeData { Name = "no.", OperandCount = 1 });
            OpCodeDb2.Add("1E", new OpCodeData { Name = "readonly.", OperandCount = 0 });
            OpCodeDb2.Add("1D", new OpCodeData { Name = "Refanytype", OperandCount = 0 });
            OpCodeDb2.Add("1A", new OpCodeData { Name = "rethrow", OperandCount = 0 });
            OpCodeDb2.Add("1C", new OpCodeData { Name = "sizeof", OperandCount = 4 });
            OpCodeDb2.Add("0B", new OpCodeData { Name = "starg", OperandCount = 2 });
            OpCodeDb2.Add("0E", new OpCodeData { Name = "stloc", OperandCount = 2 });
            OpCodeDb2.Add("14", new OpCodeData { Name = "tail.", OperandCount = 0 });
            OpCodeDb2.Add("12", new OpCodeData { Name = "unaligned.", OperandCount = 1 });
            OpCodeDb2.Add("13", new OpCodeData { Name = "volatile.", OperandCount = 0 });

        }
    }
}
