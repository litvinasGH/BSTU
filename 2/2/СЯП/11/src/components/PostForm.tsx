import React, { useState } from 'react';
import { useAppDispatch } from '../hooks';
import { addNewPost } from '../features/posts/postsSlice';
import { NewPost } from '../features/posts/postsAPI';

const PostForm: React.FC = () => {
  const dispatch = useAppDispatch();
  const [title, setTitle] = useState('');
  const [body, setBody] = useState('');

  const onSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    if (title && body) {
      const newPost: NewPost = { userId: 1, title, body };
      dispatch(addNewPost(newPost));
      setTitle('');
      setBody('');
    }
  };

  return (
    <form className="post-form" onSubmit={onSubmit}>
      <input
        type="text"
        placeholder="Title"
        value={title}
        onChange={e => setTitle(e.target.value)}
      />
      <textarea
        placeholder="Body"
        value={body}
        onChange={e => setBody(e.target.value)}
      />
      <button type="submit">Add Post</button>
    </form>
  );
};

export default PostForm;