import { createSlice, PayloadAction } from "@reduxjs/toolkit";
import { Todo } from "./types";

const initialState: Todo[] = [];

const todosSlice = createSlice({
  name: "todos",
  initialState,
  reducers: {
    addTodo: (state, action: PayloadAction<string>) => {
      state.push({
        id: Date.now(),
        text: action.payload,
        completed: false,
      });
    },
    toggleTodo: (state, action: PayloadAction<number>) => {
      const todo = state.find((t) => t.id === action.payload);
      if (todo) todo.completed = !todo.completed;
    },
    editTodo: (
      state,
      action: PayloadAction<{ id: number; text: string }>
    ) => {
      const todo = state.find((t) => t.id === action.payload.id);
      if (todo) todo.text = action.payload.text;
    },
    deleteTodo: (state, action: PayloadAction<number>) => {
      return state.filter((todo) => todo.id !== action.payload);
    },
  },
});

export const { addTodo, toggleTodo, editTodo, deleteTodo } = todosSlice.actions;
export default todosSlice.reducer;