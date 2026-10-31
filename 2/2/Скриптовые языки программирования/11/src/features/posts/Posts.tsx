import React, { useEffect } from 'react';
import { useAppDispatch, useAppSelector } from '../../hooks';
import { loadPosts, selectPosts } from './postsSlice';
import PostItem from '../../components/PostItem';
import PostForm from '../../components/PostForm';

const Posts: React.FC = () => {
  const dispatch = useAppDispatch();
  const posts = useAppSelector(selectPosts);

  useEffect(() => {
    dispatch(loadPosts());
  }, [dispatch]);

  return (
    <div className="posts-container">
      <h1>Posts Manager</h1>
      <PostForm />
      {posts.map(post => (
        <PostItem key={post.id} post={post}  />
      ))}
    </div>
  );
};

export default Posts;