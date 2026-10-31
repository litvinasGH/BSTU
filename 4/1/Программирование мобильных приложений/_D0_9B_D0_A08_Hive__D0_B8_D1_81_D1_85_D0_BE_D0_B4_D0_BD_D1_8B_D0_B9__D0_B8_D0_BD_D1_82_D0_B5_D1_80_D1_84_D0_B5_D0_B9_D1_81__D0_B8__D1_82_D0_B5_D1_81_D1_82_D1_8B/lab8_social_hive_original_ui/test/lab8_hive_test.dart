import 'dart:convert';
import 'dart:io';

import 'package:flutter_test/flutter_test.dart';
import 'package:lab_4/data/hive_social_store.dart';
import 'package:lab_4/models/social_models.dart';

void main() {
  late Directory testDirectory;

  setUp(() async {
    testDirectory = await Directory.systemTemp.createTemp('lab8_hive_test_');
    await HiveSocialStore.initialize(rootDirectory: testDirectory);
  });

  tearDown(() async {
    await HiveSocialStore.close();
    if (await testDirectory.exists()) {
      await testDirectory.delete(recursive: true);
    }
  });

  test('encrypted Hive storage has an internal 256-bit key file', () async {
    final keyFile = File('${testDirectory.path}${Platform.pathSeparator}support${Platform.pathSeparator}social_hive_aes_key.txt');
    expect(await keyFile.exists(), isTrue);
    final key = base64Decode((await keyFile.readAsString()).trim());
    expect(key.length, 32);
    expect(HiveSocialStore.posts.isOpen, isTrue);
  });

  test('initial Hive data comes from the original social network feed', () {
    final authors = HiveSocialStore.posts.values.map((post) => post.authorName).toSet();

    expect(authors, containsAll(<String>['Nilesh', 'Ultra Runner']));
    expect(HiveSocialStore.stories.length, 5);
    expect(HiveSocialStore.users.get('darlene')?.role, 'admin');
    expect(HiveSocialStore.users.get('ultra_runner')?.role, 'manager');
    expect(HiveSocialStore.users.get('nilesh')?.role, 'user');
  });

  test('only an admin or manager can create and delete posts', () async {
    final user = HiveSocialStore.users.get('nilesh')!;
    final manager = HiveSocialStore.users.get('ultra_runner')!;
    final post = _samplePost('role_test_post');

    await expectLater(
      HiveSocialStore.savePost(actor: user, post: post),
      throwsA(isA<StateError>()),
    );
    expect(HiveSocialStore.posts.containsKey(post.id), isFalse);

    await HiveSocialStore.savePost(actor: manager, post: post);
    expect(HiveSocialStore.posts.containsKey(post.id), isTrue);

    await expectLater(
      HiveSocialStore.deletePost(actor: user, postId: post.id),
      throwsA(isA<StateError>()),
    );
    expect(HiveSocialStore.posts.containsKey(post.id), isTrue);

    await HiveSocialStore.deletePost(actor: manager, postId: post.id);
    expect(HiveSocialStore.posts.containsKey(post.id), isFalse);
  });

  test('favorites are isolated per user and can be toggled', () async {
    final post = HiveSocialStore.posts.values.first;

    await HiveSocialStore.toggleFavorite(userId: 'nilesh', postId: post.id);
    expect(HiveSocialStore.isFavorite(userId: 'nilesh', postId: post.id), isTrue);
    expect(HiveSocialStore.favoritesFor('nilesh').map((item) => item.id), contains(post.id));
    expect(HiveSocialStore.isFavorite(userId: 'darlene', postId: post.id), isFalse);
    expect(HiveSocialStore.favoritesFor('darlene'), isEmpty);

    await HiveSocialStore.toggleFavorite(userId: 'nilesh', postId: post.id);
    expect(HiveSocialStore.isFavorite(userId: 'nilesh', postId: post.id), isFalse);
    expect(HiveSocialStore.favoritesFor('nilesh'), isEmpty);
  });

  test('Hive compact leaves the stored records intact', () async {
    final countBefore = HiveSocialStore.posts.length;
    final idsBefore = HiveSocialStore.posts.keys.toSet();

    await HiveSocialStore.compactAllBoxes();

    expect(HiveSocialStore.posts.length, countBefore);
    expect(HiveSocialStore.posts.keys.toSet(), idsBefore);
    expect(HiveSocialStore.posts.get('post_nilesh_1')?.authorName, 'Nilesh');
  });

  test('a wrong encryption key is rejected or cannot read the test record', () async {
    final originalText = HiveSocialStore.posts.get('post_nilesh_1')?.text;

    final result = await HiveSocialStore.demonstrateWrongKey();

    expect(
      result,
      anyOf(
        contains('Ожидаемый результат'),
        contains('не прочиталась'),
      ),
    );
    // The encryption demonstration must not damage the real app data.
    expect(HiveSocialStore.posts.get('post_nilesh_1')?.text, originalText);
  });
}

SocialPost _samplePost(String id) => SocialPost(
      id: id,
      authorId: 'ultra_runner',
      authorName: 'Ultra Runner',
      avatarPath: 'assets/postsAndStor/3.jpg',
      createdAtMillis: DateTime.now().millisecondsSinceEpoch,
      subgroup: 'The Ultra Tribe',
      text: 'Test publication for Lab 8',
      imagePaths: const <String>[],
    );
