import 'package:sqflite/sqflite.dart';
import 'package:path/path.dart';

import '../computer_game.dart';
import '../rpg.dart';
import '../shooter.dart';
import '../strategy.dart';
import 'gamerecord.dart';
import 'sort_field.dart';

class GameDatabase {
  GameDatabase._init();

  static final GameDatabase instance = GameDatabase._init();

  Database? _database;

  Future<Database> get database async {
    if (_database != null) {
      return _database!;
    }

    _database = await _initDB('games.db');
    return _database!;
  }

  Future<Database> _initDB(String fileName) async {
    final dbPath = await getDatabasesPath();
    final path = join(dbPath, fileName);

    return openDatabase(
      path,
      version: 1,
      onCreate: (db, version) async {
        await db.execute('''
          CREATE TABLE games (
            id INTEGER PRIMARY KEY AUTOINCREMENT,
            title TEXT NOT NULL,
            developer TEXT NOT NULL,
            year INTEGER NOT NULL,
            type TEXT NOT NULL
          )
        ''');
      },
    );
  }

  Future<int> createGame(GameRecord game) async {
    final db = await instance.database;

    return db.insert('games', game.toMap()..remove('id'));
  }

  Future<List<GameRecord>> getGames({
    String search = '',
    List<SortField> sortFields = const <SortField>[],
  }) async {
    final db = await instance.database;

    final query = search.trim();

    final rows = query.isEmpty
        ? await db.query('games', orderBy: _orderBy(sortFields))
        : await db.query(
            'games',
            where: '''
              title LIKE ?
              OR developer LIKE ?
              OR type LIKE ?
              OR CAST(year AS TEXT) LIKE ?
            ''',
            whereArgs: List.filled(4, '%$query%'),
            orderBy: _orderBy(sortFields),
          );

    return rows.map(GameRecord.fromMap).toList();
  }

  Future<GameRecord?> getGame(int id) async {
    final db = await instance.database;

    final rows = await db.query('games', where: 'id = ?', whereArgs: [id], limit: 1);

    return rows.isEmpty ? null : GameRecord.fromMap(rows.first);
  }

  Future<int> updateGame(GameRecord game) async {
    if (game.id == null) {
      throw ArgumentError('Для изменения игры необходим id');
    }

    final db = await instance.database;

    return db.update('games', game.toMap()..remove('id'), where: 'id = ?', whereArgs: [game.id]);
  }

  Future<int> deleteGame(int id) async {
    final db = await instance.database;

    return db.delete('games', where: 'id = ?', whereArgs: [id]);
  }

  Future<void> seedIfEmpty() async {
    final db = await instance.database;

    final total = Sqflite.firstIntValue(await db.rawQuery('SELECT COUNT(*) FROM games')) ?? 0;

    if (total > 0) {
      return;
    }

    final List<ComputerGame> games = <ComputerGame>[
      Shooter('Counter-Strike 2', 'Valve', 2023),
      Strategy('Civilization VI', 'Firaxis Games', 2016),
      Shooter('DOOM Eternal', 'id Software', 2020),
      Rpg('Skyrim', 'Беседка', 2012),
    ];

    for (final ComputerGame game in games) {
      await createGame(GameRecord.fromComputerGame(game));
    }
  }

  Future<void> close() async {
    final db = _database;

    if (db != null) {
      await db.close();
      _database = null;
    }
  }

  Future<void> dropDatabase() async {
    await close();

    await deleteDatabase(join(await getDatabasesPath(), 'games.db'));
  }

  String _orderBy(List<SortField> sortFields) {
    final allowed = sortFields.where((SortField field) => SortField.isAllowed(field.column)).toList();

    if (allowed.isEmpty) {
      return 'title ASC';
    }

    return allowed.map((SortField field) => field.orderExpression).join(', ');
  }
}
