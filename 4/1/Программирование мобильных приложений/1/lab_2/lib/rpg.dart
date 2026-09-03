import 'computer_game.dart';

class Rpg extends ComputerGame {
  Rpg(super.title, super.developer, super.year);

  void lernMag() {
    print('$title: игрок изучает магию.');
  }

  @override
  void isCool() {
    print("COOOL!");
  }
  
}