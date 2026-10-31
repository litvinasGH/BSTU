import 'dart:convert';

import 'package:flutter/material.dart';

import '../database/game_database.dart';
import '../database/gamerecord.dart';
import '../services/directory_service.dart';
import '../services/file_service.dart';
import '../services/isolate_service.dart';
import '../widgets/simple_button.dart';

class Isolatescreen extends StatefulWidget {
  const Isolatescreen({super.key});

  @override
  State<Isolatescreen> createState() => _IsolatescreenState();
}

class _IsolatescreenState extends State<Isolatescreen> {
  List<IsolateRun> _runs = <IsolateRun>[];
  String _filePath = '';

  @override
  void initState() {
    super.initState();
    _prepare();
  }

  Future<void> _prepare() async {
  const String directory = '/storage/emulated/0/Download';

  final String fileName =
      FileService.fileNameFor(AppDirectory.temporary);

  if (!mounted) {
    return;
  }

  setState(() {
    _filePath = '$directory/$fileName';
  });
}

  Future<void> _write({required bool spawn}) async {
    if (_filePath.isEmpty) {
      return;
    }

    final List<GameRecord> games = await GameDatabase.instance.getGames();

    final String content = const JsonEncoder.withIndent('  ').convert(
      <Map<String, Object?>>[
        for (final GameRecord game in games) game.toJson(),
      ],
    );

    final IsolateRun run = await IsolateService.write(_filePath, content, spawn: spawn);

    setState(() => _runs = <IsolateRun>[run, ..._runs]);
  }

  Future<void> _read({required bool spawn}) async {
    if (_filePath.isEmpty) {
      return;
    }

    final IsolateRun run = await IsolateService.read(_filePath, spawn: spawn);

    setState(() => _runs = <IsolateRun>[run, ..._runs]);
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: const Text('Изоляты')),
      body: ListView(
        padding: const EdgeInsets.all(8),
        children: [
          Text('Файл: $_filePath', style: const TextStyle(fontSize: 12)),
          const SizedBox(height: 8),
          Row(
            children: [
              SimpleButton(text: 'Записать compute()', onPressed: () => _write(spawn: false)),
              const SizedBox(width: 8),
              SimpleButton(text: 'Записать Isolate.spawn()', onPressed: () => _write(spawn: true)),
            ],
          ),
          const SizedBox(height: 8),
          Row(
            children: [
              SimpleButton(text: 'Прочитать compute()', onPressed: () => _read(spawn: false)),
              const SizedBox(width: 8),
              SimpleButton(text: 'Прочитать Isolate.spawn()', onPressed: () => _read(spawn: true)),
            ],
          ),
          const SizedBox(height: 8),
          for (final IsolateRun run in _runs)
            Container(
              margin: const EdgeInsets.only(bottom: 4),
              padding: const EdgeInsets.all(8),
              decoration: BoxDecoration(
                color: Colors.white,
                border: Border.all(color: Colors.black),
              ),
              child: Text(
                '${run.mode}, ${run.operation}: ${run.details} (${run.durationText})',
                style: const TextStyle(fontSize: 13),
              ),
            ),
        ],
      ),
    );
  }
}
