import 'computer_game.dart';

class Strategy extends ComputerGame {
  Strategy(super.title, super.developer, super.year);

  Strategy.fromDefault()
      : super('Civilization VI', 'Firaxis Games', 2016);

  void buildBase() {
    print('$title: игрок строит базу.');
  }

  @override
  void isCool() {
    // TODO: implement isCool
  }


}
