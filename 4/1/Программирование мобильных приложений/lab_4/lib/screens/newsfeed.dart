import 'package:flutter/material.dart';

import '../widgets/newsfeed_header.dart';
import '../widgets/myscaffold.dart';
import '../widgets/stories_feed.dart';
import '../widgets/feed.dart';

class NewsFeed extends StatelessWidget {
  const NewsFeed({super.key});

  @override
  Widget build(BuildContext context) {
    return MyScaffold(
      children: [
        NewsFeedHeader(),
        SizedBox(height: 30),
        StoriesFeed(),
        PostFeed(),
      ],
    );
  }
}
