# REST01 — лабораторная №2 (.NET 8)

## Запуск

1. Откройте `REST01.sln` в Visual Studio 2022.
2. Убедитесь, что установлен .NET 8 SDK и рабочая нагрузка **ASP.NET и веб-разработка**.
3. Выберите проект `REST01` стартовым.
4. Нажмите `F5`.
5. После запуска откроется Swagger.

Адрес по умолчанию:

`http://localhost:5088/swagger`

## Тестовые учётные записи

Пользователи создаются автоматически при первом запуске приложения.

| Логин | Пароль | Роль |
|---|---|---|
| `reader` | `Reader123!` | `READER` |
| `writer` | `Writer123!` | `WRITER` |

Это демонстрационные данные для лабораторной работы.

## REST API

### Авторизация

`POST /api/Results/SignIn`

```json
{
  "login": "writer",
  "password": "Writer123!"
}
```

В ответе возвращается JWT-токен.

### Получение всей коллекции

`GET /api/Results`

Доступно ролям `READER` и `WRITER`.

### Получение элемента

`GET /api/Results/{k}`

Доступно ролям `READER` и `WRITER`.

### Добавление элемента

`POST /api/Results`

```json
{
  "value": "hello"
}
```

Доступно роли `WRITER`.

### Изменение элемента

`PUT /api/Results/{k}`

```json
{
  "value": "updated"
}
```

Доступно роли `WRITER`.

### Удаление элемента

`DELETE /api/Results/{k}`

Доступно роли `WRITER`.

## Хранение данных

Коллекция сохраняется в:

`REST01/Data/results.json`

Данные Identity Framework сохраняются в:

`REST01/Data/identity.db`

Сервис `Results` зарегистрирован как `Transient` и использует блокировку JSON-файла через `FileShare.None` и общий объект синхронизации.

## Локальные NuGet-пакеты

Для создания локальных пакетов запустите из корня решения:

```powershell
./pack-local.ps1
```

Пакеты будут помещены в каталог `local-nuget`.

После этого можно проверить вариант с подключением именно через NuGet:

```powershell
dotnet run --project REST01 -p:UseLocalNuget=true
```

Обычный запуск Visual Studio использует `ProjectReference`, поэтому проект можно запустить сразу после открытия решения.

## Postman

Коллекция Postman находится здесь:

`Postman/REST01.postman_collection.json`

После выполнения `SignIn` токен можно передать в заголовке:

```text
Authorization: Bearer <token>
```
