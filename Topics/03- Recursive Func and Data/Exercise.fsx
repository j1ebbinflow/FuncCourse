// If you haven't already, write the factorial function in a tail recursive manner, using an accumulator variable. 
let rec factorial x = 0
//Rewrite the factorial function in a tail recursive manner, using continuation passing
let rec factorial' x = 0

let rec fib x = 
    if x < 2 then 
        1 
    else 
        fib (x-2) + fib(x-1)

//Rewrite the fibonacci function in a tail recursive manner (Only try continuation passing if you feel like it)
let rec fibTail x = 0    

// Solve this code wars problem recursively: // https://www.codewars.com/kata/550f22f4d758534c1100025a
let exampleDirections = ["NORTH";"SOUTH";"SOUTH";"EAST";"WEST";"NORTH";"NORTH";"WEST";"SOUTH"]

let reduceDir x = []

// ReImplement the library functions using recusion. modify the signature as necessary 
// (Only try continuation passing if you feel like it, its probably easier to do this using an accumulator)
// Use a seperate value so that you can compare to the real signature 
let functionToCopy = List.groupBy

let functionToCopy2 = List.collect

let functionToCopy3 = List.windowed


// Using sequences, generate numbers using the transformation y = sin(x) + log(x) and the range beteen start and finish (inclusive) with a step
let generator start finish step = "TODO"

// Using recursion, manually create a generator for a sequence of numbers using the transformation  y = sin(x) + log(x)
// Don't use sequences!
// This should require a series of function calls by the user to build up the sequence. 
// Remember that you need some way to tell the function caller that there are no more values in the sequence. 

let generator' start finish step = "TODO"

// Generalise the function above so that it can work on any transformation function.
// You will need to modify the signature as well. 
let generator'' start finish step = "TODO"

// Generalise the function above further so that it can produce an infinite series
// You will need to modify the signature as well. 
let generator''' start finish step = "TODO"
