module ParserDemo2

open java.io
open java.util

open edu.stanford.nlp.ling
open edu.stanford.nlp.``process``
open edu.stanford.nlp.trees
open edu.stanford.nlp.parser.lexparser

// Usage: ParserDemo2 [[grammar] textFile] 
let main (model:string option) (fileName:string option) = 
    let grammar = 
        match model with
        | Some(model) -> model
        | None -> @"..\..\..\..\temp\stanford-parser-full-2013-06-20\edu\stanford\nlp\models\lexparser\englishPCFG.ser.gz"
    let options =[|"-maxLength"; "80"; "-retainTmpSubcategories"|]
    let lp = LexicalizedParser.loadModel(grammar, options);
    let tlp = PennTreebankLanguagePack();
    let gsf = tlp.grammaticalStructureFactory();

    let sentences = 
        match fileName with
        | Some(file) -> 
            DocumentPreprocessor(file)
            |> Iterable.toSeq
            |> Seq.cast<ArrayList>
        | None ->
            // Showing tokenization and parsing in code a couple of different ways.
            seq {
                let sent = [| "This"; "is"; "an"; "easy"; "sentence"; "." |] 
                yield (sent |> Seq.map (fun w-> Word(w)) |> Iterable.fromSeq) :?> ArrayList

                let sent2 ="This is a slightly longer and more complex sentence requiring tokenization."
                let toke = tlp.getTokenizerFactory().getTokenizer(new StringReader(sent2));
                yield toke.tokenize() :?> ArrayList
            }

    for sentence in sentences do
        let parse = lp.apply(sentence)
        parse.pennPrint()
        printfn "\n%O\n" (parse.taggedYield())
        
        let gs = gsf.newGrammaticalStructure(parse)
        let tdl = gs.typedDependenciesCCprocessed(true)
        printfn "%O\n" tdl
