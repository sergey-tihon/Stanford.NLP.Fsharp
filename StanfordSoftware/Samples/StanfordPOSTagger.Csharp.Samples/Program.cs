using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StanfordPOSTagger.Csharp.Samples
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 2)
                throw new ArgumentException("Not enough arguments");
            switch (args[0])
            {
                case "1":
                    if (args.Length < 3)
                        throw new ArgumentException("Not enough arguments");
                    switch (args[1])
                    {
                        case "file":
                            TaggerDemo.TagFile(args[2]);
                            break;
                        case "text":
                            TaggerDemo.TagText(args[2]);
                            break;
                        default:
                            throw new Exception("Invalid Model 1 mode");
                    }
                    break;
                case "2":
                    TaggerDemo2.Execute(args[1]);
                    break;
                default:
                    throw new Exception("Invalid mode");
            }
        }
    }
}
