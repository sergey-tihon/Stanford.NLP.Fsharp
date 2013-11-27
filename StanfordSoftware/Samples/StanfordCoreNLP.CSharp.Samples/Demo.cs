using System;
using System.IO;

using NUnit.Framework;

using edu.stanford.nlp.ling;
using edu.stanford.nlp.pipeline;
using edu.stanford.nlp.semgraph;
using edu.stanford.nlp.trees;
using edu.stanford.nlp.util;

using java.io;
using java.util;

using Console = System.Console;

namespace StanfordCoreNLP.CSharp.Samples
{
    [TestFixture]
    public class Demo
    {
        public void CustomAnnotationPrint(Annotation annotation)
        {
            Console.WriteLine("-------------");
            Console.WriteLine("Custom print:");
            Console.WriteLine("-------------");
            var sentences = (ArrayList)annotation.get(new CoreAnnotations.SentencesAnnotation().getClass());
            foreach(CoreMap sentence in sentences)
            {
                Console.WriteLine("\n\nSentence : '{0}'", sentence);

                var tokens = (ArrayList)sentence.get(new CoreAnnotations.TokensAnnotation().getClass());
                foreach (CoreLabel token in tokens)
                {
                    var word = token.get(new CoreAnnotations.TextAnnotation().getClass());
                    var pos  = token.get(new CoreAnnotations.PartOfSpeechAnnotation().getClass());
                    var ner  = token.get(new CoreAnnotations.NamedEntityTagAnnotation().getClass());
                    Console.WriteLine("{0} \t[pos={1}; ner={2}]", word, pos, ner);
                }

                Console.WriteLine("\nTree:");
                var tree = (Tree)sentence.get(new TreeCoreAnnotations.TreeAnnotation().getClass());
                using(var stream = new ByteArrayOutputStream())
                {
                    tree.pennPrint(new PrintWriter(stream));
                    Console.WriteLine("The first sentence parsed is:\n {0}", stream.toString());
                }

                Console.WriteLine("\nDependencies:");
                var deps = (SemanticGraph)sentence.get(new SemanticGraphCoreAnnotations.CollapsedDependenciesAnnotation().getClass());
                foreach (SemanticGraphEdge edge in deps.edgeListSorted().toArray())
                {
                    var gov = edge.getGovernor();
                    var dep = edge.getDependent();
                    Console.WriteLine(
                        "{0}({1}-{2},{3}-{4})", edge.getRelation(), 
                        gov.word(), gov.index(), dep.word(), dep.index());
                }
            }
        }

        [Test] 
        public void StanfordCoreNlpDemoThatChangeCurrentDirectory()
        {
            const string Text = "Kosgi Santosh sent an email to Stanford University. He didn't get a reply.";

            // Annotation pipeline configuration
            var props = new Properties();
            props.setProperty("annotators", "tokenize, ssplit, pos, lemma, ner, parse, dcoref");
            props.setProperty("sutime.binders", "0");

            // we should change current directory so StanfordCoreNLP could find all the model files
            var curDir = Environment.CurrentDirectory;
            Directory.SetCurrentDirectory(Config.JarRoot);
            var pipeline = new edu.stanford.nlp.pipeline.StanfordCoreNLP(props);
            Directory.SetCurrentDirectory(curDir);

            // Annotation
            var annotation = new Annotation(Text);
            pipeline.annotate(annotation);
    
            // Result - Pretty Print
            using (var stream = new ByteArrayOutputStream())
            {
                pipeline.prettyPrint(annotation, new PrintWriter(stream));
                Console.WriteLine(stream.toString());
            }

            this.CustomAnnotationPrint(annotation);
        }

        [Test]
        public void StanfordCoreNlpDemoManualConfiguration()
        {
            Console.WriteLine(Environment.CurrentDirectory);
            const string Text = "Kosgi Santosh sent an email to Stanford University. He didn't get a reply.";

            // Annotation pipeline configuration
            var props = new Properties();

            props.setProperty("annotators", "tokenize, ssplit, pos, lemma, ner, parse, dcoref");
            props.setProperty("pos.model", Config.GetModel(@"pos-tagger\english-bidirectional\english-bidirectional-distsim.tagger"));
            props.setProperty("ner.model", Config.GetModel(@"ner\english.all.3class.distsim.crf.ser.gz"));
            props.setProperty("parse.model", Config.GetModel(@"lexparser\englishPCFG.ser.gz"));
    
            props.setProperty("dcoref.demonym", Config.GetModel(@"dcoref\demonyms.txt"));
            props.setProperty("dcoref.states", Config.GetModel(@"dcoref\state-abbreviations.txt"));
            props.setProperty("dcoref.animate", Config.GetModel(@"dcoref\animate.unigrams.txt"));
            props.setProperty("dcoref.inanimate", Config.GetModel(@"dcoref\inanimate.unigrams.txt"));
            props.setProperty("dcoref.male", Config.GetModel(@"dcoref\male.unigrams.txt"));
            props.setProperty("dcoref.neutral", Config.GetModel(@"dcoref\neutral.unigrams.txt"));
            props.setProperty("dcoref.female", Config.GetModel(@"dcoref\female.unigrams.txt"));
            props.setProperty("dcoref.plural", Config.GetModel(@"dcoref\plural.unigrams.txt"));
            props.setProperty("dcoref.singular", Config.GetModel(@"dcoref\singular.unigrams.txt"));
            props.setProperty("dcoref.countries", Config.GetModel(@"dcoref\countries"));
            props.setProperty("dcoref.extra.gender", Config.GetModel(@"dcoref\namegender.combine.txt"));
            props.setProperty("dcoref.states.provinces", Config.GetModel(@"dcoref\statesandprovinces"));
            props.setProperty("dcoref.singleton.predictor", Config.GetModel(@"dcoref\singleton.predictor.ser"));
            props.setProperty("dcoref.big.gender.number", Config.GetModel(@"dcoref\gender.data.gz"));

            var sutimeRules = new[] {
                                      Config.GetModel(@"sutime\defs.sutime.txt"),
                                      Config.GetModel(@"sutime\english.holidays.sutime.txt"),
                                      Config.GetModel(@"sutime\english.sutime.txt")
                                  };
            props.setProperty("sutime.rules", String.Join(",", sutimeRules));
            props.setProperty("sutime.binders", "0");

            var pipeline = new edu.stanford.nlp.pipeline.StanfordCoreNLP(props);

            // Annotation
            var annotation = new Annotation(Text);
            pipeline.annotate(annotation);
    
            // Result - Pretty Print
            using (var stream = new ByteArrayOutputStream())
            {
                pipeline.prettyPrint(annotation, new PrintWriter(stream));
                Console.WriteLine(stream.toString());
            }

            this.CustomAnnotationPrint(annotation);
        }
    }
}
