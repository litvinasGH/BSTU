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
    return Padding(
      padding: const EdgeInsets.only(bottom: 10),
      child: Container(
        decoration: BoxDecoration(
          color: Colors.white,
          borderRadius: BorderRadius.circular(20),
          boxShadow: [
            BoxShadow(
              color: Colors.grey.withValues(alpha: 0.3),
              spreadRadius: 0.1,
              blurRadius: 5,
              offset: Offset(5, 6),
            ),
          ],
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
                          Text(
                            postedTime,
                            style: TextStyle(color: Colors.grey),
                          ),
                        ],
                      ),
                    ],
                  ),
                ],
              ),
              SizedBox(height: 15),
              FormattedText(text: text, style: TextStyle(fontSize: 16)),
              if (images.isNotEmpty)
                Padding(
                  padding: const EdgeInsets.only(top: 10),
                  child: SizedBox(child: Gallery(images: images), height: 180),
                )
              else
                SizedBox(height: 20),
            ],
          ),
        ),
      ),
    );
  }
}

class Gallery extends StatelessWidget {
  const Gallery({super.key, required this.images});

  final List<String> images;

  @override
  Widget build(BuildContext context) {
    switch (images.length) {
      case 1:
        return _oneImage();

      case 2:
        return _twoImages();

      case 3:
        return _threeImages();

      default:
        return _manyImages();
    }
  }

  // 1 КАРТИНКА

  Widget _oneImage() {
    return SizedBox(
      height: 220,
      width: double.infinity,
      child: Stack(
        children: [
          // Центральная
          Positioned(left: 160, top: 5, child: _image(images[0])),
        ],
      ),
    );
  }

  // 2 КАРТИНКИ

  Widget _twoImages() {
    return SizedBox(
      height: 220,
      width: double.infinity,
      child: Stack(
        children: [
          // Левая
          Positioned(
            left: 65,
            top: 5,
            child: Transform.rotate(angle: -0.10, child: _image(images[0])),
          ),

          // Правая
          Positioned(
            right: 65,
            top: 5,
            child: Transform.rotate(angle: 0.10, child: _image(images[1])),
          ),
        ],
      ),
    );
  }

  // 3 КАРТИНКИ

  Widget _threeImages() {
    return SizedBox(
      height: 220,
      width: double.infinity,
      child: Stack(
        children: [
          // Левая
          Positioned(
            left: 65,
            top: 5,
            child: Transform.rotate(angle: -0.10, child: _image(images[0])),
          ),

          // Правая
          Positioned(
            right: 65,
            top: 5,
            child: Transform.rotate(angle: 0.10, child: _image(images[2])),
          ),

          // Центральная
          Positioned(left: 160, top: 20, child: _image(images[1])),
        ],
      ),
    );
  }

  // 4+ КАРТИНОК

  Widget _manyImages() {
    final remaining = images.length - 3;

    return SizedBox(
      height: 220,
      width: double.infinity,
      child: Stack(
        children: [
          // Левая
          Positioned(
            left: 65,
            top: 5,
            child: Transform.rotate(angle: -0.10, child: _image(images[0])),
          ),

          // Правая
          Positioned(
            right: 65,
            top: 5,
            child: Transform.rotate(
              angle: 0.10,
              child: _imageWithOverlay(images[2], "+$remaining"),
            ),
          ),

          // Центральная
          Positioned(left: 160, top: 20, child: _image(images[1])),
        ],
      ),
    );
  }

  // Обычная картинка
  Widget _image(String path) {
    return ClipRRect(
      borderRadius: BorderRadius.circular(14),
      child: Image.asset(path, width: 145, height: 190, fit: BoxFit.cover),
    );
  }

  // Картинка с +N
  Widget _imageWithOverlay(String path, String text) {
    return ClipRRect(
      borderRadius: BorderRadius.circular(14),
      child: Stack(
        children: [
          Image.asset(path, width: 145, height: 190, fit: BoxFit.cover),

          // Затемнение
          Container(
            width: 145,
            height: 190,
            color: Colors.black.withValues(alpha: 0.45),
          ),

          // +N
          Positioned.fill(
            child: Center(
              child: Text(
                text,
                style: const TextStyle(
                  color: Colors.white,
                  fontSize: 32,
                  fontWeight: FontWeight.bold,
                ),
              ),
            ),
          ),
        ],
      ),
    );
  }
}
