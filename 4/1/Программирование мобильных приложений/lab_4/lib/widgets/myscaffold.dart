import 'package:flutter/material.dart';

class MyScaffold extends StatelessWidget {
  const MyScaffold({super.key, required this.children});

  final List<Widget> children;

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
            transform: GradientScaleTransform(1, 0.5),
          ),
        ),
        child: SafeArea(child: ListView(children: children)),
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
