function basicOperations(operation,value1,value2){
    switch(operation){
        case '+':return value1+value2; 
        case '-':return value1-value2;
        case '*':return value1*value2;
        case '/':return value1/value2;
        default:return"Я не могу этого сделать =(";
    }
}


console.group("Первое задание");
console.log(basicOperations('+',3,2));
console.log(basicOperations('-',5,2));
console.log(basicOperations('*',6,8));
console.log(basicOperations('/',50,0));
console.groupEnd();


function cubic(number){
    let sum = 0;
    for(let i = 1;i<=number;i++){
        sum+=Math.pow(i,3);
    }
    return sum;
}
console.log(cubic(10));

function mid(arr){
    let length = arr.length;
    let sum = 0;
    for(let i=0;i<length;i++){
        sum+=arr[i];
    }
    return (sum/length).toFixed(5);
}

console.log(mid([1,2,3,4,5,6]));


function invert(str){
    let result = "";
    for(let i=str.length-1;i>=0;i--){
        if((str[i]>='A'&&str[i]<='Z')||(str[i]>='a'&&str[i]<='z')){
            result+=str[i];
        }
        
    }
    return result;
}

console.log(invert("JavaScr53э? ipt"));


function dublicate(str,num){
    let result = "";
    for(let i=0;i<num;i++){
        result+=str;
    }
    return result;
}

console.log(dublicate("hello ",2));

function subtracting(arr1,arr2){
    let arr3 = [];
    let buf = 0;
    let length = arr1.length;
    for(let i=0;i<length;i++){
        if(!(arr2.includes(arr1[i]))){
            arr3[buf] = arr1[i];
            buf++;
        }
    }
    return arr3;
}

console.log(subtracting(["Привет","мир","js"],["Как","дела","js"]));