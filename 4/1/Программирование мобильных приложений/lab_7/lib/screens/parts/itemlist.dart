import 'package:flutter/material.dart';
import 'package:lab_2/computer_game.dart';

class ItemList extends StatelessWidget {
  const new({super.key, required this.game});

  final ComputerGame game;

  @override
  Widget build(BuildContext context) {
    return Container(child: Text(game.title));
  }
}
