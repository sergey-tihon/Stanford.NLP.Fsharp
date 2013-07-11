using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using edu.stanford.nlp.ie;
using edu.stanford.nlp.ie.crf;
using edu.stanford.nlp.io;
using edu.stanford.nlp.ling;
using java.util;


namespace StanfordNER.Csharp.Samples
{
    class Program
    {
        public static CRFClassifier Classifier =
            CRFClassifier.getClassifierNoExceptions(
                @"..\..\..\..\temp\stanford-ner-2013-06-20\classifiers\english.all.3class.distsim.crf.ser.gz");

        // For either a file to annotate or for the hardcoded text example,
        // this demo file shows two ways to process the output, for teaching
        // purposes.  For the file, it shows both how to run NER on a String
        // and how to run it on a whole file.  For the hard-coded String,
        // it shows how to run it on a single sentence, and how to do this
        // and produce an inline XML output format.

        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                var fileContent = File.ReadAllText(args[0]);
                foreach (List sentence in Classifier.classify(fileContent).toArray())
                {
                    foreach (CoreLabel word in sentence.toArray())
                    {
                        Console.Write( "{0}/{1} ", word.word(), word.get(new CoreAnnotations.AnswerAnnotation().getClass()));
                    }
                    Console.WriteLine();
                }
            } else
            {
                const string S1 = "Good afternoon Rajat Raina, how are you today?";
                const string S2 = "I go to school at Stanford University, which is located in California.";
                Console.WriteLine("{0}\n", Classifier.classifyToString(S1));
                Console.WriteLine("{0}\n", Classifier.classifyWithInlineXML(S2));
                Console.WriteLine("{0}\n", Classifier.classifyToString(S2, "xml", true));

                var classification = Classifier.classify(S2).toArray();

                for (var i = 0; i < classification.Length; i++)
                {
                    Console.WriteLine("{0}\n:{1}\n", i, classification[i]);
                }
            }
        }
    }
}
