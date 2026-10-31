import 'package:flutter/material.dart';

class SimpleButton extends StatelessWidget {
  const SimpleButton({super.key, required this.text, required this.onPressed, this.color});

  final String text;
  final VoidCallback? onPressed;
  final Color? color;

  @override
  Widget build(BuildContext context) {
    return GestureDetector(
      onTap: onPressed,
      child: Container(
        height: 36,
        padding: const EdgeInsets.symmetric(horizontal: 12),
        alignment: Alignment.center,
        decoration: BoxDecoration(
          color: onPressed == null ? Colors.grey.shade300 : (color ?? Colors.white),
          border: Border.all(color: Colors.black),
        ),
        child: Text(text, style: const TextStyle(fontSize: 14)),
      ),
    );
  }
}
