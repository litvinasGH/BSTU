import React, { useState, ChangeEvent, FormEvent } from 'react';
import { Link } from 'react-router-dom';
import FormInput from '../components/FormInput';

const ResetPassword: React.FC = () => {
  const [email, setEmail] = useState<string>('');
  const [error, setError] = useState<string>('');
  const [success, setSuccess] = useState<string>('');

  const validate = (): string => {
    if (!email) return 'Email is required';
    if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(email)) return 'Invalid email';
    return '';
  };

  const handleSubmit = (e: FormEvent) => {
    e.preventDefault();
    const errText = validate();
    if (!errText) { setSuccess(`Your new password is: NewPass123`); setError(''); }
    else { setError(errText); setSuccess(''); }
  };

  const handleChange = (e: ChangeEvent<HTMLInputElement>) => setEmail(e.target.value);

  return (
    <div className="form-page">
      <h2>Reset Password</h2>
      <form onSubmit={handleSubmit} noValidate>
        <FormInput label="Email" type="email" name="email" value={email} onChange={handleChange} error={error} />
        <button type="submit" className="btn">Send Reset Link</button>
        {success && <div className="success-message">{success}</div>}
      </form>
      <p><Link to="/sign-in">Back to Sign In</Link></p>
    </div>
  );
};

export default ResetPassword;
