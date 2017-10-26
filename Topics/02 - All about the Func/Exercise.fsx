let test = "The quick brown fox jumps over the lazy dog"

let chars = ['a' .. 'z']

let panagram' =
    ['a' .. 'z']
    |> List.map (string >> test.Contains) 
    |> List.reduce (&&) 


let rec panagramRec inputString = true

let rec sumList list = 0

// Reimplementing standard library functions

//TODO Reimplement List.map -> https://msdn.microsoft.com/en-us/library/ee370378.aspx
// Map takes a function and a list, applies the function to each element in the list, and returns a new list of the results. 
let rec mapList mapping list = []
//TODO Reimplement List.filter -> https://msdn.microsoft.com/en-us/library/ee370294.aspx
// Fold takes a function to test each list element (function returns a boolean) and a list. Returns a new list containly only the elements for which the predicate returned true. 
let rec filterList predicate list = []
//TODO Reimplement List.fold -> https://msdn.microsoft.com/en-us/library/ee353894.aspx
// Takes a function to update the accumulated values given the input element, the initial value, and the list. returns the final accumulated value
let rec foldList folder acc list = []
//TODO Reimplement List.reduce
// Takes a function to update the accumulated values given the input element and the list. returns the final accumulated value
//Throws an ArgumentException if the list is empty (Use invalidArg function -> https://msdn.microsoft.com/en-us/library/dd233178.aspx ) 
let rec reduceList reducer list = []

//Using 