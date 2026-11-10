import 'package:flutter/material.dart';
import 'package:hive_flutter/hive_flutter.dart';

import '../data/hive_social_store.dart';
import '../models/social_models.dart';
import 'favorite_posts_screen.dart';
import 'hive_diagnostics_screen.dart';
import 'post_editor_screen.dart';

/// Все дополнительные действия ЛР 8 вынесены на отдельный экран.
/// Внешний вид исходной ленты при этом не меняется.
class SocialManagementScreen extends StatefulWidget {
  const SocialManagementScreen({super.key});

  @override
  State<SocialManagementScreen> createState() => _SocialManagementScreenState();
}

class _SocialManagementScreenState extends State<SocialManagementScreen> {
  SocialUser get _activeUser => HiveSocialStore.activeUser;

  Future<void> _switchUser(String? userId) async {
    if (userId == null) return;
    await HiveSocialStore.setActiveUser(userId);
    if (mounted) setState(() {});
  }

  Future<void> _createPost() async {
    final actor = _activeUser;
    if (!actor.canManagePosts) {
      _showMessage('Для этой роли создание публикаций запрещено.');
      return;
    }

    final post = await Navigator.of(context).push<SocialPost>(
      MaterialPageRoute(builder: (_) => PostEditorScreen(actor: actor)),
    );
    if (post == null || !mounted) return;
    try {
      await HiveSocialStore.savePost(actor: HiveSocialStore.activeUser, post: post);
      if (mounted) _showMessage('Публикация сохранена.');
    } catch (error) {
      if (mounted) _showMessage(error.toString());
    }
  }

  Future<void> _editPost(SocialPost post) async {
    final actor = _activeUser;
    if (!actor.canManagePosts) {
      _showMessage('Только администратор или менеджер может редактировать публикации.');
      return;
    }
    final changed = await Navigator.of(context).push<SocialPost>(
      MaterialPageRoute(
        builder: (_) => PostEditorScreen(actor: actor, existingPost: post),
      ),
    );
    if (changed == null || !mounted) return;
    try {
      await HiveSocialStore.savePost(actor: HiveSocialStore.activeUser, post: changed);
      if (mounted) _showMessage('Изменения сохранены.');
    } catch (error) {
      if (mounted) _showMessage(error.toString());
    }
  }

  Future<void> _deletePost(SocialPost post) async {
    final actor = _activeUser;
    if (!actor.canManagePosts) {
      _showMessage('Только администратор или менеджер может удалять публикации.');
      return;
    }
    final confirmed = await showDialog<bool>(
      context: context,
      builder: (dialogContext) => AlertDialog(
        title: const Text('Удалить публикацию?'),
        content: Text('Публикация ${post.authorName} будет удалена из Hive.'),
        actions: [
          TextButton(onPressed: () => Navigator.pop(dialogContext, false), child: const Text('Отмена')),
          FilledButton(onPressed: () => Navigator.pop(dialogContext, true), child: const Text('Удалить')),
        ],
      ),
    );
    if (confirmed != true || !mounted) return;
    try {
      await HiveSocialStore.deletePost(actor: HiveSocialStore.activeUser, postId: post.id);
      if (mounted) _showMessage('Публикация удалена.');
    } catch (error) {
      if (mounted) _showMessage(error.toString());
    }
  }

  void _showMessage(String message) {
    ScaffoldMessenger.of(context).showSnackBar(SnackBar(content: Text(message)));
  }

  @override
  Widget build(BuildContext context) {
    final activeUser = _activeUser;
    return Scaffold(
      appBar: AppBar(
        title: const Text('Управление'),
        actions: [
          if (activeUser.canManagePosts)
            IconButton(
              tooltip: 'Добавить публикацию',
              onPressed: _createPost,
              icon: const Icon(Icons.add),
            ),
        ],
      ),
      body: ListView(
        padding: const EdgeInsets.all(16),
        children: [
          const Text('Текущий пользователь', style: TextStyle(fontSize: 18, fontWeight: FontWeight.bold)),
          const SizedBox(height: 8),
          DropdownButtonFormField<String>(
            value: activeUser.id,
            decoration: const InputDecoration(border: OutlineInputBorder(), labelText: 'Пользователь и роль'),
            items: HiveSocialStore.allUsers.map((user) {
              return DropdownMenuItem<String>(
                value: user.id,
                child: Text('${user.name} — ${user.roleLabel}'),
              );
            }).toList(),
            onChanged: _switchUser,
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
                child: Text('Публикации', style: TextStyle(fontSize: 18, fontWeight: FontWeight.bold)),
              ),
              Text('${HiveSocialStore.posts.length}', style: const TextStyle(color: Colors.grey)),
            ],
          ),
          const SizedBox(height: 8),
          ValueListenableBuilder<Box<SocialPost>>(
            valueListenable: HiveSocialStore.posts.listenable(),
            builder: (context, box, _) {
              final posts = box.values.toList()
                ..sort((a, b) => b.createdAtMillis.compareTo(a.createdAtMillis));
              if (posts.isEmpty) return const ListTile(title: Text('Нет публикаций'));
              return Column(
                children: [
                  for (final post in posts)
                    Card(
                      child: ListTile(
                        leading: const Icon(Icons.article_outlined),
                        title: Text(post.authorName, maxLines: 1, overflow: TextOverflow.ellipsis),
                        subtitle: Text(post.text.isEmpty ? 'Публикация с изображениями' : post.text,
                            maxLines: 2, overflow: TextOverflow.ellipsis),
                        trailing: activeUser.canManagePosts
                            ? Wrap(
                                spacing: 0,
                                children: [
                                  IconButton(
                                    tooltip: 'Редактировать',
                                    onPressed: () => _editPost(post),
                                    icon: const Icon(Icons.edit_outlined),
                                  ),
                                  IconButton(
                                    tooltip: 'Удалить',
                                    onPressed: () => _deletePost(post),
                                    icon: const Icon(Icons.delete_outline),
                                  ),
                                ],
                              )
                            : null,
                      ),
                    ),
                ],
              );
            },
          ),
        ],
      ),
    );
  }
}
