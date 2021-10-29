namespace mdam

open Antlr4.Runtime
open Antlr4.Runtime.Misc
open Antlr4.Runtime.Tree
open mdamparser

module TermParser =
    
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


