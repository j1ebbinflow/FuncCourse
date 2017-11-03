
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



let factorialTail x = 
    let rec factorialT total n = 
        match n with 
        | 0 -> total
        | n -> factorialT (total*n) (n-1)        
    factorialT 1 x    



let rec fib x = 
    if x < 2 then 
        1 
    else 
        fib (x-2) + fib(x-1)

let fibTailR x = 0    










    

let fibTail x = 
    let rec fibT value1 value2 fibNumber = 
        match fibNumber with
        | 0 -> value1
        | fibNumber -> fibT value2 (value1 + value2) (fibNumber - 1)
    fibT 0 1 x    

let directions = ["NORTH";"SOUTH";"SOUTH";"EAST";"WEST";"NORTH";"NORTH";"WEST";"SOUTH"]
// https://www.codewars.com/kata/550f22f4d758534c1100025a

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

//The following two are the same, so why bother with the first one?
let square1 = fun x -> x * x
let square2 x = x * x

let peopleWithFirstNamesWeHaveEnoughOf = 
    ["Matthew One"; "Matthew Two"; "Matthew Three"; "Matthew Four";
        "John One"; "John Two";
        "Steven One"; "Stephen Two";
        "Jason One"]

let firstNamePredicate (fullNameList: string list) fullName = 
    let rejectedNames = 
        printfn "Creating"
        fullNameList 
        |> List.map (fun x -> 
            let names = x.Split ' ' 
            names.[0])
        |> List.distinct   
        |> Set.ofList     
    rejectedNames.Contains fullName
    
let firstNamePredicate' (fullNameList: string list) = 
    let rejectedNames = 
        printfn "Creating"
        fullNameList 
        |> List.map (fun x -> 
            let names = x.Split ' ' 
            names.[0])
        |> List.distinct   
        |> Set.ofList     
    fun (fullName: string) ->  rejectedNames.Contains fullName

(*Memoisation via closures*)

// let dict = new System.Collections.Generic.Dictionary<_,_>()
let rec fib x = 0; 














let fibCached = 
    let dict = new System.Collections.Generic.Dictionary<_,_>()
    let rec fibR n = 
        match dict.TryGetValue(n) with
        | true, value -> value
        | false, _ ->
            let newValue = 
                match n with
                | 0 -> 0
                | 1 -> 1
                | _ -> fibR (n - 1) + fibR (n - 2)
            dict.Add(n, newValue)
            newValue
    fibR        

(*Active Patterns*)
// Taken from : https://docs.microsoft.com/en-us/dotnet/fsharp/language-reference/active-patterns#parameterized-active-patterns

// Basic Active Pattern

let (|Even|Odd|) input = if input % 2 = 0 then Even else Odd

let TestNumber input =
   match input with
   | Even -> printfn "%d is even" input
   | Odd -> printfn "%d is odd" input

// Deconstructing Active Pattern

open System.Drawing

let (|RGB|) (col : System.Drawing.Color) =
     ( col.R, col.G, col.B )

let (|HSB|) (col : System.Drawing.Color) =
   ( col.GetHue(), col.GetSaturation(), col.GetBrightness() )

let printRGB (col: System.Drawing.Color) =
   match col with
   | RGB(r, g, b) -> printfn " Red: %d Green: %d Blue: %d" r g b

let printHSB (col: System.Drawing.Color) =
   match col with
   | HSB(h, s, b) -> printfn " Hue: %f Saturation: %f Brightness: %f" h s b

let printAll col colorString =
  printfn "%s" colorString
  printRGB col
  printHSB col

printAll Color.Red "Red"
printAll Color.Black "Black"
printAll Color.White "White"
printAll Color.Gray "Gray"
printAll Color.BlanchedAlmond "BlanchedAlmond"

// Partial Active Pattern

let (|Integer|_|) (str: string) =
   let mutable intvalue = 0
   if System.Int32.TryParse(str, &intvalue) then Some(intvalue)
   else None

let (|Float|_|) (str: string) =
   let mutable floatvalue = 0.0
   if System.Double.TryParse(str, &floatvalue) then Some(floatvalue)
   else None

let parseNumeric str =
   match str with
     | Integer i -> printfn "%d : Integer" i
     | Float f -> printfn "%f : Floating point" f
     | _ -> printfn "%s : Not matched." str

// Parameterized (Partial) Active Patterns
open System.Text.RegularExpressions

// ParseRegex parses a regular expression and returns a list of the strings that match each group in
// the regular expression.
// List.tail is called to eliminate the first element in the list, which is the full matched expression,
// since only the matches for each group are wanted.
let (|ParseRegex|_|) regex str =
   let m = Regex(regex).Match(str)
   if m.Success
   then Some (List.tail [ for x in m.Groups -> x.Value ])
   else None

// Three different date formats are demonstrated here. The first matches two-
// digit dates and the second matches full dates. This code assumes that if a two-digit
// date is provided, it is an abbreviation, not a year in the first century.
let parseDate str =
   match str with
     | ParseRegex "(\d{1,2})/(\d{1,2})/(\d{1,2})$" [Integer m; Integer d; Integer y]
          -> System.DateTime(y + 2000, m, d)
     | ParseRegex "(\d{1,2})/(\d{1,2})/(\d{3,4})" [Integer m; Integer d; Integer y]
          -> System.DateTime(y, m, d)
     | ParseRegex "(\d{1,4})-(\d{1,2})-(\d{1,2})" [Integer y; Integer m; Integer d]
          -> System.DateTime(y, m, d)
     | _ -> System.DateTime()

let dt1 = parseDate "12/22/08"
let dt2 = parseDate "1/1/2009"
let dt3 = parseDate "2008-1-15"
let dt4 = parseDate "1995-12-28"