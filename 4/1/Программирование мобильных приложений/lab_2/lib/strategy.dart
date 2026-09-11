import 'computer_game.dart';

class Strategy extends ComputerGame {
  Strategy(super.title, super.developer, super.year);

  Strategy.fromDefault()
      : super('Civilization VI', 'Firaxis Games', 2016);

  Strategy.name(Strategy obj):super(obj.title, obj.developer, obj.year);

  void buildBase() {
    print('$title: игрок строит базу.');
  }

  @override
  void isCool() {
    // TODO: implement isCool
  }


}
