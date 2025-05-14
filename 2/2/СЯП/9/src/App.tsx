import React, { useState } from "react";
import { Provider } from "react-redux";
import store from "./redux/store";
import TodoForm from "./components/TodoForm";
import TodoList from "./components/TodoList";
import "./App.css";

const App: React.FC = () => {
  const [editId, setEditId] = useState<number | null>(null);
  const [editText, setEditText] = useState("");

  const handleEdit = (id: number, text: string) => {
    setEditId(id);
    setEditText(text);
  };

  return (
    <Provider store={store}>
      <div className="app">
        <h1>Список дел</h1>
        <TodoForm
          editId={editId}
          editText={editText}
          setEditId={setEditId}
          setEditText={setEditText}
        />
        <TodoList onEdit={handleEdit} />
      </div>
    </Provider>
  );
};

export default App;