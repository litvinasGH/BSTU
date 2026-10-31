import 'package:flutter/material.dart';

import '../database/game_database.dart';
import '../database/gamerecord.dart';
import '../services/directory_service.dart';
import '../services/file_service.dart';
import '../widgets/simple_button.dart';

class Filescreen extends StatefulWidget {
  const Filescreen({super.key});

  @override
  State<Filescreen> createState() => _FilescreenState();
}

class _FilescreenState extends State<Filescreen> {
  List<DirectoryInfo> _directories = <DirectoryInfo>[];
  List<String> _files = <String>[];
  String _status = '';

  @override
  void initState() {
    super.initState();
    _refresh();
  }

  Future<void> _refresh() async {
    final List<DirectoryInfo> directories = await DirectoryService.resolveAll();
    final List<String> files = await FileService.listAllFiles();

    if (!mounted) {
      return;
    }

    setState(() {
      _directories = directories;
      _files = files;
    });
  }

  Future<void> _write(DirectoryInfo info) async {
    final List<GameRecord> games = await GameDatabase.instance.getGames();

    try {
      final String path = await FileService.writeGames(info.directory, games);

      setState(() => _status = 'Записано: $path');
    } catch (error) {
      setState(() => _status = 'Ошибка записи: $error');
    }

    await _refresh();
  }

  Future<void> _read(DirectoryInfo info) async {
    try {
      final List<GameRecord> games = await FileService.readGames(info.directory);

      setState(() => _status = 'Прочитано записей: ${games.length}');

      if (!mounted) {
        return;
      }

      await showDialog<void>(
        context: context,
        builder: (BuildContext context) => AlertDialog(
          title: Text(info.title),
          content: SizedBox(
            width: double.infinity,
            child: Column(
              mainAxisSize: MainAxisSize.min,
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                for (final GameRecord game in games)
                  Text('${game.title} — ${game.developer}, ${game.year}, ${game.type}'),
              ],
            ),
          ),
          actions: [
            SimpleButton(text: 'Закрыть', onPressed: () => Navigator.pop(context)),
          ],
        ),
      );
    } catch (error) {
      setState(() => _status = 'Ошибка чтения: $error');
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: const Text('Файловая система')),
      body: ListView(
        padding: const EdgeInsets.all(8),
        children: [
          if (_status.isNotEmpty) Text(_status, style: const TextStyle(fontSize: 13)),
          for (final DirectoryInfo info in _directories) _directoryBox(info),
          const SizedBox(height: 8),
          const Text('Расположение существующих файлов', style: TextStyle(fontSize: 15)),
          for (final String path in _files) Text(path, style: const TextStyle(fontSize: 12)),
        ],
      ),
    );
  }

  Widget _directoryBox(DirectoryInfo info) {
    return Container(
      margin: const EdgeInsets.only(bottom: 8),
      padding: const EdgeInsets.all(8),
      decoration: BoxDecoration(
        color: Colors.white,
        border: Border.all(color: Colors.black),
      ),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          Text(info.title, style: const TextStyle(fontSize: 15)),
          Text(info.apiName, style: const TextStyle(fontSize: 12)),
          Text(info.statusText, style: const TextStyle(fontSize: 12)),
          const SizedBox(height: 4),
          Row(
            children: [
              SimpleButton(text: 'Записать', onPressed: info.isResolved ? () => _write(info) : null),
              const SizedBox(width: 8),
              SimpleButton(text: 'Прочитать', onPressed: info.isResolved ? () => _read(info) : null),
            ],
          ),
        ],
      ),
    );
  }
}
