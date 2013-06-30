namespace FSharp.IKVM.Util

open java.lang
open java.util

module Iterable = 
    let toSeq (iter:Iterable) =
        let rec loop (x:Iterator) = 
           seq { 
                yield x.next()
                if x.hasNext() then 
                    yield! (loop x)
                }
        iter.iterator() |> loop

    let castToSeq<'T> iter =
        toSeq iter
        |> Seq.cast<'T>

    let toList iter =
        toSeq iter
        |> Seq.toList

    let castToList<'T> iter =
        castToSeq<'T> iter
        |> Seq.toList

    let toArray iter =
        toSeq iter
        |> Seq.toArray

    let castToArray<'T> iter =
        castToSeq<'T> iter
        |> Seq.toArray

    let fromSeq seq =
        let list = ArrayList()
        seq |> Seq.iter (fun x -> list.Add(x))
        list :> Iterable