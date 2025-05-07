import React, { useState, ChangeEvent, FormEvent } from 'react';
import { Link } from 'react-router-dom';
import FormInput from '../components/FormInput';
import { validateEmail, validatePassword } from '../utils/validation';

interface SignInValues { email: string; password: string; }

const SignIn: React.FC = () => {
  const [values, setValues] = useState<SignInValues>({ email: '', password: '' });
  const [errors, setErrors] = useState<Partial<Record<keyof SignInValues | 'form', string>>>({});
  const [success, setSuccess] = useState<string>('');

  const validate = () => {
    const errs: Partial<Record<keyof SignInValues, string>> = {};
    errs.email = validateEmail(values.email) || '';
    errs.password = validatePassword(values.password) || '';
    return Object.fromEntries(Object.entries(errs).filter(([_, v]) => v)) as Partial<Record<keyof SignInValues, string>>;
  };

  const handleChange = (e: ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setValues(prev => ({ ...prev, [name]: value }));
  };

  const handleSubmit = (e: FormEvent) => {
    e.preventDefault();
    const validation = validate();
    if (Object.keys(validation).length === 0) {
      if (/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(values.email) && /(?=.*[a-z])(?=.*[A-Z])(?=.*\d)/.test(values.password)) {
        setSuccess('Login successful!'); setErrors({});
      } else { setErrors({ form: 'Invalid credentials' }); setSuccess(''); }
    } else { setErrors(validation); setSuccess(''); }
  };

  return (
    <div className="form-page">
      <h2>Sign In</h2>
      <form onSubmit={handleSubmit} noValidate>
        <FormInput label="Email" type="email" name="email" value={values.email} onChange={handleChange} error={errors.email ?? errors.form} />
        <FormInput label="Password" type="password" name="password" value={values.password} onChange={handleChange} error={errors.password} />
        <button type="submit" className="btn">Login</button>
        {success && <div className="success-message">{success}</div>}
      </form>
      <p><Link to="/reset-password">Forgot password?</Link></p>
      <p>Don't have an account? <Link to="/sign-up">Register</Link></p>
    </div>
  );
};

export default SignIn;
