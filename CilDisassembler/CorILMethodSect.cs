namespace CilDisassembler
{
    public class CorILMethodSect
    {
        public const byte CorILMethodSectReserved = 0;
        public const byte CorILMethodSectEHTable = 1;
        public const byte CorILMethodSectOptILTable = 2;
        public const byte CorILMethodSectKindMask = 0x3F;
        public const byte CorILMethodSectFatFormat = 0x40;
        public const byte CorILMethodSectMoreSects = 0x80;
    }
}
