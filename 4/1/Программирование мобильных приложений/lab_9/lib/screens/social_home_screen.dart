import 'package:flutter/material.dart';
import 'package:hive_flutter/hive_flutter.dart';

import '../data/hive_social_store.dart';
import '../models/social_models.dart';
import '../widgets/path_image.dart';
import '../widgets/social_post_card.dart';
import '../widgets/story_strip.dart';
import 'post_editor_screen.dart';

class SocialHomeScreen extends StatefulWidget {
  const SocialHomeScreen({super.key});

  @override
  State<SocialHomeScreen> createState() => _SocialHomeScreenState();
}

class _SocialHomeScreenState extends State<SocialHomeScreen> {
  int _selectedIndex = 0;
  bool _newestFirst = true;
  String _searchQuery = '';
  final TextEditingController _searchController = TextEditingController();

  SocialUser get _user => HiveSocialStore.activeUser;

  @override
  void dispose() {
    _searchController.dispose();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    final currentUser = _user;
    return Scaffold(
      appBar: AppBar(
        titleSpacing: 18,
        title: const Text(
          'socially',
          style: TextStyle(fontSize: 27, fontWeight: FontWeight.w900, letterSpacing: -1.2),
        ),
        actions: [
          PopupMenuButton<String>(
            tooltip: 'Переключить пользователя',
            onSelected: (id) async {
              await HiveSocialStore.setActiveUser(id);
              if (!mounted) return;
              setState(() {
                if (_selectedIndex == 3 && !HiveSocialStore.activeUser.canManagePosts) {
                  _selectedIndex = 0;
                }
              });
            },
            itemBuilder: (context) => HiveSocialStore.allUsers.map((user) {
              return PopupMenuItem<String>(
                value: user.id,
                child: Row(
                  children: [
                    UserAvatar(imagePath: user.avatarPath, name: user.name, radius: 17),
                    const SizedBox(width: 10),
                    Expanded(
                      child: Column(
                        crossAxisAlignment: CrossAxisAlignment.start,
                        children: [
                          Text(user.name, style: const TextStyle(fontWeight: FontWeight.w700)),
                          Text(user.roleLabel, style: const TextStyle(fontSize: 11, color: Colors.grey)),
                        ],
                      ),
                    ),
                    if (user.id == currentUser.id) const Icon(Icons.check, size: 18),
                  ],
                ),
              );
            }).toList(),
            child: Padding(
              padding: const EdgeInsets.only(right: 14, left: 12),
              child: Row(
                children: [
                  UserAvatar(imagePath: currentUser.avatarPath, name: currentUser.name, radius: 18),
                  const SizedBox(width: 7),
                  const Icon(Icons.keyboard_arrow_down, size: 20),
                ],
              ),
            ),
          ),
        ],
      ),
      body: DecoratedBox(
        decoration: const BoxDecoration(
          gradient: RadialGradient(
            center: Alignment.topCenter,
            radius: 1.2,
            colors: [Color(0xFFBFF8EC), Color(0xFFF1FFFB)],
          ),
        ),
        child: SafeArea(child: _buildPage(currentUser)),
      ),
      bottomNavigationBar: NavigationBar(
        selectedIndex: _selectedIndex,
        onDestinationSelected: (index) => setState(() => _selectedIndex = index),
        destinations: const [
          NavigationDestination(icon: Icon(Icons.home_outlined), selectedIcon: Icon(Icons.home_filled), label: 'Лента'),
          NavigationDestination(icon: Icon(Icons.favorite_border), selectedIcon: Icon(Icons.favorite), label: 'Избранное'),
          NavigationDestination(icon: Icon(Icons.person_outline), selectedIcon: Icon(Icons.person), label: 'Профиль'),
          NavigationDestination(icon: Icon(Icons.admin_panel_settings_outlined), selectedIcon: Icon(Icons.admin_panel_settings), label: 'Управление'),
        ],
      ),
    );
  }

  Widget _buildPage(SocialUser user) {
    return switch (_selectedIndex) {
      0 => _buildFeed(user),
      1 => _buildFavorites(user),
      2 => _buildProfile(user),
      _ => _buildManagement(user),
    };
  }

  Widget _buildFeed(SocialUser user) {
    return ValueListenableBuilder<Box<SocialStory>>(
      valueListenable: HiveSocialStore.stories.listenable(),
      builder: (context, storyBox, _) {
        return ValueListenableBuilder<Box<SocialPost>>(
          valueListenable: HiveSocialStore.posts.listenable(),
          builder: (context, postBox, _) {
            return ValueListenableBuilder<Box<FavoriteEntry>>(
              valueListenable: HiveSocialStore.favorites.listenable(),
              builder: (context, favoriteBox, _) {
                var visiblePosts = postBox.values.where((post) {
                  final query = _searchQuery.trim().toLowerCase();
                  if (query.isEmpty) return true;
                  return post.text.toLowerCase().contains(query) ||
                      post.authorName.toLowerCase().contains(query) ||
                      post.subgroup.toLowerCase().contains(query);
                }).toList();
                visiblePosts.sort((a, b) => _newestFirst
                    ? b.createdAtMillis.compareTo(a.createdAtMillis)
                    : a.createdAtMillis.compareTo(b.createdAtMillis));

                return ListView(
                  padding: const EdgeInsets.fromLTRB(18, 12, 18, 22),
                  children: [
                    Row(
                      children: [
                        Expanded(
                          child: Text(
                            'Добро пожаловать, ${user.name.split(' ').first}',
                            style: const TextStyle(fontSize: 21, fontWeight: FontWeight.w800),
                          ),
                        ),
                        _RoleChip(roleLabel: user.roleLabel),
                      ],
                    ),
                    const SizedBox(height: 18),
                    StoryStrip(stories: storyBox.values.toList(), currentUser: user),
                    const SizedBox(height: 20),
                    Row(
                      children: [
                        Expanded(
                          child: TextField(
                            controller: _searchController,
                            onChanged: (value) => setState(() => _searchQuery = value),
                            decoration: const InputDecoration(
                              prefixIcon: Icon(Icons.search),
                              hintText: 'Поиск по постам',
                              contentPadding: EdgeInsets.symmetric(vertical: 13),
                            ),
                          ),
                        ),
                        const SizedBox(width: 8),
                        IconButton.filledTonal(
                          tooltip: _newestFirst ? 'Сначала старые' : 'Сначала новые',
                          onPressed: () => setState(() => _newestFirst = !_newestFirst),
                          icon: Icon(_newestFirst ? Icons.south : Icons.north),
                        ),
                      ],
                    ),
                    const SizedBox(height: 19),
                    Row(
                      children: [
                        const Expanded(
                          child: Text('Recently Post', style: TextStyle(color: Colors.blueGrey, fontSize: 16)),
                        ),
                        if (user.canManagePosts)
                          FilledButton.tonalIcon(
                            onPressed: _createPost,
                            icon: const Icon(Icons.add, size: 18),
                            label: const Text('Пост'),
                          ),
                      ],
                    ),
                    const SizedBox(height: 12),
                    if (visiblePosts.isEmpty)
                      const _EmptyState(
                        icon: Icons.search_off,
                        title: 'Ничего не найдено',
                        message: 'Попробуй изменить поисковый запрос.',
                      )
                    else
                      for (final post in visiblePosts)
                        SocialPostCard(
                          post: post,
                          isFavorite: favoriteBox.containsKey('${user.id}::${post.id}'),
                          canManage: user.canManagePosts,
                          onToggleFavorite: () => _toggleFavorite(post),
                          onEdit: () => _editPost(post),
                          onDelete: () => _confirmDelete(post),
                        ),
                  ],
                );
              },
            );
          },
        );
      },
    );
  }

  Widget _buildFavorites(SocialUser user) {
    return ValueListenableBuilder<Box<FavoriteEntry>>(
      valueListenable: HiveSocialStore.favorites.listenable(),
      builder: (context, favoriteBox, _) {
        return ValueListenableBuilder<Box<SocialPost>>(
          valueListenable: HiveSocialStore.posts.listenable(),
          builder: (context, postBox, _) {
            final ids = favoriteBox.values
                .where((favorite) => favorite.userId == user.id)
                .map((favorite) => favorite.postId)
                .toSet();
            final posts = postBox.values.where((post) => ids.contains(post.id)).toList()
              ..sort((a, b) => b.createdAtMillis.compareTo(a.createdAtMillis));
            return ListView(
              padding: const EdgeInsets.all(18),
              children: [
                const Text('Избранное', style: TextStyle(fontSize: 25, fontWeight: FontWeight.w900)),
                const SizedBox(height: 6),
                Text('Список публикаций пользователя ${user.name}', style: const TextStyle(color: Colors.blueGrey)),
                const SizedBox(height: 18),
                if (posts.isEmpty)
                  const _EmptyState(
                    icon: Icons.favorite_border,
                    title: 'Пока ничего нет',
                    message: 'Добавляй понравившиеся публикации в избранное из ленты.',
                  )
                else
                  for (final post in posts)
                    SocialPostCard(
                      post: post,
                      isFavorite: true,
                      canManage: user.canManagePosts,
                      onToggleFavorite: () => _toggleFavorite(post),
                      onEdit: () => _editPost(post),
                      onDelete: () => _confirmDelete(post),
                    ),
              ],
            );
          },
        );
      },
    );
  }

  Widget _buildProfile(SocialUser user) {
    return ValueListenableBuilder<Box<FavoriteEntry>>(
      valueListenable: HiveSocialStore.favorites.listenable(),
      builder: (context, favoriteBox, _) {
        return ValueListenableBuilder<Box<SocialPost>>(
          valueListenable: HiveSocialStore.posts.listenable(),
          builder: (context, postBox, _) {
            final userPosts = postBox.values.where((post) => post.authorId == user.id).toList()
              ..sort((a, b) => b.createdAtMillis.compareTo(a.createdAtMillis));
            final favoriteCount = favoriteBox.values.where((favorite) => favorite.userId == user.id).length;
            return ListView(
              padding: const EdgeInsets.all(18),
              children: [
                Container(
                  padding: const EdgeInsets.fromLTRB(20, 24, 20, 22),
                  decoration: BoxDecoration(color: Colors.white, borderRadius: BorderRadius.circular(26)),
                  child: Column(
                    children: [
                      UserAvatar(imagePath: user.avatarPath, name: user.name, radius: 54),
                      const SizedBox(height: 14),
                      Text(user.name, style: const TextStyle(fontSize: 24, fontWeight: FontWeight.w900)),
                      Text('@${user.handle}', style: const TextStyle(color: Colors.blueGrey)),
                      const SizedBox(height: 8),
                      _RoleChip(roleLabel: user.roleLabel),
                      if (user.bio.isNotEmpty) ...[
                        const SizedBox(height: 12),
                        Text(user.bio, textAlign: TextAlign.center, style: const TextStyle(height: 1.35)),
                      ],
                      const SizedBox(height: 20),
                      Row(
                        mainAxisAlignment: MainAxisAlignment.spaceAround,
                        children: [
                          _ProfileStat(value: '${userPosts.length}', label: 'Посты'),
                          _ProfileStat(value: _compactCount(user.followers), label: 'Подписчики'),
                          _ProfileStat(value: _compactCount(user.following), label: 'Подписки'),
                          _ProfileStat(value: '$favoriteCount', label: 'Избранное'),
                        ],
                      ),
                    ],
                  ),
                ),
                const SizedBox(height: 20),
                const Text('Публикации', style: TextStyle(fontSize: 20, fontWeight: FontWeight.w800)),
                const SizedBox(height: 12),
                if (userPosts.isEmpty)
                  const _EmptyState(
                    icon: Icons.photo_library_outlined,
                    title: 'Нет публикаций',
                    message: 'Публикации этого пользователя появятся здесь.',
                  )
                else
                  for (final post in userPosts)
                    SocialPostCard(
                      post: post,
                      isFavorite: favoriteBox.containsKey('${user.id}::${post.id}'),
                      canManage: user.canManagePosts,
                      onToggleFavorite: () => _toggleFavorite(post),
                      onEdit: () => _editPost(post),
                      onDelete: () => _confirmDelete(post),
                    ),
              ],
            );
          },
        );
      },
    );
  }

  Widget _buildManagement(SocialUser user) {
    if (!user.canManagePosts) {
      return const Center(
        child: Padding(
          padding: EdgeInsets.all(28),
          child: _EmptyState(
            icon: Icons.lock_outline,
            title: 'Нет доступа',
            message: 'Создавать, редактировать и удалять публикации могут только администратор и менеджер. Переключи пользователя в правом верхнем углу, чтобы проверить роли.',
          ),
        ),
      );
    }

    return ValueListenableBuilder<Box<FavoriteEntry>>(
      valueListenable: HiveSocialStore.favorites.listenable(),
      builder: (context, favoriteBox, _) {
        return ValueListenableBuilder<Box<SocialPost>>(
          valueListenable: HiveSocialStore.posts.listenable(),
          builder: (context, postBox, _) {
            return ValueListenableBuilder<Box<SocialUser>>(
              valueListenable: HiveSocialStore.users.listenable(),
              builder: (context, userBox, _) {
                return ValueListenableBuilder<Box<SocialStory>>(
                  valueListenable: HiveSocialStore.stories.listenable(),
                  builder: (context, storyBox, _) {
                    final sortedPosts = postBox.values.toList()
                      ..sort((a, b) => b.createdAtMillis.compareTo(a.createdAtMillis));
                    return ListView(
                      padding: const EdgeInsets.all(18),
                      children: [
                        const Text('Управление', style: TextStyle(fontSize: 25, fontWeight: FontWeight.w900)),
                        const SizedBox(height: 6),
                        Text('Вход выполнен как ${user.name} · ${user.roleLabel}', style: const TextStyle(color: Colors.blueGrey)),
                        const SizedBox(height: 16),
                        Wrap(
                          spacing: 10,
                          runSpacing: 10,
                          children: [
                            _StatCard(label: 'Публикации', value: '${postBox.length}', icon: Icons.article_outlined),
                            _StatCard(label: 'Пользователи', value: '${userBox.length}', icon: Icons.people_outline),
                            _StatCard(label: 'Истории', value: '${storyBox.length}', icon: Icons.auto_awesome_outlined),
                            _StatCard(label: 'Избранное', value: '${favoriteBox.length}', icon: Icons.favorite_border),
                          ],
                        ),
                        const SizedBox(height: 20),
                        FilledButton.icon(
                          onPressed: _createPost,
                          icon: const Icon(Icons.add_circle_outline),
                          label: const Text('Создать публикацию'),
                          style: FilledButton.styleFrom(padding: const EdgeInsets.symmetric(vertical: 14)),
                        ),
                        const SizedBox(height: 20),
                        Container(
                          padding: const EdgeInsets.all(16),
                          decoration: BoxDecoration(color: Colors.white, borderRadius: BorderRadius.circular(22)),
                          child: Column(
                            crossAxisAlignment: CrossAxisAlignment.start,
                            children: [
                              const Text('Обслуживание Hive', style: TextStyle(fontSize: 18, fontWeight: FontWeight.w800)),
                              const SizedBox(height: 5),
                              const Text(
                                'Тест использует временный Box и не изменяет основную базу. Уплотнение выполняется для основных Box.',
                                style: TextStyle(color: Colors.blueGrey, fontSize: 12, height: 1.35),
                              ),
                              const SizedBox(height: 12),
                              OutlinedButton.icon(
                                onPressed: _runWrongKeyTest,
                                icon: const Icon(Icons.key_off_outlined),
                                label: const Text('Проверить неправильный ключ'),
                              ),
                              OutlinedButton.icon(
                                onPressed: _compactBoxes,
                                icon: const Icon(Icons.compress),
                                label: const Text('Выполнить compact()'),
                              ),
                            ],
                          ),
                        ),
                        const SizedBox(height: 20),
                        Row(
                          children: [
                            const Expanded(child: Text('Все публикации', style: TextStyle(fontSize: 20, fontWeight: FontWeight.w800))),
                            Text('${postBox.length}', style: const TextStyle(color: Colors.blueGrey)),
                          ],
                        ),
                        const SizedBox(height: 12),
                        if (sortedPosts.isEmpty)
                          const _EmptyState(icon: Icons.article_outlined, title: 'Публикаций нет', message: 'Создай первую публикацию.')
                        else
                          for (final post in sortedPosts)
                            SocialPostCard(
                              post: post,
                              isFavorite: favoriteBox.containsKey('${user.id}::${post.id}'),
                              canManage: true,
                              onToggleFavorite: () => _toggleFavorite(post),
                              onEdit: () => _editPost(post),
                              onDelete: () => _confirmDelete(post),
                            ),
                      ],
                    );
                  },
                );
              },
            );
          },
        );
      },
    );
  }

  Future<void> _createPost() async {
    final actor = _user;
    if (!actor.canManagePosts) {
      _showMessage('Недостаточно прав: нужна роль администратора или менеджера.');
      return;
    }
    final post = await Navigator.of(context).push<SocialPost>(
      MaterialPageRoute(builder: (_) => PostEditorScreen(actor: actor)),
    );
    if (post == null || !mounted) return;
    try {
      await HiveSocialStore.savePost(actor: actor, post: post);
      setState(() => _selectedIndex = 0);
      _showMessage('Публикация сохранена в Hive.');
    } catch (error) {
      _showMessage(error.toString());
    }
  }

  Future<void> _editPost(SocialPost post) async {
    final actor = _user;
    if (!actor.canManagePosts) {
      _showMessage('Недостаточно прав для редактирования.');
      return;
    }
    final changedPost = await Navigator.of(context).push<SocialPost>(
      MaterialPageRoute(builder: (_) => PostEditorScreen(actor: actor, existingPost: post)),
    );
    if (changedPost == null || !mounted) return;
    try {
      await HiveSocialStore.savePost(actor: actor, post: changedPost);
      _showMessage('Изменения сохранены.');
    } catch (error) {
      _showMessage(error.toString());
    }
  }

  Future<void> _confirmDelete(SocialPost post) async {
    final actor = _user;
    if (!actor.canManagePosts) {
      _showMessage('Недостаточно прав для удаления.');
      return;
    }
    final confirmed = await showDialog<bool>(
      context: context,
      builder: (context) => AlertDialog(
        title: const Text('Удалить публикацию?'),
        content: Text('Публикация автора «${post.authorName}» будет удалена. Связанные записи избранного также удалятся.'),
        actions: [
          TextButton(onPressed: () => Navigator.pop(context, false), child: const Text('Отмена')),
          FilledButton(onPressed: () => Navigator.pop(context, true), child: const Text('Удалить')),
        ],
      ),
    );
    if (confirmed != true || !mounted) return;
    try {
      await HiveSocialStore.deletePost(actor: actor, postId: post.id);
      _showMessage('Публикация удалена.');
    } catch (error) {
      _showMessage(error.toString());
    }
  }

  Future<void> _toggleFavorite(SocialPost post) async {
    try {
      await HiveSocialStore.toggleFavorite(userId: _user.id, postId: post.id);
    } catch (error) {
      _showMessage(error.toString());
    }
  }

  Future<void> _runWrongKeyTest() async {
    _showMessage('Проверяем ключ на временном зашифрованном Box…');
    try {
      final result = await HiveSocialStore.demonstrateWrongKey();
      if (mounted) await _showResultDialog('Тест неправильного ключа', result);
    } catch (error) {
      if (mounted) await _showResultDialog('Ошибка теста', error.toString());
    }
  }

  Future<void> _compactBoxes() async {
    try {
      await HiveSocialStore.compactAllBoxes();
      _showMessage('compact() выполнен для всех Box.');
    } catch (error) {
      _showMessage('Ошибка уплотнения: $error');
    }
  }

  Future<void> _showResultDialog(String title, String message) async {
    await showDialog<void>(
      context: context,
      builder: (context) => AlertDialog(
        title: Text(title),
        content: SingleChildScrollView(child: SelectableText(message)),
        actions: [TextButton(onPressed: () => Navigator.pop(context), child: const Text('Закрыть'))],
      ),
    );
  }

  void _showMessage(String message) {
    if (!mounted) return;
    ScaffoldMessenger.of(context)
      ..hideCurrentSnackBar()
      ..showSnackBar(SnackBar(content: Text(message), behavior: SnackBarBehavior.floating));
  }

  static String _compactCount(int value) {
    if (value >= 1000000) return '${(value / 1000000).toStringAsFixed(1)}M';
    if (value >= 1000) return '${(value / 1000).toStringAsFixed(value >= 10000 ? 0 : 1)}K';
    return '$value';
  }
}

class _RoleChip extends StatelessWidget {
  const _RoleChip({required this.roleLabel});
  final String roleLabel;

  @override
  Widget build(BuildContext context) {
    return Container(
      padding: const EdgeInsets.symmetric(horizontal: 10, vertical: 6),
      decoration: BoxDecoration(color: const Color(0xFFD7FBF2), borderRadius: BorderRadius.circular(20)),
      child: Text(roleLabel, style: const TextStyle(fontSize: 11, color: Color(0xFF167766), fontWeight: FontWeight.w700)),
    );
  }
}

class _EmptyState extends StatelessWidget {
  const _EmptyState({required this.icon, required this.title, required this.message});
  final IconData icon;
  final String title;
  final String message;

  @override
  Widget build(BuildContext context) {
    return Container(
      padding: const EdgeInsets.symmetric(horizontal: 24, vertical: 28),
      decoration: BoxDecoration(color: Colors.white, borderRadius: BorderRadius.circular(24)),
      child: Column(
        children: [
          Icon(icon, size: 43, color: const Color(0xFF53BDA9)),
          const SizedBox(height: 12),
          Text(title, textAlign: TextAlign.center, style: const TextStyle(fontSize: 17, fontWeight: FontWeight.w800)),
          const SizedBox(height: 7),
          Text(message, textAlign: TextAlign.center, style: const TextStyle(color: Colors.blueGrey, height: 1.4)),
        ],
      ),
    );
  }
}

class _StatCard extends StatelessWidget {
  const _StatCard({required this.label, required this.value, required this.icon});
  final String label;
  final String value;
  final IconData icon;

  @override
  Widget build(BuildContext context) {
    return Container(
      width: 155,
      padding: const EdgeInsets.all(14),
      decoration: BoxDecoration(color: Colors.white, borderRadius: BorderRadius.circular(20)),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          Icon(icon, color: const Color(0xFF279C87)),
          const SizedBox(height: 10),
          Text(value, style: const TextStyle(fontWeight: FontWeight.w900, fontSize: 23)),
          Text(label, style: const TextStyle(color: Colors.blueGrey, fontSize: 12)),
        ],
      ),
    );
  }
}

class _ProfileStat extends StatelessWidget {
  const _ProfileStat({required this.value, required this.label});
  final String value;
  final String label;

  @override
  Widget build(BuildContext context) {
    return Column(
      children: [
        Text(value, style: const TextStyle(fontWeight: FontWeight.w900, fontSize: 18)),
        const SizedBox(height: 4),
        Text(label, style: const TextStyle(color: Colors.blueGrey, fontSize: 11)),
      ],
    );
  }
}
