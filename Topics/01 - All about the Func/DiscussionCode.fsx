(* Values
    Basic Values.
    Use Let to define values
*)
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

let three = numbers.[2]

let generatedList = [ 0 .. 100 ] 

let events = [ 0 .. 2 .. 100 ] 

let squares = [ for x in 0 .. 100 do yield x * x ]

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

let 




