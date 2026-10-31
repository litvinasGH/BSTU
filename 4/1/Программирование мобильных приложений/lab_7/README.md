# Лабораторная №7 — SQFLite, File System, изоляты

Flutter-приложение по лабораторной работе №7: список компьютерных игр с хранением
в SQLite, работа с файловой системой ОС через `path_provider` и демонстрация
изолятов (`compute()` и `Isolate.spawn()`).

Ответы на вопросы лабораторной — в файле [answers.md](answers.md).

Интерфейс намеренно минимальный: плоские блоки с рамками, без темы, карточек
и лишних элементов.

## Структура проекта

```
lib/
├── main.dart                          точка входа, MaterialApp + Listscreen
├── database/
│   ├── gamerecord.dart                сущность (title, developer, year, type) + map/JSON
│   ├── game_database.dart             SQLite: CRUD, поиск, мульти-сортировка, seed
│   └── sort_field.dart                модель поля сортировки (колонка + направление)
├── services/
│   ├── directory_service.dart         каталоги ОС + матрица платформ + обработка ошибок
│   ├── file_service.dart               запись/чтение/листинг файлов (dart:io)
│   └── isolate_service.dart           работа в изолятах: compute() и Isolate.spawn()
├── widgets/
│   └── simple_button.dart              кнопка: рамка + фон + фиксированная высота
└── screens/
    ├── listscreen.dart                список с ItemExtent, поиск, мульти-сортировка
    ├── detailscreen.dart              подробная информация о выбранной игре
    ├── editscreen.dart                добавление и редактирование записи
    ├── filescreen.dart                каталоги ОС, запись/чтение, расположение файлов
    ├── isolatescreen.dart             демонстрация изолятов
    └── parts/itemlist.dart            строка элемента списка
```

## Соответствие пунктам задания

| Пункт | Реализация |
|---|---|
| 1. Список из двух экранов, сущность ≥ 4 свойств | `listscreen.dart` + `detailscreen.dart`, сущность `GameRecord` (title, developer, year, type) |
| 2. Параметр `ItemExtent` | `ListView.builder(itemExtent: 60, ...)` в `listscreen.dart` |
| 3. CRUD в SQLite | `database/game_database.dart` (пакет `sqflite`), UI — список, детали, форма |
| 4. Страница изменения и добавления | `screens/editscreen.dart` (одна форма на оба случая) |
| 5. Сортировка и поиск | Поиск по 4 полям в `getGames`, сортировка по нескольким полям с выбором направления — панель `_SortPanel` |
| 6. Работа с ФС: 9 каталогов, разные данные | `services/directory_service.dart` + `services/file_service.dart`, экран «Файлы» |
| 7. Ошибки получения каталогов по платформам | `DirectoryService.resolve` — `MissingPlatformDirectoryException`, `UnsupportedError`, сообщение со списком неподдерживаемых платформ |
| 8. Изоляты (запись и чтение файла) | `services/isolate_service.dart`: `compute()` и `Isolate.spawn()` с `SendPort`/`ReceivePort`, экран «Изоляты» |
| 9. Расположение существующих файлов | Раздел «Расположение существующих файлов» на экране «Файлы» (`FileService.listAllFiles`) |

## Запуск

```bash
flutter pub get
flutter run            # Android или iOS
```

Приложение использует `dart:io` и плагин `path_provider`, поэтому сборка под Web
не поддерживается — лабораторная ориентирована на Android и iOS.

## Тесты

```bash
flutter test                                    # юнит- и widget-тесты
flutter test integration_test/app_test.dart -d <device>   # интеграционные тесты на устройстве
```

Интеграционные тесты проверяют на реальном устройстве: загрузку списка из SQLite,
экран деталей, добавление записи через форму, поиск, панель сортировки,
определение каталогов с сообщением о неподдерживаемых платформах, запись/чтение
файлов в разных каталогах и работу в изолятах.
