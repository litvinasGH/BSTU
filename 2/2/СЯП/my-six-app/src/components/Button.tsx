import React from 'react';

interface ButtonProps {
  label: string;
  onClick: () => void;
  className?: string;
}

const Button: React.FC<ButtonProps> = ({ label, onClick, className }) => {
  return (
    <button
      className={`p-4 text-xl font-medium rounded-lg transition-colors 
        ${className || 'bg-gray-200 hover:bg-gray-300 dark:bg-gray-700 dark:hover:bg-gray-600'}`}
      onClick={onClick}
    >
      {label}
    </button>
  );
};

export default Button;