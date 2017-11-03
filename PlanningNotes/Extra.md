
let factorialTail x = 
    let rec factorialT total n = 
        match n with 
        | 0 -> total
        | n -> factorialT (total*n) (n-1)        
    factorialT 1 x    


let fibTail x = 
    let rec fibT value1 value2 fibNumber = 
        match fibNumber with
        | 0 -> value1
        | fibNumber -> fibT value2 (value1 + value2) (fibNumber - 1)
    fibT 0 1 x    