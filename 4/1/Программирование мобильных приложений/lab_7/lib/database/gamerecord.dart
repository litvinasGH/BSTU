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

  Map<String, Object?> toJson() {
    return {
      'id': id,
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

  factory GameRecord.fromJson(Map<String, Object?> json) {
    return GameRecord(
      id: json['id'] as int?,
      title: json['title'] as String,
      developer: json['developer'] as String,
      year: json['year'] as int,
      type: json['type'] as String,
    );
  }

  @override
  bool operator ==(Object other) {
    return other is GameRecord &&
        other.id == id &&
        other.title == title &&
        other.developer == developer &&
        other.year == year &&
        other.type == type;
  }

  @override
  int get hashCode => Object.hash(id, title, developer, year, type);
}
