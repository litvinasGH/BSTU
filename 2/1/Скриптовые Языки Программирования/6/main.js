const numbers = [10, 20, 30, 40, 50];
const [,y,,x] = numbers;
console.log(y);




const user = {
  name: "Retr0",
  age: 28
};

const admin = {
  ...user,
  admin: true
};
console.log(user);
console.log(admin);




let store = {
  state: { // 1 уровень
      profilePage: { // 2 уровень
          posts: [ // 3 уровень
              {id: 1, message: 'Hi', likesCount: 12},
              {id: 2, message: 'By', likesCount: 1},
          ],
          newPostText: 'About me',
      },
      dialogsPage: {
          dialogs: [
              {id: 1, name: 'Valera'},
              {id: 2, name: 'Andrey'},
              {id: 3, name: 'Sasha'},
              {id: 4, name: 'Viktor'},
          ],
          messages: [
              {id: 1, message: 'hi'},
              {id: 2, message: 'hi hi'},
              {id: 3, message: 'hi hi hi'},
          ],
      },
      sidebar: [],
  },
}


let {
  state:{
    profilePage:{
      posts
    },
    dialogsPage:{
      dialogs, messages
    }
  }
} = store

console.log(posts.map(post => post.likesCount));

console.log(dialogs.filter(dialog => dialog.id % 2 === 0));

messages = messages.map(message => ({...message, message: "Hello user"}))
console.log(messages);






let tasks = [
  { id: 1, title: "HTML&CSS", isDone: true },
  { id: 2, title: "JS", isDone: true },
  { id: 3, title: "ReactJS", isDone: false },
  { id: 4, title: "Rest API", isDone: false },
  { id: 5, title: "GraphQL", isDone: false },
];
tasks = [...tasks, { id: 6, title: "Get a final build", isDone: false }];
console.log(tasks);


function sumValues(x, y, z) {
  return x + y + z;
}

const params = [1, 2, 3]
console.log(...params)


