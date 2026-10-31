import 'package:flutter/material.dart';
import 'package:hive_flutter/hive_flutter.dart';

import '../data/hive_social_store.dart';
import '../models/social_models.dart';
import 'story.dart';

class StoriesFeed extends StatelessWidget {
  const StoriesFeed({super.key, this.isTextOn = true});

  final bool isTextOn;

  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: const EdgeInsets.only(top: 0, left: 20),
      child: Column(
        children: [
          if (isTextOn)
            const Row(
              children: [
                Text('Discover', style: TextStyle(fontWeight: FontWeight.bold, fontSize: 24)),
                SizedBox(width: 20),
                Text('Following', style: TextStyle(color: Colors.grey, fontSize: 24)),
              ],
            ),
          const SizedBox(height: 10),
          SizedBox(
            height: 120,
            child: ValueListenableBuilder<Box<SocialStory>>(
              valueListenable: HiveSocialStore.stories.listenable(),
              builder: (context, storyBox, _) {
                final stories = storyBox.values.toList()
                  ..sort((a, b) => b.createdAtMillis.compareTo(a.createdAtMillis));
                return ListView(
                  scrollDirection: Axis.horizontal,
                  children: [
                    Container(
                      decoration: BoxDecoration(
                        color: Colors.white,
                        borderRadius: BorderRadius.circular(15),
                      ),
                      width: 105,
                      child: Column(
                        children: [
                          Expanded(
                            child: Stack(
                              children: [
                                const SizedBox.expand(),
                                Center(
                                  child: Transform.translate(
                                    offset: const Offset(0, -2),
                                    child: const CircleAvatar(
                                      backgroundImage: AssetImage('assets/mainavatar.jpg'),
                                      radius: 30,
                                    ),
                                  ),
                                ),
                                Center(
                                  child: Transform.translate(
                                    offset: const Offset(0, 25),
                                    child: Container(
                                      decoration: BoxDecoration(
                                        borderRadius: BorderRadius.circular(15),
                                        color: Colors.cyanAccent,
                                        border: Border.all(color: Colors.white, width: 2),
                                      ),
                                      child: const Icon(Icons.add, size: 20, color: Colors.grey),
                                    ),
                                  ),
                                ),
                              ],
                            ),
                          ),
                          Text(isTextOn ? 'You Story' : 'Add Story', style: const TextStyle(color: Colors.grey)),
                        ],
                      ),
                    ),
                    for (final story in stories)
                      Story(
                        key: ValueKey(story.id),
                        image1: story.imagePath,
                        litimage: story.avatarPath,
                        isAvatarOn: isTextOn,
                      ),
                  ],
                );
              },
            ),
          ),
        ],
      ),
    );
  }
}
