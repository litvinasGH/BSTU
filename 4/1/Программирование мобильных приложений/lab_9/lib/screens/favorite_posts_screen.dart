import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

import '../providers/social_provider.dart';
import '../widgets/postitem.dart';

class FavoritePostsScreen extends StatelessWidget {
  const FavoritePostsScreen({super.key});

  @override
  Widget build(BuildContext context) {
    final social = context.watch<SocialProvider>();
    final userId = social.activeUserId;
    final posts = social.favoritesFor(userId);

    return Scaffold(
      appBar: AppBar(title: const Text('Избранное')),
      body: posts.isEmpty
          ? const Center(child: Text('Пока нет избранных публикаций.'))
          : ListView(
              padding: const EdgeInsets.only(left: 20, right: 20, top: 12, bottom: 24),
              children: [
                for (final post in posts)
                  PostItem(
                    key: ValueKey(post.id),
                    postId: post.id,
                    isLiked: true,
                    onLikeToggle: () => context.read<SocialProvider>().toggleFavorite(
                      userId: userId,
                      postId: post.id,
                    ),
                    images: post.imagePaths,
                    avatar: post.avatarPath,
                    name: post.authorName,
                    postedTime: 'Избранное',
                    subgroup: post.subgroup,
                    text: post.text,
                  ),
              ],
            ),
    );
  }
}
