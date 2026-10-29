import 'dart:io';

import 'package:flutter/foundation.dart';
import 'package:path_provider/path_provider.dart';

enum AppDirectory {
  temporary,
  applicationSupport,
  applicationLibrary,
  applicationDocuments,
  applicationCache,
  externalStorage,
  externalCacheDirectories,
  externalStorageDirectories,
  downloads,
}

class DirectorySpec {
  const DirectorySpec({required this.title, required this.apiName, required this.supported});

  final String title;
  final String apiName;
  final List<String> supported;
}

class DirectoryInfo {
  const DirectoryInfo({
    required this.directory,
    required this.title,
    required this.apiName,
    required this.supported,
    required this.unsupported,
    required this.paths,
    this.error,
  });

  final AppDirectory directory;
  final String title;
  final String apiName;
  final List<String> supported;
  final List<String> unsupported;
  final List<String> paths;
  final String? error;

  bool get isResolved => paths.isNotEmpty && error == null;

  String get primaryPath => paths.isEmpty ? '' : paths.first;

  String get statusText => error ?? (paths.isEmpty ? 'Путь не найден' : paths.join('\n'));
}

class DirectoryService {
  DirectoryService._();

  static const List<String> allPlatforms = <String>['Android', 'iOS', 'macOS', 'Windows', 'Linux'];

  static const Map<AppDirectory, DirectorySpec> specs = <AppDirectory, DirectorySpec>{
    AppDirectory.temporary: DirectorySpec(
      title: 'Temporary',
      apiName: 'getTemporaryDirectory()',
      supported: <String>['Android', 'iOS', 'macOS', 'Windows', 'Linux'],
    ),
    AppDirectory.applicationSupport: DirectorySpec(
      title: 'Application Support',
      apiName: 'getApplicationSupportDirectory()',
      supported: <String>['Android', 'iOS', 'macOS', 'Windows', 'Linux'],
    ),
    AppDirectory.applicationLibrary: DirectorySpec(
      title: 'Application Library',
      apiName: 'getLibraryDirectory()',
      supported: <String>['iOS', 'macOS'],
    ),
    AppDirectory.applicationDocuments: DirectorySpec(
      title: 'Application Documents',
      apiName: 'getApplicationDocumentsDirectory()',
      supported: <String>['Android', 'iOS', 'macOS', 'Windows', 'Linux'],
    ),
    AppDirectory.applicationCache: DirectorySpec(
      title: 'Application Cache',
      apiName: 'getApplicationCacheDirectory()',
      supported: <String>['Android', 'iOS', 'macOS', 'Windows', 'Linux'],
    ),
    AppDirectory.externalStorage: DirectorySpec(
      title: 'External Storage',
      apiName: 'getExternalStorageDirectory()',
      supported: <String>['Android'],
    ),
    AppDirectory.externalCacheDirectories: DirectorySpec(
      title: 'External Cache Directories',
      apiName: 'getExternalCacheDirectories()',
      supported: <String>['Android'],
    ),
    AppDirectory.externalStorageDirectories: DirectorySpec(
      title: 'External Storage Directories',
      apiName: 'getExternalStorageDirectories()',
      supported: <String>['Android'],
    ),
    AppDirectory.downloads: DirectorySpec(
      title: 'Downloads',
      apiName: 'getDownloadsDirectory()',
      supported: <String>['Android', 'iOS', 'macOS', 'Windows', 'Linux'],
    ),
  };

  static String get currentPlatform => _platformName(defaultTargetPlatform);

  static List<String> unsupportedPlatformsOf(AppDirectory directory) {
    final List<String> supported = specs[directory]!.supported;

    return allPlatforms.where((String platform) => !supported.contains(platform)).toList();
  }

  static Future<DirectoryInfo> resolve(AppDirectory directory) async {
    final DirectorySpec spec = specs[directory]!;
    final List<String> unsupported = unsupportedPlatformsOf(directory);

    try {
      final List<String> paths = await _pathsOf(directory);

      if (paths.isEmpty) {
        return _failed(directory, spec, unsupported, 'система не вернула путь');
      }

      return DirectoryInfo(
        directory: directory,
        title: spec.title,
        apiName: spec.apiName,
        supported: spec.supported,
        unsupported: unsupported,
        paths: paths,
      );
    } on MissingPlatformDirectoryException catch (error) {
      return _failed(directory, spec, unsupported, error.message);
    } on UnsupportedError catch (error) {
      return _failed(directory, spec, unsupported, error.message ?? 'операция не поддерживается');
    } catch (error) {
      return _failed(directory, spec, unsupported, error.toString());
    }
  }

  static Future<List<DirectoryInfo>> resolveAll() async {
    return <DirectoryInfo>[
      for (final AppDirectory directory in AppDirectory.values) await resolve(directory),
    ];
  }

  static Future<List<String>> _pathsOf(AppDirectory directory) async {
    switch (directory) {
      case AppDirectory.temporary:
        return <String>[(await getTemporaryDirectory()).path];
      case AppDirectory.applicationSupport:
        return <String>[(await getApplicationSupportDirectory()).path];
      case AppDirectory.applicationLibrary:
        return <String>[(await getLibraryDirectory()).path];
      case AppDirectory.applicationDocuments:
        return <String>[(await getApplicationDocumentsDirectory()).path];
      case AppDirectory.applicationCache:
        return <String>[(await getApplicationCacheDirectory()).path];
      case AppDirectory.externalStorage:
        final Directory? storage = await getExternalStorageDirectory();
        return storage == null ? const <String>[] : <String>[storage.path];
      case AppDirectory.externalCacheDirectories:
        final List<Directory>? directories = await getExternalCacheDirectories();
        return directories?.map((Directory directory) => directory.path).toList() ?? const <String>[];
      case AppDirectory.externalStorageDirectories:
        final List<Directory>? directories = await getExternalStorageDirectories();
        return directories?.map((Directory directory) => directory.path).toList() ?? const <String>[];
      case AppDirectory.downloads:
        final Directory? downloads = await getDownloadsDirectory();
        return downloads == null ? const <String>[] : <String>[downloads.path];
    }
  }

  static DirectoryInfo _failed(
    AppDirectory directory,
    DirectorySpec spec,
    List<String> unsupported,
    String reason,
  ) {
    final String error = 'Каталог «${spec.title}» недоступен на платформе $currentPlatform: $reason. '
        'Не поддерживаются платформы: ${unsupported.join(', ')}. '
        'Поддерживаются платформы: ${spec.supported.join(', ')}.';

    return DirectoryInfo(
      directory: directory,
      title: spec.title,
      apiName: spec.apiName,
      supported: spec.supported,
      unsupported: unsupported,
      paths: const <String>[],
      error: error,
    );
  }

  static String _platformName(TargetPlatform platform) {
    switch (platform) {
      case TargetPlatform.android:
        return 'Android';
      case TargetPlatform.iOS:
        return 'iOS';
      case TargetPlatform.macOS:
        return 'macOS';
      case TargetPlatform.windows:
        return 'Windows';
      case TargetPlatform.linux:
        return 'Linux';
      case TargetPlatform.fuchsia:
        return 'Fuchsia';
    }
  }
}
