using java.util;
using edu.stanford.nlp.ie.crf;

namespace StanfordSegmenter.Csharp.Samples
{
    /** This is a very simple demo of calling the Chinese Word Segmenter
     *  programmatically.  It assumes an input file in UTF8.
     *  <p/>
     *  <code>
     *  Usage: StanfordSegmenter.Csharp.Samples.exe filename
     *  </code>
     *  This will run correctly in the distribution home directory.  To
     *  run in general, the properties for where to find dictionaries or
     *  normalizations have to be set.
     *
     *  @author Christopher Manning
     */
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                System.Console.WriteLine("usage: StanfordSegmenter.Csharp.Samples.exe filename");
                return;
            }

            var props = new Properties();
            props.setProperty("sighanCorporaDict", @"..\..\..\..\temp\stanford-segmenter-2013-06-20\data");
            // props.setProperty("NormalizationTable", @"..\..\..\..\temp\stanford-segmenter-2013-06-20\data\norm.simp.utf8");
            // props.setProperty("normTableEncoding", "UTF-8");
            // below is needed because CTBSegDocumentIteratorFactory accesses it
            props.setProperty("serDictionary", @"..\..\..\..\temp\stanford-segmenter-2013-06-20\data\dict-chris6.ser.gz");
            props.setProperty("testFile", args[0]);
            props.setProperty("inputEncoding", "UTF-8");
            props.setProperty("sighanPostProcessing", "true");

            var segmenter = new CRFClassifier(props);
            segmenter.loadClassifierNoExceptions(@"..\..\..\..\temp\stanford-segmenter-2013-06-20\data\ctb.gz", props);
            segmenter.classifyAndWriteAnswers(args[0]);
        }
    }
}
