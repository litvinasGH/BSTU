import 'package:flutter/material.dart';
import 'package:lab_4/widgets/feed.dart';
import 'package:lab_4/widgets/stories_feed.dart';

import '../widgets/profileinfobox.dart';
import '../widgets/myscaffold.dart';
import '../widgets/profileheader.dart';

class ProfileScreen extends StatelessWidget {
  const new({super.key});

  @override
  Widget build(BuildContext context) {
    return MyScaffold(
      isMenuVisible: false,
      children: [
        const Profileheader(),
        const ProfileInfoBox(),
        Padding(
          padding: const EdgeInsets.only(top: 20.0),
          child: StoriesFeed(isTextOn: false),
        ),
        PostFeed(isProfileDcreen: true),
      ],
    );
  }
}
