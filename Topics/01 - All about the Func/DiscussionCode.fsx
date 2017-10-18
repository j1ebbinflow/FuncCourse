(* Values
    Values, not variables
    Use Let to define values
*)

let nothing = ()

// Int
let numberOfAnimals = 3

// Actually zero
let percentangeOfBirds = 2 / numberOfAnimals

// Casting example
let realPercentageOfBirds = float 2 / float numberOfAnimals

let percentageOfBirds2 = 2.0 / 3.0 

let catName = "Lyra"

let theBirds = sprintf "The birds are %s and %s" "Jack" "Whiskey"

printfn "We have %i animals. The cat is %s. %s" numberOfAnimals catName theBirds

let ``Your animals get along?`` = not true && false
 
(* Tuples
     Note the comma!
*)

//These are objects (Reference types)
let birds = ("Jack", "Whiskey")

let mixedTuple = (1, "Jack", true) 

//These are structs (Value tupes)
let mixedStruct = struct (2, "Whiskey", 1.0f)

(* Lists
    Note the semicolon
*)

let empty = []

let animals = [ "Jack"; "Lyra"; "Whiskey"]

let numbers = [
    1
    2
    3
]

let zeroAndNumbers = 0 :: numbers

let moreNumbers = numbers @ [4;5;6]

let three = numbers.[2]

let generatedList = [ 0 .. 100 ] 

let evens = [ 0 .. 2 .. 100 ] 

let chars = ['a' .. 'z']


let squares = [ for x in evens do yield x * x ]

let alternatingSquares = 
    [for x in 0 .. 10 do 
        yield! [x; x*x]]

let buzzing = 
    [ for elem in 0 .. 100 do 
        if elem % 3 = 0 then 
            yield (sprintf "%i: Buzz" elem) ]

(* Arrays

*)

let emptyA = [| |]

let helloAll = [| "Hello"; "Everyone"; "Out"; "Tehre" |]

helloAll.[3] <- "There"

let arraySquares =  [| for x in 0 .. 100 do yield x * x |]

let slides = arraySquares.[0..3]

(* Sequences
    Logicial series of elements (Like IEnumerable or Java streams) 
    Potentially Lazy, potentially infinite!
*)

let emptySeq = Seq.empty

let helloWorld = seq { yield "Hello"; yield "World" }

let numbersSeq = seq { 1 .. 100 }

let squaresSeq = seq { for x in 0 .. 100 do yield x * x }

(* Functions
    Functions are values too! Let there be func!
*)

let identity x = x

let square x = x * x 
let multiply x y = x * y
let add x y = x + y

let isPowerOf2 x = x &&& (x-1) = 0

let value = isPowerOf2 12

let quadratic a b c x = a * (x * x) + b * x + c

let quadValue = quadratic 2 3 4 10

//let stringIsLong0 s = s.Length > 5
let stringIsLong (s:string) = s.Length > 5

let declareStuffReturnOne a b = 
    let list = [0 .. a]
    let (c,d) = b
    let converted = [for x in list do yield x * c + d]
    converted

//Order of application is important

//This is wrong
//let result = multiply square 4 add 12 33 

// This is right
let result = multiply (square 4) (add 12 33)

(*Expressions
    Now that we've looked at basic functions, now we look at making choices
*)

//If expression
//Indentation is important
let sampleFunc x = 
    if x < 10.0 then 
        x * 2.0 - x / 1.5 + 50.0
    elif x > 20.0 then  
        x / 2.0    
    else 
        //What if I return 2 instead?
        2.0

(* Pattern matching basics*)

let numberPatternTest x =
    match x with 
    | negative when x < 0 -> "Negative number"
    | 0 -> "Found Zero"
    | 1 -> "Found One"
    | x when x < 3 -> "Found Two"
    | _ -> "I don't care about this number"

let animalTest name =
    match name with
    | "Lyra" -> "My cat is Lyra"
    | "Whiskey" -> "My grey bird is Whiskey"
    | _ -> "That isn't my pet"
    | "Jack" -> "My yellow bird is Jack"
    //Hey what about jack? 

// Destructuring
let name, age = ("Jason", 26)

let listTest list = 
    match list with 
    | [] -> "Nothing"
    | [ "Lyra"] -> "Cat"
    | ["Whiskey"; "Jack"] -> "Two Birds"
    | [ "Lyra"; _ ] -> "Cat and someone Else"
    | "Whiskey"::"Jack"::rest -> "At least I have the birds"
    | _ -> "I don't care"


(*Function values and lambda functions*)
//The following are the same:

let square1 = fun x -> x * x
let square2 x = x * x

//This is not:
//I wonder how we can use this later?...
let square3() = fun x ->  x * x

(*First order functions*)

//Function as parameter
// Automatic generalisation
let doStuffToRange mapper start finish = 
    let range = [start .. finish]
    [for x in  range do yield mapper x]

// Lambda  function! (Anonymous function)
let stuff = doStuffToRange (fun x -> x.ToString()) 10 20

let fizzBuzzTest number = "TODO"

let fizzBuzzed = doStuffToRange fizzBuzzTest 0 100

(*Currying
    Lets look at two functions from before: 
*)

//Do Stuff to range and the quadratic function from above.

(*Partial Application
    
*)

(*Function Composition*)

// Manual composition
// Operators

(*Function Pipelining*)

(*Recursion and Tail Call optimisaton*)

(*Memoisation ?*)








