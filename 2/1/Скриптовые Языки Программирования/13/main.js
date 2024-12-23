class Task{
    constructor(Id, Name, Condition){
        this.Id = Id;
        this.Name = Name;
        this.Condition = Condition;
    }
    Edit(NewName){
        this.Name = NewName;
    }

    editCondition(){
        this.Condition = !this.Condition;
    }

};

class ToDoList{
    constructor( Id, Name, Tasks){
        this.Tasks = Tasks;
        this.Id = Id;
        this.Name = Name;
        
    }


    _Delete(task){
        this.Tasks.delete(task);
    }

    Add(id, name, con){
        this.Tasks.push(new Task (id, name, con));
    }
    EditName(NewName){
        this.Name = NewName;
    }
    Filter(Condition){
        return Array.from(this.Tasks).filter(Task => Task.Condition === Condition);
    }

}


let List = new ToDoList(1, "Tasks",[])


function createTask(task){
    

    let div = document.createElement('div');
    div.className = "task";

    let divCheck = document.createElement('div');
    divCheck.id = 'divCheck';
    let check = document.createElement('input');
    check.type = 'checkbox';
    if(task.Condition == true){
        check.checked = true;
    }

    check.addEventListener('click', ()=>{
        task.editCondition();
    });

    divCheck.appendChild(check);

    let divName = document.createElement("div");
    divName.id = "divName";
    let taskName = document.createElement('span');
    taskName.textContent = task.Name;
    divName.appendChild(taskName);

    div.appendChild(divCheck);
    div.appendChild(divName);

    let divButtons = document.createElement("div");
    divButtons.id = 'divButtons';
    let editButton = document.createElement("button");
    editButton.innerText = "Изменить";

editButton.addEventListener('click', ()=>{
    let newName = prompt("Введите новое название");

    if(newName == undefined){
        alert("имя должно быть определено");
        return;
    }

    task.Name = newName;

    taskName.textContent = newName;


})

    let deleteButton = document.createElement("button");
    deleteButton.innerText = "Удалить";

deleteButton.addEventListener("click", ()=>{
    List.Tasks = List.Tasks.filter(t => t !== task);
    div.parentElement.removeChild(div);

});

 
    divButtons.appendChild(editButton);
    divButtons.appendChild(deleteButton);

    div.appendChild(divButtons);

    document.getElementById('checkList').appendChild(div);
}


document.getElementById("addButton").addEventListener("click", () => {
    
    let name = document.getElementById("name").value;
    
    List.Add(List.Tasks.lenght, name, false);

});


document.getElementById("showButton").addEventListener("click", ()=>{

    document.getElementById('checkList').innerHTML = '';


    List.Tasks.forEach(task => {
        createTask(task);
    });


});



document.getElementById('completedButton').addEventListener('click', ()=>{

    document.getElementById('checkList').innerHTML = '';

    List.Filter(true).forEach(task => {

    
        createTask(task);
    
});


});


document.getElementById('uncompletedButton').addEventListener('click', ()=>{

    document.getElementById('checkList').innerHTML = '';

    List.Filter(false).forEach(task => {

        createTask(task);
    
});


});
