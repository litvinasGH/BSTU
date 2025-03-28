import React from 'react';

interface ThemeToggleProps {
  isDarkTheme: boolean;
  setIsDarkTheme: (value: boolean) => void;
}

const ThemeToggle: React.FC<ThemeToggleProps> = ({ isDarkTheme, setIsDarkTheme }) => {
  return (
    <button
      onClick={() => setIsDarkTheme(!isDarkTheme)}
      className="mb-4 p-2 rounded-lg bg-gray-200 hover:bg-gray-300 dark:bg-gray-700 dark:hover:bg-gray-600 transition-colors"
    >
      {isDarkTheme ? '🌞 Светлая' : '🌙 Темная'}
    </button>
  );
};

export default ThemeToggle;