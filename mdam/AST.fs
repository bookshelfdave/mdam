namespace mdam
open System.Collections.Generic

type RegMap = Dictionary<string, int>

module AST =
    
    type L0Expr  =
        | TVar of name:string 
        | TFunctor of name:string * subterms:L0Expr list 
              
        member this.ToRep () = 
            match this with
            | TVar name -> sprintf "TVar %s" name
            | TFunctor (name, subterms) ->
                let head = sprintf "TFunctor %s (" name 
                let middle = String.concat "," <| List.map (fun (st:L0Expr)  -> st.ToRep()) subterms
                let tail = sprintf ")"
                sprintf "%s%s%s" head middle tail
        // get a representation of this type that is suitable as a key for register mapping
        member this.getNameForReg () =
            match this with
            | TVar name -> sprintf "%s" name
            | TFunctor (name, subterms) -> sprintf "%s/%d" name subterms.Length

    type L0ExprRegs  =
        | RVar of name:string * regid:int 
        | RFunctor of name:string * subterms:L0ExprRegs list * regid:int 
        
    // I'm going for convenience here, not full on fp
    let assignRegs (l:L0Expr) =
        let regs = new RegMap()                       
        let assignReg(l:L0Expr) =
            regs.TryAdd(l.getNameForReg(), regs.Count+1) |> ignore
            regs.[l.getNameForReg()]
        let rec setQRegs_ (l:L0Expr) =
            match l with
            | TVar name ->
                RVar (name, assignReg(l))
            | TFunctor (name, subterms) ->                
                let regid = assignReg(l)
                // first, assign regs to each subterm (non-recursively)
                let _ = List.map assignReg subterms
                // then recurse into each subterm
                let newsubterms = List.map (fun (st:L0Expr) -> setQRegs_ (st)) subterms
                RFunctor (name, newsubterms, regid)
        let exprsWithRegs = setQRegs_ l
        (exprsWithRegs, regs)    

(*
    p(Z, h(Z, W), f(W))    
    
    X1 = p(X2, X3, X4)
    X2 = Z
    X3 = h(X2, X5)
    X4 = f(X5)
    X5 = W
    *)
    
  