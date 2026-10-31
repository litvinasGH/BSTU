import React, { useState, ChangeEvent, FormEvent } from 'react';
import { Link } from 'react-router-dom';
import FormInput from '../components/FormInput';
import { validateName, validateEmail, validatePassword, validateConfirmPassword } from '../utils/validation';

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
    errs.name = validateName(values.name) || '';
    errs.email = validateEmail(values.email) || '';
    errs.password = validatePassword(values.password) || '';
    errs.confirm = validateConfirmPassword(values.password, values.confirm) || '';
    return Object.fromEntries(Object.entries(errs).filter(([_, v]) => v)) as Partial<Record<keyof SignUpValues, string>>;
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