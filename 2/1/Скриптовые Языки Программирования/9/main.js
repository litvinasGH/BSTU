let square = {
  width:100,
  height:100,
  color:"yellow",
  getSquare(){
      return this.width*this.height;
  },
  getInfo(){
      return `height:${this.height} width:${this.width} color:${this.color}`;
  }
};
let small_square = {
  __proto__:square,
  width:50,
  height:50
};

let circle = {
  radius:100,
  color:"white",
  getSquare(){
      return Math.PI*Math.pow(this.radius,2);
  },
  getInfo(){
      `radius:${this.radius} color:${this.color}`;
  }
};

let another_circle = {
  __proto__:circle,
  color:"green"
};

let triangle_with_lines = {
  base:20,
  height:40,
  lines:1,
  color:"white",
  getSquare(){
      return (this.base/2)*this.height;
  },
  getInfo(){
      return ` lines: ${this.lines}  color:${this.color} `;
  }
};

let another_triangle = {
  __proto__:triangle_with_lines,
  lines:3
};
console.group("Figures");
console.log(square.getInfo());
console.log(small_square.getInfo());
console.log(circle.getInfo());
console.log(another_circle.getInfo());
console.log(triangle_with_lines.getInfo());
console.log(another_triangle.getInfo());
console.groupEnd();














class Human {
  constructor(firstName, lastName, birthYear, address) {
    this.firstName = firstName; 

    this.lastName = lastName;   

    this.birthYear = birthYear; 

    this.address = address;     

  }

  

  get age() {
    const currentYear = new Date().getFullYear();
    return currentYear - this.birthYear;
  }

  

  set age(newAge) {
    const currentYear = new Date().getFullYear();
    this.birthYear = currentYear - newAge;
  }

  

  changeAddress(newAddress) {
    this.address = newAddress;
  }
}

const hum = new Human("FN", "LN", 2000, "Adress")
hum.age = 20;
console.log(hum.birthYear)

class Student extends Human {
  constructor(firstName, lastName, birthYear, address, faculty, course, group, recordBookNumber) {
    super(firstName, lastName, birthYear, address);
    this.faculty = faculty;            

    this.course = course;              

    this.group = group;                

    this.recordBookNumber = recordBookNumber; 

  }

  

  changeCourse(newCourse) {
    this.course = newCourse;
  }

  

  changeGroup(newGroup) {
    this.group = newGroup;
  }

  

  getFullName() {
    return `${this.firstName} ${this.lastName}`;
  }

  

  parseRecordBookNumber() {
    const facultyCode = parseInt(this.recordBookNumber[0], 10);
    const specialtyCode = parseInt(this.recordBookNumber[1], 10);
    const year = parseInt(this.recordBookNumber.slice(2, 4), 10);
    const budgetOrPaid = parseInt(this.recordBookNumber[4], 10);
    const serial = parseInt(this.recordBookNumber.slice(5), 10);

    return {
      facultyCode,
      specialtyCode,
      year,
      budgetOrPaid,
      serial,
    };
  }
}



class Faculty {
  constructor(name, groupCount, studentCount) {
    this.name = name;           

    this.groupCount = groupCount; 

    this.studentCount = studentCount; 

    this.students = [];          

  }

  

  changeGroupCount(newGroupCount) {
    this.groupCount = newGroupCount;
  }

  

  changeStudentCount(newStudentCount) {
    this.studentCount = newStudentCount;
  }

  

  addStudent(student) {
    this.students.push(student);
    this.studentCount++;
  }

  

  getDev() {
    return this.students.filter((student) => {
      return student.parseRecordBookNumber() === 3; 
    }).length;
  }

  

  getGroupe(group) {
    return this.students.filter((student) => student.group === group);
  }
}






const student1 = new Student("Иван", "Иванов", 2006, "Минск", "ФИТ", 2, 6, "71201301");
const student2 = new Student("Алексей", "Смирнов", 2005, "Гродно", "ФИТ", 3, 7, "71202302");
const student3 = new Student("Мария", "Кузнецова", 2004, "Минск", "ФИТ", 4, 6, "71301303");

student1.changeAddress("New Address")


const fitFaculty = new Faculty("Факультет информационных технологий", 10, 100);



fitFaculty.addStudent(student1);
fitFaculty.addStudent(student2);
fitFaculty.addStudent(student3);



console.log("Полное имя:", student1.getFullName());



student1.changeCourse(3);
console.log("Курс после изменения:", student1.course);



console.log("Количество студентов ДЭВИ:", fitFaculty.getDev());



console.log("Студенты группы ФИТ-6:", fitFaculty.getGroupe(6));
