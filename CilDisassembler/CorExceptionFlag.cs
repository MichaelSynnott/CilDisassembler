namespace CilDisassembler
{
    public class CorExceptionFlag
    {
        public const byte CorILExceptionClauseNone = 0;
        public const byte CorILExceptionClauseOffsetLen = 0;
        public const byte CorILExceptionClauseDeprecated = 0;
        public const byte CorILExceptionClauseFilter = 1;
        public const byte CorILExceptionClauseFinally = 2;
        public const byte CorILExceptionClauseFault = 4;
        public const byte CorILExceptionClauseDuplicated = 8;
    }
}
