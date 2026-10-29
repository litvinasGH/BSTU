import 'package:flutter/material.dart';
import 'package:flutter/services.dart';

import '../database/game_database.dart';
import '../database/gamerecord.dart';
import '../widgets/simple_button.dart';

class Editscreen extends StatefulWidget {
  const Editscreen({super.key, this.record});

  final GameRecord? record;

  @override
  State<Editscreen> createState() => _EditscreenState();
}

class _EditscreenState extends State<Editscreen> {
  final GlobalKey<FormState> _formKey = GlobalKey<FormState>();
  final TextEditingController _titleController = TextEditingController();
  final TextEditingController _developerController = TextEditingController();
  final TextEditingController _yearController = TextEditingController();

  late String _type = widget.record?.type ?? 'Shooter';

  static const List<String> _types = <String>['Shooter', 'Strategy', 'Rpg', 'OnlineGame'];

  bool get _isEditing => widget.record != null;

  @override
  void initState() {
    super.initState();

    final record = widget.record;

    if (record != null) {
      _titleController.text = record.title;
      _developerController.text = record.developer;
      _yearController.text = '${record.year}';
      _type = record.type;
    }
  }

  @override
  void dispose() {
    _titleController.dispose();
    _developerController.dispose();
    _yearController.dispose();
    super.dispose();
  }

  Future<void> _save() async {
    if (!(_formKey.currentState?.validate() ?? false)) {
      return;
    }

    final record = GameRecord(
      id: widget.record?.id,
      title: _titleController.text.trim(),
      developer: _developerController.text.trim(),
      year: int.parse(_yearController.text.trim()),
      type: _type,
    );

    if (_isEditing) {
      await GameDatabase.instance.updateGame(record);
    } else {
      await GameDatabase.instance.createGame(record);
    }

    if (mounted) {
      Navigator.pop(context, true);
    }
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: Text(_isEditing ? 'Редактирование' : 'Добавление')),
      body: Form(
        key: _formKey,
        child: ListView(
          padding: const EdgeInsets.all(8),
          children: [
            TextFormField(
              controller: _titleController,
              decoration: const InputDecoration(labelText: 'Название'),
              validator: (String? value) =>
                  (value == null || value.trim().isEmpty) ? 'Введите название' : null,
            ),
            TextFormField(
              controller: _developerController,
              decoration: const InputDecoration(labelText: 'Разработчик'),
              validator: (String? value) =>
                  (value == null || value.trim().isEmpty) ? 'Введите разработчика' : null,
            ),
            TextFormField(
              controller: _yearController,
              keyboardType: TextInputType.number,
              inputFormatters: <TextInputFormatter>[FilteringTextInputFormatter.digitsOnly],
              decoration: const InputDecoration(labelText: 'Год выпуска'),
              validator: (String? value) {
                final year = int.tryParse(value?.trim() ?? '');

                if (year == null) {
                  return 'Введите корректный год';
                }

                return null;
              },
            ),
            DropdownButtonFormField<String>(
              initialValue: _type,
              decoration: const InputDecoration(labelText: 'Жанр'),
              items: <DropdownMenuItem<String>>[
                for (final String type in _types) DropdownMenuItem<String>(value: type, child: Text(type)),
              ],
              onChanged: (String? value) {
                if (value != null) {
                  setState(() => _type = value);
                }
              },
            ),
            const SizedBox(height: 8),
            SimpleButton(text: _isEditing ? 'Сохранить' : 'Добавить', onPressed: _save),
          ],
        ),
      ),
    );
  }
}
