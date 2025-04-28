import React, { useState, ChangeEvent, FormEvent } from 'react';
import { Link } from 'react-router-dom';
import FormInput from '../components/FormInput';

interface SignUpValues {
  name: string;
  email: string;
  password: string;
  confirm: string;
}

const SignUp: React.FC = () => {
  const [values, setValues] = useState<SignUpValues>({ name: '', email: '', password: '', confirm: '' });
  const [errors, setErrors] = useState<Partial<Record<keyof SignUpValues, string>>>({});
  const [success, setSuccess] = useState<string>('');

  const validate = () => {
    const errs: Partial<Record<keyof SignUpValues, string>> = {};
    if (!values.name.trim()) errs.name = 'Name is required';
    else if (!/^[A-Za-zА-Яа-яЁё ]{2,50}$/.test(values.name)) errs.name = 'Invalid name';

    if (!values.email) errs.email = 'Email is required';
    else if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(values.email)) errs.email = 'Invalid email';

    if (!values.password) errs.password = 'Password is required';
    else if (values.password.length < 8) errs.password = 'Minimum 8 chars';
    else if (!/(?=.*[a-z])(?=.*[A-Z])(?=.*\d)/.test(values.password)) errs.password = 'Must include uppercase, lowercase and digit';

    if (!values.confirm) errs.confirm = 'Confirm password';
    else if (values.confirm !== values.password) errs.confirm = 'Passwords do not match';

    return errs;
  };

  const handleChange = (e: ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    setValues(prev => ({ ...prev, [name]: value }));
  };

  const handleSubmit = (e: FormEvent) => {
    e.preventDefault();
    const validation = validate();
    if (Object.keys(validation).length === 0) {
      setSuccess('Registration successful!');
      setErrors({});
    } else {
      setErrors(validation);
      setSuccess('');
    }
  };

  return (
    <div className="form-page">
      <h2>Sign Up</h2>
      <form onSubmit={handleSubmit} noValidate>
        <FormInput label="Name" type="text" name="name" value={values.name} onChange={handleChange} onBlur={() => setErrors(validate())} error={errors.name} />
        <FormInput label="Email" type="email" name="email" value={values.email} onChange={handleChange} onBlur={() => setErrors(validate())} error={errors.email} />
        <FormInput label="Password" type="password" name="password" value={values.password} onChange={handleChange} onBlur={() => setErrors(validate())} error={errors.password} />
        <FormInput label="Confirm Password" type="password" name="confirm" value={values.confirm} onChange={handleChange} onBlur={() => setErrors(validate())} error={errors.confirm} />
        <button type="submit" className="btn">Register</button>
        {success && <div className="success-message">{success}</div>}
      </form>
      <p>Already have an account? <Link to="/sign-in">Sign In</Link></p>
    </div>
  );
};

export default SignUp;