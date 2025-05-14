import React, { useState } from "react";
import { Provider } from "react-redux";
import { store } from "./redux/store";
import TodoForm from "./components/TodoForm";
import TodoList from "./components/TodoList";
import "./App.css";

const App: React.FC = () => {
  const [editId, setEditId] = useState<number | null>(null);
  const [editText, setEditText] = useState("");

  return (
    <Provider store={store}>
      <div className="app">
        <h1>Todolist (RTK)</h1>
        <TodoForm
          editId={editId}
          editText={editText}
          setEditId={setEditId}
          setEditText={setEditText}
        />
        <TodoList 
          onEdit={(id, text) => {
            setEditId(id);
            setEditText(text);
          }} 
        />
      </div>
    </Provider>
  );
};

export default App;