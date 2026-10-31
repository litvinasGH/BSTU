class Task {
    constructor(id, title, completed = false) {
        this.id = id;
        this.title = title;
        this.completed = completed;
    }

    setTitle(newTitle) {
        this.title = newTitle;
    }

    toggleCompleted() {
        this.completed = !this.completed;
    }
}

class Todolist {
    constructor(id, title) {
        this.id = id;
        this.title = title;
        this.tasks = [];
    }

    addTask(task) {
        this.tasks.push(task);
    }

    filterTasks(state) {
        return this.tasks.filter(task => task.completed === state);
    }
}

const todolist1 = new Todolist(1, "Рабочие таски");
const todolist2 = new Todolist(2, "Персональные таски");

const task1 = new Task(1, "Сделать финальный билд", true);
const task2 = new Task(2, "Пофиксить код");
const task3 = new Task(3, "Купить еды");

todolist1.addTask(task1);
todolist1.addTask(task2);
todolist2.addTask(task3);

todolist1.tasks.forEach(task => console.log(`${task.title} (${task.completed ? 'Completed' : 'Not Completed'})`));

todolist1.filterTasks(true).forEach(task => console.log(`${task.title}`));

todolist2.tasks[0].setTitle("Проснуться");
todolist2.tasks[0].toggleCompleted();
console.log(`${todolist2.tasks[0].title} (${todolist2.tasks[0].lcompleted ? 'Completed' : 'Not Completed'})`);