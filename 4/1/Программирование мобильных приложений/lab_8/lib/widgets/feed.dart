import 'package:flutter/material.dart';
import 'package:hive_flutter/hive_flutter.dart';

import '../data/hive_social_store.dart';
import '../models/social_models.dart';
import '../screens/social_management_screen.dart';
import 'postitem.dart';

class PostFeed extends StatelessWidget {
  const PostFeed({super.key, this.isProfileDcreen = false});

  final bool isProfileDcreen;

  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: const EdgeInsets.only(top: 30, left: 20, right: 20),
      child: Column(
        children: [
          if (!isProfileDcreen)
            Row(
              mainAxisAlignment: MainAxisAlignment.spaceBetween,
              children: [
                const Text(
                  'Recently Post',
                  style: TextStyle(color: Colors.grey, fontSize: 16),
                ),
                // Оставляем исходную иконку на прежнем месте. Управление
                // открывается отдельным экраном, без изменения ленты.
                InkWell(
                  borderRadius: BorderRadius.circular(20),
                  onTap: () => Navigator.of(context).push(
                    MaterialPageRoute(
                      builder: (_) => const SocialManagementScreen(),
                    ),
                  ),
                  child: const Padding(
                    padding: EdgeInsets.all(4),
                    child: Icon(Icons.more_horiz, color: Colors.grey, size: 25),
                  ),
                ),
              ],
            )
          else
            Row(
              mainAxisAlignment: MainAxisAlignment.spaceEvenly,
              children: [
                Expanded(
                  child: Container(
                    decoration: const BoxDecoration(
                      border: Border(bottom: BorderSide(width: 2)),
                    ),
                    child: const Padding(
                      padding: EdgeInsets.all(8.0),
                      child: Row(
                        mainAxisAlignment: MainAxisAlignment.center,
                        children: [
                          Icon(Icons.window_outlined),
                          Text('Post', style: TextStyle(fontWeight: FontWeight.bold)),
                        ],
                      ),
                    ),
                  ),
                ),
                Expanded(
                  child: Container(
                    decoration: const BoxDecoration(
                      border: Border(
                        bottom: BorderSide(width: 2, color: Color.fromARGB(255, 233, 233, 233)),
                      ),
                    ),
                    child: const Padding(
                      padding: EdgeInsets.all(8.0),
                      child: Row(
                        mainAxisAlignment: MainAxisAlignment.center,
                        children: [
                          Icon(Icons.alternate_email, color: Colors.grey),
                          Text('Mention', style: TextStyle(fontWeight: FontWeight.bold, color: Colors.grey)),
                        ],
                      ),
                    ),
                  ),
                ),
              ],
            ),
          const SizedBox(height: 15),
          ValueListenableBuilder<Box<String>>(
            valueListenable: HiveSocialStore.settings.listenable(),
            builder: (context, settingsBox, _) {
              final userId = settingsBox.get('activeUserId', defaultValue: 'darlene')!;
              return ValueListenableBuilder<Box<SocialPost>>(
                valueListenable: HiveSocialStore.posts.listenable(),
                builder: (context, postBox, _) {
                  final posts = postBox.values.toList()
                    ..sort((a, b) => b.createdAtMillis.compareTo(a.createdAtMillis));
                  if (posts.isEmpty) {
                    return const Padding(
                      padding: EdgeInsets.all(24),
                      child: Text('Публикаций пока нет.'),
                    );
                  }
                  return ValueListenableBuilder<Box<FavoriteEntry>>(
                    valueListenable: HiveSocialStore.favorites.listenable(),
                    builder: (context, favoriteBox, _) {
                      return Column(
                        children: [
                          for (final post in posts)
                            PostItem(
                              key: ValueKey(post.id),
                              postId: post.id,
                              isLiked: favoriteBox.containsKey('$userId::${post.id}'),
                              onLikeToggle: () => HiveSocialStore.toggleFavorite(
                                userId: userId,
                                postId: post.id,
                              ),
                              images: post.imagePaths,
                              avatar: post.avatarPath,
                              name: post.authorName,
                              postedTime: _relativeTime(post.createdAt),
                              subgroup: post.subgroup,
                              text: post.text,
                            ),
                        ],
                      );
                    },
                  );
                },
              );
            },
          ),
        ],
      ),
    );
  }

  static String _relativeTime(DateTime dateTime) {
    final difference = DateTime.now().difference(dateTime);
    if (difference.inMinutes < 1) return 'только что';
    if (difference.inHours < 1) return '${difference.inMinutes} мин. назад';
    if (difference.inDays < 1) return '${difference.inHours}h ago';
    if (difference.inDays < 7) return '${difference.inDays} дн. назад';
    return '${dateTime.day.toString().padLeft(2, '0')}.${dateTime.month.toString().padLeft(2, '0')}.${dateTime.year}';
  }
}
