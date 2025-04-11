import React, { useState, useCallback, useEffect } from 'react';
import './App.css';
import Display from './components/Display';
import Button from './components/Button';
import History from './components/History';
import ThemeToggle from './components/ThemeToggle';

const operators = ['+', '-', '*', '/'];

const Calculator: React.FC = () => {
  const [currentInput, setCurrentInput] = useState('');
  const [result, setResult] = useState<string | null>(null);
  const [history, setHistory] = useState<string[]>([]);
  const [theme, setTheme] = useState<'light' | 'dark'>('light');

  const evaluateExpression = useCallback((expression: string): string => {
    try {
      const result = eval(expression);


      
      if (typeof result !== 'number' || isNaN(result)) {
        throw new Error('Invalid expression');
      }
      
      if (!isFinite(result)) {
        throw new Error('Ошибка: Деление на ноль');
      }
      
      return result.toString();
    } catch (error: any) {
      if (error.message === 'Ошибка: Деление на ноль' || error.message ===  'Invalid expression'){
        throw error;
      }
      throw new Error('Ошибка: Неверное выражение');
    }
  }, []);

  const handleNumber = useCallback((num: string) => {
    setCurrentInput(prev => {
      const lastChar = prev.slice(-1);
      if (prev === '0' && num === '0') return prev;
      if (prev === '0' && num !== '0') return num;
      return prev + num;
    });
  }, []);

  const handleOperator = useCallback((op: string) => {
    setCurrentInput(prev => {
      if (prev === '' && op === '-') return op;
      const lastChar = prev.slice(-1);
      
      if (operators.includes(lastChar)) {
        return prev.slice(0, -1) + op;
      }
      return prev + op;
    });
  }, []);

  const handleDecimal = useCallback(() => {
    setCurrentInput(prev => {
      const parts = prev.split(/[+\-*/]/);
      const lastPart = parts[parts.length - 1];
      
      if (!lastPart.includes('.')) {
        return prev + (lastPart === '' ? '0.' : '.');
      }
      return prev;
    });
  }, []);

  const handleEqual = useCallback(() => {
    try {
      if (!currentInput) return;
      
      const res = evaluateExpression(currentInput);
      setResult(res);
      setHistory(prev => [...prev, `${currentInput}=${res}`]);
      setCurrentInput('');
    } catch (error) {
      setResult(error instanceof Error ? error.message : 'Unknown error');
      setCurrentInput('');
    }
  }, [currentInput, evaluateExpression]);

  const handleClear = useCallback(() => {
    setCurrentInput('');
    setResult(null);
  }, []);

  const handleBackspace = useCallback(() => {
    setCurrentInput(prev => prev.slice(0, -1));
  }, []);

  const handleKeyPress = useCallback((e: KeyboardEvent) => {
    if (e.key >= '0' && e.key <= '9') handleNumber(e.key);
    if (operators.includes(e.key)) handleOperator(e.key);
    if (e.key === '.') handleDecimal();
    if (e.key === 'Enter') handleEqual();
    if (e.key === 'Backspace') handleBackspace();
    if (e.key === 'Escape') handleClear();
  }, [handleNumber, handleOperator, handleDecimal, handleEqual, handleBackspace, handleClear]);

  useEffect(() => {
    window.addEventListener('keydown', handleKeyPress);
    return () => window.removeEventListener('keydown', handleKeyPress);
  }, [handleKeyPress]);

  const toggleTheme = () => {
    setTheme(prev => prev === 'light' ? 'dark' : 'light');
  };

  return (
    <div className={`App ${theme}`}>
      <div className="calculator">
        <ThemeToggle theme={theme} toggleTheme={toggleTheme} />
        <Display currentInput={currentInput} result={result} />
        <div className="buttons">
          <Button value="C" onClick={handleClear} />
          <Button value="⌫" onClick={handleBackspace} />
          <Button value="(" onClick={() => {}} disabled />
          <Button value=")" onClick={() => {}} disabled />
          {[7, 8, 9].map(n => (
            <Button key={n} value={n.toString()} onClick={handleNumber} />
          ))}
          <Button value="/" onClick={handleOperator} />
          {[4, 5, 6].map(n => (
            <Button key={n} value={n.toString()} onClick={handleNumber} />
          ))}
          <Button value="*" onClick={handleOperator} />
          {[1, 2, 3].map(n => (
            <Button key={n} value={n.toString()} onClick={handleNumber} />
          ))}
          <Button value="-" onClick={handleOperator} />
          <Button value="0" onClick={handleNumber} />
          <Button value="." onClick={handleDecimal} />
          <Button value="+" onClick={handleOperator} />
          <Button value="=" onClick={handleEqual} className="equals" />
        </div>
        <History history={history} />
      </div>
    </div>
  );
};

export default Calculator;