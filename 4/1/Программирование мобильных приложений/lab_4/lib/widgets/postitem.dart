import 'package:flutter/material.dart';

import 'formatted_text.dart';

class PostItem extends StatelessWidget {
  new({
    super.key,
    required this.images,
    required this.avatar,
    required this.name,
    required this.postedTime,
    this.text = "",
    this.subgroup = "",
  });

  List<String> images;
  String avatar;
  String name;
  String postedTime;
  String text;
  String subgroup = "";

  @override
  Widget build(BuildContext context) {
    return Container(
      decoration: BoxDecoration(
        color: Colors.white,
        borderRadius: BorderRadius.circular(20),
      ),
      child: Padding(
        padding: const EdgeInsets.only(top: 20, left: 15, right: 15),
        child: Column(
          crossAxisAlignment: .start,
          children: [
            Row(
              children: [
                Padding(
                  padding: const EdgeInsets.only(right: 10.0),
                  child: CircleAvatar(
                    backgroundImage: AssetImage(avatar),
                    radius: 25,
                  ),
                ),
                Column(
                  crossAxisAlignment: .start,
                  children: [
                    Text(name, style: TextStyle(fontWeight: FontWeight.bold)),
                    Row(
                      children: [
                        subgroup.isNotEmpty
                            ? Text(
                                "Posted in $subgroup - ",
                                style: TextStyle(
                                  color: Colors.grey,
                                  fontWeight: FontWeight.bold,
                                ),
                              )
                            : SizedBox.shrink(),
                        Text(postedTime, style: TextStyle(color: Colors.grey)),
                      ],
                    ),
                  ],
                ),
              ],
            ),
            SizedBox(height: 15),
            FormattedText(text: text, style: TextStyle(fontSize: 16)),

            Stack(),
          ],
        ),
      ),
    );
  }
}
