module Config

let (@@) a b = System.IO.Path.Combine(a,b)
let jarRoot = __SOURCE_DIRECTORY__ @@ @"..\..\temp\stanford-corenlp-full-2013-11-12\stanford-corenlp-3.3.0-models\"
let modelsRoot = jarRoot @@ @"edu\stanford\nlp\models\"
let (!) path = modelsRoot @@ path