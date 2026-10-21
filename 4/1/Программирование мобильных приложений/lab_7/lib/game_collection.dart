import 'computer_game.dart';
import 'game_iterator.dart';

class GameCollection implements Iterable<ComputerGame> {
  final List<ComputerGame> _games;

  GameCollection(this._games);

  @override
  Iterator<ComputerGame> get iterator {
    return GameIterator(_games);
  }

  @override
  bool any(bool Function(ComputerGame element) test) {
    // TODO: implement any
    throw UnimplementedError();
  }

  @override
  Iterable<R> cast<R>() {
    // TODO: implement cast
    throw UnimplementedError();
  }

  @override
  bool contains(Object? element) {
    // TODO: implement contains
    throw UnimplementedError();
  }

  @override
  ComputerGame elementAt(int index) {
    // TODO: implement elementAt
    throw UnimplementedError();
  }

  @override
  bool every(bool Function(ComputerGame element) test) {
    // TODO: implement every
    throw UnimplementedError();
  }

  @override
  Iterable<T> expand<T>(Iterable<T> Function(ComputerGame element) toElements) {
    // TODO: implement expand
    throw UnimplementedError();
  }

  @override
  // TODO: implement first
  ComputerGame get first => throw UnimplementedError();

  @override
  ComputerGame firstWhere(bool Function(ComputerGame element) test, {ComputerGame Function()? orElse}) {
    // TODO: implement firstWhere
    throw UnimplementedError();
  }

  @override
  T fold<T>(T initialValue, T Function(T previousValue, ComputerGame element) combine) {
    // TODO: implement fold
    throw UnimplementedError();
  }

  @override
  Iterable<ComputerGame> followedBy(Iterable<ComputerGame> other) {
    // TODO: implement followedBy
    throw UnimplementedError();
  }

  @override
  void forEach(void Function(ComputerGame element) action) {
    // TODO: implement forEach
  }

  @override
  // TODO: implement isEmpty
  bool get isEmpty => throw UnimplementedError();

  @override
  // TODO: implement isNotEmpty
  bool get isNotEmpty => throw UnimplementedError();

  @override
  String join([String separator = ""]) {
    // TODO: implement join
    throw UnimplementedError();
  }

  @override
  // TODO: implement last
  ComputerGame get last => throw UnimplementedError();

  @override
  ComputerGame lastWhere(bool Function(ComputerGame element) test, {ComputerGame Function()? orElse}) {
    // TODO: implement lastWhere
    throw UnimplementedError();
  }

  @override
  // TODO: implement length
  int get length => throw UnimplementedError();

  @override
  Iterable<T> map<T>(T Function(ComputerGame e) toElement) {
    // TODO: implement map
    throw UnimplementedError();
  }

  @override
  ComputerGame reduce(ComputerGame Function(ComputerGame value, ComputerGame element) combine) {
    // TODO: implement reduce
    throw UnimplementedError();
  }

  @override
  // TODO: implement single
  ComputerGame get single => throw UnimplementedError();

  @override
  ComputerGame singleWhere(bool Function(ComputerGame element) test, {ComputerGame Function()? orElse}) {
    // TODO: implement singleWhere
    throw UnimplementedError();
  }

  @override
  Iterable<ComputerGame> skip(int count) {
    // TODO: implement skip
    throw UnimplementedError();
  }

  @override
  Iterable<ComputerGame> skipWhile(bool Function(ComputerGame value) test) {
    // TODO: implement skipWhile
    throw UnimplementedError();
  }

  @override
  Iterable<ComputerGame> take(int count) {
    // TODO: implement take
    throw UnimplementedError();
  }

  @override
  Iterable<ComputerGame> takeWhile(bool Function(ComputerGame value) test) {
    // TODO: implement takeWhile
    throw UnimplementedError();
  }

  @override
  List<ComputerGame> toList({bool growable = true}) {
    // TODO: implement toList
    throw UnimplementedError();
  }

  @override
  Set<ComputerGame> toSet() {
    // TODO: implement toSet
    throw UnimplementedError();
  }

  @override
  Iterable<ComputerGame> where(bool Function(ComputerGame element) test) {
    // TODO: implement where
    throw UnimplementedError();
  }

  @override
  Iterable<T> whereType<T>() {
    // TODO: implement whereType
    throw UnimplementedError();
  }
}
