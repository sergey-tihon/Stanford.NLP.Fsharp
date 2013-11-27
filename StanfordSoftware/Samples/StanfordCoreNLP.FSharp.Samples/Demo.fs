#if INTERACTIVE
#I @"..\packages\IKVM.7.3.4830.0\lib"
#I @"..\packages\Stanford.NLP.CoreNLP.3.2.0.1\lib"
#r @"..\packages\NUnit.2.6.3\lib\nunit.framework.dll"
#r "stanford-corenlp-3.2.0.dll"
#r "IKVM.Runtime.dll"
#r "IKVM.OpenJDK.Core.dll"
#endif

module CoreNLPTests

open NUnit.Framework
open java.util
open java.io
open edu.stanford.nlp.ling
open edu.stanford.nlp.pipeline
open edu.stanford.nlp.util
open edu.stanford.nlp.io
open edu.stanford.nlp.trees
open edu.stanford.nlp.semgraph
open Config


let customAnnotationPrint (annotation:Annotation) = 
    printfn "-------------"
    printfn "Custom print:"
    printfn "-------------"
    let sentences = annotation.get(CoreAnnotations.SentencesAnnotation().getClass()) :?> java.util.ArrayList
    for sentence in sentences |> Seq.cast<CoreMap> do
        printfn "\n\nSentence : '%O'" sentence

        let tokens = sentence.get(CoreAnnotations.TokensAnnotation().getClass()) :?> java.util.ArrayList
        for token in (tokens |> Seq.cast<CoreLabel>) do
            let word = token.get(CoreAnnotations.TextAnnotation().getClass())
            let pos  = token.get(CoreAnnotations.PartOfSpeechAnnotation().getClass())
            let ner  = token.get(CoreAnnotations.NamedEntityTagAnnotation().getClass())
            printfn "%O \t[pos=%O; ner=%O]" word pos ner

        printfn "\nTree:"
        let tree = sentence.get(TreeCoreAnnotations.TreeAnnotation().getClass()) :?> Tree
        use stream = new ByteArrayOutputStream()
        tree.pennPrint(new PrintWriter(stream))
        printfn "The first sentence parsed is:\n %O" (stream.toString())

        printfn "\nDependencies:"
        let deps = sentence.get(SemanticGraphCoreAnnotations.CollapsedDependenciesAnnotation().getClass()) :?> SemanticGraph
        for edge in deps.edgeListSorted().toArray() |> Seq.cast<SemanticGraphEdge> do
            let gov = edge.getGovernor()
            let dep = edge.getDependent()
            printfn "%O(%s-%d,%s-%d)" 
                (edge.getRelation())
                (gov.word()) (gov.index())  
                (dep.word()) (dep.index())


let [<Test>] ``StanfordCoreNlpDemo.java that change current directory`` () =
    let text = "Kosgi Santosh sent an email to Stanford University. He didn't get a reply.";

    // Annotation pipeline configuration
    let props = Properties()
    props.setProperty("annotators","tokenize, ssplit, pos, lemma, ner, parse, dcoref") |> ignore
    props.setProperty("sutime.binders","0") |> ignore

    // we should change current directory so StanfordCoreNLP could find all the model files
    let curDir = System.Environment.CurrentDirectory
    System.IO.Directory.SetCurrentDirectory(jarRoot)
    let pipeline = StanfordCoreNLP(props)
    System.IO.Directory.SetCurrentDirectory(curDir)

    // Annotation
    let annotation = Annotation(text)
    pipeline.annotate(annotation)
    
    // Result - Pretty Print
    use stream = new ByteArrayOutputStream()
    pipeline.prettyPrint(annotation, new PrintWriter(stream))
    printfn "%O" (stream.toString())

    customAnnotationPrint annotation

/// Constants/Keys - https://github.com/stanfordnlp/CoreNLP/blob/1d5d8914500e1110f5c6577a70e49897ccb0d084/src/edu/stanford/nlp/dcoref/Constants.java
/// DefaultPaths/Values - https://github.com/stanfordnlp/CoreNLP/blob/master/src/edu/stanford/nlp/pipeline/DefaultPaths.java
/// Dictionaries/Matching - https://github.com/stanfordnlp/CoreNLP/blob/8f70e42dcd39e40685fc788c3f22384779398d63/src/edu/stanford/nlp/dcoref/Dictionaries.java
let [<Test>] ``StanfordCoreNlpDemo.java with manual configuration`` () =
    let text = "Kosgi Santosh sent an email to Stanford University. He didn't get a reply.";

    // Annotation pipeline configuration
    let props = Properties()
    let (<==) key value = props.setProperty(key, value) |> ignore
    "annotators"    <== "tokenize, ssplit, pos, lemma, ner, parse, dcoref"
    "pos.model"     <== ! @"pos-tagger\english-bidirectional\english-bidirectional-distsim.tagger"
    "ner.model"     <== ! @"ner\english.all.3class.distsim.crf.ser.gz"
    "parse.model"   <== ! @"lexparser\englishPCFG.ser.gz"
    
    "dcoref.demonym"            <== ! @"dcoref\demonyms.txt"
    "dcoref.states"             <== ! @"dcoref\state-abbreviations.txt"
    "dcoref.animate"            <== ! @"dcoref\animate.unigrams.txt"
    "dcoref.inanimate"          <== ! @"dcoref\inanimate.unigrams.txt"
    "dcoref.male"               <== ! @"dcoref\male.unigrams.txt"
    "dcoref.neutral"            <== ! @"dcoref\neutral.unigrams.txt"
    "dcoref.female"             <== ! @"dcoref\female.unigrams.txt"
    "dcoref.plural"             <== ! @"dcoref\plural.unigrams.txt"
    "dcoref.singular"           <== ! @"dcoref\singular.unigrams.txt"
    "dcoref.countries"          <== ! @"dcoref\countries"
    "dcoref.extra.gender"       <== ! @"dcoref\namegender.combine.txt"
    "dcoref.states.provinces"   <== ! @"dcoref\statesandprovinces"
    "dcoref.singleton.predictor"<== ! @"dcoref\singleton.predictor.ser"
    "dcoref.big.gender.number"  <== ! @"dcoref\gender.data.gz"
    
    let sutimeRules = 
        [| ! @"sutime\defs.sutime.txt";
           ! @"sutime\english.holidays.sutime.txt";
           ! @"sutime\english.sutime.txt" |]
        |> String.concat ","
    "sutime.rules"      <== sutimeRules
    "sutime.binders"    <== "0"

    let pipeline = StanfordCoreNLP(props)

    // Annotation
    let annotation = Annotation(text)
    pipeline.annotate(annotation)
    
    // Result - Pretty Print
    use stream = new ByteArrayOutputStream()
    pipeline.prettyPrint(annotation, new PrintWriter(stream))
    printfn "%O" (stream.toString())

    customAnnotationPrint annotation