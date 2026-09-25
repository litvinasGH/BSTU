import 'package:flutter/material.dart';

class Profileheader extends StatelessWidget {
  const new({super.key});

  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: const EdgeInsets.all(10.0),
      child: Row(
        mainAxisAlignment: .spaceBetween,

        children: [
          Row(
            children: [
              Text(
                "Darlene",
                style: TextStyle(fontWeight: FontWeight.bold, fontSize: 20),
              ),
              Icon(Icons.arrow_drop_down, size: 30),
            ],
          ),
          Row(
            children: [
              Container(
                decoration: BoxDecoration(
                  color: Colors.white,
                  borderRadius: BorderRadius.circular(20),
                ),
                child: Padding(
                  padding: const EdgeInsets.symmetric(
                    vertical: 6,
                    horizontal: 15,
                  ),
                  child: Text(
                    "Edit",
                    style: TextStyle(fontWeight: FontWeight.bold),
                  ),
                ),
              ),
              SizedBox(width: 15),
              Icon(Icons.menu),
            ],
          ),
        ],
      ),
    );
  }
}
