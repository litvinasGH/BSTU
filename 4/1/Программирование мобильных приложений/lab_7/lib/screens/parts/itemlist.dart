import 'package:flutter/material.dart';

import '../../database/gamerecord.dart';

class ItemList extends StatelessWidget {
  const ItemList({super.key, required this.record, this.onTap});

  final GameRecord record;
  final VoidCallback? onTap;

  @override
  Widget build(BuildContext context) {
    return GestureDetector(
      onTap: onTap,
      child: Container(
        height: 56,
        margin: const EdgeInsets.symmetric(horizontal: 8, vertical: 2),
        padding: const EdgeInsets.symmetric(horizontal: 8),
        decoration: BoxDecoration(
          color: Colors.white,
          border: Border.all(color: Colors.black),
        ),
        child: Row(
          children: [
            Expanded(
              child: Text(record.title, style: const TextStyle(fontSize: 15)),
            ),
            Text(
              '${record.developer} • ${record.year}',
              style: const TextStyle(fontSize: 13),
            ),
            const SizedBox(width: 8),
            Text(record.type, style: const TextStyle(fontSize: 13)),
          ],
        ),
      ),
    );
  }
}
