import 'package:sqflite/sqflite.dart';
import 'package:path/path.dart';

import 'gamerecord.dart';

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

    final values = game.toMap()..remove('id');

    return db.insert('games', values);
  }

  Future<List<GameRecord>> getGames({
    String search = '',
    String sortBy = 'title',
    bool ascending = true,
  }) async {
    final db = await instance.database;

    const allowedColumns = {'title', 'developer', 'year', 'type'};

    if (!allowedColumns.contains(sortBy)) {
      sortBy = 'title';
    }

    final orderBy = '$sortBy ${ascending ? 'ASC' : 'DESC'}';
    final query = search.trim();

    final rows = query.isEmpty
        ? await db.query('games', orderBy: orderBy)
        : await db.query(
            'games',
            where: '''
              title LIKE ?
              OR developer LIKE ?
              OR type LIKE ?
              OR CAST(year AS TEXT) LIKE ?
            ''',
            whereArgs: List.filled(4, '%$query%'),
            orderBy: orderBy,
          );

    return rows.map(GameRecord.fromMap).toList();
  }

  Future<int> updateGame(GameRecord game) async {
    if (game.id == null) {
      throw ArgumentError('Для изменения игры необходим id');
    }

    final db = await instance.database;
    final values = game.toMap()..remove('id');

    return db.update('games', values, where: 'id = ?', whereArgs: [game.id]);
  }

  Future<int> deleteGame(int id) async {
    final db = await instance.database;

    return db.delete('games', where: 'id = ?', whereArgs: [id]);
  }

  Future<void> close() async {
    final db = _database;

    if (db != null) {
      await db.close();
      _database = null;
    }
  }
}
