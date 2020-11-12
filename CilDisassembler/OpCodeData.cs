namespace CilDisassembler
{
    internal class OpCodeData
    {
        public string Name { get; set; }
        public int OperandCount { get; set; }

        public bool IsTwoByteOpCode { get; set; }

        public bool HasArgList { get; set; }

        public bool IsBranch { get; set; }
    }
}