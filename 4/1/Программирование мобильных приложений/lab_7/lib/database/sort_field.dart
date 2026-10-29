class SortField {
  const SortField({required this.column, required this.ascending});

  final String column;
  final bool ascending;

  String get orderExpression => '$column ${ascending ? 'ASC' : 'DESC'}';

  static const List<String> columns = <String>['title', 'developer', 'year', 'type'];

  static const Map<String, String> labels = <String, String>{
    'title': 'Название',
    'developer': 'Разработчик',
    'year': 'Год выпуска',
    'type': 'Жанр',
  };

  static bool isAllowed(String column) => columns.contains(column);

  static String labelOf(String column) => labels[column] ?? column;
}
