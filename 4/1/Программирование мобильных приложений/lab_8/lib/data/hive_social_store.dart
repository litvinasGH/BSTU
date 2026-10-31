import 'dart:convert';
import 'dart:io';

import 'package:hive_flutter/hive_flutter.dart';
import 'package:path_provider/path_provider.dart';

import '../models/social_models.dart';

class HiveSocialStore {
  HiveSocialStore._();

  static late Box<SocialUser> users;
  static late Box<SocialPost> posts;
  static late Box<SocialStory> stories;
  static late Box<FavoriteEntry> favorites;
  static late Box<String> settings;
  static late Directory hiveDirectory;
  static late File _encryptionKeyFile;

  static const _defaultUserId = 'darlene';
  static const _keyFileName = 'social_hive_aes_key.txt';
  static const _testBoxName = 'wrong_key_demo_temp';

  static bool _shouldCompact(int entries, int deletedEntries) {
    if (entries <= 0) return false;
    return deletedEntries >= 20 && deletedEntries / entries >= 0.20;
  }

  static Future<void> initialize({Directory? rootDirectory}) async {
    late final Directory supportDirectory;

    if (rootDirectory != null) {
      await rootDirectory.create(recursive: true);
      hiveDirectory = Directory(
        '${rootDirectory.path}${Platform.pathSeparator}hive',
      );
      supportDirectory = Directory(
        '${rootDirectory.path}${Platform.pathSeparator}support',
      );
    } else {
      final documents = await getApplicationDocumentsDirectory();
      final support = await getApplicationSupportDirectory();
      hiveDirectory = Directory(
        '${documents.path}${Platform.pathSeparator}social_hive',
      );
      supportDirectory = support;
    }

    await hiveDirectory.create(recursive: true);
    await supportDirectory.create(recursive: true);
    _encryptionKeyFile = File(
      '${supportDirectory.path}${Platform.pathSeparator}$_keyFileName',
    );

    final key = await _loadOrCreateEncryptionKey();
    final cipher = HiveAesCipher(key);
    Hive.init(hiveDirectory.path);

    if (!Hive.isAdapterRegistered(70))
      Hive.registerAdapter(SocialUserAdapter());
    if (!Hive.isAdapterRegistered(71))
      Hive.registerAdapter(SocialPostAdapter());
    if (!Hive.isAdapterRegistered(72))
      Hive.registerAdapter(SocialStoryAdapter());
    if (!Hive.isAdapterRegistered(73))
      Hive.registerAdapter(FavoriteEntryAdapter());

    users = await Hive.openBox<SocialUser>(
      'users',
      encryptionCipher: cipher,
      compactionStrategy: _shouldCompact,
    );
    posts = await Hive.openBox<SocialPost>(
      'posts',
      encryptionCipher: cipher,
      compactionStrategy: _shouldCompact,
    );
    stories = await Hive.openBox<SocialStory>(
      'stories',
      encryptionCipher: cipher,
      compactionStrategy: _shouldCompact,
    );
    favorites = await Hive.openBox<FavoriteEntry>(
      'favorites',
      encryptionCipher: cipher,
      compactionStrategy: _shouldCompact,
    );
    settings = await Hive.openBox<String>('settings');

    await _seedInitialData();
  }

  static Future<List<int>> _loadOrCreateEncryptionKey() async {
    if (await _encryptionKeyFile.exists()) {
      final decoded = base64Decode(
        (await _encryptionKeyFile.readAsString()).trim(),
      );
      if (decoded.length != 32) {
        throw StateError(
          'Файл ключа Hive существует, но не содержит 32 байта. '
          'Не удаляйте его, пока нужны сохранённые данные.',
        );
      }
      return decoded;
    }

    final generatedKey = Hive.generateSecureKey();
    await _encryptionKeyFile.parent.create(recursive: true);
    await _encryptionKeyFile.writeAsString(
      base64Encode(generatedKey),
      flush: true,
    );
    return generatedKey;
  }

  static Future<void> _seedInitialData() async {
    if (!users.containsKey('darlene')) {
      await users.put(
        'darlene',
        const SocialUser(
          id: 'darlene',
          name: 'Darlene Beats',
          handle: 'dw_beats',
          avatarPath: 'assets/mainavatar.jpg',
          role: 'admin',
          bio: 'Фото, моменты и истории из жизни.',
          followers: 160000,
          following: 140000,
        ),
      );
    }
    if (!users.containsKey('nilesh')) {
      await users.put(
        'nilesh',
        const SocialUser(
          id: 'nilesh',
          name: 'Nilesh',
          handle: 'nilesh',
          avatarPath: 'assets/postsAndStor/5.jpg',
          role: 'user',
          bio: 'Люблю приключения и новые места.',
          followers: 2400,
          following: 510,
        ),
      );
    }
    if (!users.containsKey('ultra_runner')) {
      await users.put(
        'ultra_runner',
        const SocialUser(
          id: 'ultra_runner',
          name: 'Ultra Runner',
          handle: 'the_ultra_tribe',
          avatarPath: 'assets/postsAndStor/3.jpg',
          role: 'manager',
          bio: 'Бег, горы, дистанции и спорт.',
          followers: 9800,
          following: 620,
        ),
      );
    }

    // Начальные записи перенесены из демонстрационного PostFeed лабораторной 4–5.
    // Seed выполняется только при пустом Box, поэтому изменения не перезаписываются.
    if (posts.isEmpty) {
      final now = DateTime.now();
      await posts.put(
        'post_nilesh_1',
        SocialPost(
          id: 'post_nilesh_1',
          authorId: 'nilesh',
          authorName: 'Nilesh',
          avatarPath: 'assets/postsAndStor/5.jpg',
          createdAtMillis: now
              .subtract(const Duration(hours: 1))
              .millisecondsSinceEpoch,
          subgroup: 'u8s',
          text: "Discover adventure in patagonia's peaks or serenity provence's @hamlets - arrival",
          imagePaths: const [
            'assets/postsAndStor/6.jpg',
            'assets/postsAndStor/7.jpg',
            'assets/postsAndStor/5.jpg',
          ],
        ),
      );
      await posts.put(
        'post_ultra_1',
        SocialPost(
          id: 'post_ultra_1',
          authorId: 'ultra_runner',
          authorName: 'Ultra Runner',
          avatarPath: 'assets/postsAndStor/3.jpg',
          createdAtMillis: now
              .subtract(const Duration(hours: 2))
              .millisecondsSinceEpoch,
          subgroup: 'The Ultra Tribe',
          text:
              'The elements, the terrain, and the miles. '
              'The full spectrum of extreme running in just a few frames. '
              'Which shot captures the vibe best for you? 👇 '
              '@the_ultra_tribe #XMarathon #RunnerLife #GritAndMiles #AthletesOfInstagram',
          imagePaths: const [
            'assets/postsAndStor/8.jpg',
            'assets/postsAndStor/9.jpg',
            'assets/postsAndStor/10.jpg',
          ],
        ),
      );
    }

    if (stories.isEmpty) {
      final now = DateTime.now().millisecondsSinceEpoch;
      for (var i = 0; i < 5; i++) {
        final user = i.isEven
            ? users.get('nilesh')!
            : users.get('ultra_runner')!;
        await stories.put(
          'story_$i',
          SocialStory(
            id: 'story_$i',
            userId: user.id,
            userName: user.name,
            avatarPath: 'assets/splitanimage/img_${i + 1}.png',
            imagePath: 'assets/postsAndStor/$i.jpg',
            createdAtMillis: now - (i * 60000),
          ),
        );
      }
    }

    final selected = settings.get('activeUserId');
    if (selected == null || !users.containsKey(selected)) {
      await settings.put('activeUserId', _defaultUserId);
    }
  }

  static String get activeUserId =>
      settings.get('activeUserId', defaultValue: _defaultUserId)!;

  static SocialUser get activeUser =>
      users.get(activeUserId) ??
      users.get(_defaultUserId) ??
      users.values.first;

  static List<SocialUser> get allUsers =>
      users.values.toList()
        ..sort((a, b) => a.name.toLowerCase().compareTo(b.name.toLowerCase()));

  static Future<void> setActiveUser(String userId) async {
    if (!users.containsKey(userId)) {
      throw ArgumentError.value(userId, 'userId', 'Пользователь не найден');
    }
    await settings.put('activeUserId', userId);
  }

  static bool canManagePosts(SocialUser actor) => actor.canManagePosts;

  static Future<void> savePost({
    required SocialUser actor,
    required SocialPost post,
  }) async {
    if (!canManagePosts(actor)) {
      throw StateError(
        'Роль «${actor.roleLabel}» не может изменять публикации.',
      );
    }
    await posts.put(post.id, post);
  }

  static Future<void> deletePost({
    required SocialUser actor,
    required String postId,
  }) async {
    if (!canManagePosts(actor)) {
      throw StateError(
        'Роль «${actor.roleLabel}» не может удалять публикации.',
      );
    }
    await posts.delete(postId);
    final favoriteKeys = favorites.keys
        .where((key) => favorites.get(key)?.postId == postId)
        .toList();
    await favorites.deleteAll(favoriteKeys);
  }

  static String _favoriteId(String userId, String postId) => '$userId::$postId';

  static bool isFavorite({required String userId, required String postId}) =>
      favorites.containsKey(_favoriteId(userId, postId));

  static Future<void> toggleFavorite({
    required String userId,
    required String postId,
  }) async {
    final id = _favoriteId(userId, postId);
    if (favorites.containsKey(id)) {
      await favorites.delete(id);
    } else {
      await favorites.put(
        id,
        FavoriteEntry(
          id: id,
          userId: userId,
          postId: postId,
          addedAtMillis: DateTime.now().millisecondsSinceEpoch,
        ),
      );
    }
  }

  static List<SocialPost> favoritesFor(String userId) {
    final favoritePostIds = favorites.values
        .where((favorite) => favorite.userId == userId)
        .map((favorite) => favorite.postId)
        .toSet();
    return posts.values
        .where((post) => favoritePostIds.contains(post.id))
        .toList()
      ..sort((a, b) => b.createdAtMillis.compareTo(a.createdAtMillis));
  }

  static Future<void> compactAllBoxes() async {
    await posts.compact();
    await users.compact();
    await stories.compact();
    await favorites.compact();
    await settings.compact();
  }

  /// Проверяет неверный ключ на отдельном временном Box:
  /// основное хранилище не закрывается, не копируется и не изменяется.
  static Future<String> demonstrateWrongKey() async {
    await _removeTestBoxIfPresent();

    final correctKey = base64Decode(
      (await _encryptionKeyFile.readAsString()).trim(),
    );
    final sourcePost = posts.values.isNotEmpty
        ? posts.values.first
        : const SocialPost(
            id: 'temporary_probe',
            authorId: 'probe',
            authorName: 'Probe',
            avatarPath: '',
            createdAtMillis: 0,
            subgroup: '',
            text: 'Тест шифрования',
            imagePaths: <String>[],
          );

    final validBox = await Hive.openBox<SocialPost>(
      _testBoxName,
      encryptionCipher: HiveAesCipher(correctKey),
    );
    await validBox.put('probe', sourcePost);
    await validBox.flush();
    await validBox.close();

    String result;
    try {
      final wrongKey = Hive.generateSecureKey();
      final wrongBox = await Hive.openBox<SocialPost>(
        _testBoxName,
        encryptionCipher: HiveAesCipher(wrongKey),
      );
      try {
        final readValue = wrongBox.get('probe');
        result = readValue == null
            ? 'Box открылся, но запись не прочиталась. Проверка завершена.'
            : 'Внимание: Hive вернул объект с неправильным ключом. Проверьте версию Hive.';
      } finally {
        await wrongBox.close();
      }
    } catch (error) {
      result =
          'Ожидаемый результат: Hive отклонил неправильный ключ.\n'
          '${error.runtimeType}: $error';
    } finally {
      await _removeTestBoxIfPresent();
    }
    return result;
  }

  static Future<void> _removeTestBoxIfPresent() async {
    if (Hive.isBoxOpen(_testBoxName)) {
      await Hive.box<SocialPost>(_testBoxName).close();
    }
    final file = File(
      '${hiveDirectory.path}${Platform.pathSeparator}$_testBoxName.hive',
    );
    final lock = File(
      '${hiveDirectory.path}${Platform.pathSeparator}$_testBoxName.lock',
    );
    if (await file.exists() || await lock.exists()) {
      try {
        await Hive.deleteBoxFromDisk(_testBoxName);
      } catch (_) {
        // Очищаются только тестовые файлы, основная база не затрагивается.
        if (await file.exists()) await file.delete();
        if (await lock.exists()) await lock.delete();
      }
    }
  }

  /// Закрывает открытые коробки. Используется тестами и не меняет данные на диске.
  static Future<void> close() async {
    if (Hive.isBoxOpen('users')) await users.close();
    if (Hive.isBoxOpen('posts')) await posts.close();
    if (Hive.isBoxOpen('stories')) await stories.close();
    if (Hive.isBoxOpen('favorites')) await favorites.close();
    if (Hive.isBoxOpen('settings')) await settings.close();
    if (Hive.isBoxOpen(_testBoxName)) {
      await Hive.box<SocialPost>(_testBoxName).close();
    }
    await Hive.close();
  }
}
