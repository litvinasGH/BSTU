// Было: export const App = () => ...
// Стало: использовать default export
import React from 'react';
import { Provider } from 'react-redux';
import { store } from './redux/store';
import { Counter } from './components/Counter';

const App: React.FC = () => (
  <Provider store={store}>
    <div className="app">
      <Counter />
    </div>
  </Provider>
);

export default App; // Добавьте default export