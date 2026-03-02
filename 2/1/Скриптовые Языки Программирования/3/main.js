const nestedArrays = [1, [1, 2,[3,4]], [2,4]];

const combinedArray = nestedArrays.reduce((acc, curr) => {
  if (Array.isArray(curr)) {
    return acc.concat(curr.flat(Infinity));
  } else {
    return acc.concat(curr);
  }
}, []);

console.log(combinedArray);

const newnestedArrays = [1, [1, 2,[3,4]], [2,4]];

const newcombinedArray = newnestedArrays.reduce((acc, curr) => {
    if (Array.isArray(curr)) {
      return acc.concat(curr.flat(Infinity));
    } else {
      return acc.concat(curr);
    }
}, []);

  console.log(newcombinedArray.reduce((sum, cuur)=>{sum += cuur; return sum}));





  function groupStudentsByGroup(students) {
    const result = {};
  
    students.forEach(student => {
      if (student.age > 17) {
        if (!result[student.groupId]) {
          result[student.groupId] = [];
        }
        result[student.groupId].push(student);
      }
    });
    return result;
  }


const students = [
    { name: "Иван", age: 18, groupId: 1 },
    { name: "Мария", age: 19, groupId: 1 },
    { name: "Петр", age: 16, groupId: 2 },
    { name: "Анна", age: 20, groupId: 2 },
    { name: "Сергей", age: 21, groupId: 3 }
  ];
  
  const groupedStudents = groupStudentsByGroup(students);
  console.log(groupedStudents);



  function number4(str) {
    let total1 = "";
    let total2 = "";

    for (let i = 0; i < str.length; i++) {
        total1 += str.charCodeAt(i);
    }
    for (let char of total1) {
        if (char == 7){
            total2 += 1;
        }
        else{
            total2 += char;
        }
    };

    console.log(total1);
    console.log(total2);
    return total1 - total2;
}

const result = number4('ABC');
console.log(result);



function extend(...objects) {
    return Object.assign({}, ...objects);
  }

  const result1 = extend({a: 1, b: 2}, {c: 3});
console.log(result1); 

const result2 = extend({a: 1, b: 2}, {c: 3}, {d: 4});
console.log(result2); 

const result3 = extend({a: 1, b: 2}, {a: 3, c: 3});
console.log(result3); 



function createPyramid(height) {
    height *= 2
    console.group("Pyramid")
    for (let i = 1; i <= height; i+=2) {
      console.log(' '.repeat((height - i)/2) + '*'.repeat(i));
    }
    
    console.groupEnd();
  }
  
  createPyramid(5)