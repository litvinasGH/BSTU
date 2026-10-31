let a = 5
console.log(typeof a, a)
let namee = "Name"
console.log(typeof namee, namee)
let i = 0
console.log(typeof i, i)
let double = 0.23
console.log(typeof double, double)
let result = 1/0
console.log(typeof result, result)
let answer = true
console.log(typeof answer, answer)
let no = null
console.log(typeof no, no)

// let A = 45*21
// let B = 5*5
let count = 0
for(var il = 5; il <= 45; il+=5){
    for(var li = 5; li <= 21; li+=5){
        count++;
    }
}
console.log(typeof count, count)

let ii = 2
let aa = ++ii;
let bb = ii++;
console.log(ii, aa, bb)
console.log(' a<b   a>b   a=b')
console.log(aa < bb, aa > bb, aa == bb);

console.log("Котик" == "котик"? "Равно" : "Котик" > "котик"? "Больше" : "Меньше");
console.log("Котик" == "китик"? "Равно" : "Котик" > "китик"? "Больше" : "Меньше");
console.log("Кот" == "Котик"? "Равно" : "Кот" > "Котик"? "Больше" : "Меньше");
console.log("Привет" == "Пока"? "Равно" : "Привет" > "Пока"? "Больше" : "Меньше");
console.log(73 == "53"? "Равно" : 73 > "53"? "Больше" : "Меньше");
console.log(false == 0? "Равно" : false > 0? "Больше" : "Меньше");
console.log(false == 0.54? "Равно" : false > 0.54? "Больше" : "Меньше");
console.log(0.54 == true? "Равно" : 0.54 > true? "Больше" : "Меньше");
console.log(123 == false ? "Равно" : 123 > false ? "Больше" : "Меньше");
console.log(true == "3" ? "Равно" : true > "3" ? "Больше" : "Меньше");
console.log(3 == "5мм" ? "Равно" : 3 > "5mm" ? "Больше" : "Меньше");
console.log(8 == "-2" ? "Равно" : 8 > "-2" ? "Больше" : "Меньше");
console.log(34 == 34 ? "Равно" : 34 > 34 ? "Больше" : "Меньше");
console.log(null == undefined ? "Равно" : null > undefined ? "Больше" : "Меньше");

const teacherFullName = "Алина Николаевна Щербакова";
let userName = prompt("Введите свое имя:");

if (!userName) {
    alert("Вы не ввели свое имя.");
} else if (teacherFullName.toLowerCase().includes(userName.toLowerCase())) {
    alert("Введенные данные верны!");
} else {
    alert("Введенные данные неверны.");
    window.location.reload();
}


function check_exams(rus, math, eng) {
    const Pass = 60;
    let exams = [rus, math, eng];
    let fail_counter = 0;
    for (let i = 0; i < 3; i++) {
        if (exams[i] < Pass) {
            fail_counter++;
        }
    }
    if (fail_counter === 0) {
        return "Здал";
    }
    else if (fail_counter === 1) {
        return "Перездача";
    }
    else {
        return "Завалил";
    }

}
console.log(check_exams(100, 100, 100));
console.log(check_exams(100, 50, 60));
console.log(check_exams(30, 20, 40));


console.log(true + true);
console.log(0 + "5");
console.log(5 + "мм");
console.log(8 / Infinity);
console.log(9 * "\n9");
console.log(null - 1);
console.log("5" - 2);
console.log("5px" - 3);
console.log(true - 3);
console.log(7 || 0);

function Is_chet(num) {
   return num % 2 == 0
}
let array = new Array(10);
for (let i = 1; i <= 10; i++) {
    if(Is_chet(i)){
        array[i-1] = (i+2)+"";
    }
    else{
        array[i-1] = i+""+"мм";
    }
}
console.group("Массив");
for (let i = 0; i < 10; i++) {
    console.log(array[i]);
}
console.groupEnd();


const Days_obj = {
    1:"ПН",
    2:"ВТ",
    3:"СР",
    4:"ЧТ",
    5:"ПТ",
    6:"СБ",
    7:"ВС"
}
let number_of_day = parseInt(prompt("Ввод дня:"));
if(number_of_day>=1&&number_of_day<=7){
    alert(`Номер ${number_of_day} это ${Days_obj[number_of_day]}`)
}
else{
    alert("Error");
}
let Days_str = ["ПН","ВТ","СР","ЧТ","ПТ","СБ","ВС"];
let day_number = parseInt(prompt("Ввод:"));
if(day_number>=1&&day_number<=7){
    alert(`Номер ${day_number} это ${Days_str[day_number-1]}`);
}
else{
    alert("Error");
}



function form_string(first="Привет", second, third)
{
    return first+second+third;
}
let third_parm = prompt("Ввод:");
console.log(form_string(undefined,", ",third_parm));


function params(a, b){
    if (a == b){
        return 4 * a
    }
    else{
        return a*b
    }
}

const paramsE = function(a, b) {
    if (a == b) {
        return 4 * a;
    } else {
        return a * b;
    }
};

const paramsA = (a, b) => a == b ? 4 * a : a * b;

a = 10;
b = 10;
console.log(params(a, b))
b = 20
console.log(paramsE(a, b))
a = 20
console.log(paramsA(a, b))
