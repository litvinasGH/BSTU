import 'dart:io';

import 'package:flutter/material.dart';

class PathImage extends StatelessWidget {
  const PathImage({
    super.key,
    required this.path,
    this.fit = BoxFit.cover,
    this.width,
    this.height,
    this.icon = Icons.image_outlined,
    this.borderRadius = BorderRadius.zero,
  });

  final String path;
  final BoxFit fit;
  final double? width;
  final double? height;
  final IconData icon;
  final BorderRadius borderRadius;

  @override
  Widget build(BuildContext context) {
    final fallback = Container(
      width: width,
      height: height,
      decoration: BoxDecoration(
        gradient: const LinearGradient(
          colors: [Color(0xFFB4F4E7), Color(0xFFE8FFFA)],
          begin: Alignment.topLeft,
          end: Alignment.bottomRight,
        ),
        borderRadius: borderRadius,
      ),
      child: Center(child: Icon(icon, color: const Color(0xFF238F7D), size: 30)),
    );

    if (path.trim().isEmpty) return fallback;
    final image = path.startsWith('assets/')
        ? Image.asset(
            path,
            width: width,
            height: height,
            fit: fit,
            errorBuilder: (_, __, ___) => fallback,
          )
        : Image.file(
            File(path),
            width: width,
            height: height,
            fit: fit,
            errorBuilder: (_, __, ___) => fallback,
          );

    return ClipRRect(borderRadius: borderRadius, child: image);
  }
}

class UserAvatar extends StatelessWidget {
  const UserAvatar({
    super.key,
    required this.imagePath,
    required this.name,
    this.radius = 24,
  });

  final String imagePath;
  final String name;
  final double radius;

  @override
  Widget build(BuildContext context) {
    final size = radius * 2;
    return ClipOval(
      child: SizedBox(
        width: size,
        height: size,
        child: imagePath.isEmpty
            ? _initials()
            : PathImage(
                path: imagePath,
                width: size,
                height: size,
                icon: Icons.person_outline,
              ),
      ),
    );
  }

  Widget _initials() {
    final initial = name.trim().isEmpty ? '?' : name.trim()[0].toUpperCase();
    return Container(
      color: const Color(0xFFB4F4E7),
      alignment: Alignment.center,
      child: Text(
        initial,
        style: TextStyle(fontWeight: FontWeight.bold, fontSize: radius * 0.9),
      ),
    );
  }
}
