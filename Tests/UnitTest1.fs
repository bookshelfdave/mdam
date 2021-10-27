module Tests

open NUnit.Framework
open mdam.Core

[<SetUp>]
let Setup () =
    ()

[<Test>]
let TestTerm () =
    let x = Term.Str 1
    Assert.AreEqual("STR 1", x.Rep)
    let x = Term.Ref 50
    Assert.AreEqual("REF 50", x.Rep)
    let x = Term.Functor ("f", 2uy)
    Assert.AreEqual("f/2", x.Rep)
    
    
[<Test>]
let TestHeap () =
    let h = Heap(1024)
    let result = h.Append <| Term.Str 1
    
    match result with
        | Ok n ->
                Assert.AreEqual(0, n)
                Assert.AreEqual(1, h.ElementCount)
        | Error e -> Assert.Fail(e)
    
    Assert.Pass()
    (*
    let result = h.Append <| Term.Functor ("f",1uy)
    Assert.AreEqual(2, result)
    Assert.AreEqual(2, h.ElementCount)
    Assert.AreEqual(Term.Str 1, h.[0])
    Assert.AreEqual(Term.Functor ("f", 1uy), h.[0])
    *)