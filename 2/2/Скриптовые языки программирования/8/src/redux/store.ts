import { createStore } from 'redux';
import { counterReducer } from './reducer';

export const store = createStore(counterReducer);

export type RootState = ReturnType<typeof counterReducer>;