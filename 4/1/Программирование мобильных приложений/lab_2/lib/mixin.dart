mixin GameLogger {
  void logAction(String message) {
    print('[LOG] $message');
  }

  void logGameStart(String title) {
    print('[LOG] Игра "$title" запущена.');
  }

  void logGameEnd(String title) {
    print('[LOG] Игра "$title" завершена.');
  }
}
