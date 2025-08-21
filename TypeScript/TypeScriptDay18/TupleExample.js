var Person = ["Baburao", 30];
var cordinates = [10, 20];
function displayTuple(tuple) {
    console.log("Name: ".concat(tuple[0], ", Age: ").concat(tuple[1]));
}
displayTuple(Person);
console.log("Cordinates: (".concat(cordinates[0], ", ").concat(cordinates[1], ")"));
