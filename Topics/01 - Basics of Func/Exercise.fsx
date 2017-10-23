(*
    Exercise 01
    Things Covered: 
        Values
        Basic Data structures
        Comprehensions
        Basic function syntax
        Basic Pattern Matching
    Note: 
        This file is best viewed in VS code or visual studio. though in visual studio, you will need to hover over a declaration to see the type.
        It does seem like visual studio have a better colour highlighting.
        In Vscode, make sure you have Ionide installed

    The following can be completed by executing things in the Fsharp interactive (FSI)
    To send something to the interactive, highlight the full declaration (And anything the declaration uses)
    and press Alt-Enter. 
    If you need to update a term, you can modify it and send it to the interactive again. 
    If you want to enter anything in the interactive, you need to end with ";;" to have it executed. 
    This is so that you can enter multiple lines without a delimiter.
    Remember indentation is important here and in the interactive as well!

    Remember to use the discussion code for help, or ask a question in the slack group
*)

//Change this to the name we have too many of
let nameWeHaveTooManyOf = ""

// Change this to declare the number of Matts in the office
let numberOfMatts = 0

// Calculate the percentage of matts in the office
// You can cast using: float 0 or just use 0.0 to use a float instead
let percentageOfMatts = 0

// Use sprintf to make a formatted string about the number and percentage of matts in the office
let commentAboutToManyMatts = ""

// Use the range list comprehension to generate a decreasing list of odd numbers below 100
// Hint: You will need to include a step
let decreasingOddNumbers = []

// Use the generator syntax to create a array of numbers below 1000 containing 3
// Note, When you call the ToString() of a number, you will have to leave a space in between, like: 100 .ToString()
let numbersWithThreeInThemBelow1000 = [||]

(*
    Using pattern matching and the sequence generator syntax, create a lazily generated series starting from 0 to 1000000
    With each number followed by the number multipled by 10 and the number multipled by 100
    However, if the number is zero, it should only have zero generated,
    And a multiple of ten should just have the value. 
    An output would look like: 0; 1; 10;100; 2;20;200; ..... 10; 11;110;1100 etc 

    You will need to use yield! (yield bang)

    To Test this use Seq.take {numberOfElements}. See below.
*)
let numbersTensAndHundreds = seq { yield 0 }

//Note, with Seq.take, if the sequence doesn't have enough elements, it will give an error
let first100 = Seq.take 100 numbersTensAndHundreds

// Create a function that returns (a + b) / c
let calculate a b c = 0

// Create a function that returns the max of the 4 inputs. 
let max4 a b c d = 0

// Create a function that returns a list of tuples, with the first tuple containing the
// higher two numbers, and the second containing the lowest two. Both pairs should have the 
// higher number first. 
// Decide what to do in special cases
let pairNumbers a b c d = 0

// Create a function that is a more generic version of fizz buzz.

// The function should return a a list of strings based on the range {start} and {finish}
// If the number doesn't match any conditions, return the number to string. 
// If the number is a multiple of multipleOfA, return stringIfMultipleOfA
// If the number is a multiple of multipleOfB, return stringIfMultipleOfB
let genericFizzBuzz (multipleOfA, stringIfMultipleOfA: int*string) (multipleOfB, stringIfMultipleOfB: int*string) start finish = []



