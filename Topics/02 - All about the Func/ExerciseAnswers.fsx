open System

let test = "The quick brown fox jumps over the lazy dog"

let chars = ['a' .. 'z']

let panagram' =
    ['a' .. 'z']
    |> List.map (string >> test.Contains) 
    |> List.reduce (&&) 

(*
    Above is an implementation of the test for a panagram that uses higher order functions and composition.
    Reimplement the panagram test using recusion, in a way that does a single pass over the inputString. 
    If possible, avoid using higher order Collection functions (E.g. List.map)
    Note: this exercise has been pulled from http://exercism.io/exercises/fsharp/pangram/readme
*)
let panagramRec (inputString: string) =
    let uniqueChars = set ['a' .. 'z']
    let inputChars = inputString.ToLowerInvariant()
    let rec panagramTest (characters: Set<char>) (input: string) (index: int) : bool = 
        if characters.IsEmpty then 
            true
        elif index = input.Length then
            characters.IsEmpty
        else 
            let current = input.Chars index 
            if characters.Contains current then
                panagramTest (characters.Remove current) input (index + 1)
            else
                panagramTest characters input (index + 1)

    panagramTest uniqueChars inputChars 0

let allCharString = ['a' .. 'z'] |> String.Concat


//Implement a function that recursively sums a list of functions. 

let rec sumList list =
    match list with
    | [] -> 0
    | head::rest -> head + sumList rest

let sumList' list = 
    let rec sumListInt sum list = 
        match list with
        | [] -> sum
        | head::rest -> sumListInt (sum + head) rest
    sumListInt 0 list    

// Reimplementing standard library functions
// NOTE: The answers for these functions can be easily found online, so don't search for them until you have given it a try and you've reached trouble. 


//TODO Reimplement List.map -> https://msdn.microsoft.com/en-us/library/ee370378.aspx
// Map takes a function and a list, applies the function to each element in the list, and returns a new list of the results. 

let rec mapList mapping list =  
    match list with
    | head :: tail -> mapping head :: mapList mapping tail
    | [] -> []

let mapListGeneratorComprehension mapping list =
    [for a in list do yield mapping a]

//TODO Reimplement List.filter -> https://msdn.microsoft.com/en-us/library/ee370294.aspx
// Fold takes a function to test each list element (function returns a boolean) and a list. Returns a new list containly only the elements for which the predicate returned true. 
let rec filterList predicate list = 
    match list with
    | head::tail when predicate head -> head :: filterList predicate tail
    | _::tail -> filterList predicate tail
    | [] -> []


//TODO Reimplement List.fold -> https://msdn.microsoft.com/en-us/library/ee353894.aspx
// Takes a function to update the accumulated values given the input element, the initial value, and the list. returns the final accumulated value
let rec foldList folder acc list = 
    match list with
    | head :: tail -> foldList folder (folder acc head) tail
    | [] -> acc


//TODO Reimplement List.reduce
// Takes a function to update the accumulated values given the input element and the list. returns the final accumulated value
//Throws an ArgumentException if the list is empty (Use invalidArg function -> https://msdn.microsoft.com/en-us/library/dd233178.aspx ) 
let rec reduceList reducer list =
    match list with
    | [] -> invalidArg "list" "Cannot perform reduce on empty list"
    | head::tail -> foldList reducer head tail

// Tail Recursion

let rec mapListBasic2 mapping list =  
    match list with
    | [] -> []
    | head :: tail -> 
        let temp = mapListBasic2 mapping tail; 
        mapping head:: temp



let mapListTail mapping list = 
    let rec loop acc loopList = 
        match loopList with
        | [] -> List.rev acc
        | head :: tail -> loop (mapping head :: acc) tail
    loop [] list
    
// See http://stackoverflow.com/questions/3248091/f-tail-recursive-function-example for continuation style
let mapListCont mapping list = 
    let rec loop cont loopList = 
        match loopList with
        | [] -> cont []
        | head :: tail -> loop (fun acc -> cont(mapping head::acc)) tail
    loop id list // id x = x (A built in function)

//Using recursion, group an input list of numbers by the final digit. 
// Hint: To make things easier, you may want to use an array as apart of the output, but using a list or array is acceptable. 
let groupByFinalDigit (list: int list) = 
    let rec groupByLsd (acc: int list array) list = 
        match list with
        | [] -> acc
        | head::tail -> 
            let lsd = head % 10
            acc.[lsd] <- head :: acc.[lsd]
            groupByLsd acc tail
    groupByLsd (Array.create 10 []) list |> Array.toList

let groupByFinalDigit' (list: int list) = List.groupBy (fun x -> x % 10) list

// Using any approach (Recursion, Function composition or higher order functions), create a function that returns the squares of the positive numbers in a list
let getSquaresOfPositives = List.filter (fun x -> x > 0) >> List.map (fun x ->  x * x)  

// Using any approach (Recursion, Function composition or higher order functions), create a function that returns the doubles (*2) of the positive numbers in a list
let getDoublesOfPositives = List.filter (fun x -> x > 0) >> List.map ((*) 2)  

(*
    Using the functions above, create a function that takes in an input list of numbers and does the following
    - Groups the numbers by the final digit
    - For each group, if the final digit of all the numbers are even, the numbers are squared. If the final digit of all the numbers are odd, the numbers are doubled
    - For each group, the numbers are summed 
    - The square root of each sum is taken
    - The largest and smallest square root is returned, with the digit of the original group the square root came from. 
*)
let doABunchOfMathsStuff list = 
    // Assumes list is not empty.
   let sumAndSquareRoot = List.sum >> float >> Math.Sqrt
   let keyAndRoots = 
       list 
       |> groupByFinalDigit'
       |> List.map (fun (key,elements) -> 
            if key % 2 = 0 then
                key, getSquaresOfPositives elements
            else 
                key, getDoublesOfPositives elements)
       |> List.map (fun (key,elements) -> key, sumAndSquareRoot elements)
   keyAndRoots |> List.fold (fun (max, min) (key, root) ->
        let newMax = if root > snd max then (key, root) else max
        let newMin = if root < snd min then (key, root) else min
        (newMax, newMin)) (keyAndRoots.Head, keyAndRoots.Head)

            
