import React from 'react';

interface ButtonProps {
  value: string;
  onClick: (value: string) => void;
  className?: string;
  disabled?: boolean;
}

const Button: React.FC<ButtonProps> = ({ 
  value, 
  onClick, 
  className = '', 
  disabled = false 
}) => {
  return (
    <button
      className={`button ${className} ${disabled ? 'disabled' : ''}`}
      onClick={() => !disabled && onClick(value)}
      disabled={disabled}
    >
      {value}
    </button>
  );
};

export default Button;