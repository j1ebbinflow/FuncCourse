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
let rec panagramRec inputString = true

//Implement a function that recursively sums a list of functions. 
let rec sumList list = 0

// Reimplementing standard library functions
// NOTE: The answers for these functions can be easily found online, so don't search for them until you have given it a try and you've reached trouble. 

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

//Using recursion, group an input list of numbers by the final digit. 
// Hint: To make things easier, you may want to use an array as apart of the output, but using a list or array is acceptable. 
let rec groupByFinalDigit list = [||]

// Using any approach (Recursion, Function composition or higher order functions), create a function that returns the squares of the positive numbers in a list
let squares = []

// Using any approach (Recursion, Function composition or higher order functions), create a function that returns the doubles (*2) of the positive numbers in a list
let doubles = []

(*
    Using the functions above, create a function that takes in an input list of numbers and does the following
    - Groups the numbers by the final digit
    - For each group, if the final digit of all the numbers are even, the numbers are squared. If the final digit of all the numbers are odd, the numbers are doubled
    - For each group, the numbers are summed 
    - The square root of each sum is taken
    - The largest and smallest square root is returned, with the digit of the original group the square root came from. 
*)
let doABunchOfMathsStuff = []
   
