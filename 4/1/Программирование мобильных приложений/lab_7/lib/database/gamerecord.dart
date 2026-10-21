import '../computer_game.dart';

class GameRecord {
  final int? id;
  final String title;
  final String developer;
  final int year;
  final String type;

  const GameRecord({
    this.id,
    required this.title,
    required this.developer,
    required this.year,
    required this.type,
  });

  factory GameRecord.fromComputerGame(ComputerGame game) {
    return GameRecord(
      title: game.title,
      developer: game.developer,
      year: game.year,
      type: game.runtimeType.toString(),
    );
  }

  Map<String, Object?> toMap() {
    return {
      if (id != null) 'id': id,
      'title': title,
      'developer': developer,
      'year': year,
      'type': type,
    };
  }

  factory GameRecord.fromMap(Map<String, Object?> map) {
    return GameRecord(
      id: map['id'] as int?,
      title: map['title'] as String,
      developer: map['developer'] as String,
      year: map['year'] as int,
      type: map['type'] as String,
    );
  }
}
