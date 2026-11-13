import 'package:flutter/material.dart';
import 'package:provider/provider.dart';

import '../models/social_models.dart';
import '../providers/social_provider.dart';
import 'favorite_posts_screen.dart';
import 'hive_diagnostics_screen.dart';
import 'post_editor_screen.dart';

/// Дополнительные действия лабораторной №8.
/// Все общие данные экран получает из SocialProvider.
class SocialManagementScreen extends StatelessWidget {
  const SocialManagementScreen({super.key});

  Future<void> _switchUser(BuildContext context, String? userId) async {
    if (userId == null) return;
    await context.read<SocialProvider>().setActiveUser(userId);
  }

  Future<void> _createPost(BuildContext context) async {
    final actor = context.read<SocialProvider>().activeUser;
    if (!actor.canManagePosts) {
      _showMessage(context, 'Для этой роли создание публикаций запрещено.');
      return;
    }

    final post = await Navigator.of(context).push<SocialPost>(
      MaterialPageRoute(builder: (_) => PostEditorScreen(actor: actor)),
    );
    if (post == null || !context.mounted) return;
    try {
      await context.read<SocialProvider>().savePost(
        actor: context.read<SocialProvider>().activeUser,
        post: post,
      );
      if (context.mounted) _showMessage(context, 'Публикация сохранена.');
    } catch (error) {
      if (context.mounted) _showMessage(context, error.toString());
    }
  }

  Future<void> _editPost(BuildContext context, SocialPost post) async {
    final actor = context.read<SocialProvider>().activeUser;
    if (!actor.canManagePosts) {
      _showMessage(
        context,
        'Только администратор или менеджер может редактировать публикации.',
      );
      return;
    }
    final changed = await Navigator.of(context).push<SocialPost>(
      MaterialPageRoute(
        builder: (_) => PostEditorScreen(actor: actor, existingPost: post),
      ),
    );
    if (changed == null || !context.mounted) return;
    try {
      await context.read<SocialProvider>().savePost(
        actor: context.read<SocialProvider>().activeUser,
        post: changed,
      );
      if (context.mounted) _showMessage(context, 'Изменения сохранены.');
    } catch (error) {
      if (context.mounted) _showMessage(context, error.toString());
    }
  }

  Future<void> _deletePost(BuildContext context, SocialPost post) async {
    final actor = context.read<SocialProvider>().activeUser;
    if (!actor.canManagePosts) {
      _showMessage(
        context,
        'Только администратор или менеджер может удалять публикации.',
      );
      return;
    }
    final confirmed = await showDialog<bool>(
      context: context,
      builder: (dialogContext) => AlertDialog(
        title: const Text('Удалить публикацию?'),
        content: Text('Публикация ${post.authorName} будет удалена из Hive.'),
        actions: [
          TextButton(
            onPressed: () => Navigator.pop(dialogContext, false),
            child: const Text('Отмена'),
          ),
          FilledButton(
            onPressed: () => Navigator.pop(dialogContext, true),
            child: const Text('Удалить'),
          ),
        ],
      ),
    );
    if (confirmed != true || !context.mounted) return;
    try {
      await context.read<SocialProvider>().deletePost(
        actor: context.read<SocialProvider>().activeUser,
        postId: post.id,
      );
      if (context.mounted) _showMessage(context, 'Публикация удалена.');
    } catch (error) {
      if (context.mounted) _showMessage(context, error.toString());
    }
  }

  void _showMessage(BuildContext context, String message) {
    ScaffoldMessenger.of(context)
        .showSnackBar(SnackBar(content: Text(message)));
  }

  @override
  Widget build(BuildContext context) {
    final social = context.watch<SocialProvider>();
    final activeUser = social.activeUser;
    final posts = social.posts
      ..sort((a, b) => b.createdAtMillis.compareTo(a.createdAtMillis));

    return Scaffold(
      appBar: AppBar(
        title: const Text('Управление'),
        actions: [
          if (activeUser.canManagePosts)
            IconButton(
              tooltip: 'Добавить публикацию',
              onPressed: () => _createPost(context),
              icon: const Icon(Icons.add),
            ),
        ],
      ),
      body: ListView(
        padding: const EdgeInsets.all(16),
        children: [
          const Text(
            'Текущий пользователь',
            style: TextStyle(fontSize: 18, fontWeight: FontWeight.bold),
          ),
          const SizedBox(height: 8),
          DropdownButtonFormField<String>(
            value: activeUser.id,
            decoration: const InputDecoration(
              border: OutlineInputBorder(),
              labelText: 'Пользователь и роль',
            ),
            items: social.allUsers.map((user) {
              return DropdownMenuItem<String>(
                value: user.id,
                child: Text('${user.name} — ${user.roleLabel}'),
              );
            }).toList(),
            onChanged: (id) => _switchUser(context, id),
          ),
          const SizedBox(height: 8),
          Text(
            activeUser.canManagePosts
                ? 'Эта роль может создавать, редактировать и удалять публикации.'
                : 'Эта роль может просматривать публикации и управлять собственным избранным.',
            style: const TextStyle(color: Colors.blueGrey),
          ),
          const SizedBox(height: 18),
          OutlinedButton.icon(
            onPressed: () => Navigator.of(context).push(
              MaterialPageRoute(builder: (_) => const FavoritePostsScreen()),
            ),
            icon: const Icon(Icons.favorite_border),
            label: const Text('Открыть избранное'),
          ),
          OutlinedButton.icon(
            onPressed: () => Navigator.of(context).push(
              MaterialPageRoute(builder: (_) => const HiveDiagnosticsScreen()),
            ),
            icon: const Icon(Icons.security),
            label: const Text('Проверки Hive и шифрования'),
          ),
          const SizedBox(height: 22),
          Row(
            children: [
              const Expanded(
                child: Text(
                  'Публикации',
                  style: TextStyle(fontSize: 18, fontWeight: FontWeight.bold),
                ),
              ),
              Text(
                '${posts.length}',
                style: const TextStyle(color: Colors.grey),
              ),
            ],
          ),
          const SizedBox(height: 8),
          if (posts.isEmpty)
            const ListTile(title: Text('Нет публикаций'))
          else
            for (final post in posts)
              Card(
                child: ListTile(
                  leading: const Icon(Icons.article_outlined),
                  title: Text(
                    post.authorName,
                    maxLines: 1,
                    overflow: TextOverflow.ellipsis,
                  ),
                  subtitle: Text(
                    post.text.isEmpty
                        ? 'Публикация с изображениями'
                        : post.text,
                    maxLines: 2,
                    overflow: TextOverflow.ellipsis,
                  ),
                  trailing: activeUser.canManagePosts
                      ? Wrap(
                          spacing: 0,
                          children: [
                            IconButton(
                              tooltip: 'Редактировать',
                              onPressed: () => _editPost(context, post),
                              icon: const Icon(Icons.edit_outlined),
                            ),
                            IconButton(
                              tooltip: 'Удалить',
                              onPressed: () => _deletePost(context, post),
                              icon: const Icon(Icons.delete_outline),
                            ),
                          ],
                        )
                      : null,
                ),
              ),
        ],
      ),
    );
  }
}
