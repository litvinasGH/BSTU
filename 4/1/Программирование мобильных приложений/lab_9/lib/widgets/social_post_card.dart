import 'package:flutter/material.dart';

import '../models/social_models.dart';
import 'path_image.dart';

class SocialPostCard extends StatelessWidget {
  const SocialPostCard({
    super.key,
    required this.post,
    required this.isFavorite,
    required this.canManage,
    required this.onToggleFavorite,
    this.onEdit,
    this.onDelete,
    this.showFavoriteButton = true,
  });

  final SocialPost post;
  final bool isFavorite;
  final bool canManage;
  final VoidCallback onToggleFavorite;
  final VoidCallback? onEdit;
  final VoidCallback? onDelete;
  final bool showFavoriteButton;

  @override
  Widget build(BuildContext context) {
    final images = post.imagePaths.take(3).toList();
    return Container(
      margin: const EdgeInsets.only(bottom: 16),
      padding: const EdgeInsets.all(16),
      decoration: BoxDecoration(
        color: Colors.white,
        borderRadius: BorderRadius.circular(24),
        boxShadow: [
          BoxShadow(
            color: const Color(0xFF396E63).withValues(alpha: 0.07),
            blurRadius: 16,
            offset: const Offset(0, 7),
          ),
        ],
      ),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          Row(
            children: [
              UserAvatar(imagePath: post.avatarPath, name: post.authorName, radius: 23),
              const SizedBox(width: 11),
              Expanded(
                child: Column(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    Text(post.authorName, style: const TextStyle(fontWeight: FontWeight.w800)),
                    const SizedBox(height: 3),
                    Text(
                      '${post.subgroup.isEmpty ? 'Публикация' : post.subgroup} · ${_relativeTime(post.createdAt)}',
                      style: const TextStyle(fontSize: 12, color: Colors.grey),
                      maxLines: 1,
                      overflow: TextOverflow.ellipsis,
                    ),
                  ],
                ),
              ),
              if (canManage)
                PopupMenuButton<String>(
                  tooltip: 'Действия с публикацией',
                  onSelected: (value) {
                    if (value == 'edit') onEdit?.call();
                    if (value == 'delete') onDelete?.call();
                  },
                  itemBuilder: (_) => const [
                    PopupMenuItem(value: 'edit', child: Text('Редактировать')),
                    PopupMenuItem(value: 'delete', child: Text('Удалить')),
                  ],
                  icon: const Icon(Icons.more_horiz, color: Colors.grey),
                )
              else
                const Icon(Icons.more_horiz, color: Colors.grey),
            ],
          ),
          if (post.text.trim().isNotEmpty) ...[
            const SizedBox(height: 14),
            _FormattedSocialText(text: post.text),
          ],
          if (images.isNotEmpty) ...[
            const SizedBox(height: 14),
            _ImageGallery(images: images),
          ],
          const SizedBox(height: 13),
          Row(
            children: [
              if (showFavoriteButton)
                InkWell(
                  borderRadius: BorderRadius.circular(20),
                  onTap: onToggleFavorite,
                  child: Padding(
                    padding: const EdgeInsets.symmetric(vertical: 4, horizontal: 3),
                    child: Row(
                      children: [
                        Icon(
                          isFavorite ? Icons.favorite : Icons.favorite_border,
                          color: isFavorite ? Colors.pinkAccent : Colors.blueGrey,
                          size: 23,
                        ),
                        const SizedBox(width: 5),
                        Text(isFavorite ? 'В избранном' : 'В избранное',
                            style: TextStyle(color: isFavorite ? Colors.pinkAccent : Colors.blueGrey)),
                      ],
                    ),
                  ),
                ),
              const Spacer(),
              const Icon(Icons.mode_comment_outlined, color: Colors.blueGrey, size: 20),
              const SizedBox(width: 5),
              const Text('Обсудить', style: TextStyle(color: Colors.blueGrey)),
            ],
          ),
        ],
      ),
    );
  }

  static String _relativeTime(DateTime dateTime) {
    final difference = DateTime.now().difference(dateTime);
    if (difference.inMinutes < 1) return 'только что';
    if (difference.inHours < 1) return '${difference.inMinutes} мин. назад';
    if (difference.inDays < 1) return '${difference.inHours} ч. назад';
    if (difference.inDays < 7) return '${difference.inDays} дн. назад';
    return '${dateTime.day.toString().padLeft(2, '0')}.${dateTime.month.toString().padLeft(2, '0')}.${dateTime.year}';
  }
}

class _ImageGallery extends StatelessWidget {
  const _ImageGallery({required this.images});

  final List<String> images;

  @override
  Widget build(BuildContext context) {
    if (images.length == 1) {
      return ClipRRect(
        borderRadius: BorderRadius.circular(18),
        child: PathImage(path: images.first, width: double.infinity, height: 210),
      );
    }
    if (images.length == 2) {
      return SizedBox(
        height: 180,
        child: Row(
          children: [
            Expanded(child: PathImage(path: images[0], height: 180, borderRadius: BorderRadius.circular(16))),
            const SizedBox(width: 8),
            Expanded(child: PathImage(path: images[1], height: 180, borderRadius: BorderRadius.circular(16))),
          ],
        ),
      );
    }
    return SizedBox(
      height: 190,
      child: Row(
        children: [
          Expanded(flex: 3, child: PathImage(path: images[0], height: 190, borderRadius: BorderRadius.circular(16))),
          const SizedBox(width: 8),
          Expanded(
            flex: 2,
            child: Column(
              children: [
                Expanded(child: PathImage(path: images[1], width: double.infinity, borderRadius: BorderRadius.circular(14))),
                const SizedBox(height: 8),
                Expanded(child: PathImage(path: images[2], width: double.infinity, borderRadius: BorderRadius.circular(14))),
              ],
            ),
          ),
        ],
      ),
    );
  }
}

class _FormattedSocialText extends StatelessWidget {
  const _FormattedSocialText({required this.text});
  final String text;

  @override
  Widget build(BuildContext context) {
    final pattern = RegExp(r'@[\p{L}\p{N}_]+|#[\p{L}\p{N}_]+', unicode: true);
    final spans = <TextSpan>[];
    var start = 0;
    for (final match in pattern.allMatches(text)) {
      if (match.start > start) spans.add(TextSpan(text: text.substring(start, match.start)));
      spans.add(TextSpan(
        text: match.group(0),
        style: const TextStyle(color: Color(0xFF148FCE), fontWeight: FontWeight.w600),
      ));
      start = match.end;
    }
    if (start < text.length) spans.add(TextSpan(text: text.substring(start)));
    return RichText(
      text: TextSpan(
        style: const TextStyle(color: Color(0xFF263B37), fontSize: 15, height: 1.45),
        children: spans,
      ),
    );
  }
}
