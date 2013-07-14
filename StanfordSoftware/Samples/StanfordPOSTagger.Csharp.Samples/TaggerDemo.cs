using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using java.io;
using java.util;

using edu.stanford.nlp.ling;
using edu.stanford.nlp.tagger.maxent;

namespace StanfordPOSTagger.Csharp.Samples
{
    public static class TaggerDemo
    {
        public const string Model =
            @"..\..\..\..\temp\stanford-postagger-2013-06-20\models\wsj-0-18-bidirectional-nodistsim.tagger";

        private static void TagReader(Reader reader)
        {
            var tagger = new MaxentTagger(Model);
            foreach (List sentence in MaxentTagger.tokenizeText(reader).toArray())
            {
                var tSentence = tagger.tagSentence(sentence);
                System.Console.WriteLine(Sentence.listToString(tSentence, false));
            }
        }

        public static void TagFile (string fileName)
        {
            TagReader(new BufferedReader(new FileReader(fileName)));
        }

        public static void TagText(string text)
        {
            TagReader(new StringReader(text));
        }
    }
}
