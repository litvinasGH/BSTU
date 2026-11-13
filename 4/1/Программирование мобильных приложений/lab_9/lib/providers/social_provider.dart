import 'dart:async';

import 'package:flutter/foundation.dart';

import '../data/hive_social_store.dart';
import '../models/social_models.dart';


class SocialProvider extends ChangeNotifier {
  SocialProvider() {
    _subscriptions.add(HiveSocialStore.users.watch().listen((_) => notifyListeners()));
    _subscriptions.add(HiveSocialStore.posts.watch().listen((_) => notifyListeners()));
    _subscriptions.add(HiveSocialStore.stories.watch().listen((_) => notifyListeners()));
    _subscriptions.add(HiveSocialStore.favorites.watch().listen((_) => notifyListeners()));
    _subscriptions.add(HiveSocialStore.settings.watch().listen((_) => notifyListeners()));
  }

  final List<StreamSubscription<dynamic>> _subscriptions = [];

  SocialUser get activeUser => HiveSocialStore.activeUser;
  String get activeUserId => HiveSocialStore.activeUserId;

  List<SocialUser> get users => HiveSocialStore.users.values.toList();
  List<SocialUser> get allUsers => HiveSocialStore.allUsers;
  List<SocialPost> get posts => HiveSocialStore.posts.values.toList();
  List<SocialStory> get stories => HiveSocialStore.stories.values.toList();
  List<FavoriteEntry> get favorites => HiveSocialStore.favorites.values.toList();

  bool isFavorite({required String userId, required String postId}) {
    return HiveSocialStore.isFavorite(userId: userId, postId: postId);
  }

  List<SocialPost> favoritesFor(String userId) {
    return HiveSocialStore.favoritesFor(userId);
  }


  Future<void> setActiveUser(String userId) async {
    await HiveSocialStore.setActiveUser(userId);
  }

  Future<void> savePost({
    required SocialUser actor,
    required SocialPost post,
  }) async {
    await HiveSocialStore.savePost(actor: actor, post: post);
  }

  Future<void> deletePost({
    required SocialUser actor,
    required String postId,
  }) async {
    await HiveSocialStore.deletePost(actor: actor, postId: postId);
  }

  Future<void> toggleFavorite({
    required String userId,
    required String postId,
  }) async {
    await HiveSocialStore.toggleFavorite(userId: userId, postId: postId);
  }

  @override
  void dispose() {
    for (final subscription in _subscriptions) {
      unawaited(subscription.cancel());
    }
    super.dispose();
  }
}
