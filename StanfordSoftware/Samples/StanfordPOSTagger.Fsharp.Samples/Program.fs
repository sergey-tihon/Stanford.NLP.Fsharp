type DemoModes =
    | ParserDemo1
    | ParserDemo2

[<EntryPoint>]
let main argv = 
    printfn "%A" argv
    match argv with 
    | [|"1"; "file"; fileName|] -> TaggerDemo.tagFile fileName
    | [|"1"; "text"; text|] -> TaggerDemo.tagText text
    | [|"2"; fileName|] -> TaggerDemo2.main fileName
    | _ -> failwith "Incorrect input parameters"

    0 // return an integer exit code