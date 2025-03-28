import React, { useState, useEffect, useCallback } from 'react';
import Display from './components/Display';
import Button from './components/Button';
import History from './components/History';
import ThemeToggle from './components/ThemeToggle';

const Calculator: React.FC = () => {
  const [expression, setExpression] = useState('');
  const [result, setResult] = useState('');
  const [history, setHistory] = useState<string[]>([]);
  const [isDarkTheme, setIsDarkTheme] = useState(false);
  const [isNewCalculation, setIsNewCalculation] = useState(false);

  const calculateResult = useCallback(() => {
    try {
      let expr = expression;
      const lastChar = expr.slice(-1);
      if ('+-*/'.includes(lastChar)) expr = expr.slice(0, -1);
      if (!expr) return;

      const evalResult = new Function(`return ${expr}`)();
      if (!isFinite(evalResult)) throw new Error('Деление на ноль');

      setResult(evalResult.toString());
      setHistory(prev => [...prev, `${expr} = ${evalResult}`]);
      setIsNewCalculation(true);
    } catch (error) {
      setResult(error instanceof Error ? error.message : 'Ошибка');
    }
  }, [expression]);

  const handleNumber = useCallback((value: string) => {
    if (isNewCalculation && !'+-*/'.includes(value)) {
      setExpression(value);
      setIsNewCalculation(false);
    } else {
      setExpression(prev => {
        const lastChar = prev.slice(-1);
        if (value === '.') {
          const parts = prev.split(/[-+*/]/);
          const currentNumber = parts[parts.length - 1];
          if (currentNumber.includes('.')) return prev;
          if (prev === '' || '+-*/'.includes(lastChar)) return prev + '0.';
          return prev + '.';
        }
        
        if (prev === '0' && value === '0') return prev;
        if (prev === '0') return value;
        return prev + value;
      });
    }
    setResult('');
  }, [isNewCalculation]);

  const handleOperator = useCallback((op: string) => {
    setExpression(prev => {
      if (prev === '') return op === '-' ? '-' : prev;
      const lastChar = prev.slice(-1);
      
      if ('+-*/'.includes(lastChar)) {
        if (op === '-' && lastChar !== '-') return prev + op;
        return prev.slice(0, -1) + op;
      }
      
      if (lastChar === '.') return prev + '0' + op;
      return prev + op;
    });
    setIsNewCalculation(false);
    setResult('');
  }, []);

  const handleClear = useCallback(() => {
    setExpression('');
    setResult('');
  }, []);

  const handleBackspace = useCallback(() => {
    setExpression(prev => prev.slice(0, -1));
  }, []);

  const handleKeyPress = useCallback((e: KeyboardEvent) => {
    const key = e.key;
    if (key >= '0' && key <= '9') handleNumber(key);
    else if (key === '.') handleNumber('.');
    else if ('+-*/'.includes(key)) handleOperator(key);
    else if (key === 'Enter') calculateResult();
    else if (key === 'Backspace') handleBackspace();
    else if (key === 'Escape') handleClear();
  }, [handleNumber, handleOperator, calculateResult, handleBackspace, handleClear]);

  useEffect(() => {
    window.addEventListener('keydown', handleKeyPress);
    return () => window.removeEventListener('keydown', handleKeyPress);
  }, [handleKeyPress]);

  return (
    <div className={`min-h-screen p-4 transition-colors duration-200 ${isDarkTheme ? 'bg-gray-800 text-white' : 'bg-white text-gray-800'}`}>
      <div className="max-w-md mx-auto">
        <ThemeToggle isDarkTheme={isDarkTheme} setIsDarkTheme={setIsDarkTheme} />
        <Display expression={expression} result={result} />
        
        <div className="grid grid-cols-4 gap-2">
          <Button label="C" onClick={handleClear} className="col-span-2 bg-red-200 hover:bg-red-300 dark:bg-red-700 dark:hover:bg-red-600" />
          <Button label="⌫" onClick={handleBackspace} className="bg-yellow-200 hover:bg-yellow-300 dark:bg-yellow-700 dark:hover:bg-yellow-600" />
          <Button label="/" onClick={() => handleOperator('/')} className="bg-blue-200 hover:bg-blue-300 dark:bg-blue-700 dark:hover:bg-blue-600" />
          
          {[7, 8, 9].map(num => (
            <Button key={num} label={num.toString()} onClick={() => handleNumber(num.toString())} />
          ))}
          <Button label="*" onClick={() => handleOperator('*')} className="bg-blue-200 hover:bg-blue-300 dark:bg-blue-700 dark:hover:bg-blue-600" />
          
          {[4, 5, 6].map(num => (
            <Button key={num} label={num.toString()} onClick={() => handleNumber(num.toString())} />
          ))}
          <Button label="-" onClick={() => handleOperator('-')} className="bg-blue-200 hover:bg-blue-300 dark:bg-blue-700 dark:hover:bg-blue-600" />
          
          {[1, 2, 3].map(num => (
            <Button key={num} label={num.toString()} onClick={() => handleNumber(num.toString())} />
          ))}
          <Button label="+" onClick={() => handleOperator('+')} className="bg-blue-200 hover:bg-blue-300 dark:bg-blue-700 dark:hover:bg-blue-600" />
          
          <Button label="0" onClick={() => handleNumber('0')} className="col-span-2" />
          <Button label="." onClick={() => handleNumber('.')} />
          <Button label="=" onClick={calculateResult} className="bg-green-200 hover:bg-green-300 dark:bg-green-700 dark:hover:bg-green-600" />
        </div>

        <History history={history} />
      </div>
    </div>
  );
};

export default Calculator;

