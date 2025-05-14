import React from "react";
import { useSelector } from "react-redux";
import { Todo } from "../redux/types";
import TodoItem from "./TodoItem";

interface TodoListProps {
  onEdit: (id: number, text: string) => void;
}

const TodoList: React.FC<TodoListProps> = ({ onEdit }) => {
  const todos = useSelector((state: Todo[]) => state);

  return (
    <div>
      {todos.map((todo) => (
        <TodoItem key={todo.id} todo={todo} onEdit={onEdit} />
      ))}
    </div>
  );
};

export default TodoList;