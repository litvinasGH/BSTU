export interface Todo {
    id: number;
    text: string;
    completed: boolean;
  }
  
  export type TodoActionTypes =
    | { type: "ADD_TODO"; payload: string }
    | { type: "TOGGLE_TODO"; payload: number }
    | { type: "EDIT_TODO"; payload: { id: number; text: string } }
    | { type: "DELETE_TODO"; payload: number };