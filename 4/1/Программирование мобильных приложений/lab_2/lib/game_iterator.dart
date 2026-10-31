import 'computer_game.dart';

class GameIterator implements Iterator<ComputerGame> {
  final List<ComputerGame> games;
  int _index = -1;

  GameIterator(this.games);

  @override
  ComputerGame get current => games[_index];

  @override
  bool moveNext() {
    _index++;

    return _index < games.length;
  }
}
