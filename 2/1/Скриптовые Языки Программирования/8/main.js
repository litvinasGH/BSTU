// Исходные данные
// 1
let user = {
  name: 'Masha',
  age: 21
};

// 2
let numbers = [1, 2, 3];

// 3
let user1 = {
  name: 'Masha',
  age: 23,
  location: {
    city: 'Minsk',
    country: 'Belarus'
  }
};

// 4
let user2 = {
  name: 'Masha',
  age: 28,
  skills: ["HTML", "CSS", "JavaScript", "React"]
};

// 5
const array = [
  { id: 1, name: 'Vasya', group: 10 },
  { id: 2, name: 'Ivan', group: 11 },
  { id: 3, name: 'Masha', group: 12 },
  { id: 4, name: 'Petya', group: 10 },
  { id: 5, name: 'Kira', group: 11 }
];

// 6
let user4 = {
  name: 'Masha',
  age: 19,
  studies: {
    university: 'BSTU',
    speciality: 'designer',
    year: 2020,
    exams: {
      maths: true,
      programming: false
    }
  }
};

// 7
let user5 = {
  name: 'Masha',
  age: 22,
  studies: {
    university: 'BSTU',
    speciality: 'designer',
    year: 2020,
    department: {
      faculty: 'FIT',
      group: 10
    },
    exams: [
      { maths: true, mark: 8 },
      { programming: true, mark: 4 }
    ]
  }
};

// 8
let user6 = {
  name: 'Masha',
  age: 21,
  studies: {
    university: 'BSTU',
    speciality: 'designer',
    year: 2020,
    department: {
      faculty: 'FIT',
      group: 10
    },
    exams: [
      {
        maths: true,
        mark: 8,
        professor: {
          name: 'Ivan Ivanov',
          degree: 'PhD'
        }
      },
      {
        programming: true,
        mark: 10,
        professor: {
          name: 'Petr Petrov',
          degree: 'PhD'
        }
      }
    ]
  }
};

// 9
let user7 = {
  name: 'Masha',
  age: 20,
  studies: {
    university: 'BSTU',
    speciality: 'designer',
    year: 2020,
    department: {
      faculty: 'FIT',
      group: 10
    },
    exams: [
      {
        maths: true,
        mark: 8,
        professor: {
          name: 'Ivan Petrov',
          degree: 'PhD',
          articles: [
            { title: "About HTML", pagesNumber: 3 },
            { title: "About CSS", pagesNumber: 5 },
            { title: "About JavaScript", pagesNumber: 1 }
          ]
        }
      },
      {
        programming: true,
        mark: 10,
        professor: {
          name: 'Petr Ivanov',
          degree: 'PhD',
          articles: [
            { title: "About HTML", pagesNumber: 3 },
            { title: "About CSS", pagesNumber: 5 },
            { title: "About JavaScript", pagesNumber: 1 }
          ]
        }
      }
    ]
  }
};

// 10
let store = {
  state: {
    profilePage: {
      posts: [
        { id: 1, message: 'Hi', likesCount: 12 },
        { id: 2, message: 'By', likesCount: 1 }
      ],
      newPostText: 'About me'
    },
    dialogsPage: {
      dialogs: [
        { id: 1, name: 'Valera' },
        { id: 2, name: 'Andrey' },
        { id: 3, name: 'Sasha' },
        { id: 4, name: 'Viktor' }
      ],
      messages: [
        { id: 1, message: 'hi' },
        { id: 2, message: 'hi hi' },
        { id: 3, message: 'hi hi hi' }
      ]
    },
    sidebar: []
  }
};

// Решения
// 1
let userCopy = { ...user };

// 2
let numbersCopy = [...numbers];

// 3
let user1Copy = {
  ...user1,
  location: { ...user1.location }
};

// 4
let user2Copy = {
  ...user2,
  skills: [...user2.skills]
};

// 5
let arrayCopy = [
  { ...array[0] },
  { ...array[1] },
  { ...array[2] },
  { ...array[3] },
  { ...array[4] }
];

// 6
let user4Copy = {
  ...user4,
  studies: {
    ...user4.studies,
    exams: { ...user4.studies.exams }
  }
};

// 7
let user5Copy = {
  ...user5,
  studies: {
    ...user5.studies,
    department: { ...user5.studies.department, group: 12 },
    exams: [
      { ...user5.studies.exams[0], mark: user5.studies.exams[0].programming ? 10 : user5.studies.exams[0].mark },
      { ...user5.studies.exams[1], mark: user5.studies.exams[1].programming ? 10 : user5.studies.exams[1].mark }
    ]
  }
};

// 8
let user6Copy = {
  ...user6,
  studies: {
    ...user6.studies,
    exams: [
      {
        ...user6.studies.exams[0],
        professor: { ...user6.studies.exams[0].professor, name: "New Professor Name" }
      },
      {
        ...user6.studies.exams[1],
        professor: { ...user6.studies.exams[1].professor, name: "New Professor Name" }
      }
    ]
  }
};

// 9
let user7Copy = {
  ...user7,
  studies: {
    ...user7.studies,
    exams: [
      {
        ...user7.studies.exams[0],
        professor: {
          ...user7.studies.exams[0].professor,
          articles: [
            { ...user7.studies.exams[0].professor.articles[0] },
            { ...user7.studies.exams[0].professor.articles[1], pagesNumber: 3 },
            { ...user7.studies.exams[0].professor.articles[2] }
          ]
        }
      },
      {
        ...user7.studies.exams[1],
        professor: {
          ...user7.studies.exams[1].professor,
          articles: [
            { ...user7.studies.exams[1].professor.articles[0] },
            { ...user7.studies.exams[1].professor.articles[1], pagesNumber: 3 },
            { ...user7.studies.exams[1].professor.articles[2] }
          ]
        }
      }
    ]
  }
};

// 10
let storeCopy = {
  ...store,
  state: {
    ...store.state,
    profilePage: {
      ...store.state.profilePage,
      posts: [
        { ...store.state.profilePage.posts[0], message: "Hello" },
        { ...store.state.profilePage.posts[1], message: "Hello" }
      ]
    },
    dialogsPage: {
      ...store.state.dialogsPage,
      messages: [
        { ...store.state.dialogsPage.messages[0], message: "Hello" },
        { ...store.state.dialogsPage.messages[1], message: "Hello" },
        { ...store.state.dialogsPage.messages[2], message: "Hello" }
      ]
    }
  }
};

console.log("userCopy");
console.log(user);
console.log(userCopy);
console.log("numbersCopy");
console.log(numbers);
console.log(numbersCopy);
console.log("user1Copy");
console.log(user1);
console.log(user1Copy);
console.log("user2Copy");
console.log(user2);
console.log(user2Copy);
console.log("arrayCopy");
console.log(array);
console.log(arrayCopy);
console.log("user4Copy");
console.log(user4);
console.log(user4Copy);
console.log("user5Copy");
console.log(user5);
console.log(user5Copy);
console.log("user6Copy");
console.log(user6);
console.log(user6Copy);
console.log("user7Copy");
console.log(user7);
console.log(user7Copy);
console.log("storeCopy");
console.log(store);
console.log(storeCopy);
