import 'package:flutter/material.dart';
import 'package:lab_4/screens/newsfeed.dart';

import 'screens/profile.dart';

void main() {
  runApp(const MainApp());
}

class MainApp extends StatelessWidget {
  const MainApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      debugShowCheckedModeBanner: false,
      initialRoute: '/',
      routes: {
        '/': (context) => const NewsFeed(),
        '/profile': (context) => ProfileScreen(),
      },
    );
  }
}
