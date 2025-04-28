import React, { useState, FocusEvent, ChangeEvent } from 'react';
import ErrorMessage from './ErrorMessage';

interface FormInputProps {
  label: string;
  type: string;
  name: string;
  value: string;
  onChange: (e: ChangeEvent<HTMLInputElement>) => void;
  onBlur?: (e: FocusEvent<HTMLInputElement>) => void;
  error?: string;
}

const FormInput: React.FC<FormInputProps> = ({ label, type, name, value, onChange, onBlur, error }) => {
  const [show, setShow] = useState(false);
  const isPassword = type === 'password';
  const inputType = isPassword ? (show ? 'text' : 'password') : type;

  return (
    <div className="form-group">
      <label htmlFor={name}>{label}</label>
      <div className="password-wrapper">
        <input
          id={name}
          name={name}
          type={inputType}
          value={value}
          onChange={onChange}
          onBlur={onBlur}
          className={error ? 'input error' : 'input'}
        />
        {isPassword && (
          <button
            type="button"
            className="toggle-btn"
            onClick={() => setShow(prev => !prev)}
          >
            {show ? 'Hide' : 'Show'}
          </button>
        )}
      </div>
      {error && <ErrorMessage message={error} />}
    </div>
  );
};

export default FormInput;