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
    public static class ParserDemo
    {
        public static void DemoDP(LexicalizedParser lp, string fileName)
        {
            // This option shows loading and sentence-segment and tokenizing
            // a file using DocumentPreprocessor
            var tlp = new PennTreebankLanguagePack();
            var gsf = tlp.grammaticalStructureFactory();
            // You could also create a tokenizer here (as below) and pass it
            // to DocumentPreprocessor
            foreach (List sentence in new DocumentPreprocessor(fileName))
            {
                var parse = lp.apply(sentence);
                parse.pennPrint();

                var gs = gsf.newGrammaticalStructure(parse);
                var tdl = gs.typedDependenciesCCprocessed(true);
                System.Console.WriteLine("\n{0}\n", tdl);
            }
        }

        public static void DemoAPI(LexicalizedParser lp)
        {
            // This option shows parsing a list of correctly tokenized words
            var sent = new[] { "This", "is", "an", "easy", "sentence", "." };
            var rawWords = Sentence.toCoreLabelList(sent);
            var parse = lp.apply(rawWords);
            parse.pennPrint();

            // This option shows loading and using an explicit tokenizer
            const string Sent2 = "This is another sentence.";
            var tokenizerFactory = PTBTokenizer.factory(new CoreLabelTokenFactory(), "");
            var sent2Reader = new StringReader(Sent2);
            var rawWords2 = tokenizerFactory.getTokenizer(sent2Reader).tokenize();
            parse = lp.apply(rawWords2);

            var tlp = new PennTreebankLanguagePack();
            var gsf = tlp.grammaticalStructureFactory();
            var gs = gsf.newGrammaticalStructure(parse);
            var tdl = gs.typedDependenciesCCprocessed();
            System.Console.WriteLine("\n{0}\n", tdl);

            var tp = new TreePrint("penn,typedDependenciesCollapsed");
            tp.printTree(parse);
        }

        public static void Start(string fileName)
        {
            var lp =LexicalizedParser.loadModel(Program.ParserModel);
            if (!String.IsNullOrEmpty(fileName))
            {
                DemoDP(lp, fileName);
            }
            else
            {
                DemoAPI(lp);
            }
        }
    }
}
