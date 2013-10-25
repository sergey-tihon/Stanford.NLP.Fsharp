using System;

using java.lang;
using java.util;
using edu.stanford.nlp.tagger.maxent;
using edu.stanford.nlp.ling;
using edu.stanford.nlp.pipeline;
using edu.stanford.nlp.time;
using edu.stanford.nlp.util;

using NUnit.Framework;

using String = System.String;

namespace StanfordCoreNLP.CSharp.Samples
{
    [TestFixture]
    public class SUTimeTests
    {
        /// http://nlp.stanford.edu/software/sutime.shtml#Usage
        [Test]
        public void SUTimeDefautTest()
        {
            var pipeline = new AnnotationPipeline();
            pipeline.addAnnotator(new PTBTokenizerAnnotator(false));
            pipeline.addAnnotator(new WordsToSentencesAnnotator(false));

            var tagger =
                new MaxentTagger(
                    Config.GetModel(@"pos-tagger\english-bidirectional\english-bidirectional-distsim.tagger"));
            pipeline.addAnnotator(new POSTaggerAnnotator(tagger));

            var sutimeRules = new[] {
                                      Config.GetModel(@"sutime\defs.sutime.txt"),
                                      Config.GetModel(@"sutime\english.holidays.sutime.txt"),
                                      Config.GetModel(@"sutime\english.sutime.txt")
                                  };

            var props = new Properties();
            props.setProperty("sutime.rules", String.Join(",", sutimeRules));
            props.setProperty("sutime.binders", "0");
            pipeline.addAnnotator(new TimeAnnotator("sutime", props));

            const string text = "Three interesting dates are 18 Feb 1997, the 20th of july and 4 days from today.";
            var annotation = new Annotation(text);
            annotation.set(new CoreAnnotations.DocDateAnnotation().getClass(), "2013-07-14");
            pipeline.annotate(annotation);

            Console.WriteLine(annotation.get(new CoreAnnotations.TextAnnotation().getClass())+"\n");
            var timexAnnsAll = (ArrayList)annotation.get(new TimeAnnotations.TimexAnnotations().getClass());
            foreach (CoreMap cm in timexAnnsAll)
            {
                var tokens = (java.util.List)cm.get(new CoreAnnotations.TokensAnnotation().getClass());
                var first = tokens.get(0);
                var last = tokens.get(tokens.size() - 1);
                var time = (TimeExpression)cm.get(new TimeExpression.Annotation().getClass());
                Console.WriteLine("{0} [from char offset '{1}' to '{2}'] --> {3}",
                    cm, first, last, (time.getTemporal()));
            }
        }
    }
}
