function optionalPow(a, b) {
    let button = "CANCEL"
    var result
    if (button == "OK")
    {
        var result = Math.pow(a, b)
    }
    else if (button == "CANCEL")
    {
        var result = Math.pow(b, a)
    }
    return result
}

console.log(optionalPow(2, 3));