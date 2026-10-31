import 'package:hive/hive.dart';

/// Роль пользователя. В Hive сохраняется строковый код роли,
/// чтобы изменение отображаемых названий не требовало миграции данных.
class SocialUser {
  const SocialUser({
    required this.id,
    required this.name,
    required this.handle,
    required this.avatarPath,
    required this.role,
    this.bio = '',
    this.followers = 0,
    this.following = 0,
  });

  final String id;
  final String name;
  final String handle;
  final String avatarPath;
  final String role; // admin | manager | user
  final String bio;
  final int followers;
  final int following;

  String get roleLabel => switch (role) {
        'admin' => 'Администратор',
        'manager' => 'Менеджер',
        _ => 'Пользователь',
      };

  bool get canManagePosts => role == 'admin' || role == 'manager';
}

class SocialPost {
  const SocialPost({
    required this.id,
    required this.authorId,
    required this.authorName,
    required this.avatarPath,
    required this.createdAtMillis,
    required this.subgroup,
    required this.text,
    required this.imagePaths,
  });

  final String id;
  final String authorId;
  final String authorName;
  final String avatarPath;
  final int createdAtMillis;
  final String subgroup;
  final String text;
  final List<String> imagePaths;

  DateTime get createdAt => DateTime.fromMillisecondsSinceEpoch(createdAtMillis);

  SocialPost copyWith({
    String? authorId,
    String? authorName,
    String? avatarPath,
    int? createdAtMillis,
    String? subgroup,
    String? text,
    List<String>? imagePaths,
  }) {
    return SocialPost(
      id: id,
      authorId: authorId ?? this.authorId,
      authorName: authorName ?? this.authorName,
      avatarPath: avatarPath ?? this.avatarPath,
      createdAtMillis: createdAtMillis ?? this.createdAtMillis,
      subgroup: subgroup ?? this.subgroup,
      text: text ?? this.text,
      imagePaths: imagePaths ?? this.imagePaths,
    );
  }
}

class SocialStory {
  const SocialStory({
    required this.id,
    required this.userId,
    required this.userName,
    required this.avatarPath,
    required this.imagePath,
    required this.createdAtMillis,
  });

  final String id;
  final String userId;
  final String userName;
  final String avatarPath;
  final String imagePath;
  final int createdAtMillis;
}

class FavoriteEntry {
  const FavoriteEntry({
    required this.id,
    required this.userId,
    required this.postId,
    required this.addedAtMillis,
  });

  final String id;
  final String userId;
  final String postId;
  final int addedAtMillis;
}

// Ручные адаптеры избавляют от необходимости запускать build_runner.
// typeId фиксированы: их нельзя менять после появления сохранённых данных.
class SocialUserAdapter extends TypeAdapter<SocialUser> {
  @override
  int get typeId => 70;

  @override
  SocialUser read(BinaryReader reader) {
    final count = reader.readByte();
    final fields = <int, dynamic>{};
    for (var i = 0; i < count; i++) {
      fields[reader.readByte()] = reader.read();
    }
    return SocialUser(
      id: (fields[0] as String?) ?? '',
      name: (fields[1] as String?) ?? 'Пользователь',
      handle: (fields[2] as String?) ?? '',
      avatarPath: (fields[3] as String?) ?? '',
      role: (fields[4] as String?) ?? 'user',
      bio: (fields[5] as String?) ?? '',
      followers: (fields[6] as int?) ?? 0,
      following: (fields[7] as int?) ?? 0,
    );
  }

  @override
  void write(BinaryWriter writer, SocialUser obj) {
    writer
      ..writeByte(8)
      ..writeByte(0)
      ..write(obj.id)
      ..writeByte(1)
      ..write(obj.name)
      ..writeByte(2)
      ..write(obj.handle)
      ..writeByte(3)
      ..write(obj.avatarPath)
      ..writeByte(4)
      ..write(obj.role)
      ..writeByte(5)
      ..write(obj.bio)
      ..writeByte(6)
      ..write(obj.followers)
      ..writeByte(7)
      ..write(obj.following);
  }
}

class SocialPostAdapter extends TypeAdapter<SocialPost> {
  @override
  int get typeId => 71;

  @override
  SocialPost read(BinaryReader reader) {
    final count = reader.readByte();
    final fields = <int, dynamic>{};
    for (var i = 0; i < count; i++) {
      fields[reader.readByte()] = reader.read();
    }
    final rawImages = fields[7];
    return SocialPost(
      id: (fields[0] as String?) ?? '',
      authorId: (fields[1] as String?) ?? '',
      authorName: (fields[2] as String?) ?? 'Пользователь',
      avatarPath: (fields[3] as String?) ?? '',
      createdAtMillis: (fields[4] as int?) ?? 0,
      subgroup: (fields[5] as String?) ?? '',
      text: (fields[6] as String?) ?? '',
      imagePaths: rawImages is List
          ? rawImages.map((value) => value.toString()).toList()
          : <String>[],
    );
  }

  @override
  void write(BinaryWriter writer, SocialPost obj) {
    writer
      ..writeByte(8)
      ..writeByte(0)
      ..write(obj.id)
      ..writeByte(1)
      ..write(obj.authorId)
      ..writeByte(2)
      ..write(obj.authorName)
      ..writeByte(3)
      ..write(obj.avatarPath)
      ..writeByte(4)
      ..write(obj.createdAtMillis)
      ..writeByte(5)
      ..write(obj.subgroup)
      ..writeByte(6)
      ..write(obj.text)
      ..writeByte(7)
      ..write(obj.imagePaths);
  }
}

class SocialStoryAdapter extends TypeAdapter<SocialStory> {
  @override
  int get typeId => 72;

  @override
  SocialStory read(BinaryReader reader) {
    final count = reader.readByte();
    final fields = <int, dynamic>{};
    for (var i = 0; i < count; i++) {
      fields[reader.readByte()] = reader.read();
    }
    return SocialStory(
      id: (fields[0] as String?) ?? '',
      userId: (fields[1] as String?) ?? '',
      userName: (fields[2] as String?) ?? 'Пользователь',
      avatarPath: (fields[3] as String?) ?? '',
      imagePath: (fields[4] as String?) ?? '',
      createdAtMillis: (fields[5] as int?) ?? 0,
    );
  }

  @override
  void write(BinaryWriter writer, SocialStory obj) {
    writer
      ..writeByte(6)
      ..writeByte(0)
      ..write(obj.id)
      ..writeByte(1)
      ..write(obj.userId)
      ..writeByte(2)
      ..write(obj.userName)
      ..writeByte(3)
      ..write(obj.avatarPath)
      ..writeByte(4)
      ..write(obj.imagePath)
      ..writeByte(5)
      ..write(obj.createdAtMillis);
  }
}

class FavoriteEntryAdapter extends TypeAdapter<FavoriteEntry> {
  @override
  int get typeId => 73;

  @override
  FavoriteEntry read(BinaryReader reader) {
    final count = reader.readByte();
    final fields = <int, dynamic>{};
    for (var i = 0; i < count; i++) {
      fields[reader.readByte()] = reader.read();
    }
    return FavoriteEntry(
      id: (fields[0] as String?) ?? '',
      userId: (fields[1] as String?) ?? '',
      postId: (fields[2] as String?) ?? '',
      addedAtMillis: (fields[3] as int?) ?? 0,
    );
  }

  @override
  void write(BinaryWriter writer, FavoriteEntry obj) {
    writer
      ..writeByte(4)
      ..writeByte(0)
      ..write(obj.id)
      ..writeByte(1)
      ..write(obj.userId)
      ..writeByte(2)
      ..write(obj.postId)
      ..writeByte(3)
      ..write(obj.addedAtMillis);
  }
}
