import 'package:flutter/material.dart';

import 'postitem.dart';

class PostFeed extends StatelessWidget {
  const PostFeed({super.key});

  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: const EdgeInsets.only(top: 30, left: 20, right: 20),
      child: Column(
        children: [
          Row(
            mainAxisAlignment: .spaceBetween,

            children: [
              Text(
                "Recently Post",
                style: TextStyle(color: Colors.grey, fontSize: 16),
              ),
              Icon(Icons.more_horiz, color: Colors.grey, size: 25),
            ],
          ),
          SizedBox(height: 15),
          PostItem(
            images: ['assets/404.png', 'assets/404.png', 'assets/404.png'],
            avatar: 'assets/404.png',
            name: 'Nilesh',
            postedTime: '1h ago',
            subgroup: "u8s",
            text: "Discover adventure in patagonia's peaks or serenity provence's @hamlets - arrival",
          ),
          PostItem(
            images: ['assets/404.png', 'assets/404.png', 'assets/404.png'],
            avatar: 'assets/404.png',
            name: 'Nilesh',
            postedTime: '1h ago',
            subgroup: "u8s",
            text: "Discover adventure in patagonia's peaks or serenity provence's @hamlets - arrival",
          ),
        ],
      ),
    );
  }
}
