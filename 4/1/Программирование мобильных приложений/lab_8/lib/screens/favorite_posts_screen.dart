import 'package:flutter/material.dart';
import 'package:hive_flutter/hive_flutter.dart';

import '../data/hive_social_store.dart';
import '../models/social_models.dart';
import '../widgets/postitem.dart';

class FavoritePostsScreen extends StatelessWidget {
  const FavoritePostsScreen({super.key});

  @override
  Widget build(BuildContext context) {
    final userId = HiveSocialStore.activeUserId;
    return Scaffold(
      appBar: AppBar(title: const Text('Избранное')),
      body: ValueListenableBuilder<Box<FavoriteEntry>>(
        valueListenable: HiveSocialStore.favorites.listenable(),
        builder: (context, favoritesBox, _) {
          return ValueListenableBuilder<Box<SocialPost>>(
            valueListenable: HiveSocialStore.posts.listenable(),
            builder: (context, postsBox, _) {
              final ids = favoritesBox.values
                  .where((entry) => entry.userId == userId)
                  .map((entry) => entry.postId)
                  .toSet();
              final posts = postsBox.values.where((post) => ids.contains(post.id)).toList()
                ..sort((a, b) => b.createdAtMillis.compareTo(a.createdAtMillis));
              if (posts.isEmpty) {
                return const Center(child: Text('Пока нет избранных публикаций.'));
              }
              return ListView(
                padding: const EdgeInsets.only(left: 20, right: 20, top: 12, bottom: 24),
                children: [
                  for (final post in posts)
                    PostItem(
                      key: ValueKey(post.id),
                      postId: post.id,
                      isLiked: true,
                      onLikeToggle: () => HiveSocialStore.toggleFavorite(userId: userId, postId: post.id),
                      images: post.imagePaths,
                      avatar: post.avatarPath,
                      name: post.authorName,
                      postedTime: 'Избранное',
                      subgroup: post.subgroup,
                      text: post.text,
                    ),
                ],
              );
            },
          );
        },
      ),
    );
  }
}
