import { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';

export default function ResetPassword() {
  const [email, setEmail] = useState('');
  const [error, setError] = useState('');
  const [success, setSuccess] = useState(false);
  const [newPassword, setNewPassword] = useState('');

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    
    if (!/^\S+@\S+\.\S+$/.test(email)) {
      setError('Неверный формат email');
      return;
    }

    // Генерация пароля
    const generatedPassword = Math.random().toString(36).slice(-8);
    setNewPassword(generatedPassword);
    setSuccess(true);
    setError('');
  };

  return (
    <form onSubmit={handleSubmit} className="form">
      <h2>Восстановление пароля</h2>
      {success && (
        <div className="success">
          Новый пароль: <strong>{newPassword}</strong>
        </div>
      )}
      {error && <div className="error-text">{error}</div>}

      <div className="form-group">
        <input
          value={email}
          onChange={(e) => setEmail(e.target.value)}
          placeholder="Email"
          className={error ? 'error-field' : ''}
        />
      </div>

      <button type="submit">Восстановить</button>
      <Link to="/sign-in">Вспомнили пароль? Войти</Link>
    </form>
  );
}