[<EntryPoint>]
let main argv = 
    printfn "%A" argv
    match argv with 
    | [|fileName|] -> NERDemo.main (Some(fileName))
    | [||] -> NERDemo.main None
    | _ -> failwith "Incorrect input parameters"

    0 // return an integer exit code
