"use strict";
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
const myPromise = new Promise((resolve) => {
    setTimeout(() => {
        const randomNumber = Math.random();
        resolve(randomNumber);
    }, 3000);
});
myPromise.then((number) => {
    console.log('Сгенерированное число:', number);
});
function createPromise(delay) {
    return new Promise((resolve) => {
        setTimeout(() => {
            const randomNumber = Math.random();
            resolve(randomNumber);
        }, delay);
    });
}
const promises = [
    createPromise(3000),
    createPromise(3000),
    createPromise(3000)
];
Promise.all(promises)
    .then((results) => {
    console.log('Все сгенерированные числа:', results);
});
let pr = new Promise((res, rej) => {
    rej('ku');
});
pr
    .then(() => console.log(1))
    .catch(() => console.log(2))
    .catch(() => console.log(3))
    .then(() => console.log(4))
    .then(() => console.log(5));
const promise = Promise.resolve(21);
promise
    .then((num) => {
    console.log(num);
    return num;
})
    .then((num) => {
    console.log(num * 2);
});
function proFunc() {
    return __awaiter(this, void 0, void 0, function* () {
        const firstResult = yield promise;
        console.log(firstResult);
        const secondResult = firstResult * 2;
        console.log(secondResult);
        // catch (error) {
        //   console.error('Ошибка:', error);
        // }
    });
}
proFunc();
