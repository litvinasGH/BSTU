import 'dart:io';
import 'dart:isolate';

import 'package:flutter/foundation.dart';

class IsolateRun {
  const IsolateRun({
    required this.mode,
    required this.operation,
    required this.details,
    required this.durationMs,
    required this.success,
  });

  final String mode;
  final String operation;
  final String details;
  final int durationMs;
  final bool success;

  String get durationText => '$durationMs мс';
}

class IsolateService {
  IsolateService._();

  static Future<IsolateRun> write(String path, String content, {required bool spawn}) async {
    return _run(mode: spawn ? 'Isolate.spawn()' : 'compute()', operation: 'запись', path: path, content: content, spawn: spawn);
  }

  static Future<IsolateRun> read(String path, {required bool spawn}) async {
    return _run(mode: spawn ? 'Isolate.spawn()' : 'compute()', operation: 'чтение', path: path, content: '', spawn: spawn);
  }

  static Future<IsolateRun> _run({
    required String mode,
    required String operation,
    required String path,
    required String content,
    required bool spawn,
  }) async {
    final Stopwatch stopwatch = Stopwatch()..start();

    try {
      final Map<String, Object?> args = <String, Object?>{
        'operation': operation,
        'path': path,
        'content': content,
      };

      final Map<String, Object?> result = spawn ? await _spawn(args) : await compute(_task, args);

      stopwatch.stop();

      final bool success = result['ok'] == true;

      return IsolateRun(
        mode: mode,
        operation: operation,
        details: success
            ? 'размер ${result['size']} Б, контрольная сумма ${result['checksum']}'
            : '${result['error']}',
        durationMs: stopwatch.elapsedMilliseconds,
        success: success,
      );
    } catch (error) {
      stopwatch.stop();

      return IsolateRun(
        mode: mode,
        operation: operation,
        details: error.toString(),
        durationMs: stopwatch.elapsedMilliseconds,
        success: false,
      );
    }
  }

  static Future<Map<String, Object?>> _spawn(Map<String, Object?> args) async {
    final ReceivePort receivePort = ReceivePort();

    await Isolate.spawn<List<Object>>(
      _spawnEntryPoint,
      <Object>[receivePort.sendPort, args],
    );

    final Map<String, Object?> result = await receivePort.first as Map<String, Object?>;
    receivePort.close();

    return result;
  }
}

Future<Map<String, Object?>> _task(Map<String, Object?> args) async {
  final String operation = args['operation'] as String;
  final String path = args['path'] as String;
  final String content = (args['content'] as String?) ?? '';

  try {
    if (operation == 'запись') {
      final File file = File(path);

      await file.parent.create(recursive: true);
      await file.writeAsString(content);

      return <String, Object?>{
        'ok': true,
        'size': content.length,
        'checksum': _checksum(content),
      };
    }

    final File file = File(path);

    if (!file.existsSync()) {
      return <String, Object?>{
        'ok': false,
        'error': 'файл не найден: $path',
      };
    }

    final String text = await file.readAsString();

    return <String, Object?>{
      'ok': true,
      'size': text.length,
      'checksum': _checksum(text),
    };
  } catch (error) {
    return <String, Object?>{
      'ok': false,
      'error': error.toString(),
    };
  }
}

void _spawnEntryPoint(List<Object> arguments) {
  final SendPort sendPort = arguments[0] as SendPort;
  final Map<String, Object?> args = arguments[1] as Map<String, Object?>;

  _task(args).then(sendPort.send);
}

int _checksum(String content) {
  int sum = 0;

  for (final int unit in content.codeUnits) {
    sum = (sum * 31 + unit) & 0x7FFFFFFF;
  }

  return sum;
}
