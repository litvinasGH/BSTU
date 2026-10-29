import 'package:flutter/material.dart';
import 'package:flutter_test/flutter_test.dart';
import 'package:integration_test/integration_test.dart';
import 'package:lab_2/database/game_database.dart';
import 'package:lab_2/database/gamerecord.dart';
import 'package:lab_2/main.dart' as app;
import 'package:lab_2/services/directory_service.dart';
import 'package:lab_2/services/file_service.dart';
import 'package:lab_2/services/isolate_service.dart';

void main() {
  IntegrationTestWidgetsFlutterBinding.ensureInitialized();

  group('Экран списка', () {
    testWidgets('загружает игры из базы данных SQLite', (WidgetTester tester) async {
      await GameDatabase.instance.close();
      await GameDatabase.instance.dropDatabase();

      app.main();
      await tester.pumpAndSettle(const Duration(seconds: 2));

      expect(find.text('Список игр'), findsOneWidget);
      expect(find.text('Counter-Strike 2'), findsOneWidget);
      expect(find.text('Civilization VI'), findsOneWidget);
    });

    testWidgets('открывает экран с подробной информацией', (WidgetTester tester) async {
      app.main();
      await tester.pumpAndSettle(const Duration(seconds: 2));

      await tester.tap(find.text('Civilization VI'));
      await tester.pumpAndSettle();

      expect(find.text('О игре'), findsOneWidget);
      expect(find.textContaining('Firaxis Games'), findsOneWidget);
      expect(find.textContaining('2016'), findsOneWidget);
    });

    testWidgets('добавляет новую запись через форму', (WidgetTester tester) async {
      app.main();
      await tester.pumpAndSettle(const Duration(seconds: 2));

      await tester.tap(find.text('Добавить'));
      await tester.pumpAndSettle();

      await tester.enterText(find.widgetWithText(TextFormField, 'Название'), 'Тестовая игра');
      await tester.enterText(find.widgetWithText(TextFormField, 'Разработчик'), 'Test Studio');
      await tester.enterText(find.widgetWithText(TextFormField, 'Год выпуска'), '2024');
      await tester.pumpAndSettle();

      await tester.tap(find.text('Добавить'));
      await tester.pumpAndSettle(const Duration(seconds: 2));

      expect(find.text('Тестовая игра'), findsOneWidget);
    });

    testWidgets('ищет записи по подстроке', (WidgetTester tester) async {
      app.main();
      await tester.pumpAndSettle(const Duration(seconds: 2));

      await tester.enterText(find.byType(TextField).first, 'Skyrim');
      await tester.pumpAndSettle(const Duration(seconds: 2));

      expect(find.text('Skyrim'), findsWidgets);
      expect(find.text('DOOM Eternal'), findsNothing);
    });

    testWidgets('открывает панель сортировки по нескольким полям', (WidgetTester tester) async {
      app.main();
      await tester.pumpAndSettle(const Duration(seconds: 2));

      await tester.tap(find.text('Сортировка'));
      await tester.pumpAndSettle();

      expect(find.text('Сортировка по нескольким полям'), findsOneWidget);
      expect(find.text('Название'), findsWidgets);
      expect(find.text('Год выпуска'), findsWidgets);
    });

    testWidgets('сортирует по выбранному полю и меняет направление', (WidgetTester tester) async {
      app.main();
      await tester.pumpAndSettle(const Duration(seconds: 2));

      await tester.tap(find.text('Сортировка'));
      await tester.pumpAndSettle();

      final Finder titleRow = find.ancestor(of: find.text('Название'), matching: find.byType(Row)).first;
      final Finder yearRow = find.ancestor(of: find.text('Год выпуска'), matching: find.byType(Row)).first;

      await tester.tap(find.descendant(of: titleRow, matching: find.byType(Checkbox)));
      await tester.pumpAndSettle();

      await tester.tap(find.descendant(of: yearRow, matching: find.byType(Checkbox)));
      await tester.pumpAndSettle();

      await tester.tap(find.text('Применить'));
      await tester.pumpAndSettle(const Duration(seconds: 2));

      expect(
        tester.getTopLeft(find.text('Skyrim')).dy,
        lessThan(tester.getTopLeft(find.text('Counter-Strike 2')).dy),
      );
    });

    testWidgets('записывает файл на экране файловой системы', (WidgetTester tester) async {
      app.main();
      await tester.pumpAndSettle(const Duration(seconds: 2));

      await tester.tap(find.text('Файлы'));
      await tester.pumpAndSettle(const Duration(seconds: 2));

      expect(find.text('Temporary'), findsWidgets);
      expect(find.text('Application Support'), findsWidgets);

      await tester.tap(find.text('Записать').first);
      await tester.pumpAndSettle(const Duration(seconds: 3));

      expect(find.textContaining('Записано:'), findsOneWidget);

      final Finder scrollable = find.byType(Scrollable).first;

      await tester.scrollUntilVisible(find.text('Application Library'), 300, scrollable: scrollable);
      await tester.pumpAndSettle();

      expect(find.textContaining('Не поддерживаются платформы'), findsWidgets);

      await tester.scrollUntilVisible(
        find.text('Расположение существующих файлов'),
        300,
        scrollable: scrollable,
      );
      await tester.pumpAndSettle();

      expect(find.textContaining('games_'), findsWidgets);
    });

    testWidgets('читает файл и показывает данные на экране', (WidgetTester tester) async {
      app.main();
      await tester.pumpAndSettle(const Duration(seconds: 2));

      await tester.tap(find.text('Файлы'));
      await tester.pumpAndSettle(const Duration(seconds: 2));

      await tester.tap(find.text('Записать').first);
      await tester.pumpAndSettle(const Duration(seconds: 3));

      await tester.tap(find.text('Прочитать').first);
      await tester.pumpAndSettle(const Duration(seconds: 2));

      expect(find.textContaining('[Temporary]'), findsWidgets);

      await tester.tap(find.text('Закрыть'));
      await tester.pumpAndSettle();
    });

    testWidgets('запускает задачу в изоляте с экрана изолятов', (WidgetTester tester) async {
      app.main();
      await tester.pumpAndSettle(const Duration(seconds: 2));

      await tester.tap(find.text('Изоляты'));
      await tester.pumpAndSettle();

      await tester.tap(find.text('Записать compute()'));
      await tester.pumpAndSettle(const Duration(seconds: 3));

      expect(find.textContaining('compute()'), findsWidgets);
    });
  });

  group('Файловая система', () {
    testWidgets('определяет каталоги платформы и сообщает о неподдерживаемых',
        (WidgetTester tester) async {
      final List<DirectoryInfo> infos = await DirectoryService.resolveAll();

      expect(infos.length, AppDirectory.values.length);

      for (final DirectoryInfo info in infos) {
        if (info.isResolved) {
          expect(info.paths, isNotEmpty);
        } else {
          expect(info.error, isNotNull);
          expect(info.error, contains('Не поддерживаются платформы'));
        }
      }
    });

    testWidgets('записывает сущность в разные каталоги и читает обратно',
        (WidgetTester tester) async {
      final List<GameRecord> games = await GameDatabase.instance.getGames();

      expect(games, isNotEmpty);

      final DirectoryInfo temporary = await DirectoryService.resolve(AppDirectory.temporary);
      final DirectoryInfo documents = await DirectoryService.resolve(AppDirectory.applicationDocuments);

      expect(temporary.isResolved, isTrue);
      expect(documents.isResolved, isTrue);

      final String temporaryPath = await FileService.writeGames(AppDirectory.temporary, games);
      final String documentsPath = await FileService.writeGames(AppDirectory.applicationDocuments, games);

      expect(temporaryPath, isNot(documentsPath));

      final List<GameRecord> fromTemporary = await FileService.readGames(AppDirectory.temporary);
      final List<GameRecord> fromDocuments = await FileService.readGames(AppDirectory.applicationDocuments);

      expect(fromTemporary.length, games.length);
      expect(fromDocuments.length, games.length);
      expect(fromTemporary.first.title, isNot(fromDocuments.first.title));

      final List<String> files = await FileService.listAllFiles();

      expect(files.contains(temporaryPath), isTrue);
      expect(files.contains(documentsPath), isTrue);
    });
  });

  group('Изоляты', () {
    testWidgets('выполняет запись и чтение файла в отдельных изолятах',
        (WidgetTester tester) async {
      final DirectoryInfo temporary = await DirectoryService.resolve(AppDirectory.temporary);
      final String path = '${temporary.primaryPath}/isolate_integration_test.json';

      final IsolateRun writeCompute = await IsolateService.write(path, 'payload-compute', spawn: false);
      final IsolateRun readCompute = await IsolateService.read(path, spawn: false);

      expect(writeCompute.success, isTrue);
      expect(readCompute.success, isTrue);
      expect(readCompute.details, contains('15'));

      final IsolateRun writeSpawn = await IsolateService.write(path, 'payload-spawn', spawn: true);
      final IsolateRun readSpawn = await IsolateService.read(path, spawn: true);

      expect(writeSpawn.success, isTrue);
      expect(readSpawn.success, isTrue);
      expect(writeSpawn.mode, 'Isolate.spawn()');
      expect(writeCompute.mode, 'compute()');
    });
  });
}
