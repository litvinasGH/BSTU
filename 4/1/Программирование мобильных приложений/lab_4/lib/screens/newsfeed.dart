import 'dart:async';

import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:lab_4/screens/profile.dart';

import '../widgets/newsfeed_header.dart';
import '../widgets/myscaffold.dart';
import '../widgets/stories_feed.dart';
import '../widgets/feed.dart';
import 'CameraPage.dart';

const platform2 = MethodChannel('browser');

Future<void> openGitHub(String uri) async {
  try {
    await platform2.invokeMethod('openBrowser', uri);
  } on PlatformException catch (e) {
    print('Ошибка: ${e.message}');
  }
}

const platform = MethodChannel('gyroscope');

Future<String> getGyroscope() async {
  try {
    final result = await platform.invokeMethod('getGyroscope');
    return 'Гироскоп: $result';
  } on PlatformException catch (e) {
    return 'Ошибка: ${e.message}';
  }
}

class NewsFeed extends StatefulWidget {
  const NewsFeed({super.key});

  @override
  State<NewsFeed> createState() => _NewsFeedState();
}

class _NewsFeedState extends State<NewsFeed> {
  String _gyroscopeRawString = 'Получение данных...';
  Timer? _timer;

  double _offsetX = 0.0;
  double _offsetY = 0.0;

  @override
  void initState() {
    super.initState();

    _timer = Timer.periodic(const Duration(milliseconds: 30), (timer) async {
      final value = await getGyroscope();

      if (!mounted) return;

      setState(() {
        _gyroscopeRawString = value;

        final regExp = RegExp(r'-?\d+\.?\d*');
        final matches = regExp
            .allMatches(value)
            .map((m) => double.tryParse(m.group(0) ?? '') ?? 0.0)
            .toList();

        if (matches.length >= 2) {
          _offsetX = (matches[0] * 15.0).clamp(-50.0, 50.0);
          _offsetY = (matches[1] * 15.0).clamp(-50.0, 50.0);
        } else {
          _offsetX = _offsetX * 0.9;
          _offsetY = _offsetY * 0.9;
        }
      });
    });
  }

  @override
  void dispose() {
    _timer?.cancel();
    super.dispose();
  }

  @override
  Widget build(BuildContext context) {
    return PageView(
      children: [
        MyScaffold(
          children: [
            const NewsFeedHeader(),
            const SizedBox(height: 30),
            StoriesFeed(),
            PostFeed(),
          ],
        ),
        ProfileScreen(image: 'assets/mainavatar.jpg', name: "Darlene Beats"),

        // Третий экран с эффектом параллакса
        Container(
          color: Colors.black87,
          child: Center(
            child: Stack(
              alignment: Alignment.center,
              children: [
                Transform.translate(
                  offset: Offset(_offsetX * 1.5, _offsetY * 1.5),
                  child: Text(
                    'GYRO',
                    style: TextStyle(
                      fontSize: 100,
                      fontWeight: FontWeight.bold,
                      color: Colors.white.withValues(alpha: 0.05),
                    ),
                  ),
                ),
                Transform.translate(
                  offset: Offset(_offsetX, _offsetY),
                  child: Container(
                    padding: const EdgeInsets.all(24),
                    decoration: BoxDecoration(
                      color: Colors.white.withValues(alpha: 0.1),
                      borderRadius: BorderRadius.circular(20),
                      border: Border.all(color: Colors.white24),
                    ),
                    child: Column(
                      mainAxisSize: MainAxisSize.min,
                      children: [
                        const Icon(
                          Icons.screen_rotation,
                          size: 48,
                          color: Colors.blueAccent,
                        ),
                        const SizedBox(height: 16),
                        const Text(
                          'Параллакс Текст',
                          style: TextStyle(
                            fontSize: 22,
                            fontWeight: FontWeight.bold,
                            color: Colors.white,
                          ),
                        ),
                        const SizedBox(height: 10),
                        Text(
                          _gyroscopeRawString,
                          textAlign: TextAlign.center,
                          style: const TextStyle(
                            fontSize: 14,
                            color: Colors.white70,
                            fontFamily: 'monospace',
                          ),
                        ),
                      ],
                    ),
                  ),
                ),
              ],
            ),
          ),
        ),

        //4 экран браузер
        MySimpleScreen(),

        //Батарейка
        BatterPage(),

        //Camera
        CameraPage(),
      ],
    );
  }
}

class MySimpleScreen extends StatelessWidget {
  MySimpleScreen({super.key});

  final TextEditingController _textController = TextEditingController(
    text: 'https://github.com/litvinasGH',
  );

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Padding(
        padding: const EdgeInsets.all(16.0),
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            TextField(
              controller: _textController,
              decoration: const InputDecoration(border: OutlineInputBorder()),
            ),
            const SizedBox(height: 20),

            ElevatedButton(
              onPressed: () {
                String textValue = _textController.text;
                openGitHub(textValue);
              },
              child: const Text('Browse'),
            ),
          ],
        ),
      ),
    );
  }
}

//Батарейка

// Get battery level.

class BatterPage extends StatefulWidget {
  const new({super.key});

  @override
  State<BatterPage> createState() => _BatterPageState();
}

class _BatterPageState extends State<BatterPage> {
  static const platform = MethodChannel('battery');

  // Get battery level.
  String _batteryLevel = 'Unknown battery level.';

  Future<void> _getBatteryLevel() async {
    String batteryLevel;
    try {
      final result = await platform.invokeMethod<int>('getBatteryLevel');
      batteryLevel = 'Battery level at $result % .';
    } on PlatformException catch (e) {
      batteryLevel = "Failed to get battery level: '${e.message}'.";
    }

    setState(() {
      _batteryLevel = batteryLevel;
    });
  }

  @override
  Widget build(BuildContext context) {
    return Material(
      child: Center(
        child: Column(
          mainAxisAlignment: MainAxisAlignment.spaceEvenly,
          children: [
            ElevatedButton(
              onPressed: _getBatteryLevel,
              child: const Text('Get Battery Level'),
            ),
            Text(_batteryLevel),
          ],
        ),
      ),
    );
  }
}
