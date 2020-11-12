using System.Runtime.ExceptionServices;

namespace CilDisassembler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class FormatHelper
    {
        public static string Lineify(string byteString)
        {
            var purged = PurgeSeparators(byteString);
            var sb = new StringBuilder();
            for (int i = 0; i < purged.Length; i += 2)
            {
                sb.Append(purged.Substring(i, 2)).Append("\r\n");
            }

            return sb.ToString();
        }

        public static string PurgeSeparators(string rawBytes)
        {
            var retVal = rawBytes.ToUpper()
                .Replace(" ", "")
                .Replace(",", "")
                .Replace("0X", "")
                .Replace("\r", "")
                .Replace("\n", "")
                .Replace("\t", "");

            return retVal;
        }
    }
}
