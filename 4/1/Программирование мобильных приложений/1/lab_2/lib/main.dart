import 'shooter.dart';
import 'rpg.dart';
import 'online_game.dart';
import 'strategy.dart';
import 'computer_game.dart';

void main() {
  Shooter shooter = Shooter('GTA 6', 'RockStar', 2026);
  Rpg rpg = Rpg('Persona 4 R', 'Atlus', 2027);

  shooter.showInfo();
  shooter.startGame();
  shooter.shoot();
  shooter.endGame();

  print('');

  rpg.showInfo();
  rpg.lernMag();

  OnlineGame game = OnlineGame('HellDivers2', '///', 2024);

  game.showInfo();

  print('Название игры: ${game.title}');

  Strategy game2 = Strategy.name(Strategy.fromDefault());
  game2.showInfo();
  print(game2.popularity);

  shooter.gameTitle = 'GTA6 O';

  ComputerGame.showGameCount();


  shooter.updateInfo(
    newDeveloper: 'RockStar North',
  );



  shooter.launch();



  shooter.processGame((gameName) {
    print('Обрабатываем игру: $gameName');
  });



  List<ComputerGame> games = [
    Shooter('Counter-Strike 2', 'Valve', 2023),
    Strategy('Civilization VI', 'Firaxis Games', 2016),
    Shooter('DOOM Eternal', 'id Software', 2020), 
    Rpg("Skyrim", "Беседка", 2012)
  ];


  for (ComputerGame game in games) {
    print(game.title);
  }


  Map<String, int> gameRatings = {
    'Counter-Strike 2': 9,
    'Civilization VI': 10,
    'DOOM Eternal': 9,
    'Skyrim': 10
  };

  print(gameRatings['Skyrim']);

  gameRatings['Age of Empires IV'] = 8;

  Set<String> genres = {
    'Shooter',
    'Strategy',
    'RPG',
    'Shooter',
  };

  for (String gen in genres) {
    print(gen);
  }

  genres.add('Adventure');

  print(genres.contains('RPG'));



  for (ComputerGame game in games) {
    if (game.year < 2020) {
      continue;
    }
    if (game.title == 'DOOM Eternal') {
      print('Игра найдена: ${game.title}');
      break;
    }

    print('${game.title} — ${game.year}');
  }



  try {
    shooter.setHealth(-100);
  }
  catch (e){
    print("Произошла ошибка: $e");
  }
  finally{
    print('Проверка здоровья завершена.');
  }

    try {
    shooter.setHealth(100);
  }
  catch (e){
    print("Произошла ошибка: $e");
  }
  finally{
    print('Проверка здоровья завершена.');
  }

}