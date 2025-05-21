import React from "react";
import { useDispatch } from "react-redux";
import { Todo } from "../redux/types";
import { toggleTodo, deleteTodo } from "../redux/todoActions";

interface TodoItemProps {
  todo: Todo;
  onEdit: (id: number, text: string) => void;
}

const TodoItem: React.FC<TodoItemProps> = ({ todo, onEdit }) => {
  const dispatch = useDispatch();

  return (
    <div className={`todo-item ${todo.completed ? "completed" : ""}`}>
      <input
        type="checkbox"
        checked={todo.completed}
        onChange={() => dispatch(toggleTodo(todo.id))}
      />
      <span>{todo.text}</span>
      <div className="buttonDiv">
        <button onClick={() => onEdit(todo.id, todo.text)}>Изменить</button>
        <button onClick={() => dispatch(deleteTodo(todo.id))}>Удалить</button>
      </div>
    </div>
  );
};

export default TodoItem;