import 'package:flutter/material.dart';

import '../data/hive_social_store.dart';

class HiveDiagnosticsScreen extends StatefulWidget {
  const HiveDiagnosticsScreen({super.key});

  @override
  State<HiveDiagnosticsScreen> createState() => _HiveDiagnosticsScreenState();
}

class _HiveDiagnosticsScreenState extends State<HiveDiagnosticsScreen> {
  bool _working = false;
  String _result = 'Выбери проверку. Основные пользовательские данные не изменяются.';

  Future<void> _compact() async {
    setState(() {
      _working = true;
      _result = 'Выполняется уплотнение Hive Box...';
    });
    try {
      final before = HiveSocialStore.posts.length;
      await HiveSocialStore.compactAllBoxes();
      if (!mounted) return;
      setState(() {
        _result = 'Уплотнение выполнено. Количество публикаций сохранено: $before.';
      });
    } catch (error) {
      if (!mounted) return;
      setState(() => _result = 'Ошибка уплотнения: $error');
    } finally {
      if (mounted) setState(() => _working = false);
    }
  }

  Future<void> _wrongKey() async {
    setState(() {
      _working = true;
      _result = 'Создаётся временный Box и выполняется попытка открыть его неправильным ключом...';
    });
    try {
      final result = await HiveSocialStore.demonstrateWrongKey();
      if (!mounted) return;
      setState(() => _result = result);
    } catch (error) {
      if (!mounted) return;
      setState(() => _result = 'Проверка завершилась ошибкой: $error');
    } finally {
      if (mounted) setState(() => _working = false);
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: const Text('Проверки Hive')),
      body: ListView(
        padding: const EdgeInsets.all(18),
        children: [
          const Text('Диагностика хранилища', style: TextStyle(fontSize: 21, fontWeight: FontWeight.bold)),
          const SizedBox(height: 10),
          const Text(
            'Основные Box с пользователями, публикациями, историями и избранным зашифрованы. '
            'Проверка неправильного ключа использует отдельный временный Box и не трогает основную базу.',
          ),
          const SizedBox(height: 20),
          FilledButton.icon(
            onPressed: _working ? null : _wrongKey,
            icon: const Icon(Icons.key_off),
            label: const Text('Проверить неправильный ключ'),
          ),
          const SizedBox(height: 10),
          OutlinedButton.icon(
            onPressed: _working ? null : _compact,
            icon: const Icon(Icons.compress),
            label: const Text('Выполнить compact() для всех Box'),
          ),
          const SizedBox(height: 20),
          if (_working) const LinearProgressIndicator(),
          const SizedBox(height: 12),
          const Text('Результат', style: TextStyle(fontWeight: FontWeight.bold)),
          const SizedBox(height: 8),
          SelectableText(_result),
        ],
      ),
    );
  }
}
