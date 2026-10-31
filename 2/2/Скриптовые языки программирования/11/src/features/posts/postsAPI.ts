import axios from 'axios';

export interface Post {
  userId: number;
  id: number;
  title: string;
  body: string;
}

export interface NewPost {
  userId: number;
  title: string;
  body: string;
}

const BASE_URL = 'http://localhost:5000/api/posts';

export const fetchPosts = async (): Promise<Post[]> => {
  const res = await axios.get<Post[]>(BASE_URL);
  return res.data;
};

export const createPost = async (newPost: NewPost): Promise<Post> => {
  const res = await axios.post<Post>(BASE_URL, newPost);
  return res.data;
};

export const updatePost = async (post: Post): Promise<Post> => {
  const res = await axios.put<Post>(`${BASE_URL}/${post.id}`, post);
  return res.data;
};

export const deletePost = async (id: number): Promise<void> => {
  await axios.delete(`${BASE_URL}/${id}`);
};
