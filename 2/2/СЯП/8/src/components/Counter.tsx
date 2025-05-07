import React from 'react';
import { useSelector, useDispatch } from 'react-redux';
import { RootState } from '../redux/store';
import { increment, decrement, reset } from '../redux/actions';
import { Button } from './Button';
import styles from './Counter.module.css';

export const Counter: React.FC = () => {
  const count = useSelector((state: RootState) => state.count);
  const dispatch = useDispatch();

  return (
    <div className={styles.counter}>
      <h1 className={styles.value}>{count}</h1>
      <div className={styles.buttons}>
        <Button onClick={() => dispatch(decrement())}>-</Button>
        <Button onClick={() => dispatch(reset())}>Reset</Button>
        <Button onClick={() => dispatch(increment())}>+</Button>
      </div>
    </div>
  );
};