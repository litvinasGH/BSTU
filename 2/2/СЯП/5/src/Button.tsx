import React from 'react';

interface ButtonProps {
  title: string;
  callback: () => void;
  disabled?: boolean;
}

export const Button: React.FC<ButtonProps> = ({
  title,
  callback,
  disabled = false
}) => {
  return (
    <button 
      onClick={callback}
      disabled={disabled}
      style={{margin: '0 5px', padding: '5px 15px'}}
    >
      {title}
    </button>
  );
};