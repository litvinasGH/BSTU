import 'computer_game.dart';

class OnlineGame implements ComputerGame {
  @override
  String title;

  @override
  String developer;

  @override
  int year;

  OnlineGame(this.title, this.developer, this.year);

  @override
  void showInfo() {
    print("НЕТ ПОДКЛЮЧЕНИЯ К СЕТИ(");
  }

  @override
  void isCool() {
    // TODO: implement isCool
  }

  @override
  set gameTitle(String newTitle) {
    // TODO: implement gameTitle
  }

  @override
  // TODO: implement popularity
  double get popularity => throw UnimplementedError();

  @override
  void updateInfo({required String newDeveloper, int newYear = 2026}) {
    // TODO: implement updateInfo
  }

  @override
  void launch([String? mode]) {
    // TODO: implement launch
  }

  @override
  void processGame(void Function(String) action) {
    // TODO: implement processGame
  }

  @override
  void setHealth(int health) {
    // TODO: implement setHealth
  }

  @override
  void logAction(String message) {
    // TODO: implement logAction
  }

  @override
  void logGameEnd(String title) {
    // TODO: implement logGameEnd
  }

  @override
  void logGameStart(String title) {
    // TODO: implement logGameStart
  }

  @override
  int compareTo(ComputerGame other) {
    // TODO: implement compareTo
    throw UnimplementedError();
  }

  @override
  Map<String, dynamic> toJson() {
    // TODO: implement toJson
    throw UnimplementedError();
  }

  
}