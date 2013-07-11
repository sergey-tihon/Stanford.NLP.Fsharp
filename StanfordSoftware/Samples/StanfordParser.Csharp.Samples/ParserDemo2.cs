using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using edu.stanford.nlp.ling;
using edu.stanford.nlp.parser.lexparser;
using edu.stanford.nlp.process;
using edu.stanford.nlp.trees;

using java.io;
using java.util;

namespace StanfordParser.Csharp.Samples
{
    public static class ParserDemo2
    {
        public static void Start(string model, string fileName)
        {
            var grammar = (!String.IsNullOrEmpty(model)) ? model : Program.ParserModel;
            var options = new[] { "-maxLength", "80", "-retainTmpSubcategories" };
            var lp = LexicalizedParser.loadModel(grammar, options);
            var tlp = new PennTreebankLanguagePack();
            var gsf = tlp.grammaticalStructureFactory();

            var sentences = new List<ArrayList>();
            if (!string.IsNullOrEmpty(fileName))
            {
                sentences.AddRange(new DocumentPreprocessor(fileName).Cast<ArrayList>());
            }
            else
            {
                var sent = new[] { "This", "is", "an", "easy", "sentence", "." };
                var arrList = new ArrayList();
                foreach (var s in sent)
                {
                    arrList.Add(new Word(s));
                }
                sentences.Add(arrList);

                const string Sent2 = "This is a slightly longer and more complex sentence requiring tokenization.";
                var toke = tlp.getTokenizerFactory().getTokenizer(new StringReader(Sent2));
                sentences.Add((ArrayList)toke.tokenize());
            }

            foreach (var sentence in sentences)
            {
                var parse = lp.apply(sentence);
                parse.pennPrint();
                System.Console.WriteLine("\n{0}\n", (parse.taggedYield()));

                var gs = gsf.newGrammaticalStructure(parse);
                var tdl = gs.typedDependenciesCCprocessed(true);
                System.Console.WriteLine("{0}\n", tdl);
            }
       
        }
    }
}
