[<AutoOpen>]
module FSharp.NLP.Stanford.Parser.PennTreebankIIPatterns

open edu.stanford.nlp.trees

let isClauseLevel = function 
    | S | SBAR | SBARQ | SQ | SINV -> true
    | _ -> false

let isPhraseLevel = function
    | ADJP | ADVP | CONJP | FRAG | INTJ | LST 
    | NAC | NP | NX | PP | PRN | PRT | QP | RRC 
    | UCP | VP | WHADJP | WHADVP | WHNP | WHPP | X -> true
    | _ -> false

let isWordLevel = function
    | CC | CD | DT | EX | FW | IN | JJ | JJR 
    | JJS | LS | MD | NN | NNS | NNP | NNPS 
    | PDT | POS | PRP | PRPS | RB | RBR | RBS 
    | RP | SYM | TO | UH | VB | VBD | VBG 
    | VBN | VBP | VBZ | WDT | WP | WPS | WRB -> true
    | _ -> false

let isPunctuation = function
    | SentenceCloser | Comma | Colon | LRB | RRB -> true
    | _ -> false


let private tagMatch predicate tag =
    match predicate tag with
    | true -> Some()
    | false -> None 

let (|ClauseLevel|_|) tag = tag |> tagMatch isClauseLevel
let (|PhraseLevel|_|) tag = tag |> tagMatch isPhraseLevel
let (|WordLevel|_|)   tag = tag |> tagMatch isWordLevel
let (|Punctuation|_|) tag = tag |> tagMatch isPunctuation



let getLabel (node:Tree) = 
    match node.isLeaf(), node.label() with
    | true, _ -> None
    | _, null -> None
    | _, label ->
        match label.value() with
        | "ROOT" -> Some(ROOT)
        | "ADJP" -> Some(ADJP)
        | "ADVP" -> Some(ADVP)
        | "CC"   -> Some(CC)
        | "CD"   -> Some(CD) 
        | "CONJP"-> Some(CONJP)
        | "DT"   -> Some(DT)
        | "EX"   -> Some(EX)
        | "FRAG" -> Some(FRAG)
        | "FW"   -> Some(FW)
        | "IN"   -> Some(IN)
        | "INTJ" -> Some(INTJ)
        | "JJ"   -> Some(JJ)
        | "JJR"  -> Some(JJR)
        | "JJS"  -> Some(JJS)
        | "LS"   -> Some(LS)
        | "LST"  -> Some(LST)
        | "MD"   -> Some(MD)
        | "NAC"  -> Some(NAC)
        | "NN"   -> Some(NN)
        | "NNS"  -> Some(NNS)
        | "NNP"  -> Some(NNP)
        | "NNPS" -> Some(NNPS)
        | "NP"   -> Some(NP)
        // to support Stanford '-retainTmpSubcategories' http://nlp.stanford.edu/software/parser-faq.shtml#s
        | "NP-TMP" | "NP-ADV" -> Some(NP) 
        | "NX"   -> Some(NX)
        | "PDT"  -> Some(PDT)
        | "POS"  -> Some(POS) 
        | "PP"   -> Some(PP)
        | "PRN"  -> Some(PRN)
        | "PRP"  -> Some(PRP)
        | "PRP$" | "PRP-S" -> Some(PRPS)
        | "PRT"  -> Some(PRT) 
        | "QP"   -> Some(QP)
        | "RB"   -> Some(RB)
        | "RBR"  -> Some(RBR)
        | "RBS"  -> Some(RBS)
        | "RP"   -> Some(RP) 
        | "RRC"  -> Some(RRC)
        | "S"    -> Some(S)
        | "SBAR" -> Some(SBAR)
        | "SBARQ"-> Some(SBARQ) 
        | "SINV" -> Some(SINV)
        | "SQ"   -> Some(SQ)
        | "SYM"  -> Some(SYM)
        | "TO"   -> Some(TO)
        | "UCP"  -> Some(UCP)
        | "UH"   -> Some(UH)
        | "VB"   -> Some(VB)
        | "VBD"  -> Some(VBD)
        | "VBG"  -> Some(VBG) 
        | "VBN"  -> Some(VBN) 
        | "VBP"  -> Some(VBP) 
        | "VBZ"  -> Some(VBZ) 
        | "VP"   -> Some(VP)
        | "WDT"  -> Some(WDT)
        | "WHADJP"->Some(WHADJP)
        | "WHADVP"->Some(WHADVP) 
        | "WHNP" -> Some(WHNP)
        | "WHPP" -> Some(WHPP)
        | "WP"   -> Some(WP)
        | "WP$" | "WP-S" -> Some(WPS)
        | "WRB"  -> Some(WRB)
        | "X"    -> Some(X)

        | "."    -> Some(SentenceCloser)
        | ","    -> Some(Comma)
        | ":"    -> Some(Colon)
        | "-LRB-"-> Some(LRB)
        | "-RRB-"-> Some(RRB)
        | "``"   -> Some(SQT)
        | "''"   -> Some(EQT)

        | "#"    -> Some(Symbol('#'))
        | tag    -> failwithf "Unknown Penn Treebank II tag '%s'" tag

let isOneOfLabels labels node =
    match getLabel node with
    | Some(x) -> 
        labels |> Seq.exists (fun label -> label = x)
    | _ -> false

let isLabel label node =
    node |> isOneOfLabels [label]

let (|Label|_|) label node =
    match isLabel label node with
    | true  -> Some()
    | false -> None