let person = {
    name: "Вац",
    age:18,
    greet() {
      console.log(`Привет, ${this.name}!`);
    },
    
    ageAfterYears(years) {
      return `${this.age + years} лет`;
    }
  };
  person.name = "Илья"
  person.greet();
  console.log(person.ageAfterYears(5));





  const car = {
    model: "Toyota Camry",
    year: 2022,
    
    getInfo() {
      console.log(`Модель: ${this.model}`);
      console.log(`Год выпуска: ${this.year}`);
    }
  };
  

  car.getInfo();



  function Book(title, author) {
    this.title = title;
    this.author = author;

    this.getTitle = function() {
      return this.title;
    };
    
    this.getAuthor = function() {
      return this.author;
    };
  }
  



const myBook = new Book("Мёртвые души", "Гоголь");

console.log(myBook.getTitle()); 
console.log(myBook.getAuthor()); 





const team = {
    players: [
      { name: "Иван Иванов", position: "Защитник" },
      { name: "Пётр Петров", position: "Нападающий" },
      { name: "Анна Сидорова", position: "Полузащитник" }
    ],
    
    printPlayerInfo: function() {
      this.players.forEach(player => {
        console.log(`Имя: ${player.name}, Позиция: ${player.position}`);
      });
    }
  };
  
  team.printPlayerInfo();







  const counter = (function() {
    let count = 0;
  
    function increment() {
      return ++this.count;
    }
  
    function decrement() {
      return --count;
    }
  
    function getCount() {
      return count;
    }
  
    return {
      increment,
      decrement,
      getCount
    };
  })();
  
  console.log(counter.increment()); 
  console.log(counter.increment()); 
  console.log(counter.decrement()); 
  console.log(counter.getCount()); 








  
const item = {
    price: 0
};


item.price = 100;

console.log(item.price); 




console.log(item.price); 
Object.defineProperty(item, 'price', {
    writable: false,
    configurable: false,
    enumerable: true,
    value: item.price
});

    item.price = 200;

    delete item.price;

console.log(item.price); 






class Circle {
    constructor(radius) {
      this._radius = radius;
    }
  
    get area() {
      return Math.PI * this._radius ** 2;
    }
  
    set radius(value) {
      if (typeof value !== 'number' || value <= 0) {
        throw new Error('Радиус должен быть положительным числом');
      }
      this._radius = value;
    }
  
    get radius() {
      return this._radius;
    }
  }
  
 
  const circle = new Circle(5);
  
  console.log(circle.area);
  circle.radius = 10;
  console.log(circle.area);







const anotherCar = {
    make: 'Toyota',
    model: 'Camry',
    year: 2022,
    

    getInfo() {
      console.log(`Make: ${this.make}`);
      console.log(`Model: ${this.model}`);
      console.log(`Year: ${this.year}`);
    }
  };
  

  anotherCar.getInfo();
  

  Object.defineProperties(anotherCar,{ make:{
    writable: false,
    configurable: false,
    enumerable: true,
    value: anotherCar.make
  },
  model: {
    writable: false,
    configurable: false,
    enumerable: true,
    value: anotherCar.model
  },
  year:{
    writable: false,
    configurable: false,
    enumerable: true,
    value: anotherCar.year
  }}

);
  
  anotherCar.make = 'Honda';
  anotherCar.model = 'Civic';
  anotherCar.year = 2023;


  anotherCar.getInfo();
  
















const array = [1, 2, 3];


Object.defineProperty(array, 'sum', {
  get: function() {
    return this.reduce((total, num) => total + num, 0);
  },
  enumerable: true,
  configurable: false
});

console.log(array.sum); 



  array.sum = 10;


















  
class Rectangle {
    constructor(width, height) {
      this.width = width;
      this.height = height;
    }
  
    
    get area() {
      return this.width * this.height;
    }
  
    
    get width() {
      return this._width;
    }
  
    
    set width(value) {
      if (typeof value !== 'number' || value <= 0) {
        throw new Error('Ширина должна быть положительным числом');
      }
      this._width = value;
    }
  
    
    get height() {
      return this._height;
    }
  
    
    set height(value) {
      if (typeof value !== 'number' || value <= 0) {
        throw new Error('Высота должна быть положительным числом');
      }
      this._height = value;
    }
  }
  
  
  const rect = new Rectangle(5, 3);
  
  console.log(`Площадь прямоугольника: ${rect.area}`);
  console.log(`Ширина: ${rect.width}, Высота: ${rect.height}`);
  
  
  rect.width = 7;
  rect.height = 4;
  
  console.log(`Новая площадь прямоугольника: ${rect.area}`);


  class User {
    constructor(firstName, lastName) {
      this.firstName = firstName;
      this.lastName = lastName;
    }
  
    get fullName() {
      return `${this.firstName} ${this.lastName}`;
    }
  
    set fullName(value) {
      const [newFirstName, newLastName] = value.trim().split(' ');
        this.firstName = newFirstName;
        this.lastName = newLastName;

    }
  }
  
  
  const user = new User("Качинскас", "Вацловас");
  
  console.log(user.fullName); 
  
  user.fullName = "К В";
  console.log(user.fullName); 
  
