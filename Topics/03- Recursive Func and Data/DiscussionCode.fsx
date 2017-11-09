
(*Mutual Recursion
    In response to a question from last session: 
*)

let rec doSomething x = 
    doSomeThingElse x
and doSomeThingElse x = 
    doSomething x

(*Tail Recursion*)

let rec factorial x =
    if x < 1 then 1
    else x * factorial  (x - 1)






let rec fib x = 
    if x < 2 then 
        1 
    else 
        fib (x-2) + fib(x-1)

let fibTailR x = 0    








