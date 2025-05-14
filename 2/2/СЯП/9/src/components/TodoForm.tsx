import React, { useState } from "react";
import { useDispatch } from "react-redux";
import { addTodo, editTodo } from "../redux/todoActions";

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
  setEditText,
}) => {
  const dispatch = useDispatch();
  const [text, setText] = useState("");

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    const currentText = editId !== null ? editText : text;
    if (!currentText.trim()) return;

    if (editId !== null) {
      dispatch(editTodo(editId, currentText));
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
        onChange={(e) => {
          if (editId !== null) {
            setEditText(e.target.value);
          } else {
            setText(e.target.value);
          }
        }}
        placeholder="Введите задачу"
      />
      <button type="submit">{editId !== null ? "Сохранить" : "Добавить"}</button>
    </form>
  );
};

export default TodoForm;