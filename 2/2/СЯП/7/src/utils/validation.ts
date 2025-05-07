export const validateName = (name: string): string | null => {
    if (!name.trim()) return 'Name is required';
    if (!/^[A-Za-zА-Яа-яЁё ]{2,50}$/.test(name)) return 'Invalid name';
    return null;
};

export const validateEmail = (email: string): string | null => {
    if (!email) return 'Email is required';
    if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(email)) return 'Invalid email';
    return null;
};

export const validatePassword = (password: string): string | null => {
    if (!password) return 'Password is required';
    if (password.length < 8) return 'Minimum 8 chars';
    if (!/(?=.*[a-z])(?=.*[A-Z])(?=.*\d)/.test(password)) return 'Must include uppercase, lowercase and digit';
    return null;
};

export const validateConfirmPassword = (password: string, confirm: string): string | null => {
    if (!confirm) return 'Confirm password';
    if (confirm !== password) return 'Passwords do not match';
    return null;
};

// Добавляем пустой экспорт, чтобы файл считался модулем
export {};