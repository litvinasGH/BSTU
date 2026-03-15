#!/usr/bin/env fish

echo "Тестирование веб-сервера GO03_01"
echo "================================"
echo ""

# Проверяем, запущен ли сервер
echo "Проверка доступности сервера..."
if not curl -s http://localhost:3000 > /dev/null
    echo "Ошибка: Сервер недоступен"
    exit 1
end

echo "Сервер доступен, начинаем тестирование..."
echo ""

# Тестирование GET /A
echo "1. Тестирование GET /A"
curl -s -X GET http://localhost:3000/A
echo ""
echo ""

# Тестирование GET /A/B
echo "2. Тестирование GET /A/B"
curl -s -X GET http://localhost:3000/A/B
echo ""
echo ""

# Тестирование POST /A
echo "3. Тестирование POST /A"
curl -s -X POST http://localhost:3000/A
echo ""
echo ""

# Тестирование POST /A/B
echo "4. Тестирование POST /A/B"
curl -s -X POST http://localhost:3000/A/B
echo ""
echo ""

# Тестирование PUT /A
echo "5. Тестирование PUT /A"
curl -s -X PUT http://localhost:3000/A
echo ""
echo ""

# Тестирование PUT /A/B
echo "6. Тестирование PUT /A/B"
curl -s -X PUT http://localhost:3000/A/B
echo ""
echo ""

# Тестирование нестандартных маршрутов
echo "7. Тестирование GET /custom"
curl -s -X GET http://localhost:3000/custom
echo ""
echo ""

echo "8. Тестирование POST /other/path"
curl -s -X POST http://localhost:3000/other/path
echo ""
echo ""

echo "9. Тестирование PUT /unlisted"
curl -s -X PUT http://localhost:3000/unlisted
echo ""
echo ""

echo "10. Тестирование корневого пути /"
curl -s -X GET http://localhost:3000/
echo ""
echo ""

echo "================================"
echo "Тестирование завершено!"
echo "Проверьте вывод сервера для просмотра записей лога."