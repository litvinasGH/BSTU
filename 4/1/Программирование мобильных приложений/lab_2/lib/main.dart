import 'dart:async';
import 'dart:convert';
import 'dart:io';

import 'package:lab_2/asyn.dart';
import 'package:lab_2/game_collection.dart';
import 'package:lab_2/game_iterator.dart';

import 'shooter.dart';
import 'rpg.dart';
import 'online_game.dart';
import 'strategy.dart';
import 'computer_game.dart';

Future<void> main() async {
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

  shooter.updateInfo(newDeveloper: 'RockStar North');

  shooter.launch();

  shooter.processGame((gameName) {
    print('Обрабатываем игру: $gameName');
  });

  List<ComputerGame> games = [
    Shooter('Counter-Strike 2', 'Valve', 2023),
    Strategy('Civilization VI', 'Firaxis Games', 2016),
    Shooter('DOOM Eternal', 'id Software', 2020),
    Rpg("Skyrim", "Беседка", 2012),
  ];

  for (ComputerGame game in games) {
    print(game.title);
  }

  Map<String, int> gameRatings = {
    'Counter-Strike 2': 9,
    'Civilization VI': 10,
    'DOOM Eternal': 9,
    'Skyrim': 10,
  };

  print(gameRatings['Skyrim']);

  gameRatings['Age of Empires IV'] = 8;

  Set<String> genres = {'Shooter', 'Strategy', 'RPG', 'Shooter'};

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
  } catch (e) {
    print("Произошла ошибка: $e");
  } finally {
    print('Проверка здоровья завершена.');
  }

  try {
    shooter.setHealth(100);
  } catch (e) {
    print("Произошла ошибка: $e");
  } finally {
    print('Проверка здоровья завершена.');
  }

  // * 3Лаба

  shooter.logAction('Игрок вошёл в игру');
  shooter.logGameStart(shooter.title);

  shooter.startGame();
  shooter.shoot();

  shooter.logGameEnd(shooter.title);

  games.sort();

  for (ComputerGame game in games) {
    print('${game.title} — ${game.year}');
  }

  GameIterator iterator = GameIterator(games);

  while (iterator.moveNext()) {
    print(iterator.current.title);
  }

  GameCollection collection = GameCollection(games);

  for (ComputerGame game in collection) {
    print(game.title);
  }

  String jsonString = jsonEncode(shooter.toJson());

  print(jsonString);

  Shooter restoredGame = Shooter.fromJson(jsonDecode(jsonString));

  restoredGame.showInfo();

  bool loading = true;

  loadGameData().then((result) {
    print('\n$result');
    loading = false;
  });

  while (loading) {
    print('Загрузка...');
    await Future.delayed(Duration(seconds: 1));
  }

  print('Работа завершена');


    try {
      int rating = await calculateGameRating(8);

      print('Рейтинг игры: $rating');
    } catch (e) {
      print('Ошибка: $e');
    }

    try {
      int rating = await calculateGameRating(-1);

      print('Рейтинг игры: $rating');
    } catch (e) {
      print('Ошибка: $e');
    }
  

  String gameName = await loadGameName();

  int year = await getGameYear(gameName);

  print('Игра: $gameName');
  print('Год: $year');

  loadGameName()
      .then((gameName) {
        print('Игра: $gameName');
        return getGameYear(gameName);
      })
      .then((year) {
        print('Год выпуска: $year');
      })
      .catchError((error) {
        print('Ошибка: $error');
      });

  StreamSubscription<int> subscription = generateNumbers().listen((number) {
    print('Получено: $number');
  });

  //late StreamSubscription<int> subscription;

  subscription = generateNumbers().listen((number) async {
    print('Получено: $number');

    if (number == 3) {
      await subscription.cancel();
      print('Подписка отменена');
    }
  });

  Stream<int> broadcastStream = generateBroadcastNumbers().asBroadcastStream();

  var subscription1 = broadcastStream.listen((number) {
    print('Подписчик 1: $number');
  });

  var subscription2 = broadcastStream.listen((number) {
    print('Подписчик 2: $number');
  });

  Stream<String> transformedStream = broadcastStream.map(
    (number) => 'Число: $number',
  );

  transformedStream.listen((value) {
    print(value);
  });

  //exit(0);
}
