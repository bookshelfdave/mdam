namespace mdam

module CLI =
   
    
            
            
    exception HeapError of string
    
//    type Heap(capacity: int) =
//        let mutable top:int = 0
//        let data = Array.create capacity Empty 
//        member this.Append(t:L0) =
//            (*
//            if top + 1 > capacity then
//                // check math etc
//                raise (HeapError("Resize not implemented"))
//            *)
//             data.[top] <- t
//             let current = top
//             top <- top + 1
//             Ok(current)
//             
//             (*    | true ->
//                     let current = top
//                     top <- top + 1
//                     Ok(current)
//                 | false -> 
//                    Error("Can't append")
//              *)
//        member this.ElementCount =
//            top
//        member this.Capacity =
//            capacity
//        member this.Item
//            with get(index) = data.[index] 
        
    [<EntryPoint>]
    let main args =
        TermParser.parseTermToRegs " p(Z, h(Z, W), f(W)) " |> ignore
        0
