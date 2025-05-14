import React from "react";
import { useAppDispatch } from "../redux/hooks";
import { Todo } from "../redux/types";
import { toggleTodo, deleteTodo } from "../redux/todosSlice";

interface TodoItemProps {
  todo: Todo;
  onEdit: (id: number, text: string) => void;
}

const TodoItem: React.FC<TodoItemProps> = ({ todo, onEdit }) => {
  const dispatch = useAppDispatch();

  return (
    <div className={`todo-item ${todo.completed ? "completed" : ""}`}>
      <input
        type="checkbox"
        checked={todo.completed}
        onChange={() => dispatch(toggleTodo(todo.id))}
      />
      <span>{todo.text}</span>
      <div className="buttonDiv">
      <button onClick={() => onEdit(todo.id, todo.text)}>✏️</button>
      <button onClick={() => dispatch(deleteTodo(todo.id))}>🗑️</button>
      </div>
    </div>
  );
};

export default TodoItem;