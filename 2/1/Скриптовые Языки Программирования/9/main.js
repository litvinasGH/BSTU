
class Shape {
  constructor(type) {
    this.type = type; 
  }
}

class Circle extends Shape {
  constructor(color) {
    super("circle");
    this.color = color; 
  }
}


class Square extends Shape {
  constructor(size, color) {
    super("square");
    this.size = size; 
    this.color = color; 
  }
}

class Triangle extends Shape {
  constructor(lines, color = null) {
    super("triangle");
    this.lines = lines; 
    this.color = color; 
  }
}

const yellowSquare = new Square("large", "yellow");
const smallSquare = new Square("small", "yellow");
const greenCircle = new Circle("green");
const emptyCircle = new Circle(null);
const triangleWithLines = new Triangle(3);
const emptyTriangle = new Triangle(0);


console.log("Свойства зеленого круга:", greenCircle);
console.log("Свойства треугольника с тремя линиями:", triangleWithLines);
console.log("Цвет маленького квадрата:", smallSquare.color);











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



const fitFaculty = new Faculty("Факультет информационных технологий", 10, 100);



fitFaculty.addStudent(student1);
fitFaculty.addStudent(student2);
fitFaculty.addStudent(student3);



console.log("Полное имя:", student1.getFullName());



student1.changeCourse(3);
console.log("Курс после изменения:", student1.course);



console.log("Количество студентов ДЭВИ:", fitFaculty.getDev());



console.log("Студенты группы ФИТ-6:", fitFaculty.getGroupe(6));
