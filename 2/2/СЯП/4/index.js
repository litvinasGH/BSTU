const myPromise = new Promise((resolve) => {
    setTimeout(() => {
      const randomNumber = Math.random();
      resolve(randomNumber);
    }, 3000);
  });
  
  myPromise.then((number) => {
    console.log('Сгенерированное число:', number);
  });


  function createPromise(delay) {
    return new Promise((resolve) => {
      setTimeout(() => {
        const randomNumber = Math.random();
        resolve(randomNumber);
      }, delay);
    });
  }
  
  const promises = [
    createPromise(3000),
    createPromise(3000),
    createPromise(3000)
  ];
  
  Promise.all(promises)
    .then((results) => {
      console.log('Все сгенерированные числа:', results);
    });

    

    let pr = new Promise((res,rej) => {
        rej('ku')
    })
    
    pr
        .then(() => console.log(1))
        .catch(() => console.log(2))
        .catch(() => console.log(3))
        .then(() => console.log(4))
        .then(() => console.log(5))
    


        const promise = Promise.resolve(21); 

promise
  .then((num) => {
    console.log(num); 
    return num;      
  })
  .then((num) => {
    console.log(num * 2);
  });



  async function processData() {
    try {
      const firstResult = await promise; 
      console.log(firstResult);          
  
      const secondResult = firstResult * 2;
      console.log(secondResult);            
    } catch (error) {
      console.error('Ошибка:', error);
    }
  }

  processData();