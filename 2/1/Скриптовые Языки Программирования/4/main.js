const products = new Set();

function addProduct(product) {
  products.add(product);
}

function removeProduct(product) {
  products.delete(product);
}

function hasProduct(product) {
  return products.has(product);
}

function countProducts() {
  return products.size;
}


addProduct("Кофе");
addProduct("Чай");

addProduct("Сок");


console.log(hasProduct("Кофе")); 
console.log(hasProduct("Молоко"));

removeProduct("Чай");

console.log(countProducts()); 







const students = new Set();

function addStudent(student) {
  students.add((student));
}

function removeStudent(id) {
  for(let i of students){
    if(i.id == id){
      students.delete(i);
    }
  }
}

function filterStudentsByGroup(group) {
  let ret = [];
  for(let i of students){
    if(i.group == group){
      ret.push(i)
    }
  }
  return ret
}

function sortStudentsByid() {
  const arr = Array.from(students).sort((a, b) => a.id - b.id);
  students.clear();
  arr.forEach(stud => students.add(stud));
  return arr;
}

addStudent({ id: '12345', group: 1, FIO: 'Иванов Иван Иванович' });
addStudent({ id: '67890', group: 2, FIO: 'Петров Петр Петрович' });
addStudent({ id: '11111', group: 1, FIO: 'Сидоров Сидор Сидорович' });
addStudent({ id: '11343', group: 1, FIO: 'Сидоров Сидор Сидорович' });

//console.log(students)
console.group("Students")
for(let obj of students){
  console.log(obj);
}
console.groupEnd()
console.log(filterStudentsByGroup(1));
removeStudent('11343')
console.log(students)
sortStudentsByid()
console.log(students)







const productStorage = new Map();

function addProductSecond(id, name, quantity, price) {
  productStorage.set(id, { name, quantity, price });
}

function removeProductById(id) {
  if (productStorage.has(id)) {
    productStorage.delete(id);
  }
}

function removeProductsByName(name) {
  productStorage.forEach((value, key) => {
    if (value.name.toLowerCase() == name.toLowerCase()) {
      productStorage.delete(key);
    }
  });
}

function updateQuantity(id, newQuantity) {
  if (productStorage.has(id)) {
    const product = productStorage.get(id);
    product.quantity = newQuantity;
    productStorage.set(id, product);
  }
}

function updatePrice(id, newPrice) {
  if (productStorage.has(id)) {
    const product = productStorage.get(id);
    product.price = newPrice;
    productStorage.set(id, product);
  }
}

function calculateTotals() {
  let totalPositions = 0;
  let totalCost = 0;

  productStorage.forEach((value) => {
    totalPositions += value.quantity;
    totalCost += value.quantity * value.price;
  });

  return { totalPositions, totalCost };
}

addProductSecond(1, 'Яблоко', 100, 0.5);
addProductSecond(2, 'Банан', 200, 0.3);
addProductSecond(3, 'Клубника', 150, 0.4);

console.group("products")
for(let obj of productStorage){
  console.log(obj);
}
console.groupEnd()
console.log(calculateTotals());
removeProductById(2);
updateQuantity(1, 120);
updatePrice(3, 0.45);
removeProductsByName('Банан');
console.log(calculateTotals());







const cache = new WeakMap();
let som = {str: "HELL E"}
function test() {
    if (cache.has(som)) {
        return "Ничего нового"
    }
    cache.set(som)
    return "Новые данные";
}

console.log(test());
som = null;
som = {str: "HELL E"};
console.log(test());
console.log(test());
