module NERDemo

open edu.stanford.nlp.ie
open edu.stanford.nlp.ie.crf
open edu.stanford.nlp.io
open edu.stanford.nlp.ling

open java.util
open System.IO

let main file =
    let classifier = 
        CRFClassifier.getClassifierNoExceptions(
            @"..\..\..\..\temp\stanford-ner-2013-11-12\classifiers\english.all.3class.distsim.crf.ser.gz")
    // For either a file to annotate or for the hardcoded text example,
    // this demo file shows two ways to process the output, for teaching
    // purposes.  For the file, it shows both how to run NER on a String
    // and how to run it on a whole file.  For the hard-coded String,
    // it shows how to run it on a single sentence, and how to do this
    // and produce an inline XML output format.
    match file with
    | Some(fileName) ->
        let fileContents = File.ReadAllText(fileName)
        classifier.classify(fileContents)
        |> Iterable.toSeq
        |> Seq.cast<java.util.List>
        |> Seq.iter (fun sentence -> 
            sentence
            |> Iterable.toSeq
            |> Seq.cast<CoreLabel>
            |> Seq.iter (fun word ->
                printf "%s/%O " (word.word()) (word.get(CoreAnnotations.AnswerAnnotation().getClass()))
            )
            printfn ""
        )
    | None -> 
        let s1 = "Good afternoon Rajat Raina, how are you today?"
        let s2 = "I go to school at Stanford University, which is located in California."
        printfn "%s\n" (classifier.classifyToString(s1))
        printfn "%s\n" (classifier.classifyWithInlineXML(s2))
        printfn "%s\n" (classifier.classifyToString(s2, "xml", true));
        classifier.classify(s2)
        |> Iterable.toSeq
        |> Seq.iteri (fun i coreLabel ->
            printfn "%d\n:%O\n" i coreLabel
        )