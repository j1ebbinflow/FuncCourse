// If you haven't already, write the factorial function in a tail recursive manner, using an accumulator variable. 

let rec factorial x =
    if x < 1 then 1
    else x * factorial  (x - 1)

let factorialTail x = 
    let rec factorialT total n = 
        match n with 
        | 0 -> total
        | n -> factorialT (total*n) (n-1)        
    factorialT 1 x  

//Rewrite the factorial function in a tail recursive manner, using continuation passing
//Talk about confirming how continuation passing works
let factorial' x =
    let rec loop cont n = 
        match n with
        | 0 -> cont 1
        | n -> loop (fun acc -> cont(acc * n)) (n-1)
    loop id x   

let rec fib x = 
    if x < 2 then 
        1 
    else 
        fib (x-2) + fib(x-1)

//Rewrite the fibonacci function in a tail recursive manner
let fibTail x = 
    let rec fibT value1 value2 fibNumber = 
        match fibNumber with
        | 0 -> value1
        | fibNumber -> fibT value2 (value1 + value2) (fibNumber - 1)
    fibT 0 1 x   

//The code below doesn't work yet, leaving it in as discussion point
let fib' x = 0
    // let rec loop cont fib1 fib2 fibIndex = 
    //     match fibIndex with
    //     | 0 -> cont fib1
    //     | fibIndex -> loop (fun acc -> cont(fib1 + fib2)) fib2  (fibIndex - 1)
    // loop id 0 1 x    

// Solve this code wars problem recursively: // https://www.codewars.com/kata/550f22f4d758534c1100025a
let exampleDirections = ["NORTH";"SOUTH";"SOUTH";"EAST";"WEST";"NORTH";"NORTH";"WEST";"SOUTH"]

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

// ReImplement the library functions using recusion. modify the signature as necessary (Try the continuation passing style!)
// Use a seperate value so that you can compare to the real signature 
let functionToCopy = List.groupBy

let groupBy' projection inputList = 
    let addToMapList currentMap key value = 
        match Map.tryFind key currentMap with
        | None -> Map.add key [value] currentMap
        | Some list -> Map.add key (value::list) currentMap

    let rec grouper currentMap list = 
        match list with
        | [] -> currentMap
        | head::tail -> 
            let key = projection head
            let newMap = addToMapList currentMap key head
            grouper newMap tail

    inputList
    |> grouper Map.empty      
    |> Map.toList
    
let test = groupBy' (fun x -> (x % 10).ToString()) [0..30]

///////////

let functionToCopy2 = List.collect (fun x -> [x; x*x;x*x*x]) [0..10]

let collect''' mapper list = 
    list
    |> List.map mapper
    |> List.concat

let collect'' mapper list = 
    let rec collector inputList output = 
        match inputList with
        | [] -> output
        | head::tail -> collector tail (output @ mapper head)
    collector list []   

let collect' mapper list = 
    let rec collector inputList output = 
        match inputList with
        | [] -> output |> List.rev |> List.concat 
        | head::tail -> collector tail (mapper head::output)
    collector list []    

let result = collect' (fun x -> [x; x*x;x*x*x]) [0..10]

let functionToCopy3 = List.windowed

let windowed' windowSize (list: 'a list) : 'a list list= 
    if windowSize > 0 then 
        let rec windower inputList output remainingStarts = 
            match remainingStarts with
            | starts when starts < 0 -> output |> List.rev
            | starts -> windower (List.tail inputList) (List.take windowSize inputList::output) (starts-1)
        let finalWindowStart = list.Length - windowSize
        if finalWindowStart >= 0 then
            windower list [] finalWindowStart  
        else 
            []             
    else 
        raise <| System.ArgumentException("The input must be positive")    

open System
// Using sequences, generate numbers using the transformation y = sin(x) + log(x) and the range beteen start and finish (inclusive) with a step
let generator start step finish  =
    [for x in start .. step .. finish do yield (Math.Sin (x) + Math.Log10 (x))]

// Using recursion, manually create a generator for a sequence of numbers using the transformation  y = sin(x) + log(x)
// Don't use sequences!
// This should require a series of function calls by the user to build up the sequence. 
// Remember that you need some way to tell the function caller that there are no more values in the sequence. 

let generator' start step finish = 
    let transform x = Math.Sin x + Math.Log x
    let mutable current = start     
    let nextFunc ()= transform current
    let next () = 
        let value = nextFunc ()
        current <- current + step
        if current = finish then
            value,false
        else 
            value,true
    next


// Generalise the function above so that it can work on any transformation function.
// You will need to modify the signature as well. 
let generator'' (start: int) (step: int) (finish: int) (transform: int -> 'a) : unit -> 'a*bool = 
    let mutable current = start     
    let nextFunc ()= transform current
    let next () = 
        let value = nextFunc ()
        current <- current + step
        if current = finish then
            value,false
        else 
            value,true
    next

// Generalise the function above further so that it can produce an infinite series
// You will need to modify the signature as well. 
let generator''' (start: int) (step: int) (transform: int -> 'a) : unit -> 'a = 
    let mutable current = start     
    let nextFunc ()= transform current
    let next () = 
        let value = nextFunc ()
        current <- current + step
        value
    next
