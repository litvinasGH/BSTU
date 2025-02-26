type Student = {
    id: number,
    name: string,
    group: number
}

const array: Student[] = [
    {id: 1, name: 'Vasya', group: 10}, 
    {id: 2, name: 'Ivan', group: 11},
    {id: 3, name: 'Masha', group: 12},
    {id: 4, name: 'Petya', group: 10},
    {id: 5, name: 'Kira', group: 11},
]


type CarsType = {
    manufacturer?: string,
    model?: string
}

let car: CarsType = {}; //объект создан!
car.manufacturer = "manufacturer";
car.model = 'model';


type ArrayCarsType = {
    cars: Array<CarsType>
}


const car1: CarsType = {}; //объект создан!
car1.manufacturer = "manufacturer";
car1.model = 'model';

const car2: CarsType = {}; //объект создан!
car2.manufacturer = "manufacturer";
car2.model = 'model';

const arrayCars: Array<ArrayCarsType> = [{
    cars: [car1, car2]
}];

type MarkFilterType = 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 | 10;
type DoneType = boolean

type MarkType = {
    subject: string,
    mark: MarkFilterType, // может принимать значения от 1 до 10
    done: DoneType,
}


type GroupFilterType = 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 | 10 | 11 | 12;

type StudentType = {
    id: number,
    name: string,
    group: GroupFilterType, // может принимать значения от 1 до 12
    marks: Array<MarkType>,
}

type GroupType = {
    students: StudentType[],
    studentsFilter: (group: number) => Array<StudentType>, // фильтр по группе
    marksFilter: (mark: number) => Array<StudentType>, // фильтр по  оценке
    deleteStudent: (id: number) => void, // удалить студента по id из  исходного массива
    mark: MarkFilterType,
    group: GroupFilterType,
}


let group: GroupType = {

    students :  [
    {id: 1, name: '1', group: 6, marks: [{subject: "1", mark: 10, done: true}, ]}, 
    {id: 2, name: '2', group: 7, marks: [{subject: "2", mark: 9, done: true}, ]},
    {id: 3, name: '3', group: 8, marks: [{subject: "3", mark: 4, done: false}, ]},
    {id: 4, name: '4', group: 9, marks: [{subject: "4", mark: 5, done: true}, ]},
    {id: 5, name: '5', group: 10, marks: [{subject: "5", mark: 7, done: true}, ]},
    ], 

    studentsFilter(group: number)
    { return this.students.filter(student => student.group == group)},
    marksFilter (mark: number)
    {return this.students.filter(student => student.marks.some(m => m.mark == mark))},
    deleteStudent (id: number)
    {return this.students = this.students.filter(student => student.id != id)},
    mark: 10,
    group: 6,

}

