using Microsoft.AspNetCore.HttpLogging; // Импорт пространства имен для логирования HTTP-запросов и ответов

internal class Program // Объявление внутреннего класса Program
{
    private static void Main(string[] args) // Точка входа в программу
    {
        var builder = WebApplication.CreateBuilder(args); // Создание билдера для настройки и запуска веб-приложения

        // Настройка сервисов для логирования HTTP-запросов и ответов
        builder.Services.AddHttpLogging(o =>
        {
            o.LoggingFields = HttpLoggingFields.RequestMethod | // Логирование метода HTTP-запроса (GET, POST и т.д.)
            HttpLoggingFields.RequestPath | // Логирование пути запроса
            HttpLoggingFields.ResponseStatusCode | // Логирование статус-кода ответа
            HttpLoggingFields.ResponseBody; // Логирование тела ответа
        });

        // Настройка фильтрации логов: только логи уровня Information и выше для пространства имен "Microsoft.AspNetCore.HttpLogging"
        builder.Logging.AddFilter("Microsoft.AspNetCore.HttpLogging", LogLevel.Information);

        var app = builder.Build(); // Построение приложения

        app.UseHttpLogging(); // Включение middleware для логирования HTTP-запросов и ответов

        app.MapGet("/", () => "Моё первое ASPA"); // Настройка маршрута для GET-запросов по корневому пути "/"

        app.Run(); // Запуск приложения
    }
}