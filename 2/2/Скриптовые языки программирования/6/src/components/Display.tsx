import React from 'react';

interface DisplayProps {
  currentInput: string;
  result: string | null;
}

const Display: React.FC<DisplayProps> = ({ currentInput, result }) => {
  return (
    <div className="display">
      <div className="input">{currentInput}</div>
      <div className="result">{result}</div>
    </div>
  );
};

export default Display;