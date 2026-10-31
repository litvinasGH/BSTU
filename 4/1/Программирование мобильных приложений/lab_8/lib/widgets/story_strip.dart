import 'package:flutter/material.dart';

import '../models/social_models.dart';
import 'path_image.dart';

class StoryStrip extends StatelessWidget {
  const StoryStrip({super.key, required this.stories, required this.currentUser});

  final List<SocialStory> stories;
  final SocialUser currentUser;

  @override
  Widget build(BuildContext context) {
    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: [
        const Padding(
          padding: EdgeInsets.only(left: 2, bottom: 12),
          child: Row(
            children: [
              Text('Discover', style: TextStyle(fontSize: 22, fontWeight: FontWeight.w800)),
              SizedBox(width: 18),
              Text('Following', style: TextStyle(fontSize: 22, color: Colors.grey)),
            ],
          ),
        ),
        SizedBox(
          height: 132,
          child: ListView(
            scrollDirection: Axis.horizontal,
            children: [
              _AddStoryCard(currentUser: currentUser),
              for (final story in stories)
                Padding(
                  padding: const EdgeInsets.only(right: 10),
                  child: InkWell(
                    borderRadius: BorderRadius.circular(17),
                    onTap: () => _showStory(context, story),
                    child: SizedBox(
                      width: 100,
                      child: Stack(
                        fit: StackFit.expand,
                        children: [
                          PathImage(
                            path: story.imagePath,
                            borderRadius: BorderRadius.circular(17),
                            icon: Icons.photo_camera_outlined,
                          ),
                          DecoratedBox(
                            decoration: BoxDecoration(
                              borderRadius: BorderRadius.circular(17),
                              gradient: const LinearGradient(
                                begin: Alignment.topCenter,
                                end: Alignment.bottomCenter,
                                colors: [Colors.black26, Colors.transparent, Colors.black54],
                              ),
                            ),
                          ),
                          Positioned(
                            top: 8,
                            left: 8,
                            child: Container(
                              padding: const EdgeInsets.all(2),
                              decoration: const BoxDecoration(color: Colors.white, shape: BoxShape.circle),
                              child: UserAvatar(imagePath: story.avatarPath, name: story.userName, radius: 14),
                            ),
                          ),
                          Positioned(
                            left: 8,
                            right: 8,
                            bottom: 8,
                            child: Text(
                              story.userName,
                              maxLines: 2,
                              overflow: TextOverflow.ellipsis,
                              style: const TextStyle(color: Colors.white, fontSize: 12, fontWeight: FontWeight.w700),
                            ),
                          ),
                        ],
                      ),
                    ),
                  ),
                ),
            ],
          ),
        ),
      ],
    );
  }

  void _showStory(BuildContext context, SocialStory story) {
    showDialog<void>(
      context: context,
      builder: (context) => Dialog(
        insetPadding: const EdgeInsets.all(20),
        clipBehavior: Clip.antiAlias,
        child: SizedBox(
          height: 500,
          child: Stack(
            fit: StackFit.expand,
            children: [
              PathImage(path: story.imagePath),
              DecoratedBox(
                decoration: BoxDecoration(
                  gradient: LinearGradient(
                    begin: Alignment.topCenter,
                    end: Alignment.bottomCenter,
                    colors: [Colors.black.withValues(alpha: 0.5), Colors.transparent, Colors.black45],
                  ),
                ),
              ),
              Positioned(
                top: 16,
                left: 16,
                child: Row(
                  children: [
                    UserAvatar(imagePath: story.avatarPath, name: story.userName, radius: 20),
                    const SizedBox(width: 10),
                    Text(story.userName, style: const TextStyle(color: Colors.white, fontWeight: FontWeight.bold)),
                  ],
                ),
              ),
              Positioned(
                top: 8,
                right: 8,
                child: IconButton(
                  onPressed: () => Navigator.pop(context),
                  icon: const Icon(Icons.close, color: Colors.white),
                ),
              ),
            ],
          ),
        ),
      ),
    );
  }
}

class _AddStoryCard extends StatelessWidget {
  const _AddStoryCard({required this.currentUser});
  final SocialUser currentUser;

  @override
  Widget build(BuildContext context) {
    return Container(
      width: 100,
      margin: const EdgeInsets.only(right: 10),
      decoration: BoxDecoration(
        color: Colors.white,
        borderRadius: BorderRadius.circular(17),
        border: Border.all(color: const Color(0xFFE0F2ED)),
      ),
      child: Column(
        children: [
          Expanded(
            child: Stack(
              alignment: Alignment.center,
              children: [
                UserAvatar(imagePath: currentUser.avatarPath, name: currentUser.name, radius: 31),
                Positioned(
                  bottom: 5,
                  child: Container(
                    decoration: BoxDecoration(
                      color: const Color(0xFF67E9D0),
                      borderRadius: BorderRadius.circular(12),
                      border: Border.all(color: Colors.white, width: 2),
                    ),
                    padding: const EdgeInsets.all(3),
                    child: const Icon(Icons.add, size: 16, color: Color(0xFF27574E)),
                  ),
                ),
              ],
            ),
          ),
          Padding(
            padding: const EdgeInsets.only(bottom: 8),
            child: Text('Your Story', style: TextStyle(color: Colors.grey.shade700, fontSize: 12)),
          ),
        ],
      ),
    );
  }
}
