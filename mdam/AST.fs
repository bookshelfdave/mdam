namespace mdam

module AST =
    
    type L0Expr  =
        | TVar of name:string 
        | TFunctor of name:string * subterms:L0Expr list 
   
        member this.Show () =
            match this with
            | TVar name -> sprintf "TVar %s" name
            | TFunctor (name, subterms) ->
                let head = sprintf "TFunctor %s (" name 
                let middle = String.concat "," <| List.map (fun (st:L0Expr)  -> st.Show()) subterms
                let tail = sprintf ")"
                sprintf "%s%s%s" head middle tail
            
//    type Term =
//        | TRef of k:int // uint64 would be better, but the array type uses int? 
//        | TStructure of k:int
//        | TFunctor of name:string * arity:byte
//        | TEmpty
//   
//        member this.Rep =
//            match this with
//            | TRef k -> sprintf "REF %d" k  
//            | TStructure k -> sprintf "STR %d" k
//            | TFunctor (name, arity) -> sprintf "%s/%d" name arity
//            | TEmpty -> "EMPTY"