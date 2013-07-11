type DemoModes =
    | ParserDemo1
    | ParserDemo2

[<EntryPoint>]
let main argv = 
    printfn "%A" argv
    let (demo, fileName, model) = 
        match argv with 
        | [|"1"|] -> (ParserDemo1, None, None)
        | [|"1"; fileName|] -> (ParserDemo1, Some(fileName), None)
        | [|"2"|] -> (ParserDemo2, None, None)
        | [|"2"; fileName|] -> (ParserDemo2, Some(fileName), None)
        | [|"2"; fileName; model|] -> (ParserDemo2, Some(fileName), Some(model))
        | _ -> failwith "Incorrect input parameters"

    match demo with 
    | ParserDemo1 -> ParserDemo.main fileName
    | ParserDemo2 -> ParserDemo2.main model fileName

    0 // return an integer exit code
