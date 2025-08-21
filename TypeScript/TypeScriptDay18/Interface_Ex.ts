interface User {
    name: string;
    age: number;
};
const alice: User = {
    name:'Raju',
    age: 30
};
console.log(`User: ${alice.name}, Age: ${alice.age}`);