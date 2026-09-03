abstract class ComputerGame {
  String title;
  String developer;
  int year;
  static int gameCount = 0;

  static void showGameCount() {
  print('Создано игр: $gameCount');
}

  ComputerGame(this.title, this.developer, this.year){
    gameCount++;
  }

  void showInfo() {
    print('Название: $title');
    print('Разработчик: $developer');
    print('Год выпуска: $year');
  }

  void isCool();

  double get popularity {
    return year >= 2020 ? 10.0 : 7.0;
  }

  set gameTitle(String newTitle) {
    if (newTitle.isEmpty) {
      throw ArgumentError('Название игры не может быть пустым.');
    }
    title = newTitle;
  }


  void updateInfo({
    required String newDeveloper,
    int newYear = 2026,
  }) {
    developer = newDeveloper;
    year = newYear;
  }

  void launch([String mode = 'Одиночная игра']) {
    print('$title запущена. Режим: $mode');
  }




  void processGame(void Function(String) action) {
    action(title);
  }



  void setHealth(int health) {
  if (health < 0) {
    throw ArgumentError('Здоровье не может быть отрицательным.');
  }

  print('Здоровье установлено: $health');
}
}
