import 'package:flutter/material.dart';

import '../models/social_models.dart';

class PostEditorScreen extends StatefulWidget {
  const PostEditorScreen({super.key, required this.actor, this.existingPost});

  final SocialUser actor;
  final SocialPost? existingPost;

  @override
  State<PostEditorScreen> createState() => _PostEditorScreenState();
}

class _PostEditorScreenState extends State<PostEditorScreen> {
  final _formKey = GlobalKey<FormState>();
  late final TextEditingController _textController;
  late final TextEditingController _groupController;
  late final TextEditingController _imagesController;

  bool get _isEditing => widget.existingPost != null;

  @override
  void initState() {
    super.initState();
    final existing = widget.existingPost;
    _textController = TextEditingController(text: existing?.text ?? '');
    _groupController = TextEditingController(text: existing?.subgroup ?? '');
    _imagesController = TextEditingController(text: existing?.imagePaths.join('\n') ?? '');
  }

  @override
  void dispose() {
    _textController.dispose();
    _groupController.dispose();
    _imagesController.dispose();
    super.dispose();
  }

  void _save() {
    if (!_formKey.currentState!.validate()) return;
    final existing = widget.existingPost;
    final imagePaths = _imagesController.text
        .split(RegExp(r'[\n,;]+'))
        .map((value) => value.trim())
        .where((value) => value.isNotEmpty)
        .toList();

    final post = existing == null
        ? SocialPost(
            id: 'post_${DateTime.now().microsecondsSinceEpoch}',
            authorId: widget.actor.id,
            authorName: widget.actor.name,
            avatarPath: widget.actor.avatarPath,
            createdAtMillis: DateTime.now().millisecondsSinceEpoch,
            subgroup: _groupController.text.trim(),
            text: _textController.text.trim(),
            imagePaths: imagePaths,
          )
        : existing.copyWith(
            subgroup: _groupController.text.trim(),
            text: _textController.text.trim(),
            imagePaths: imagePaths,
          );

    Navigator.of(context).pop(post);
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text(_isEditing ? 'Редактировать публикацию' : 'Новая публикация'),
        actions: [
          TextButton.icon(
            onPressed: _save,
            icon: const Icon(Icons.check),
            label: const Text('Сохранить'),
          ),
          const SizedBox(width: 8),
        ],
      ),
      body: ListView(
        padding: const EdgeInsets.all(18),
        children: [
          Container(
            padding: const EdgeInsets.all(16),
            decoration: BoxDecoration(color: Colors.white, borderRadius: BorderRadius.circular(20)),
            child: Row(
              children: [
                CircleAvatar(
                  backgroundColor: const Color(0xFFB4F4E7),
                  child: Text(widget.actor.name.isEmpty ? '?' : widget.actor.name[0]),
                ),
                const SizedBox(width: 12),
                Expanded(
                  child: Column(
                    crossAxisAlignment: CrossAxisAlignment.start,
                    children: [
                      Text(_isEditing ? widget.existingPost!.authorName : widget.actor.name,
                          style: const TextStyle(fontWeight: FontWeight.w800)),
                      const SizedBox(height: 3),
                      Text(widget.actor.roleLabel, style: const TextStyle(color: Colors.grey, fontSize: 12)),
                    ],
                  ),
                ),
                const Icon(Icons.verified_user_outlined, color: Color(0xFF249C88)),
              ],
            ),
          ),
          const SizedBox(height: 18),
          Form(
            key: _formKey,
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              children: [
                const Text('Текст публикации', style: TextStyle(fontWeight: FontWeight.w700)),
                const SizedBox(height: 8),
                TextFormField(
                  controller: _textController,
                  minLines: 4,
                  maxLines: 8,
                  maxLength: 2000,
                  decoration: const InputDecoration(
                    hintText: 'Чем хочешь поделиться?',
                    alignLabelWithHint: true,
                  ),
                  validator: (value) {
                    if ((value ?? '').trim().isEmpty && _imagesController.text.trim().isEmpty) {
                      return 'Добавь текст или путь хотя бы к одному изображению';
                    }
                    return null;
                  },
                ),
                const SizedBox(height: 14),
                const Text('Группа / сообщество', style: TextStyle(fontWeight: FontWeight.w700)),
                const SizedBox(height: 8),
                TextFormField(
                  controller: _groupController,
                  maxLength: 80,
                  decoration: const InputDecoration(hintText: 'Например, The Ultra Tribe'),
                ),
                const SizedBox(height: 8),
                const Text('Пути к изображениям', style: TextStyle(fontWeight: FontWeight.w700)),
                const SizedBox(height: 8),
                TextFormField(
                  controller: _imagesController,
                  minLines: 2,
                  maxLines: 5,
                  decoration: const InputDecoration(
                    hintText: 'assets/postsAndStor/6.jpg\nКаждый путь — с новой строки',
                    alignLabelWithHint: true,
                  ),
                ),
                const SizedBox(height: 8),
                const Text(
                  'В этой лабораторной изображения сохраняются как пути, а сами публикации — в зашифрованном Hive Box.',
                  style: TextStyle(color: Colors.blueGrey, fontSize: 12, height: 1.4),
                ),
                const SizedBox(height: 22),
                SizedBox(
                  width: double.infinity,
                  child: FilledButton.icon(
                    onPressed: _save,
                    icon: const Icon(Icons.save_outlined),
                    label: Text(_isEditing ? 'Сохранить изменения' : 'Создать публикацию'),
                    style: FilledButton.styleFrom(padding: const EdgeInsets.symmetric(vertical: 15)),
                  ),
                ),
              ],
            ),
          ),
        ],
      ),
    );
  }
}
