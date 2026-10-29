import 'dart:convert';
import 'dart:io';

import 'package:path/path.dart' as p;

import '../database/gamerecord.dart';
import 'directory_service.dart';

class FileService {
  FileService._();

  static String fileNameFor(AppDirectory directory) {
    return 'games_${directory.name}.json';
  }

  static Future<String> writeGames(AppDirectory directory, List<GameRecord> games) async {
    final DirectoryInfo info = await DirectoryService.resolve(directory);

    if (!info.isResolved) {
      throw StateError(info.error ?? 'Каталог недоступен');
    }

    final String title = info.title;

    final String content = const JsonEncoder.withIndent('  ').convert(<Map<String, Object?>>[
      for (final GameRecord game in games)
        <String, Object?>{
          ...game.toJson(),
          'title': '${game.title} [$title]',
        },
    ]);

    final File file = File(p.join(info.primaryPath, fileNameFor(directory)));

    await file.parent.create(recursive: true);
    await file.writeAsString(content);

    return file.path;
  }

  static Future<List<GameRecord>> readGames(AppDirectory directory) async {
    final DirectoryInfo info = await DirectoryService.resolve(directory);

    if (!info.isResolved) {
      throw StateError(info.error ?? 'Каталог недоступен');
    }

    final File file = File(p.join(info.primaryPath, fileNameFor(directory)));

    if (!file.existsSync()) {
      return <GameRecord>[];
    }

    final dynamic decoded = jsonDecode(await file.readAsString());

    if (decoded is! List) {
      return <GameRecord>[];
    }

    return decoded
        .whereType<Map<String, dynamic>>()
        .map((Map<String, dynamic> item) => GameRecord.fromJson(item))
        .toList();
  }

  static Future<List<String>> listFiles(AppDirectory directory) async {
    final DirectoryInfo info = await DirectoryService.resolve(directory);

    if (!info.isResolved) {
      return <String>[];
    }

    final List<String> paths = <String>[];

    for (final String path in info.paths) {
      final Directory dir = Directory(path);

      if (!dir.existsSync()) {
        continue;
      }

      for (final FileSystemEntity entity in dir.listSync()) {
        if (entity is File) {
          paths.add(entity.path);
        }
      }
    }

    paths.sort();

    return paths;
  }

  static Future<List<String>> listAllFiles() async {
    final List<String> all = <String>[];

    for (final AppDirectory directory in AppDirectory.values) {
      all.addAll(await listFiles(directory));
    }

    return all;
  }
}
