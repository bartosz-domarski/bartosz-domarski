function fixage(array) {
    let result = []
    for (let i = 0; i < array.length; i++) 
    {
        if (array[i] >= 18 && array[i] <= 60) 
        {
            result.push(array[i])
        }
    }
    if (result.length > 0)
        return result
    else
        return "NA" 
}

console.log(fixage([18,3,30,22,11,60]))