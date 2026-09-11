Future<String> loadGameData() async {
  print('Начинаем загрузку данных...');

  await Future.delayed(Duration(seconds: 2));

  print('Данные загружены.');

  return 'Данные об игре получены';
}

Future<int> calculateGameRating(int rating) async {
  await Future.delayed(Duration(seconds: 2));

  

  if (rating < 0) {
    throw Exception('Неверный рейтинг');
  }

  return rating;
}



Future<String> loadGameName() async {
  await Future.delayed(Duration(seconds: 1));

  return 'Counter-Strike 2';
}


Future<int> getGameYear(String gameName) async {
  await Future.delayed(Duration(seconds: 1));

  return 2023;
}













Stream<int> generateNumbers() async* {
  for (int i = 1; i <= 5; i++) {
    await Future.delayed(Duration(seconds: 1));

    print('Создано число: $i');

    yield i;
  }
}



Stream<int> generateBroadcastNumbers() async* {
  for (int i = 1; i <= 5; i++) {
    await Future.delayed(Duration(seconds: 1));

    yield i;
  }
}
