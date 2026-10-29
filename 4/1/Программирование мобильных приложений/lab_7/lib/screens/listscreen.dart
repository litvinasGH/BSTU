import 'dart:async';

import 'package:flutter/material.dart';

import '../database/game_database.dart';
import '../database/gamerecord.dart';
import '../database/sort_field.dart';
import '../widgets/simple_button.dart';
import 'detailscreen.dart';
import 'editscreen.dart';
import 'filescreen.dart';
import 'isolatescreen.dart';
import 'parts/itemlist.dart';

class Listscreen extends StatefulWidget {
  const Listscreen({super.key});

  @override
  State<Listscreen> createState() => _ListscreenState();
}

class _ListscreenState extends State<Listscreen> {
  final GameDatabase _database = GameDatabase.instance;
  final TextEditingController _searchController = TextEditingController();

  List<GameRecord> _games = <GameRecord>[];
  List<SortField> _sortFields = <SortField>[const SortField(column: 'title', ascending: true)];
  Timer? _searchDebounce;
  bool _loading = true;

  @override
  void initState() {
    super.initState();
    _load();
  }

  @override
  void dispose() {
    _searchDebounce?.cancel();
    _searchController.dispose();
    super.dispose();
  }

  Future<void> _load() async {
    await _database.seedIfEmpty();

    final games = await _database.getGames(
      search: _searchController.text,
      sortFields: _sortFields,
    );

    if (!mounted) {
      return;
    }

    setState(() {
      _games = games;
      _loading = false;
    });
  }

  void _onSearchChanged(String value) {
    _searchDebounce?.cancel();
    _searchDebounce = Timer(const Duration(milliseconds: 350), _load);
  }

  Future<void> _openSort() async {
    final result = await showModalBottomSheet<List<SortField>>(
      context: context,
      builder: (BuildContext context) => _SortPanel(initial: _sortFields),
    );

    if (result == null) {
      return;
    }

    setState(() {
      _sortFields = result;
    });

    await _load();
  }

  Future<void> _openDetails(GameRecord record) async {
    final changed = await Navigator.push<bool>(
      context,
      MaterialPageRoute<bool>(builder: (BuildContext context) => Detailscreen(record: record)),
    );

    if (changed == true) {
      await _load();
    }
  }

  Future<void> _openEdit([GameRecord? record]) async {
    final saved = await Navigator.push<bool>(
      context,
      MaterialPageRoute<bool>(builder: (BuildContext context) => Editscreen(record: record)),
    );

    if (saved == true) {
      await _load();
    }
  }

  void _openFiles() {
    Navigator.push(context, MaterialPageRoute<void>(builder: (BuildContext context) => const Filescreen()));
  }

  void _openIsolates() {
    Navigator.push(context, MaterialPageRoute<void>(builder: (BuildContext context) => const Isolatescreen()));
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: const Text('Список игр')),
      body: Column(
        children: [
          Container(
            height: 44,
            margin: const EdgeInsets.all(8),
            padding: const EdgeInsets.symmetric(horizontal: 8),
            decoration: BoxDecoration(
              color: Colors.white,
              border: Border.all(color: Colors.black),
            ),
            child: TextField(
              controller: _searchController,
              onChanged: _onSearchChanged,
              decoration: const InputDecoration(
                labelText: 'Поиск',
                border: InputBorder.none,
              ),
            ),
          ),
          Padding(
            padding: const EdgeInsets.symmetric(horizontal: 8),
            child: Row(
              children: [
                SimpleButton(text: 'Сортировка', onPressed: _openSort),
                const SizedBox(width: 8),
                SimpleButton(text: 'Добавить', onPressed: () => _openEdit()),
                const SizedBox(width: 8),
                SimpleButton(text: 'Файлы', onPressed: _openFiles),
                const SizedBox(width: 8),
                SimpleButton(text: 'Изоляты', onPressed: _openIsolates),
              ],
            ),
          ),
          Expanded(child: _body()),
        ],
      ),
    );
  }

  Widget _body() {
    if (_loading) {
      return const Center(child: CircularProgressIndicator());
    }

    if (_games.isEmpty) {
      return const Center(child: Text('Записи не найдены'));
    }

    return ListView.builder(
      itemExtent: 60,
      itemCount: _games.length,
      itemBuilder: (BuildContext context, int index) {
        final record = _games[index];

        return ItemList(record: record, onTap: () => _openDetails(record));
      },
    );
  }
}

class _SortPanel extends StatefulWidget {
  const _SortPanel({required this.initial});

  final List<SortField> initial;

  @override
  State<_SortPanel> createState() => _SortPanelState();
}

class _SortPanelState extends State<_SortPanel> {
  late List<SortField> _fields = List<SortField>.from(widget.initial);

  bool _enabled(String column) => _fields.any((SortField field) => field.column == column);

  bool _ascending(String column) {
    final field = _fields.where((SortField field) => field.column == column).toList();

    return field.isEmpty ? true : field.first.ascending;
  }

  void _toggle(String column) {
    setState(() {
      if (_enabled(column)) {
        _fields = _fields.where((SortField field) => field.column != column).toList();
      } else {
        _fields = <SortField>[..._fields, SortField(column: column, ascending: true)];
        _fields.sort((SortField a, SortField b) =>
            SortField.columns.indexOf(a.column).compareTo(SortField.columns.indexOf(b.column)));
      }
    });
  }

  void _toggleDirection(String column) {
    setState(() {
      _fields = <SortField>[
        for (final SortField field in _fields)
          field.column == column ? SortField(column: column, ascending: !field.ascending) : field,
      ];
    });
  }

  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: const EdgeInsets.all(8),
      child: Column(
        mainAxisSize: MainAxisSize.min,
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          const Text('Сортировка по нескольким полям'),
          const SizedBox(height: 8),
          for (final String column in SortField.columns)
            Row(
              children: [
                Checkbox(
                  value: _enabled(column),
                  onChanged: (bool? _) => _toggle(column),
                ),
                Expanded(child: Text(SortField.labelOf(column))),
                if (_enabled(column))
                  SimpleButton(
                    text: _ascending(column) ? '↑' : '↓',
                    onPressed: () => _toggleDirection(column),
                  ),
              ],
            ),
          const SizedBox(height: 8),
          Row(
            children: [
              SimpleButton(text: 'Сбросить', onPressed: () => setState(() => _fields = <SortField>[])),
              const SizedBox(width: 8),
              SimpleButton(text: 'Применить', onPressed: () => Navigator.pop(context, _fields)),
            ],
          ),
        ],
      ),
    );
  }
}
