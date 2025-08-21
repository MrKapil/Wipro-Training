class Trainer
{
    name:string;
    exprience:number;

    constructor(name:string, exprience: number)
    {
        this.name = name;
        this.exprience = exprience;
    }
    introduce(): void{
        console.log('hi, i"am ${this.name} with ${this.experience} years of exprience');
    }
    claculateSession(): number{
        return this.exprience * 75;
    }
}
const Baburao = new Trainer("baburao", 16);
Baburao.introduce();
console.log(Baburao.claculateSession());