import React, { useState } from 'react';
import { Button } from './Button';

export const Counter: React.FC = () => {
  const [count, setCount] = useState<number>(0);

  const handleIncrease = () => {
    setCount(prev => prev + 1);
  };

  const handleReset = () => {
    setCount(0);
  };

  return (
    <div className='counter' style={{textAlign: 'center', marginTop: '50px'}}>
      <h1>{count}</h1>
      <div>
        <Button
          title="Increase"
          callback={handleIncrease}
          disabled={count >= 5}
        />
        <Button
          title="Reset"
          callback={handleReset}
          disabled={count === 0}
        />
      </div>
    </div>
  );
};