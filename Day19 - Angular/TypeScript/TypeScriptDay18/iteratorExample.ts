const fruits: string[] = ['apple', 'Aalu', 'Banana'];
for(const fruit of fruits){
    console.log(fruit);

}

const scores1 = new Map([
    ['raju', 75], 
    ['Babu', 45],
    ['shyam', 65]
]);

for(const [name, score] of scores1){
    console.log(`${name}: ${score}`);
}


//const uniqueNumber = new set
let numberSet = new Set<number>();
numberSet.add(1);
numberSet.add(2);
numberSet.add(3);
console.log(numberSet);