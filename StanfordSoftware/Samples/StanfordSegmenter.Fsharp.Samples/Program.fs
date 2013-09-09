open java.util
open edu.stanford.nlp.ie.crf

//  This is a very simple demo of calling the Chinese Word Segmenter
//  programmatically.  It assumes an input file in UTF8.
//  <p/>
//  <code>
//  Usage: StanfordSegmenter.Csharp.Samples.exe filename
//  </code>
//  This will run correctly in the distribution home directory.  To
//  run in general, the properties for where to find dictionaries or
//  normalizations have to be set.
//
//  @author Christopher Manning
[<EntryPoint>]
let main argv = 
    if (argv.Length <> 1) then
        printf "usage: StanfordSegmenter.Csharp.Samples.exe filename"
    else
        let props = Properties();
        props.setProperty("sighanCorporaDict", @"..\..\..\..\temp\stanford-segmenter-2013-06-20\data") |> ignore
        // props.setProperty("NormalizationTable", @"..\..\..\..\temp\stanford-segmenter-2013-06-20\data\norm.simp.utf8") |> ignore
        // props.setProperty("normTableEncoding", "UTF-8") |> ignore
        // below is needed because CTBSegDocumentIteratorFactory accesses it
        props.setProperty("serDictionary", @"..\..\..\..\temp\stanford-segmenter-2013-06-20\data\dict-chris6.ser.gz") |> ignore
        props.setProperty("testFile", argv.[0]) |> ignore
        props.setProperty("inputEncoding", "UTF-8") |> ignore
        props.setProperty("sighanPostProcessing", "true") |> ignore

        let segmenter = CRFClassifier(props)
        segmenter.loadClassifierNoExceptions(@"..\..\..\..\temp\stanford-segmenter-2013-06-20\data\ctb.gz", props)
        segmenter.classifyAndWriteAnswers(argv.[0])
    0
