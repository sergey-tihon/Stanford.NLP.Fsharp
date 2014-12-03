namespace System
open System.Reflection

[<assembly: AssemblyTitleAttribute("Stanford.NLP.Parser.Fsharp")>]
[<assembly: AssemblyProductAttribute("Stanford.NLP.Parser.Fsharp")>]
[<assembly: AssemblyDescriptionAttribute("F# extensions for Stanford.NLP.Parser")>]
[<assembly: AssemblyVersionAttribute("0.0.9")>]
[<assembly: AssemblyFileVersionAttribute("0.0.9")>]
do ()

module internal AssemblyVersionInformation =
    let [<Literal>] Version = "0.0.9"
