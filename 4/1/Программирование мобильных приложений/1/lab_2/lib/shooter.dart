import 'computer_game.dart';
import 'playable.dart';

class Shooter extends ComputerGame implements Playable {
  Shooter(super.title, super.developer, super.year);

  void shoot() {
    print('$title: игрок стреляет.');
  }

  @override
  void startGame() {
    print('$title: игра началась.');
  }

  @override
  void endGame() {
    print('$title: игра завершена.');
  }

  @override
  void isCool() {
    print("Cool");
  }
}