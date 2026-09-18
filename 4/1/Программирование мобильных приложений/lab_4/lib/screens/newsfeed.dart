import 'package:flutter/material.dart';

import '../widgets/story.dart';

class NewsFeed extends StatelessWidget {
  const NewsFeed({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Container(
        width: double.infinity,
        height: double.infinity,
        decoration: BoxDecoration(
          gradient: RadialGradient(
            radius: 0.9,
            center: AlignmentGeometry.topCenter,
            colors: [
              Colors.tealAccent,
              const Color.fromARGB(255, 238, 255, 250),
            ],
            transform: GradientScaleTransform(1, 0.5),
          ),
        ),
        child: SafeArea(
          child: Column(
            children: [
              // * Header
              Padding(
                padding: const EdgeInsets.only(
                  top: 20,
                  bottom: 0,
                  left: 20,
                  right: 20,
                ),
                child: Row(
                  mainAxisAlignment: MainAxisAlignment.spaceBetween,
                  children: [
                    Row(
                      children: [
                        Container(
                          width: 60,
                          decoration: BoxDecoration(
                            color: Colors.white,
                            borderRadius: BorderRadius.circular(20),
                          ),
                          child: Padding(
                            padding: EdgeInsetsGeometry.symmetric(
                              vertical: 5,
                              horizontal: 10,
                            ),
                            child: Row(
                              mainAxisAlignment: MainAxisAlignment.spaceAround,
                              children: [
                                Icon(Icons.notifications),
                                Text(
                                  "3",
                                  style: TextStyle(
                                    fontWeight: FontWeight.bold,
                                    fontSize: 20,
                                  ),
                                ),
                              ],
                            ),
                          ),
                        ),
                        SizedBox(width: 8),
                        Container(
                          decoration: BoxDecoration(
                            color: Colors.white,
                            borderRadius: BorderRadius.circular(20),
                          ),
                          child: Padding(
                            padding: EdgeInsetsGeometry.all(5),
                            child: Icon(Icons.message, color: Colors.grey),
                          ),
                        ),
                      ],
                    ),
                    Container(
                      decoration: BoxDecoration(
                        borderRadius: BorderRadius.circular(20),
                        border: Border.all(color: Colors.white, width: 3),
                      ),
                      clipBehavior: Clip.antiAlias,
                      child: ClipRRect(
                        borderRadius: BorderRadiusGeometry.circular(20),
                        child: Image.asset(
                          'assets/404.png',
                          height: 30,
                          width: 30,
                        ),
                      ),
                    ),
                  ],
                ),
              ),
              // * Header
              SizedBox(height: 30),
              // * Story
              Padding(
                padding: const EdgeInsets.only(
                  top: 20,
                  bottom: 0,
                  left: 20,
                  //right: 20,
                ),
                child: Column(
                  children: [
                    Row(
                      children: [
                        Text(
                          "Discover",
                          style: TextStyle(fontWeight: FontWeight.bold),
                        ),
                        SizedBox(width: 20),
                        Text("Following", style: TextStyle(color: Colors.grey)),
                      ],
                    ),
                    SizedBox(height: 20),
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
                                              'assets/404.png',
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
                                              borderRadius:
                                                  BorderRadius.circular(15),
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
                                  "You Story",
                                  style: TextStyle(color: Colors.grey),
                                ),
                              ],
                            ),
                          ),
                          for (int i = 0; i < 5; i++)
                            Story(
                              image1: 'assets/404.png',
                              litimage: 'assets/404.png',
                            ),
                        ],
                      ),
                    ),
                  ],
                ),
              ),
            ],
          ),
        ),
      ),
    );
  }
}

class GradientScaleTransform extends GradientTransform {
  final double scaleX;
  final double scaleY;

  const GradientScaleTransform(this.scaleX, this.scaleY);

  @override
  Matrix4? transform(Rect bounds, {TextDirection? textDirection}) {
    final topCenter = Alignment.topCenter
        .resolve(textDirection)
        .withinRect(bounds);

    return Matrix4.identity()
      ..translate(topCenter.dx, topCenter.dy)
      ..scale(scaleX, scaleY)
      ..translate(-topCenter.dx, -topCenter.dy);
  }
}
