module Tests

open NUnit.Framework
open mdam.TermParser

[<SetUp>]
let Setup () =
    ()

[<Test>]
let TestParseL0TermToRegs () =
    let (exprRegs, regs) = parseTermToRegs("p(Z, h(Z, W), f(W))")    
    
    Assert.AreEqual(1, regs.["p/3"])
    Assert.AreEqual(2, regs.["Z"])
    Assert.AreEqual(3, regs.["h/2"])
    Assert.AreEqual(4, regs.["f/1"])
    Assert.AreEqual(5, regs.["W"])
    Assert.Pass()



(*
    p(Z, h(Z, W), f(W))    
    
    X1 = p(X2, X3, X4)
    X2 = Z
    X3 = h(X2, X5)
    X4 = f(X5)
    X5 = W
    *)
    