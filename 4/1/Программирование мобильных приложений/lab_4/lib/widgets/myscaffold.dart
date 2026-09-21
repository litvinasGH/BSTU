import 'package:flutter/material.dart';

class MyScaffold extends StatelessWidget {
  const MyScaffold({
    super.key,
    required this.children,
    this.isMenuVisible = true,
  });

  final List<Widget> children;
  final bool isMenuVisible;

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Container(
        width: double.infinity,
        height: double.infinity,
        decoration: BoxDecoration(
          gradient: RadialGradient(
            radius: 0.9,
            center: Alignment.topCenter,
            colors: [
              Colors.tealAccent,
              const Color.fromARGB(255, 238, 255, 250),
            ],
            transform: const GradientScaleTransform(1, 0.5),
          ),
        ),

        child: SafeArea(
          child: Stack(
            children: [
              ListView(
                padding: EdgeInsets.only(bottom: isMenuVisible ? 100 : 0),
                children: children,
              ),

              if (isMenuVisible) ...[
                Positioned(
                  left: 28,
                  right: 105,
                  bottom: 8,
                  child: Container(
                    height: 65,
                    decoration: BoxDecoration(
                      color: Colors.white,
                      borderRadius: BorderRadius.circular(35),
                      boxShadow: [
                        BoxShadow(
                          color: Colors.grey.withValues(alpha: 0.5),
                          spreadRadius: 1,
                          blurRadius: 10,
                          offset: Offset(0, 0),
                        ),
                      ],
                    ),
                    child: Padding(
                      padding: const EdgeInsets.symmetric(horizontal: 15),
                      child: Row(
                        mainAxisAlignment: MainAxisAlignment.spaceAround,
                        children: [
                          SizedBox(
                            width: 30,
                            height: 30,
                            child: Stack(
                              alignment: Alignment.center,
                              children: [
                                const Positioned(
                                  top: 2,
                                  child: Icon(
                                    Icons.home_filled,
                                    size: 23,
                                    color: Colors.black,
                                  ),
                                ),
                                Positioned(
                                  top: 25,
                                  child: Container(
                                    width: 5,
                                    height: 5,
                                    decoration: const BoxDecoration(
                                      color: Colors.tealAccent,
                                      shape: BoxShape.circle,
                                    ),
                                  ),
                                ),
                              ],
                            ),
                          ),

                          const Icon(
                            Icons.search_outlined,
                            size: 25,
                            color: Colors.grey,
                          ),

                          const Icon(
                            Icons.video_camera_back_outlined,
                            size: 23,
                            color: Colors.grey,
                          ),

                          const Icon(
                            Icons.person_add_alt_outlined,
                            size: 23,
                            color: Colors.grey,
                          ),
                        ],
                      ),
                    ),
                  ),
                ),

                Positioned(
                  right: 20,
                  bottom: 8,
                  child: SizedBox(
                    width: 64,
                    height: 64,
                    child: Container(
                      decoration: BoxDecoration(
                        boxShadow: [
                          BoxShadow(
                            color: Colors.grey.withValues(alpha: 0.5),
                            spreadRadius: 1,
                            blurRadius: 10,
                            offset: Offset(0, 0),
                          ),
                        ],
                        shape: BoxShape.circle,
                      ),
                      child: FloatingActionButton(
                        onPressed: () {},
                        shape: const CircleBorder(),
                        elevation: 0,
                        backgroundColor: Colors.tealAccent,
                        child: const Icon(
                          Icons.add,
                          color: Colors.grey,
                          size: 30,
                        ),
                      ),
                    ),
                  ),
                ),
              ],
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
