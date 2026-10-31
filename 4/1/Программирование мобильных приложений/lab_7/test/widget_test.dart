import 'package:flutter/material.dart';
import 'package:flutter_test/flutter_test.dart';
import 'package:lab_2/database/gamerecord.dart';
import 'package:lab_2/database/sort_field.dart';
import 'package:lab_2/screens/parts/itemlist.dart';

void main() {
  group('GameRecord', () {
    const GameRecord record = GameRecord(
      id: 1,
      title: 'Counter-Strike 2',
      developer: 'Valve',
      year: 2023,
      type: 'Shooter',
    );

    test('сохраняет все четыре свойства сущности', () {
      expect(record.title, 'Counter-Strike 2');
      expect(record.developer, 'Valve');
      expect(record.year, 2023);
      expect(record.type, 'Shooter');
    });

    test('конвертируется в карту и обратно без потери данных', () {
      final GameRecord restored = GameRecord.fromMap(record.toMap());

      expect(restored, record);
    });

    test('сериализуется в JSON и обратно без потери данных', () {
      final GameRecord restored = GameRecord.fromJson(record.toJson());

      expect(restored, record);
    });
  });

  group('SortField', () {
    test('формирует выражение сортировки по возрастанию', () {
      const SortField field = SortField(column: 'title', ascending: true);

      expect(field.orderExpression, 'title ASC');
      expect(SortField.labelOf('title'), 'Название');
    });

    test('формирует выражение сортировки по убыванию', () {
      const SortField field = SortField(column: 'year', ascending: false);

      expect(field.orderExpression, 'year DESC');
      expect(SortField.labelOf('year'), 'Год выпуска');
    });

    test('разрешает только колонки таблицы игр', () {
      expect(SortField.isAllowed('title'), isTrue);
      expect(SortField.isAllowed('year'), isTrue);
      expect(SortField.isAllowed('id; DROP TABLE games'), isFalse);
    });
  });

  testWidgets('элемент списка показывает данные записи', (WidgetTester tester) async {
    const GameRecord record = GameRecord(
      id: 7,
      title: 'DOOM Eternal',
      developer: 'id Software',
      year: 2020,
      type: 'Shooter',
    );

    await tester.pumpWidget(
      const MaterialApp(
        home: Scaffold(
          body: ItemList(record: record),
        ),
      ),
    );

    expect(find.text('DOOM Eternal'), findsOneWidget);
    expect(find.text('id Software • 2020'), findsOneWidget);
    expect(find.text('Shooter'), findsOneWidget);
  });
}
