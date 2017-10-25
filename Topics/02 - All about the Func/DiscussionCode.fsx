(* 
    Note: 
        This file is best viewed in VS code or visual studio. though in visual studio, you will need to hover over a declaration to see the type.
        It does seem like visual studio have a better colour highlighting
    Things covered last time: 
        Values, Basic Data structures, Comprehensions, Basic function syntax, Basic Pattern Matching    
    Things Covered: 
            - Type inference basics
            - Basic Function syntax
            - First Class functions
            - Lambda functions / anonymous functions
            - Higher order functions
            - Currying
            - Partial application
            - Some List library functions
            - Function composition
            - Function Pipelining 
            - Recursion & tail call optimisation
*)

(* Functions
    Functions are values too! Let there be func!
*)

// Functions must return one type. No return statement is required.
// Because we are using the multiplication operator, this function is infered to have the signature int -> int. 
// I.e it takes a single in as input, and returns an int.
let square x = x * x 

// If a function has multiple lines, the final line is the return value. 
let squareNumbersTo limit = 
    let squared = [for x in 0 .. limit do yield square x]
    squared

// multiply is a function that takes in two ints and returns an int, so it is int -> int -> int
let multiply x y = x * y

// add has the same signature as multiply. If we wanted or need to make the singature explicit, we do it like this: 
let add (x: int) (y: int) : int = x + y

// If we are calling a function to pass its results to another function, make sure that the parameters are applied in the correct order. 
// Do this with brackets.
let multipled = multiply (square 4) (add 12 33)

//let stringIsLong0 s = s.Length > 5
// Here we are required to declare s as being a string. 
// This is because the compiler has no way to confirm that the input has a property Length otherwise
let stringIsLong (s:string) : bool = s.Length > 5

(*First class functions*)

// They are declared in the same way as values, as above. 

// They can be assigned to values
let multiplyAndAdd a b c =
    let actionOne = multiply
    let actionTwo = add
    actionTwo (actionOne a b) c

// They can be stored in data structures, and they can be declared in other functions

let executeSeparateFunctions input = 
    let add10 x = x + 10
    let functions = [add10; square]
    [for func in functions do yield func input]

(*Lambda (Anonymous) functions
    If necessary, we can declare them without a name
*)

//The following are the same:

let square1 = fun x -> x * x
let square2 x = x * x

(*Higher order functions
    Functions can be parameters to other functions
*)
let mapRange mapper start finish = 
    let range = [start .. finish]
    [for x in  range do yield mapper x]

// Lambda  function! (Anonymous function)
let stuff = mapRange (fun x -> x.ToString()) 10 20

let fizzBuzzTest number =
    match number with
    | n when n % 15 = 0 -> "FizzBuzz"
    | n when n % 5 = 0 -> "Buzz"
    | n when n % 3 = 0 -> "Fizz"
    | n -> sprintf "%i" n

let fizzBuzzed = mapRange fizzBuzzTest 0 100

(*Currying
    Why Currying? 
    - Named after Haskell Curry, a mathematician who was an important 
    influence on the development of functional programming.
    - Functions in programming are modelled after mathematical 
    functions, which take one input in a set of values called the domain and map them
    to the set of values called the range
    (One input, one output)
*)

let getExpSeriesNoCurry(power, limit) = 
    mapRange (fun x -> pown x power) 0 limit

let getExpSeries power limit = 
    mapRange (fun x -> pown x power) 0 limit

let getExpSeriesManualCurry = 
    fun power -> 
        fun limit -> 
            mapRange (fun x -> pown x power) 0 limit

(*Partial Application
    Fix some of the parameters of a function and get a function that takes the remaining parameters
*)

let noCurrySeries = getExpSeriesNoCurry (2, 10)

let getSquares = getExpSeries 2

let squaresOfNumbersTo20 = getSquares 20

let quadratic a b c x = a * (x * x) + b * x + c

let quadValue = quadratic 2 3 4 10

let partialQuad = quadratic 2 4 3

//Importance of parameter order

let rangeToStrings = mapRange (fun x -> x.ToString()) 

let singleDigitsToStrings = rangeToStrings 0 9
let doubleDigitsToStrings = rangeToStrings 10 99

// What happens if we move the parameters around?
let mapRangeChanged start finish mapper = 
    let range = [start .. finish]
    [for x in  range do yield mapper x]

let mapPositivesBelow100 mapper = mapRangeChanged 0 99 mapper

let squareNumbersBelow100 = mapPositivesBelow100 (fun x -> x * x)

let numbersBelow100ToStrings = mapPositivesBelow100 (fun x -> x.ToString())

//We can see this choice a lot in library functions
//Introducing List library functions (A lot of them are similar for array and seq (But potentially with slightly different behaviours))

let numbersTimes2 = List.map (fun x -> x * 2) [0..100]

//Can partially apply operators as well (They are just functions too)
let numbersPlus2 = List.map ((+)2) [0 .. 100] 

let odds = List.filter (fun x -> x % 2 <> 0) numbersPlus2

let sumOfOdds = List.reduce (+) odds

//Lets make a function out of this for the next section: 
let shiftNumbersAndSumOdds shiftBy list =         
    let shiftedNumbers = List.map ((+)shiftBy) list

    let odds = List.filter (fun x -> x % 2 <> 0) shiftedNumbers

    List.reduce (+) odds

(*Function Composition and Pipelining
    What does it mean to compose functions? Call one on the value, then call the next on the result etc...
*)

// Tick is means function is like derivative in math (Just means its a variant)
let shiftNumbersAndSumOdds' shiftBy list =
    let shiftNumber = (+)shiftBy
    let oddPredicate = fun x -> x % 2 <> 0
    List.reduce (+) (List.filter oddPredicate (List.map (shiftNumber) list))

//Lets say we want a composition function. How should it combine 2 functions and a value? 
// let F x y z = x y z ??

//Hint: Function application is normally left associative
let composeQuestion f g x = "TODO"















let compose f g x = g ( f ( x ) )

let shiftNumbersAndSumOdds'' shiftBy list =
    let shiftNumber = (+)shiftBy
    let oddPredicate = fun x -> x % 2 <> 0
    let listFunc = 
        compose (compose (List.map (shiftNumber)) (List.filter oddPredicate)) (List.reduce (+))
    listFunc list



// Compose operator

let shiftNumbersAndSumOdds''' shiftBy list =
    let shiftNumber = (+)shiftBy
    let oddPredicate = fun x -> x % 2 <> 0
    let listFunc = (List.map (shiftNumber)) >> (List.filter oddPredicate) >> (List.reduce (+))
    listFunc list 

//Left Associative compose 
let (>>+) f g x = g ( f(x) )

// Right associative composition
let shiftNumbersAndSumOdds'''' shiftBy list =
    let shiftNumber = (+)shiftBy
    let oddPredicate = fun x -> x % 2 <> 0
    let listFunc = (List.reduce (+)) << (List.filter oddPredicate) << (List.map (shiftNumber)) 
    listFunc list 

(*Function Pipelining*)

let shiftNumbersAndSumOddsPiped shiftBy list =
    let shiftNumber = (+)shiftBy
    let oddPredicate = fun x -> x % 2 <> 0
    list
    |> List.map (shiftNumber)
    |> List.filter oddPredicate
    |> List.sum

let (|>+) x f = f x

let shiftNumbersAndSumEvensPiped shiftBy list =
    let shiftNumber = (+)shiftBy
    let oddPredicate = fun x -> x % 2 <> 0
    list
    |> List.map (shiftNumber)
    |> List.filter (not << oddPredicate)
    |> List.sum

let greaterThan100 x = x > 100

let notGreaterThan100 x = not (greaterThan100 x)

let notGreaterThan100' x = not <| greaterThan100 x

let (<|+) f x= f x

let evenNumberNotGreaterThan100 x = not <|+ greaterThan100 x

(*Recursion
    Recursion always requires having a base case and recursive case
*)

let rec factorial x =
    if x < 1 then 1
    else x * factorial  (x - 1)

let rec factorial' x = 
    match x with
    | x when x < 1 -> 1
    | x -> x * factorial  (x - 1)

let rec factorial'' = function
    | x when x < 1 -> 1
    | x -> x * factorial  (x - 1)

let rec sumList list = ""













let rec sumList list =
    match list with
    | [] -> 0
    | head::rest -> head + sumList rest

let rec naiveFibonacciNumber x = x













let rec fib x = 
    if x < 2 then 
        1 
    else 
        fib (x-2) + fib(x-1)

let rec fib' x = 
    match x with
    | 0 -> 0
    | 1 ->  1
    | n -> fib'(n - 1) + fib'(n - 2)    


let test = "The quick brown fox jumps over the lazy dog"
let panagram inputString = true











let panagram' = List.forall  (string >> test.Contains) ['a' .. 'z']


let rec panagramRec inputString = true

let test () = 

    let a = ["NORTH";"SOUTH";"SOUTH";"EAST";"WEST";"NORTH";"NORTH";"WEST";"SOUTH"]

    let result = reduceDir a

    result









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

(*Next time:
    Tail Call optimisaton
    Algebraic data types
    Further pattern matching
    Option and Result 
    Immutability?
*)



