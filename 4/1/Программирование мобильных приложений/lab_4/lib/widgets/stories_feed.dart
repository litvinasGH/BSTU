import 'package:flutter/material.dart';

import 'story.dart';

class StoriesFeed extends StatelessWidget {
  StoriesFeed({super.key, this.isTextOn = true});

  bool isTextOn = true;

  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: const EdgeInsets.only(top: 0, left: 20),
      child: Column(
        children: [
          if (isTextOn)
            Row(
              children: [
                Text(
                  "Discover",
                  style: TextStyle(fontWeight: FontWeight.bold, fontSize: 24),
                ),
                SizedBox(width: 20),
                Text(
                  "Following",
                  style: TextStyle(color: Colors.grey, fontSize: 24),
                ),
              ],
            ),
          SizedBox(height: 10),
          SizedBox(
            height: 120,
            child: ListView(
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
                                offset: Offset(0, -2),
                                child: CircleAvatar(
                                  backgroundImage: AssetImage(
                                    'assets/mainavatar.jpg',
                                  ),
                                  radius: 30,
                                ),
                              ),
                            ),
                            Center(
                              child: Transform.translate(
                                offset: Offset(0, 25),
                                child: Container(
                                  decoration: BoxDecoration(
                                    borderRadius: BorderRadius.circular(15),
                                    color: Colors.cyanAccent,
                                    border: Border.all(
                                      color: Colors.white,
                                      width: 2,
                                    ),
                                  ),
                                  child: Icon(
                                    Icons.add,
                                    size: 20,
                                    color: Colors.grey,
                                  ),
                                ),
                              ),
                            ),
                          ],
                        ),
                      ),
                      Text(
                        isTextOn ? "You Story" : "Add Story",
                        style: TextStyle(color: Colors.grey),
                      ),
                    ],
                  ),
                ),
                for (int i = 0; i < 5; i++)
                  Story(
                    image1: 'assets/postsAndStor/${i}.jpg',
                    litimage: 'assets/splitanimage/img_${i + 1}.png',
                    isAvatarOn: isTextOn,
                  ),
              ],
            ),
          ),
        ],
      ),
    );
  }
}
