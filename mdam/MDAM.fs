namespace mdam

open Antlr4.Runtime
open Antlr4.Runtime.Misc
open Antlr4.Runtime.Tree
open mdamparser

module Parser =
    
    type TreeWalker() =
        inherit mdamBaseListener()
            override this.ExitMdam_terms(context:mdamParser.Mdam_termsContext) =
                printfn "Hello from tree walker"

            override this.ExitMdam_term(context) = 
                printfn "Hello from term"
               
    let run (input:string) : int =
        let stream:ICharStream = CharStreams.fromString(input)
        let lexer:ITokenSource = new mdamLexer(stream) :> ITokenSource
        let tokens:ITokenStream = new CommonTokenStream(lexer) :> ITokenStream
        let parser = new mdamParser(tokens) 
        parser.BuildParseTree <- true
        //IParseTree tree = parser.prog();
        let tree:IParseTree = parser.mdam_terms() :> IParseTree
        let walker = new TreeWalker()
        ParseTreeWalker.Default.Walk(walker, tree)
        tree.ToStringTree() |> printfn "%s" 
        0

module Core =
   
    type Term =
        | Ref of k:int // uint64 would be better, but the array type uses int? 
        | Str of k:int
        | Functor of name:string * arity:byte
        | Empty
   
        member this.Rep =
            match this with
            | Ref k -> sprintf "REF %d" k  
            | Str k -> sprintf "STR %d" k
            | Functor (name, arity) -> sprintf "%s/%d" name arity
            | Empty -> "EMPTY"
            
            
    exception HeapError of string
    
    type Heap(capacity: int) =
        let mutable top:int = 0
        let data = Array.create capacity Empty 
        member this.Append(t:Term) =
            (*
            if top + 1 > capacity then
                // check math etc
                raise (HeapError("Resize not implemented"))
            *)
             data.[top] <- t
             let current = top
             top <- top + 1
             Ok(current)
             
             (*    | true ->
                     let current = top
                     top <- top + 1
                     Ok(current)
                 | false -> 
                    Error("Can't append")
              *)
        member this.ElementCount =
            top
        member this.Capacity =
            capacity
        member this.Item
            with get(index) = data.[index] 
        
    [<EntryPoint>]
    let main args =
        Parser.run "{a,b,c}" |> ignore
        0
