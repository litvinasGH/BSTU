import 'package:flutter/material.dart';

import '../database/game_database.dart';
import '../database/gamerecord.dart';
import '../widgets/simple_button.dart';
import 'editscreen.dart';

class Detailscreen extends StatefulWidget {
  const Detailscreen({super.key, required this.record});

  final GameRecord record;

  @override
  State<Detailscreen> createState() => _DetailscreenState();
}

class _DetailscreenState extends State<Detailscreen> {
  late GameRecord _record = widget.record;

  Future<void> _edit() async {
    final changed = await Navigator.push<bool>(
      context,
      MaterialPageRoute<bool>(builder: (BuildContext context) => Editscreen(record: _record)),
    );

    if (changed == true && mounted) {
      final updated = await GameDatabase.instance.getGame(_record.id!);
      setState(() => _record = updated ?? _record);
    }
  }

  Future<void> _delete() async {
    final confirmed = await showDialog<bool>(
      context: context,
      builder: (BuildContext context) => AlertDialog(
        title: const Text('Удалить запись?'),
        content: Text(_record.title),
        actions: [
          SimpleButton(text: 'Отмена', onPressed: () => Navigator.pop(context, false)),
          const SizedBox(width: 8),
          SimpleButton(text: 'Удалить', onPressed: () => Navigator.pop(context, true)),
        ],
      ),
    );

    if (confirmed != true || !mounted) {
      return;
    }

    await GameDatabase.instance.deleteGame(_record.id!);

    if (mounted) {
      Navigator.pop(context, true);
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: const Text('О игре')),
      body: Padding(
        padding: const EdgeInsets.all(8),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            Container(
              width: double.infinity,
              padding: const EdgeInsets.all(8),
              decoration: BoxDecoration(
                color: Colors.white,
                border: Border.all(color: Colors.black),
              ),
              child: Column(
                crossAxisAlignment: CrossAxisAlignment.start,
                children: [
                  Text('Название: ${_record.title}'),
                  Text('Разработчик: ${_record.developer}'),
                  Text('Год выпуска: ${_record.year}'),
                  Text('Жанр: ${_record.type}'),
                ],
              ),
            ),
            const SizedBox(height: 8),
            Row(
              children: [
                SimpleButton(text: 'Изменить', onPressed: _edit),
                const SizedBox(width: 8),
                SimpleButton(text: 'Удалить', onPressed: _delete),
              ],
            ),
          ],
        ),
      ),
    );
  }
}
