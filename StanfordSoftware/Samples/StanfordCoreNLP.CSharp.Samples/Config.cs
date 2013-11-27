using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StanfordCoreNLP.CSharp.Samples
{
    public static class Config
    {
        public const string JarRoot =
            @"..\..\..\..\temp\stanford-corenlp-full-2013-11-12\stanford-corenlp-3.3.0-models\";

        public static readonly string ModelsRoot = Path.Combine(JarRoot, @"edu\stanford\nlp\models\");

        public static string GetModel(string relativePath)
        {
            return Path.Combine(ModelsRoot, relativePath);
        }
    }
}
