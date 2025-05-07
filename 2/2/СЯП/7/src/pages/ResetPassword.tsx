import React, { useState, ChangeEvent, FormEvent } from 'react';
import { Link } from 'react-router-dom';
import FormInput from '../components/FormInput';
import { validateEmail } from '../utils/validation';

const ResetPassword: React.FC = () => {
  const [email, setEmail] = useState<string>('');
  const [error, setError] = useState<string>('');
  const [success, setSuccess] = useState<string>('');

  const handleSubmit = (e: FormEvent) => {
    e.preventDefault();
    const errText = validateEmail(email);
    if (!errText) { 
      let pass = '';
      const symbols = "!@#$%^&*()_+-=[]{}|;:,.<>?";
      const chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789" + symbols;

      for (let i = 0; i < 12; i++) {
        const randomIndex = Math.floor(Math.random() * chars.length);
        pass += chars[randomIndex];
      }

      setSuccess(`Your new password is: ${pass}`); 
      setError('');
    } else { 
      setError(errText); 
      setSuccess(''); 
    }
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
