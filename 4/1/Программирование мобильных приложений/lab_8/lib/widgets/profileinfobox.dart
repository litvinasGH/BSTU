import 'package:flutter/material.dart';
import 'package:flutter/services.dart';

class ProfileInfoBox extends StatelessWidget {
  new({super.key, required this.image, required this.name});

  String name;
  String image;

  @override
  Widget build(BuildContext context) {
    const TextStyle numstyle = TextStyle(
      fontWeight: FontWeight.bold,
      fontSize: 20,
    );
    const TextStyle undernumstyle = TextStyle(color: Colors.grey);
    return Padding(
      padding: const EdgeInsets.only(top: 50),
      child: Column(
        children: [
          CircleAvatar(backgroundImage: AssetImage(image), radius: 65),

          Padding(
            padding: EdgeInsets.only(top: 20),
            child: Text(
              name,
              style: TextStyle(fontWeight: FontWeight.bold, fontSize: 25),
            ),
          ),
          Text("@dw_beats", style: TextStyle(color: Colors.grey)),
          SizedBox(height: 45),
          Row(
            mainAxisAlignment: .spaceAround,
            children: [
              Column(
                children: [
                  Text("360", style: numstyle),
                  Text("Post", style: undernumstyle),
                ],
              ),
              Column(
                children: [
                  Text("160k", style: numstyle),
                  Text("Follower", style: undernumstyle),
                ],
              ),
              Column(
                children: [
                  Text("140k", style: numstyle),
                  Text("Following", style: undernumstyle),
                ],
              ),
            ],
          ),
        ],
      ),
    );
  }
}
