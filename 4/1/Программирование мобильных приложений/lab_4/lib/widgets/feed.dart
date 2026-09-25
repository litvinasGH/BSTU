import 'package:flutter/material.dart';

import 'postitem.dart';

class PostFeed extends StatelessWidget {
  PostFeed({super.key, this.isProfileDcreen = false});

  bool isProfileDcreen = false;

  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: const EdgeInsets.only(top: 30, left: 20, right: 20),
      child: Column(
        children: [
          if (!isProfileDcreen)
            Row(
              mainAxisAlignment: .spaceBetween,

              children: [
                Text(
                  "Recently Post",
                  style: TextStyle(color: Colors.grey, fontSize: 16),
                ),
                Icon(Icons.more_horiz, color: Colors.grey, size: 25),
              ],
            )
          else
            Row(
              mainAxisAlignment: .spaceEvenly,
              children: [
                Expanded(
                  child: Container(
                    decoration: BoxDecoration(
                      border: Border(bottom: BorderSide(width: 2)),
                    ),
                    child: Padding(
                      padding: const EdgeInsets.all(8.0),
                      child: Row(
                        mainAxisAlignment: .center,
                        children: [
                          Icon(Icons.window_outlined),
                          Text(
                            "Post",
                            style: TextStyle(fontWeight: FontWeight.bold),
                          ),
                        ],
                      ),
                    ),
                  ),
                ),
                Expanded(
                  child: Container(
                    decoration: BoxDecoration(
                      border: Border(
                        bottom: BorderSide(
                          width: 2,
                          color: const Color.fromARGB(255, 233, 233, 233),
                        ),
                      ),
                    ),
                    child: Padding(
                      padding: const EdgeInsets.all(8.0),
                      child: Row(
                        mainAxisAlignment: .center,
                        children: [
                          Icon(Icons.alternate_email, color: Colors.grey),
                          Text(
                            "Mention",
                            style: TextStyle(
                              fontWeight: FontWeight.bold,
                              color: Colors.grey,
                            ),
                          ),
                        ],
                      ),
                    ),
                  ),
                ),
              ],
            ),
          SizedBox(height: 15),
          PostItem(
            images: [
              'assets/postsAndStor/6.jpg',
              'assets/postsAndStor/7.jpg',
              'assets/postsAndStor/5.jpg',
            ],
            avatar: 'assets/postsAndStor/5.jpg',
            name: 'Nilesh',
            postedTime: '1h ago',
            subgroup: "u8s",
            text: "Discover adventure in patagonia's peaks or serenity provence's @hamlets - arrival",
          ),
          PostItem(
            images: [
              'assets/postsAndStor/8.jpg',
              'assets/postsAndStor/9.jpg',
              'assets/postsAndStor/10.jpg',
            ],
            avatar: 'assets/postsAndStor/3.jpg',
            name: 'Ultra Runner',
            postedTime: '2h ago',
            subgroup: "The Ultra Tribe",
            text:
                "The elements, the terrain, and the miles. "
                "The full spectrum of extreme running in just a few frames. "
                "Which shot captures the vibe best for you? 👇 "
                "@the_ultra_tribe #XMarathon #RunnerLife #GritAndMiles #AthletesOfInstagram",
          ),
        ],
      ),
    );
  }
}
