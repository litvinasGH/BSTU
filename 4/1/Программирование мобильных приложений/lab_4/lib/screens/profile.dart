import 'package:flutter/material.dart';
import 'package:lab_4/widgets/feed.dart';
import 'package:lab_4/widgets/stories_feed.dart';

import '../widgets/profileinfobox.dart';
import '../widgets/myscaffold.dart';
import '../widgets/profileheader.dart';

class ProfileScreen extends StatelessWidget {
  new({super.key, this.image = "", this.name = "no_name"});

  String name = "no_name";
  String image = "";

  @override
  Widget build(BuildContext context) {
    if (image == "") {
      final args =
          ModalRoute.of(context)!.settings.arguments as Map<String, String>;
      name = args['name'] ?? name;
      image = args['image'] ?? image;
    }

    return MyScaffold(
      isMenuVisible: false,
      children: [
        const Profileheader(),
        ProfileInfoBox(image: image, name: name),
        Padding(
          padding: const EdgeInsets.only(top: 20.0),
          child: StoriesFeed(isTextOn: false),
        ),
        PostFeed(isProfileDcreen: true),
      ],
    );
  }
}
