namespace System
open System.Reflection

[<assembly: AssemblyProductAttribute("Stanford.NLP.Parser.Fsharp")>]
[<assembly: AssemblyTitleAttribute("Stanford.NLP.Parser.FSharp")>]
[<assembly: AssemblyDescriptionAttribute("F# wrapper for The Stanford Parser")>]
[<assembly: AssemblyVersionAttribute("0.0.6")>]
do ()

module internal AssemblyVersionInformation =
    let [<Literal>] Version = "0.0.6"
