var array = [
    { id: 1, name: 'Vasya', group: 10 },
    { id: 2, name: 'Ivan', group: 11 },
    { id: 3, name: 'Masha', group: 12 },
    { id: 4, name: 'Petya', group: 10 },
    { id: 5, name: 'Kira', group: 11 },
];
var car = {}; //объект создан!
car.manufacturer = "manufacturer";
car.model = 'model';
var car1 = {}; //объект создан!
car1.manufacturer = "manufacturer";
car1.model = 'model';
var car2 = {}; //объект создан!
car2.manufacturer = "manufacturer";
car2.model = 'model';
var arrayCars = [{
        cars: [car1, car2]
    }];
var group = {
    students: [
        { id: 1, name: '1', group: 6, marks: [{ subject: "1", mark: 10, done: true },] },
        { id: 2, name: '2', group: 7, marks: [{ subject: "2", mark: 9, done: true },] },
        { id: 3, name: '3', group: 8, marks: [{ subject: "3", mark: 4, done: false },] },
        { id: 4, name: '4', group: 9, marks: [{ subject: "4", mark: 5, done: true },] },
        { id: 5, name: '5', group: 10, marks: [{ subject: "5", mark: 7, done: true },] },
    ],
    studentsFilter: function (group) { return this.students.filter(function (student) { return student.group == group; }); },
    marksFilter: function (mark) { return this.students.filter(function (student) { return student.marks.some(function (m) { return m.mark == mark; }); }); },
    deleteStudent: function (id) { return this.students = this.students.filter(function (student) { return student.id != id; }); },
    mark: 10,
    group: 6,
};
