namespace FSharp.IKVM.Util

open java.lang
open java.util

module Iterable = 
    // 2 changes proposed in toSeq function :
    // 1- yield moved after if x.hasNext()
    // 2- force a copy (as proposed by Tomasp on SO : http://stackoverflow.com/questions/6605253/wrapping-a-mutable-collection-with-a-sequence
    let toSeq (iter:Iterable) =
        let rec loop (x:Iterator) = 
           seq { 
                if x.hasNext() then 
                    yield x.next()  
                    yield! (loop x)
                }
        iter.iterator() |> loop |> Array.ofSeq |> Seq.readonly


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
