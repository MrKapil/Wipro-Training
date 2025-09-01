let Person: [string, number] = ["Baburao", 30];
let cordinates: [number, number] = [10,20];

function displayTuple(tuple: [string, number]){
    console.log(`Name: ${tuple[0]}, Age: ${tuple[1]}`);

}
displayTuple(Person);
console.log(`Cordinates: (${cordinates[0]}, ${cordinates[1]})`);