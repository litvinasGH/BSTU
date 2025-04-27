import { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';

export default function SignIn() {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  const [error, setError] = useState('');
  const [success, setSuccess] = useState(false);
  const [showPassword, setShowPassword] = useState(false);
  const [formErrors, setFormErrors] = useState({
    email: '',
    password: ''
  });

  useEffect(() => {
    setFormErrors({
      email: email ? (/^\S+@\S+\.\S+$/.test(email) ? '' : 'Неверный формат email') : 'Email обязателен',
      password: password ? (password.length >= 8 ? '' : 'Минимум 8 символов') : 'Пароль обязателен'
    });
  }, [email, password]);

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    
    if (formErrors.email || formErrors.password) return;

    if (email === 'test@test.com' && password === 'test') {
      setSuccess(true);
      setError('');
    } else {
      setError('Неверные учетные данные');
    }
  };

  return (
    <form onSubmit={handleSubmit} className="form">
      <h2>Вход</h2>
      {success && <div className="success">Успешный вход!</div>}
      {error && <div className="error-text">{error}</div>}

      <div className="form-group">
        <input
          value={email}
          onChange={(e) => setEmail(e.target.value)}
          placeholder="Email"
          className={formErrors.email ? 'error-field' : ''}
        />
        {formErrors.email && <div className="error-text">{formErrors.email}</div>}
      </div>

      {/* Поле пароля с иконкой показа */}

      <button type="submit" disabled={!!formErrors.email || !!formErrors.password}>
        Войти
      </button>
      <div className="links">
        <Link to="/sign-up">Нет аккаунта? Зарегистрироваться</Link>
        <Link to="/reset-password">Забыли пароль?</Link>
      </div>
    </form>
  );
}