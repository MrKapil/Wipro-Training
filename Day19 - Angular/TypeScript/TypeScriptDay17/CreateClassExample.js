var Trainer = /** @class */ (function () {
    function Trainer(name, exprience) {
        this.name = name;
        this.exprience = exprience;
    }
    Trainer.prototype.introduce = function () {
        console.log('hi, i"am ${this.name} with ${this.experience} years of exprience');
    };
    Trainer.prototype.claculateSession = function () {
        return this.exprience * 75;
    };
    return Trainer;
}());
var Baburao = new Trainer("baburao", 16);
Baburao.introduce();
console.log(Baburao.claculateSession());
