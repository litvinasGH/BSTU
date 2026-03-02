import React, { useState } from "react";
import { useAppDispatch } from "../redux/hooks";
import { addTodo, editTodo } from "../redux/todosSlice";

interface TodoFormProps {
  editId: number | null;
  editText: string;
  setEditId: (id: number | null) => void;
  setEditText: (text: string) => void;
}

const TodoForm: React.FC<TodoFormProps> = ({ 
  editId, 
  editText, 
  setEditId, 
  setEditText 
}) => {
  const dispatch = useAppDispatch();
  const [text, setText] = useState("");

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    const currentText = editId !== null ? editText : text;
    if (!currentText.trim()) return;

    if (editId !== null) {
      dispatch(editTodo({ id: editId, text: currentText }));
      setEditId(null);
      setEditText("");
    } else {
      dispatch(addTodo(currentText));
      setText("");
    }
  };

  return (
    <form onSubmit={handleSubmit}>
      <input
        type="text"
        value={editId !== null ? editText : text}
        onChange={(e) => 
          editId !== null 
            ? setEditText(e.target.value) 
            : setText(e.target.value)
        }
        placeholder="Введите задачу"
      />
      <button type="submit">
        {editId !== null ? "Сохранить" : "Добавить"}
      </button>
    </form>
  );
};

export default TodoForm;