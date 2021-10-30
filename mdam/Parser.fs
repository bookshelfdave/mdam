namespace mdam

open Antlr4.Runtime
open Antlr4.Runtime.Tree
open mdamparser
open mdam.AST

module TermParser =
    
    type TreeWalker() =
        
        inherit mdamBaseListener()
        let treeprops = new ParseTreeProperty<System.Object>()
        member this.Treeprops = treeprops
        override this.ExitMdam_terms(context:mdamParser.Mdam_termsContext) =
            printfn "terms"

        override this.ExitMdam_term(context) = 
            let childctx = context.GetChild(0)
            treeprops.Put(context, treeprops.Get(childctx))

        override this.ExitMdam_structure(context) =
            let ss = [for st in context._subterms -> treeprops.Get(st) :?> L0Expr]
            let l = L0Expr.TFunctor (name=context.struct_name.Text, subterms=ss)
            treeprops.Put(context, l)
            
        override this.ExitMdam_variable(context) =
            treeprops.Put(context, (L0Expr.TVar (context.GetText())) )

    let run (input:string) : int =
        let stream:ICharStream = CharStreams.fromString(input)
        let lexer:ITokenSource = new mdamLexer(stream) :> ITokenSource
        let tokens:ITokenStream = new CommonTokenStream(lexer) :> ITokenStream
        let parser = new mdamParser(tokens) 
        parser.BuildParseTree <- true
        //IParseTree tree = parser.prog();
        let tree:IParseTree = parser.mdam_term() :> IParseTree
        let walker = new TreeWalker()
        ParseTreeWalker.Default.Walk(walker, tree)
        let top = walker.Treeprops.Get(tree) :?> L0Expr
        top.Show() |> printfn "%s" 
        0


