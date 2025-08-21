let scores = new Map();
//set values
scores.set("Raju", 95);
scores.set("Shyam", 25);
scores.set("Baburao", 45);
//get a value by key
console.log(scores.get("Baburao"));
console.log(scores.has("raju"));
//Iterate over entris  --for of loop
for (let [name, score] of scores) {
    console.log('${name} scored ${scores}');
}
//romove
scores.delete("Shyam");
//map size
console.log(scores.size);
//clear all entries
scores.clear();
console.log(scores.size);
