interface Person{
    name: string;
}
interface Employee extends Person {
    employeeID: number;
}

const alice25: Employee = {
    name: "Raju",
    employeeID: 12654
}
console.log(`Employee: ${alice25.name}, id: ${alice25.employeeID}`);

    //output:- Employee: alice