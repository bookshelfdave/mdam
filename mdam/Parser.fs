namespace mdam

open Antlr4.Runtime
open Antlr4.Runtime.Tree
open mdamparser
open mdam.AST

module TermParser =
    
    type TreeWalker() =
        inherit mdamBaseListener()
        let treeprops = new ParseTreeProperty<L0Expr>()
        member this.Treeprops = treeprops
        
        override this.ExitMdam_term(context) = 
            let childctx = context.GetChild(0)
            treeprops.Put(context, treeprops.Get(childctx))

        override this.ExitMdam_structure(context) =
            treeprops.Put(context, L0Expr.TFunctor
                (name=context.struct_name.Text,
                                subterms=[for st in context._subterms ->
                                            treeprops.Get(st)]))
            
        override this.ExitMdam_variable(context) =
            treeprops.Put(context, (L0Expr.TVar (context.GetText())) )
    
    let parseTerm (input:string) =
        let stream:ICharStream = CharStreams.fromString(input)
        let lexer:ITokenSource = new mdamLexer(stream) :> ITokenSource
        let tokens:ITokenStream = new CommonTokenStream(lexer) :> ITokenStream
        let parser = new mdamParser(tokens) 
        parser.BuildParseTree <- true
        let tree:IParseTree = parser.mdam_term() :> IParseTree
        tree
        
    let parseTermToRegs (input:string) =
        let tree = parseTerm input
        let walker = new TreeWalker()
        ParseTreeWalker.Default.Walk(walker, tree)
        let top = walker.Treeprops.Get(tree)
        assignRegs top


