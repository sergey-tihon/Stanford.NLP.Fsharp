using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StanfordParser.Csharp.Samples
{
    class Program
    {
        public static string ParserModel = @"..\..\..\..\temp\stanford-parser-full-2013-06-20\edu\stanford\nlp\models\lexparser\englishPCFG.ser.gz";

        static void Main(string[] args)
        {
            if (args.Length >= 1)
            {
                switch (args[0])
                {
                    case "1":
                        var fileName = (args.Length > 1) ? args[1] : "";
                        ParserDemo.Start(fileName);
                        break;
                    default:
                        throw new ArgumentException("Unexpected args[0] value");
                }
            }
            else
            {
                throw new ArgumentException("args expected");
            }
        }
    }
}
