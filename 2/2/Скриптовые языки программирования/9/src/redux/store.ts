import { createStore } from "redux";
import todoReducer from "./todoReducer";

const store = createStore(todoReducer);

export default store;