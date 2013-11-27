namespace System
open System.Reflection

[<assembly: AssemblyProductAttribute("FSharp.NLP.Stanford.Parser")>]
[<assembly: AssemblyTitleAttribute("FSharp.NLP.Stanford.Parser")>]
[<assembly: AssemblyDescriptionAttribute("F# wrapper for The Stanford Parser")>]
[<assembly: AssemblyVersionAttribute("0.0.5")>]
do ()

module internal AssemblyVersionInformation =
    let [<Literal>] Version = "0.0.5"
