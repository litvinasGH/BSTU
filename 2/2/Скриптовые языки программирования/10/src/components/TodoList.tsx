import React from "react";
import { useAppSelector } from "../redux/hooks";
import { Todo } from "../redux/types";
import TodoItem from "./TodoItem";
import { RootState } from "../redux/store"; // Добавляем импорт RootState

interface TodoListProps {
  onEdit: (id: number, text: string) => void;
}

const TodoList: React.FC<TodoListProps> = ({ onEdit }) => {
  const todos = useAppSelector((state: RootState) => state.todos);

  return (
    <div>
      {todos.map((todo: Todo) => ( 
        <TodoItem key={todo.id} todo={todo} onEdit={onEdit} />
      ))}
    </div>
  );
};

export default TodoList;