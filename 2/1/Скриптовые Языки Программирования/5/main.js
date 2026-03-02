
function makeCounter(){
  let currentCount = 1;
  return function(){
      return currentCount++;
  };
}
let counter = makeCounter();

alert(counter());
alert(counter());
alert(counter());

let counter2 = makeCounter();
alert(counter2());


let currentCount = 1;

function makeCounter1(){
  return function(){
      return currentCount++;
  };
}
let counter3 = makeCounter1();
let counter4 = makeCounter1();

alert(counter3());
alert(counter3());
alert(counter4());
alert(counter4());
// В1
// 1,2,3,1
// В2
// 1,2,3,4

const calculate = (length) => {
  return (width, height) => length * width * height;
};
const calculateWithoutLenght = calculate(10);
console.log(calculateWithoutLenght(5, 2)); 
console.log(calculateWithoutLenght(7, 3)); 


var x = 0, y = 0;
const steps = 10;

function moveObject(direction) {
  switch (direction) {
    case 'left':
      x -= steps;
      break;
    case 'right':
      x += steps;
      break;
    case 'up':
      y += steps;
      break;
    case 'down':
      y -= steps;
      break;
  }

  return {x, y};
}

function* generateMovement() {
  let direction = prompt();
  
  while (direction != null) {
    const result = moveObject(direction.toLowerCase());
    
    yield(`X=${result.x}, Y=${result.y}`);
    
    direction = prompt();
  }
  return "END";
}


console.group("Generator")
let result = generateMovement();
for(let value of result){
    console.log(value);
}

console.groupEnd();






window.x = "New"

console.log(window);
