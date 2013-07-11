module ParserDemo

open java.util
open java.io

open edu.stanford.nlp.objectbank
open edu.stanford.nlp.``process``
open edu.stanford.nlp.ling
open edu.stanford.nlp.trees
open edu.stanford.nlp.parser.lexparser

let demoDP (lp:LexicalizedParser) (fileName:string) =
    // This option shows loading and sentence-segment and tokenizing
    // a file using DocumentPreprocessor
    let tlp = PennTreebankLanguagePack();
    let gsf = tlp.grammaticalStructureFactory();
    // You could also create a tokenizer here (as below) and pass it
    // to DocumentPreprocessor
    DocumentPreprocessor(fileName)
    |> Iterable.toSeq
    |> Seq.cast<List>
    |> Seq.iter (fun sentence -> 
        let parse = lp.apply(sentence);
        parse.pennPrint();

        let gs = gsf.newGrammaticalStructure(parse);
        let tdl = gs.typedDependenciesCCprocessed(true);
        printfn "\n%O\n" tdl
        )

let demoAPI (lp:LexicalizedParser) =
    // This option shows parsing a list of correctly tokenized words
    let sent = [|"This"; "is"; "an"; "easy"; "sentence"; "." |]
    let rawWords = Sentence.toCoreLabelList(sent)
    let parse = lp.apply(rawWords)
    parse.pennPrint()

    // This option shows loading and using an explicit tokenizer
    let sent2 = "This is another sentence."
    let tokenizerFactory = PTBTokenizer.factory(CoreLabelTokenFactory(), "")
    use sent2Reader = new StringReader(sent2)
    let rawWords2 = tokenizerFactory.getTokenizer(sent2Reader).tokenize()
    let parse = lp.apply(rawWords2)

    let tlp = PennTreebankLanguagePack()
    let gsf = tlp.grammaticalStructureFactory()
    let gs = gsf.newGrammaticalStructure(parse)
    let tdl = gs.typedDependenciesCCprocessed()
    printfn "\n%O\n" tdl

    let tp = new TreePrint("penn,typedDependenciesCollapsed")
    tp.printTree(parse)

let main fileName =
    let lp = LexicalizedParser.loadModel(@"..\..\..\..\temp\stanford-parser-full-2013-06-20\edu\stanford\nlp\models\lexparser\englishPCFG.ser.gz")
    match fileName with
    | Some(file) -> demoDP lp file
    | None -> demoAPI lp
