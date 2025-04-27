import { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';

interface Errors {
  name?: string;
  email?: string;
  password?: string;
  confirmPassword?: string;
}

const mockEmails = ['existing@test.com']; // Mock существующих email

export default function SignUp() {
  const [formData, setFormData] = useState({
    name: '',
    email: '',
    password: '',
    confirmPassword: ''
  });
  const [errors, setErrors] = useState<Errors>({});
  const [success, setSuccess] = useState(false);
  const [showPassword, setShowPassword] = useState(false);

  // Валидация в реальном времени
  useEffect(() => {
    const validate = () => {
      const newErrors: Errors = {
        name: validateName(formData.name),
        email: validateEmail(formData.email),
        password: validatePassword(formData.password),
        confirmPassword: formData.password !== formData.confirmPassword 
          ? 'Пароли не совпадают' 
          : undefined
      };
      setErrors(newErrors);
    };
    validate();
  }, [formData]);

  const validateName = (value: string) => {
    if (!value) return 'Имя обязательно';
    if (!/^[a-zA-Zа-яА-Я ]+$/.test(value)) return 'Только буквы и пробелы';
    if (value.length < 2 || value.length > 50) return 'Длина от 2 до 50 символов';
  };

  const validateEmail = (value: string) => {
    if (!value) return 'Email обязателен';
    if (!/^\S+@\S+\.\S+$/.test(value)) return 'Неверный формат email';
    if (mockEmails.includes(value)) return 'Email уже используется';
  };

  const validatePassword = (value: string) => {
    if (!value) return 'Пароль обязателен';
    if (value.length < 8) return 'Минимум 8 символов';
    if (!/(?=.*\d)(?=.*[a-z])(?=.*[A-Z])/.test(value)) 
      return 'Заглавная, строчная буква и цифра';
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    
    if (Object.values(errors).some(Boolean)) return;

    // Mock запрос на проверку email
    await new Promise(resolve => setTimeout(resolve, 500));
    
    if (mockEmails.includes(formData.email)) {
      setErrors({...errors, email: 'Email уже используется'});
      return;
    }

    setSuccess(true);
    setFormData({ name: '', email: '', password: '', confirmPassword: '' });
  };

  return (
    <form onSubmit={handleSubmit} className="form">
      <h2>Регистрация</h2>
      {success && <div className="success">Успешная регистрация!</div>}
      
      <div className="form-group">
        <input
          value={formData.name}
          onChange={(e) => setFormData({...formData, name: e.target.value})}
          placeholder="Имя"
          className={errors.name ? 'error-field' : ''}
        />
        {errors.name && <div className="error-text">{errors.name}</div>}
      </div>

      {/* Аналогичные блоки для остальных полей с иконкой показа пароля */}

      <button type="submit" disabled={Object.values(errors).some(Boolean)}>
        Зарегистрироваться
      </button>
      <Link to="/sign-in">Уже есть аккаунт? Войти</Link>
    </form>
  );
}