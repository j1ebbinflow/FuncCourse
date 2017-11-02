
(*Mutual Recursion
    In response to a question from last session: 
*)

let rec doSomething x = 
    doSomeThingElse x
and doSomeThingElse x = 
    doSomething x

(*Tail Recursion*)
let directions = ["NORTH";"SOUTH";"SOUTH";"EAST";"WEST";"NORTH";"NORTH";"WEST";"SOUTH"]

let reduceDir x = []










let reduceDir' (ls : string list) =
    let doesReduce dir1 dir2 =
        match dir1, dir2 with 
        | "NORTH","SOUTH" -> true
        | "SOUTH","NORTH" -> true
        | "WEST","EAST" -> true
        | "EAST","WEST" -> true
        | _,_ -> false

    let rec reduce inputList resultList = 
        match inputList,resultList with 
        | i::rest,[] -> reduce rest [i]
        | current::inputRest,last::resultRest -> 
            if doesReduce last current then
                reduce inputRest resultRest
            else 
                reduce inputRest (current::resultList)
        | [], result -> List.rev result        
    reduce ls []

(*Caching via closures*)


(*Memoisation via closures*)

(*Active Patterns*)