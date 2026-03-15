@echo off
echo Тестирование веб-сервера GO03_01
echo ================================
echo.

REM Проверяем, запущен ли сервер
echo Проверка доступности сервера...
ping -n 1 127.0.0.1 > nul
if errorlevel 1 (
    echo Ошибка: Сервер недоступен
    exit /b 1
)

echo Сервер доступен, начинаем тестирование...
echo.

REM Тестирование GET /A
echo 1. Тестирование GET /A
curl -s -X GET http://localhost:3000/A
echo.
echo.

REM Тестирование GET /A/B
echo 2. Тестирование GET /A/B
curl -s -X GET http://localhost:3000/A/B
echo.
echo.

REM Тестирование POST /A
echo 3. Тестирование POST /A
curl -s -X POST http://localhost:3000/A
echo.
echo.

REM Тестирование POST /A/B
echo 4. Тестирование POST /A/B
curl -s -X POST http://localhost:3000/A/B
echo.
echo.

REM Тестирование PUT /A
echo 5. Тестирование PUT /A
curl -s -X PUT http://localhost:3000/A
echo.
echo.

REM Тестирование PUT /A/B
echo 6. Тестирование PUT /A/B
curl -s -X PUT http://localhost:3000/A/B
echo.
echo.

REM Тестирование нестандартных маршрутов
echo 7. Тестирование GET /нестандартный
curl -s -X GET http://localhost:3000/%D0%BD%D0%B5%D1%81%D1%82%D0%B0%D0%BD%D0%B4%D0%B0%D1%80%D1%82%D0%BD%D1%8B%D0%B9
echo.
echo.

echo 8. Тестирование POST /другой/путь
curl -s -X POST http://localhost:3000/%D0%B4%D1%80%D1%83%D0%B3%D0%BE%D0%B9/%D0%BF%D1%83%D1%82%D1%8C
echo.
echo.

echo 9. Тестирование PUT /неперечисленный
curl -s -X PUT http://localhost:3000/%D0%BD%D0%B5%D0%BF%D0%B5%D1%80%D0%B5%D1%87%D0%B8%D1%81%D0%BB%D0%B5%D0%BD%D0%BD%D1%8B%D0%B9
echo.
echo.

echo 10. Тестирование корневого пути /
curl -s -X GET http://localhost:3000/
echo.
echo.

echo ================================
echo Тестирование завершено!
echo Проверьте файл server.log для просмотра записей лога.
pause