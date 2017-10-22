(* 
    Note: 
        This file is best viewed in VS code or visual studio. though in visual studio, you will need to hover over a declaration to see the type.
        It does seem like visual studio have a better colour highlighting
    Things covered last time: 
        Values, Basic Data structures, Comprehensions, Basic function syntax, Basic Pattern Matching    
    Things Covered: 
        Basic function syntax       
        Function values
        lambda functions     
        First order functions
        Currying
        Partial Application
        Some List Library functions
        Function composition and pipelining
        Recursion and tail call optimisation


*)

(* Functions
    Functions are values too! Let there be func!
*)

// The simplest function is called the identity. 
// We don't actually need to declare this, the library version is just "id"
let identity x = x

// Functions must return one type. No return statement is required.
// Because we are using the multiplication operator, this function is infered to have the signature int -> int. 
// I.e it takes a single in as input, and returns an int.
let square x = x * x 

// multiply is a function that takes in two ints and returns an int, so it is int -> int -> int
let multiply x y = x * y

// add has the same signature as multiply. If we wanted or need to make the singature explicit, we do it like this: 
let add (x: int) (y: int) : int = x + y

// If we are calling a function to pass its results to another function, make sure that the parameters are applied in the correct order. 
// Do this with brackets.
let multipled = multiply (square 4) (add 12 33)

// Functions can have many parameters 
let quadratic a b c x = a * (x * x) + b * x + c

let quadValue = quadratic 2 3 4 10

//let stringIsLong0 s = s.Length > 5
// Here we are required to declare s as being a string. 
// This is because the compiler has no way to confirm that the input has a property Length otherwise
let stringIsLong (s:string) : bool = s.Length > 5

// If a function has multiple  lines, the final line is the return value. 
let declareStuffReturnOne a = 
    let list = [0 .. a]
    let converted = [for x in list do yield x * x]
    converted

(*Function values and lambda functions*)
//The following are the same:

let square1 = fun x -> x * x
let square2 x = x * x

//This is not:
//I wonder how we can use this later?...
let square3() = fun x ->  x * x

let func = square3 () 4
(*First order functions*)

//Function as parameter
// Automatic generalisation
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
            limit

let quadratic2 a b c x = a * (x * x) + b * x + c


(*Partial Application
    Fix some of the parameters of a function and get a function that takes the remaining parameters
*)

let noCurrySeries = getExpSeriesNoCurry (2, 10)

let getSquares = getExpSeries 2

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

let evenNumberNotGreaterThan100 x = not <| (fun num -> num > 100) x

(*Recursion and Tail Call optimisaton*)

let rec fact x =
    if x < 1 then 1
    else x * fact (x - 1)

let rec isPowerOf exponent target = 
    match target with
    | _ when target < 1 -> false
    | 1 -> true
    | _ -> if target % exponent = 0 then isPowerOf exponent (target / exponent) else false

let rec fib x = ""

let rec sumList list = ""

let rec mapList list = ""

let reduceDir (ls : string list) = ""

let test () = 

    let a = ["NORTH";"SOUTH";"SOUTH";"EAST";"WEST";"NORTH";"NORTH";"WEST";"SOUTH"]

    let result = reduceDir a

    result
//Next time?

//Data types, further pattern matching








