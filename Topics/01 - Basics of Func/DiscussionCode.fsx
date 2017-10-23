(* 
    Note: 
        This file is best viewed in VS code or visual studio. though in visual studio, you will need to hover over a declaration to see the type.
        It does seem like visual studio have a better colour highlighting.
        In Vscode, make sure you have Ionide installed
    Things Covered: 
    Values
        Integers, Floats, Strings
    Basic Data structures
        Tuples, Lists, Arrays, Sequences
    Comprehensions
        List, arrays, sequences
    Basic function syntax
    Basic Pattern Matching for: 
        Numbers, Strings, Tuples, Lists             
*)

(* Values
    In functional programming, we mainly care about values instead of variables. 
    Once the value is set, they are immutable by default, and cannot be changed unless we explicitly make them mutable
    
    There are two main ways to declare things in F#. The first we'll look at is the "let" binding. This is used to define values and functions 
*)


// This is meant to indicate the absence of a value: https://docs.microsoft.com/en-us/dotnet/fsharp/language-reference/unit-type
let nothing = ()

// Unit is not the same as null. Unit is closer to void, but still not the same. Null does exist in F#, but should be avoided as much as possible.
// If you are using .net library / framework code, you make have to deal with it, but the usual practice is to isolate it as much as possible. 
// This is something we will revisit later. 
let EwwItsANull = null

// Int
let numberOfAnimals = 3

// Actually zero because we are using ints
let percentangeOfBirds = 2 / numberOfAnimals

// Casting example. F# is quite strict on not automatically casting, but there are some ways to reduce the hassle of this, which we'll get into later. 
let realPercentageOfBirds = float 2 / float numberOfAnimals

let percentageOfBirds2 = 2.0 / 3.0 

let catName = "Lyra"

// String formatting and printing. 
// If you use sprintf and printf and printfn, you get type checking on the parameters
// The different patterns availalble for use can be found here: 
// https://msdn.microsoft.com/en-us/visualfsharpdocs/conceptual/core.printf-module-[fsharp]
let theBirds = sprintf "The birds are %s and %s" "Jack" "Whiskey"

printfn "We have %i animals. The cat is %s. %s" numberOfAnimals catName theBirds

(*
    Its possible to use a normal string as an identifier, but its more difficult to access these values unless they are in a module
    (Modules will be explained later)
    Because of this, these type of names are usually used in things like tests, where you would like a easier to read name, but 
    you aren't going to reference the value else where 
*)
let ``Your animals get along?`` = false

 
(* Tuples
     Note the comma!

    These are Reference types. These can take a number of elements (I don't know the limit) and the types each element does not have to match
    they are used when a grouped return type makes sense, or when data is in a obvious logical pairing
    Commas are used between the elements

    https://docs.microsoft.com/en-us/dotnet/fsharp/language-reference/tuples
*)

let birds = ("Jack", "Whiskey")

let mixedTuple = (1, "Jack", true) 

//These are structs (Value tupes)
let mixedStruct = struct (2, "Whiskey", 1.0f)

(* Lists
    Note the semicolon
    These are a ordered immutable series. Implemented as a single linked list
    Because of this remember that getting the first element of the list is O(1), but getting the last is O(list length)
    List tend to be used when you are expected to iterate the forward in the series (Usually all of it) starting from the head

    https://docs.microsoft.com/en-us/dotnet/fsharp/language-reference/lists
    The documentation will mention a lot of functions that work with lists. We'll talk more about them in the next sessions
*)

let empty = []

let animals = [ "Jack"; "Lyra"; "Whiskey"]

let numbers = [
    1
    2
    3
]

// Here we have the cons (::) operator it attaches a single element to the head of the list. 
let zeroAndNumbers = 0 :: numbers

// The append operator (@) returns the elements of the first list followed by the elements of the second
let moreNumbers = numbers @ [4;5;6]

//Accessing indivudal elements. Operation is O(index)
let three = numbers.[2]

// List comprehension: Range syntax. This can create an increasing or decreasing series
let generatedList = [ 0 .. 100 ] 
let evens = [ 0 .. 2 .. 100 ] 
let chars = ['a' .. 'z']

(*
    List comprehension: Generator syntax. 
    Note: In the session I mentioned that this is a use of the yield from sequences: 
    I may have been mistaken from that, as I can't find a reference for it
    
    The general syntax is "for {element} in {range|list|array|sequence} do {expression that yields elements} "
*)
let squares = [ for x in evens do yield x * x ]

// If you are yielding a single element, you an use the -> instead of do yield
let squaresV2 = [ for x in evens -> x * x ]

// If we need to yield multiple elements at once, use yield! (Usually said as yield bang).
// The output will be a list of single elements
let alternatingSquares = [for x in 0 .. 10 do yield! [x; x*x]]

// After do, we can use most other expressions as long as we eventually yield an element. 
// In this case, we use an if expression, but we can also do pattern matching
let buzzing = 
    [ for elem in 0 .. 100 do 
        // mod operator is %
        if elem % 3 = 0 then 
            yield (sprintf "%i: Buzz" elem) ]

(* Arrays
    Note that arrays use [| |] while a list is []
    Essentials are the same as arrays in other languages. These are mutable, not immutable. 
    https://docs.microsoft.com/en-us/dotnet/fsharp/language-reference/arrays
*)

let emptyA = [||]

let helloAll = [| "Hello"; "Everyone"; "Out"; "Tehre" |]

// We can mutate elements using <- instead of = . This is because we want mutation to be obvious
helloAll.[3] <- "There"

// Comprehensions are the same as the comprehenstions for a list, but surounded by the array syntax
let arraySquares =  [| for x in 0 .. 100 do yield x * x |]

// Array slices
let slides = arraySquares.[0..3]

(* Sequences
    Logicial series of elements. Represented by seq<'T> which is an alias for the general  .net System.Collections.Generic.IEnumerable
    Elements can be computed only when required, so they can be lazy or infinite. 
    https://docs.microsoft.com/en-us/dotnet/fsharp/language-reference/sequences
*)


let emptySequence = Seq.empty

// You can yield indiviual elements
let helloWorld = seq { yield "Hello"; yield "World" }

// YOu can use range generators as above
let numbersSeq = seq { 1 .. 100 }

// Comprhensions work in the same way as above, but may be lazy
// Highlight this declaration (The 4 lines of squaresSeq) and press ALT - ENter to send it to the fsharp interactive
// then type squaresSeq;;
// This will only print the first five elements. If you want more you have to explicitly iterate the sequence. 
let squaresSeq = seq { 
    for x in 0 .. 100 do 
        printfn "%i" x
        yield x * x }

//Infinite Sequence
//This is the simplest way to create a truely infinite sequence. 
// Note there is also a function Seq.initInfinite, but that has a limit of Int32.MaxValue. 
let result = 0L |> Seq.unfold (fun i -> Some(i, i + 1L))



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

(*Expressions
    Now that we've looked at basic functions, now we look at making choices
*)

//If expression
//Indentation is important.  
// The final value of each branch is the return value. all branches must return the same type
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
    // We can match using a guard (A boolean condition). We can give the value we match against a nicer name to make things readable
    | negative when x < 0 -> sprintf "Negative number: %i" negative
    // We can match on specific values
    | 0 -> "Found Zero"
    | 1 -> "Found One"
    // If we match on a guard and use the same name as the input, it will be the same value. 
    | x when x < 3 -> "Found Two"
    // All possible values must be dealt with. 
    // The default, or otherwise case can be match using simply _ if you don't care about the value, or a value name if you will use it.
    | _ -> "I don't care about this number"


let animalTest name =
    match name with
    | "Lyra" -> "My cat is Lyra"
    | "Whiskey" -> "My grey bird is Whiskey"
    | _ -> "That isn't my pet"
    | "Jack" -> "My yellow bird is Jack"
    //Hey what about jack? IF you have another case after the default case, you will get a warning to say it will not be hit. 

// Destructuring
let name, age = ("Jason", 26)

let tupleTest tuple = 
    match tuple with 
    //Here we access the fist and second element using functions fst and snd
    | ("Jason", 26) -> sprintf "Found known person: %s, who is %i" (fst tuple) (snd (tuple))
    // We can also match on one part instead  of  both
    | (name, 26) -> sprintf "Found Unknown person: %s, who is 26" name
    | (name, age) ->  sprintf "Found someone unkown: %s, who is %i" name age

// We can match against list and arrays in essentially the same way, but not sequences, so we will talk about them later. 
// We can match against specific elements, the empty list, or parts of the list
// Below are some of the patterns we can use

let listTest list = 
    match list with 
    | [] -> "Nothing"
    // Requires the list to have one element. 
    | [ "Lyra"] -> "Cat"
    | ["Whiskey"; "Jack"] -> "Two Birds"
    // Requires the list to start with "Lyra" and have 2 elements
    | [ "Lyra"; _ ] -> "Cat and someone Else"
    // Requires a list with atleast 2 elements
    | x::y::rest -> "At least I have the birds"
    | _ -> "I don't care"
