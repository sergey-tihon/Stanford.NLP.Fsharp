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


let private nodeMatch label node = 
    match getLabel node with
    | Some(x) when x = label -> Some()
    | _ -> None

let (|ROOT_|_|)   node = node |> nodeMatch ROOT
let (|ADJP_|_|)   node = node |> nodeMatch ADJP
let (|ADVP_|_|)   node = node |> nodeMatch ADVP
let (|CC_|_|)     node = node |> nodeMatch CC
let (|CD_|_|)     node = node |> nodeMatch CD
let (|CONJP_|_|)  node = node |> nodeMatch CONJP
let (|DT_|_|)     node = node |> nodeMatch DT
let (|EX_|_|)     node = node |> nodeMatch EX
let (|FRAG_|_|)   node = node |> nodeMatch FRAG
let (|FW_|_|)     node = node |> nodeMatch FW
let (|IN_|_|)     node = node |> nodeMatch IN
let (|INTJ_|_|)   node = node |> nodeMatch INTJ
let (|JJ_|_|)     node = node |> nodeMatch JJ
let (|JJR_|_|)    node = node |> nodeMatch JJR
let (|JJS_|_|)    node = node |> nodeMatch JJS
let (|LS_|_|)     node = node |> nodeMatch LS
let (|LST_|_|)    node = node |> nodeMatch LST
let (|MD_|_|)     node = node |> nodeMatch MD
let (|NAC_|_|)    node = node |> nodeMatch NAC
let (|NN_|_|)     node = node |> nodeMatch NN
let (|NNS_|_|)    node = node |> nodeMatch NNS
let (|NNP_|_|)    node = node |> nodeMatch NNP
let (|NNPS_|_|)   node = node |> nodeMatch NNPS
let (|NP_|_|)     node = node |> nodeMatch NP
let (|NX_|_|)     node = node |> nodeMatch NX
let (|PDT_|_|)    node = node |> nodeMatch PDT
let (|POS_|_|)    node = node |> nodeMatch POS
let (|PP_|_|)     node = node |> nodeMatch PP
let (|PRN_|_|)    node = node |> nodeMatch PRN
let (|PRP_|_|)    node = node |> nodeMatch PRP
let (|PRPS_|_|)   node = node |> nodeMatch PRPS
let (|PRT_|_|)    node = node |> nodeMatch PRT
let (|QP_|_|)     node = node |> nodeMatch QP
let (|RB_|_|)     node = node |> nodeMatch RB
let (|RBR_|_|)    node = node |> nodeMatch RBR
let (|RBS_|_|)    node = node |> nodeMatch RBS
let (|RP_|_|)     node = node |> nodeMatch RP
let (|RRC_|_|)    node = node |> nodeMatch RRC
let (|S_|)        node = node |> nodeMatch S
let (|SBAR_|_|)   node = node |> nodeMatch SBAR
let (|SBARQ_|_|)  node = node |> nodeMatch SBARQ
let (|SINV_|_|)   node = node |> nodeMatch SINV
let (|SQ_|_|)     node = node |> nodeMatch SQ
let (|SYM_|_|)    node = node |> nodeMatch SYM
let (|TO_|_|)     node = node |> nodeMatch TO
let (|UCP_|_|)    node = node |> nodeMatch UCP
let (|UH_|_|)     node = node |> nodeMatch UH
let (|VB_|_|)     node = node |> nodeMatch VB
let (|VBD_|_|)    node = node |> nodeMatch VBD
let (|VBG_|_|)    node = node |> nodeMatch VBG
let (|VBN_|_|)    node = node |> nodeMatch VBN
let (|VBP_|_|)    node = node |> nodeMatch VBP
let (|VBZ_|_|)    node = node |> nodeMatch VBZ
let (|VP_|_|)     node = node |> nodeMatch VP
let (|WDT_|_|)    node = node |> nodeMatch WDT
let (|WHADJP_|_|) node = node |> nodeMatch WHADJP
let (|WHADVP_|_|) node = node |> nodeMatch WHADVP
let (|WHNP_|_|)   node = node |> nodeMatch WHNP
let (|WHPP_|_|)   node = node |> nodeMatch WHPP
let (|WP_|_|)     node = node |> nodeMatch WP
let (|WPS_|_|)    node = node |> nodeMatch WPS
let (|WRB_|_|)    node = node |> nodeMatch WRB
let (|X_|_|)      node = node |> nodeMatch X

let (|SentenceCloser_|_|) node = node |> nodeMatch SentenceCloser
let (|Comma_|_|)  node = node |> nodeMatch Comma
let (|Colon_|_|)  node = node |> nodeMatch Colon
let (|LRB_|_|)    node = node |> nodeMatch LRB
let (|RRB_|_|)    node = node |> nodeMatch RRB
let (|SQT_|_|)    node = node |> nodeMatch SQT
let (|EQT_|_|)    node = node |> nodeMatch EQT