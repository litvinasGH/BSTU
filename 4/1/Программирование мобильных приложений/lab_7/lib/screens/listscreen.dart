import 'package:flutter/material.dart';
import 'package:lab_2/computer_game.dart';
import 'package:lab_2/rpg.dart';
import 'package:lab_2/screens/parts/itemlist.dart';
import 'package:lab_2/shooter.dart';
import 'package:lab_2/strategy.dart';

class Listscreen extends StatefulWidget {
  const new({super.key});

  @override
  State<Listscreen> createState() => _ListscreenState();
}

class _ListscreenState extends State<Listscreen> {
  List<ComputerGame> computerGamesList = [
    Shooter('Counter-Strike 2', 'Valve', 2023),
    Strategy('Civilization VI', 'Firaxis Games', 2016),
    Shooter('DOOM Eternal', 'id Software', 2020),
    Rpg("Skyrim", "Беседка", 2012),
  ];

  @override
  Widget build(BuildContext context) {
    return ListView(
      children: [
        for (ComputerGame game in computerGamesList) ItemList(game: game),
      ],
    );
  }
}
