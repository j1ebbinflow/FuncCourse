(*
    All about Types
*)

(* 
    Types that we've already talked about
*)

(*
    Unit type. 
    Kind of like void, but an actual value. All functions / expressions must have an output. 
*)

let thisIs : unit = ()
open Microsoft.Win32.SafeHandles
open System
open System.Runtime.InteropServices
open System.Collections.ObjectModel.ReadOnlyDictionary
open System.Collections.ObjectModel.ReadOnlyDictionary

let printPositiveOrNegative x = 
    if x > 0 then
        printfn "Positive: %i" x
        ()
    else     
        printfn "Negative: %i" x // Printfn returns unit so you don't actually have to do this.   

//Ignoring a value
let ignoredValue = ignore "Javascript"

(*
    Primative types
*)

let name = "Mary"
let age = 22
let percentage = 0.8
let highPercentage = true


(*
    Function types
*)

let isSuperMajority (percentage: float) : bool = percentage > 0.66

(*
    List, Array, Seq
*)

//List
let animals : string list= [ "Jack"; "Lyra"; "Whiskey"]

//Array
let helloAll : int array = [| 1 ; 2 |]

// We can use both versions of a type declaration depending on preference
let helloAllTwo : array<int> = [| 1 ; 2 |]

// Seq
let helloWorld = seq { yield "Hello"; yield "World" }

(*
    The new stuff
*)

(*
    Type abbreviations : These are just aliases (Erased types)
    No type safety, just to help clarify code
*)

type RealNumber = float
type NumberList = float list

// Actaully still a float list so we can use normal list functions without change
let sumNumbers (list : NumberList) = list |> List.sum

let numbers : NumberList = [ 0.0; 1.0; 2.0 ]

let floats = [ 3.0; 4.0; 5.0; ]

// Both work fine
let sumOne = sumNumbers numbers
let sumtwo = sumNumbers floats

(*
    Tuples And Records - Product types 
    These types give us values which can be described as typeOne AND typeTwo etc
*)

(*
    Tuples
    We've already seen these
    Notice the type are declared as a product of the types of the values
    Individual types can't be given a name. Order of product is important
    Remember tupes use commas, all others use semicolons

    Normally used when a lightweight type without names is obvious
*)

let numberAndPositive: int * bool= (1, true)

//If only we could give these names
let lyraTuple = ("Lyra", "Feline", 13.5, "Sleeping", "Sleeping", 0)

// Deconstruction
let number, isPositive = numberAndPositive

let name, _ , age, _, _, _ = lyraTuple

// Structural equality is automatic (Also applies to lists)
let listOne = [(1, 10); (2,20)]
let listTwo = [(1, 10); (3,30)]

let comparison = listOne = listTwo

let comparison' = (1, 10) = (1, 10)

(*
    Record: Basically a tuple with labels for the elements
    Semicolons between each element
*)

type ComplexNumberRecord = { real: float; imaginary: float }

let myComplexNumber = { real = 1.1; imaginary = 2.2 } 

// Must construct with all values the following gives an error
//let myComplexNumber2 = { real = 1.1 } 

type Pet = {
    Name: string
    Species: string
    Age: float
    CurrentStatus: string
    FavouriteActivity: string
    NumberOfAnimalFriends: int
}

let lyra = {
    Name = "Lyra"
    Species = "Feline"
    Age = 13.5
    CurrentStatus = "Sleeping"
    FavouriteActivity = "Sleeping"
    NumberOfAnimalFriends = 0
}

// Deconstruction 
let { real=realPart; imaginary=imaginaryPart } = myComplexNumber

// Can ignore one value if needed
let { real=_; imaginary=imaginaryPart' } = myComplexNumber

// Or just leave them out
let { imaginary=imaginaryPart'' } = myComplexNumber

// Or access with dot notation
let realNum = myComplexNumber.real

//Naming conflict

type Person = { Name : string; Age : int }
type Animal = { Name : string; Age : int }

//When there is a naming conflict, the most recent declaration will be used, or a warning will be shown. 
//In this case, we can specify which one we are looking to use: 

let whiskey = { Animal.Name = "Whiskey"; Age = 1 }

//Copy and Update
// Manual
let whiskeyOlder = { Name = whiskey.Name; Age = whiskey.Age + 1 }

//Shorthand
let whiskeyOlder' = { whiskey with Age = whiskey.Age + 1 }

let firstNumber = { real = 1.1; imaginary = 2.2 } 
let secondNumber = { real = 1.1; imaginary = 2.2 } 

let thirdNumber = { real = 2.0; imaginary = 3.0 } 

// Structual Equality is implemented by default
let equal = firstNumber = secondNumber

let equal = firstNumber = thirdNumber

(*
    Discriminated Unions - Sum Types
    Gives that values that can be described as typeOne OR typeTwo ...
    A labeled union. Each union case has a identifier, which must start with a upper case letter
*)

//Cases can just be named

type Signed =  Positive | Zero | Negative

// Or some can have values (And they can be recursive)
type StringTree =
  | Node of string * StringTree * StringTree
  | Leaf

type SignedInt = 
    | Positive of int
    | Negative of int
    | Zero 

let signedInteger = Positive 12

let printSign x = 
    match x with 
    | Positive value -> printfn "Positive number: %i" value
    | Negative value -> printfn "Negative number: %i" value
    | Zero -> printfn "Value is zero"










