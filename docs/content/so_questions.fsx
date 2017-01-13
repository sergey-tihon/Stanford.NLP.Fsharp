(*** hide ***)
// This block of code is omitted in the generated HTML documentation. Use
// it to define helpers that you do not want to show in the documentation.
#I "../../bin"

(**
StackOverflow questions understanding
=====================================

Let's start with sample NLP task:
We want to show related questions before user asks a new one (as it works on StackOverflow).

There are many possible solutions for this task. Let's look at one that at the first step
tries to understand key phrases that identify this question and runs the search using them.

Approach
--------

First of all, let's choose some real questions from StackOverflow to analyze them:

* [How to make an F# project work with the object browser](http://stackoverflow.com/questions/13873849/how-to-make-an-f-project-work-with-the-object-browser)

* [How can I build WebSharper on Mono 3.0 on Mac?](http://stackoverflow.com/questions/13045475/how-can-i-build-websharper-on-mono-3-0-on-mac)

* [Adding extra methods as type extensions in F#](http://stackoverflow.com/questions/13413678/adding-extra-methods-as-type-extensions-in-f)

* [How to get MonoDevelop to compile F# projects?](http://stackoverflow.com/questions/6400616/how-to-get-monodevelop-to-compile-f-projects)

Now we can use Stanford Parser GUI to visualize the structure of these questions:
<table>
<tr>
    <td><img src="/Stanford.NLP.Fsharp/img/SO_q1.png" height="300"></td>
    <td><img src="/Stanford.NLP.Fsharp/img/SO_q2.png" height="300"></td>
</tr>
<tr>
    <td><img src="/Stanford.NLP.Fsharp/img/SO_q3.png" height="300"></td>
    <td><img src="/Stanford.NLP.Fsharp/img/SO_q4.png" height="300"></td>
</tr>
</table>

We can notice that all phrases that we have selected are parts of noun phrases(NP). As a first solution we can try to analyze
tags in the tree and select `NP` that contains word level tags like (`NN`,`NNS`,`NNP`,`NNPS`).
*)
#r @"IKVM.Runtime.dll"
#r @"IKVM.OpenJDK.Core.dll"
#r @"ejml-0.23.dll"
#r @"stanford-parser.dll"
#r @"Stanford.NLP.Parser.Fsharp.dll"

open edu.stanford.nlp.parser.lexparser
open edu.stanford.nlp.trees
open java.util

open System
open Stanford.NLP.FSharp.Parser

let model = @"d:\englishPCFG.ser.gz"

let options = [|"-maxLength"; "500";"-retainTmpSubcategories"; "-MAX_ITEMS";
                "500000";"-outputFormat"; "penn,typedDependenciesCollapsed"|]
let parser = LexicalizedParser.loadModel(model, options)

let tlp = PennTreebankLanguagePack()
let gsf = tlp.grammaticalStructureFactory()

let getTree question =
    let tokenizer = tlp.getTokenizerFactory().getTokenizer(new java.io.StringReader(question))
    let sentence = tokenizer.tokenize()
    parser.apply(sentence)

let getKeyPhrases (tree:Tree) =
    let isNNx = function
        | Label NN | Label NNS
        | Label NNP | Label NNPS -> true
        | _ -> false
    let isNPwithNNx = function
        | Label NP as node ->
            node.getChildrenAsList()
            |> Iterable.castToSeq<Tree>
            |> Seq.exists isNNx
        | _ -> false
    let rec foldTree acc (node:Tree) =
        let acc =
            if node.isLeaf() then acc
            else node.getChildrenAsList()
                 |> Iterable.castToSeq<Tree>
                 |> Seq.fold
                    (fun state x -> foldTree state x)
                    acc
        if isNPwithNNx node
          then node :: acc
          else acc
    foldTree [] tree

let questions =
    [|"How to make an F# project work with the object browser"
      "How can I build WebSharper on Mono 3.0 on Mac?"
      "Adding extra methods as type extensions in F#"
      "How to get MonoDevelop to compile F# projects?"|]

questions
|> Seq.iter (fun question ->
    printfn "Question : %s" question
    question
    |> getTree
    |> getKeyPhrases
    |> List.rev
    |> List.iter (fun p ->
        p.getLeaves()
        |> Iterable.castToArray<Tree>
        |> Array.map(fun x-> x.label().value())
        |> printfn "\t%A")
)
(**
If you run this script, you will see the following:
*)
// [fsi: Question : How to make an F# project work with the object browser]
// [fsi: [|"an"; "F"; "#"; "project"; "work"|]]
// [fsi: [|"the"; "object"; "browser"|]]
// [fsi: Question : How can I build WebSharper on Mono 3.0 on Mac?]
// [fsi: [|"WebSharper"|]]
// [fsi: [|"Mono"; "3.0"|]]
// [fsi: [|"Mac"|]]
// [fsi: Question : Adding extra methods as type extensions in F#]
// [fsi: [|"extra"; "methods"|]]
// [fsi: [|"type"; "extensions"|]]
// [fsi: [|"F"; "#"|]]
// [fsi: Question : How to get MonoDevelop to compile F# projects?]
// [fsi: [|"MonoDevelop"|]]
// [fsi: [|"F"; "#"; "projects"|]]

