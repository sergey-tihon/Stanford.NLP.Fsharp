module FSharp.IKVM.JCollections

open java.util

let toSeq (iter:Iterator) =
    let rec loop (x:Iterator) = 
        seq { 
            yield x.next()
            if x.hasNext() then 
                yield! (loop x)
            }
    loop iter

let castToSeq<'T> iter =
    iter 
    |> toSeq
    |> Seq.cast<'T>

let toArrayList seq =
    let list = ArrayList()
    seq |> Seq.iter (fun x -> list.Add(x))
    list