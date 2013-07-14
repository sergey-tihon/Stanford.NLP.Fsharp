module TaggerDemo

open java.io
open java.util

open edu.stanford.nlp.ling
open edu.stanford.nlp.tagger.maxent;

let model = @"..\..\..\..\temp\stanford-postagger-2013-06-20\models\wsj-0-18-bidirectional-nodistsim.tagger"

let tagReader (reader:Reader) = 
    let tagger = MaxentTagger(model)
    MaxentTagger.tokenizeText(reader)
    |> Iterable.toSeq
    |> Seq.iter (fun sentence ->
            let tSentence = tagger.tagSentence(sentence :?> List)
            printfn "%O" (Sentence.listToString(tSentence, false))
        )

let tagFile (fileName:string) = 
    tagReader (new BufferedReader(new FileReader(fileName)))

let tagText (text:string) =
    tagReader (new StringReader(text))