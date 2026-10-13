import 'package:flutter/material.dart';

import '../screens/profile.dart';

class NewsFeedHeader extends StatefulWidget {
  const NewsFeedHeader({super.key});

  @override
  State<NewsFeedHeader> createState() => _NewsFeedHeaderState();
}

class _NewsFeedHeaderState extends State<NewsFeedHeader> {
  // 1. Создаем переменную для хранения текста (изначально "3")
  String _notificationCount = "";

  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: const EdgeInsets.only(top: 20, left: 20, right: 20),
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
                  boxShadow: [
                    BoxShadow(
                      color: Colors.grey.withValues(alpha: 0.1),
                      spreadRadius: -1,
                      blurRadius: 1,
                      offset: const Offset(5, 6),
                    ),
                  ],
                ),
                child: Padding(
                  padding: const EdgeInsets.symmetric(
                    vertical: 5,
                    horizontal: 10,
                  ),
                  child: Row(
                    mainAxisAlignment: MainAxisAlignment.spaceAround,
                    children: [
                      const Icon(Icons.notifications),

                      Text(
                        "3",
                        style: const TextStyle(
                          fontWeight: FontWeight.bold,
                          fontSize: 20,
                        ),
                      ),
                    ],
                  ),
                ),
              ),
              const SizedBox(width: 8),
              Container(
                decoration: BoxDecoration(
                  color: Colors.white,
                  borderRadius: BorderRadius.circular(20),
                  boxShadow: [
                    BoxShadow(
                      color: Colors.grey.withValues(alpha: 0.3),
                      spreadRadius: -1,
                      blurRadius: 1,
                      offset: const Offset(5, 6),
                    ),
                  ],
                ),
                child: const Padding(
                  padding: EdgeInsets.all(5),
                  child: Icon(Icons.message, color: Colors.grey),
                ),
              ),
            ],
          ),
          Text(
            _notificationCount,
            style: const TextStyle(fontWeight: FontWeight.bold, fontSize: 20),
          ),
          InkWell(
            onTap: () async {
              final result = await Navigator.push(
                context,
                MaterialPageRoute(
                  builder: (context) => ProfileScreen(
                    name: "Darlene Beats",
                    image: 'assets/mainavatar.jpg',
                  ),
                ),
              );

              // 3. Если результат вернулся и это строка, обновляем состояние
              if (result != null) {
                setState(() {
                  _notificationCount = result.toString();
                });
              }
            },
            child: Container(
              decoration: BoxDecoration(
                borderRadius: BorderRadius.circular(20),
                border: Border.all(color: Colors.white, width: 3),
                boxShadow: [
                  BoxShadow(
                    color: Colors.grey.withValues(alpha: 0.3),
                    spreadRadius: -1,
                    blurRadius: 1,
                    offset: const Offset(5, 6),
                  ),
                ],
              ),
              clipBehavior: Clip.antiAlias,
              child: ClipRRect(
                borderRadius: BorderRadius.circular(20),
                child: Image.asset(
                  'assets/mainavatar.jpg',
                  height: 30,
                  width: 30,
                ),
              ),
            ),
          ),
        ],
      ),
    );
  }
}
