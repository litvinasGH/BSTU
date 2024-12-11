//f1
console.log("Фрагмент 1");
const set = new Set([1, 1, 2, 3, 4]);
console.log(set);

//f2
console.log("Фрагмент 2");
const name = "Lydia";
age = 21;
console.log(delete name);
console.log(delete age);

//f3 
console.log("Фрагмент 3");
const numbers = [1, 2, 3, 4, 5];
const [y] = numbers;
console.log(y);

//f4
console.log("Фрагмент 4");
const user = {
    name: "Lydia",
    age: 21
};
const admin = {
    admin: true,
    ...user
};
console.log(admin);

//f5 !
console.log("Фрагмент 5");
const person = {
    name: "Lydia"
};
Object.defineProperty(person, "age", {value: 21});
console.log(person);
console.log(Object.keys(person));

//f6 !
console.log("Фрагмент 6");
const a = {};
const b = {key: "b"};
const c = {key: "c"};
a[b] = 123;
a[c] = 456;

console.log(a[b]);

//f7 !
console.log("Фрагмент 7");
let num = 10;
const increaseNumber = () => num++;
const increasePassedNumber = number => number++;
const num1 = increaseNumber();
const num2 = increasePassedNumber(num1);
console.log(num1);
console.log(num2);

//f8
console.log("Фрагмент 8");
const value = {number: 10};
const multiply = (x = {...value}) => {
    console.log((x.number *= 2));
};
multiply();
multiply();
multiply(value);
multiply(value);

//f9
console.log("Фрагмент 9");
[1, 2, 3, 4].reduce((x, y) => console.log(x, y));





