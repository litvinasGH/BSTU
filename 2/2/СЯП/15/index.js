document.addEventListener('DOMContentLoaded', function() {
    const form = document.querySelector('form');
    form.addEventListener('submit', function(e) {
        e.preventDefault();
        clearErrors();

        let isValid = true;

        const surname = document.getElementById('surname').value.trim();
        if (!surname) {
            showError('surname', 'Поле не должно быть пустым');
            isValid = false;
        } else if (surname.length > 20) {
            showError('surname', 'Максимум 20 символов');
            isValid = false;
        } else if (!/^[a-zA-Zа-яА-Я]+$/i.test(surname)) {
            showError('surname', 'Только русские/английские буквы');
            isValid = false;
        }

        const name = document.getElementById('name').value.trim();
        if (!name) {
            showError('name', 'Поле не должно быть пустым');
            isValid = false;
        } else if (name.length > 20) {
            showError('name', 'Максимум 20 символов');
            isValid = false;
        } else if (!/^[a-zA-Zа-яА-Я]+$/i.test(name)) {
            showError('name', 'Только русские/английские буквы');
            isValid = false;
        }

        const email = document.getElementById('email').value.trim();
        const emailRegex = /^[^\s@]+@[a-zA-Z]{2,5}\.[a-zA-Z]{2,3}$/;
        if (!email) {
            showError('email', 'Поле не должно быть пустым');
            isValid = false;
        } else if (!emailRegex.test(email)) {
            showError('email', 'Недопустимый формат');
            isValid = false;
        }

        const phone = document.getElementById('phone').value.trim();
        const phoneRegex = /^\(0\d{2}\)\d{3}-\d{2}-\d{2}$/;
        if (!phone) {
            showError('phone', 'Поле не должно быть пустым');
            isValid = false;
        } else if (!phoneRegex.test(phone)) {
            showError('phone', 'Неверный формат');
            isValid = false;
        }

        const city = document.getElementById('city').value.trim();
        if (!city) {
            showError('city', 'Поле не должно быть пустым');
            isValid = false;
        }

        const courseSelected = document.querySelector('input[name="course"]:checked');
        if (!courseSelected) {
            showError('course', 'Выберите курс');
            isValid = false;
        }

        const about = document.getElementById('about').value.trim();
        if (!about) {
            showError('about', 'Поле не должно быть пустым');
            isValid = false;
        } else if (about.length > 250) {
            showError('about', 'Максимум 250 символов');
            isValid = false;
        }

        if (isValid) {
            const city = document.getElementById('city').value.trim();
            const course = document.querySelector('input[name="course"]:checked')?.value;
            const isBstuChecked = document.getElementById('bstu').checked;

            let messages = [];
            if (city !== 'Минск') messages.push('Город должен быть Минск');
            if (course !== '3') messages.push('Курс должен быть 3');
            if (!isBstuChecked) messages.push('Отметьте, что учитесь в БГТУ');

            if (messages.length > 0) {
                const confirmation = confirm(
                    `Вы уверены в следующих ответах?\n- ${messages.join('\n- ')}\nНажмите OK для подтверждения.`
                );
                if (!confirmation) {
                    return; 
                }
            }

            form.submit(); 
        }
    });
});

function showError(fieldId, message) {
    const errorElement = document.getElementById(`${fieldId}-error`);
    if (errorElement) {
        errorElement.textContent = message;
    }
}

function clearErrors() {
    const errors = document.querySelectorAll('.error-message');
    errors.forEach(error => {
        error.textContent = '';
    });
}