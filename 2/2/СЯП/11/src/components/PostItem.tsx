import React, { useState } from 'react';
import { Post } from '../features/posts/postsAPI';
import { useAppDispatch } from '../hooks';
import { editPost, removePost } from '../features/posts/postsSlice';

interface Props { post: Post; }

const PostItem: React.FC<Props> = ({ post }) => {
  const dispatch = useAppDispatch();
  const [isEditing, setIsEditing] = useState(false);
  const [title, setTitle] = useState(post.title);
  const [body, setBody] = useState(post.body);

  const onSave = () => {
    dispatch(editPost({ ...post, title, body }));
    setIsEditing(false);
  };

  return (
    <div className="post-item">
      {isEditing ? (
        <>
          <input value={title} onChange={e => setTitle(e.target.value)} />
          <textarea value={body} onChange={e => setBody(e.target.value)} />
          <button onClick={onSave}>Save</button>
        </>
      ) : (
        <>
          <h2>{post.title}</h2>
          <p>{post.body}</p>
          <button onClick={() => setIsEditing(true)}>Edit</button>
          <button onClick={() => dispatch(removePost(post.id))}>Delete</button>
        </>
      )}
    </div>
  );
};

export default PostItem;