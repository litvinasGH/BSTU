import { createSlice, createAsyncThunk } from '@reduxjs/toolkit';
import { fetchPosts, createPost, updatePost, deletePost, Post, NewPost } from './postsAPI';
import { RootState } from '../../app/store';

interface PostsState {
  items: Post[];
  loading: boolean;
  error: string | null;
}

const initialState: PostsState = {
  items: [],
  loading: false,
  error: null,
};

export const loadPosts = createAsyncThunk('posts/load', async () => {
  return await fetchPosts();
});

export const addNewPost = createAsyncThunk('posts/add', async (newPost: NewPost) => {
  return await createPost(newPost);
});

export const editPost = createAsyncThunk('posts/edit', async (post: Post) => {
  return await updatePost(post);
});

export const removePost = createAsyncThunk('posts/remove', async (id: number) => {
  await deletePost(id);
  return id;
});

const postsSlice = createSlice({
  name: 'posts',
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder
      .addCase(loadPosts.pending, (state) => { state.loading = true; })
      .addCase(loadPosts.fulfilled, (state, action) => {
        state.items = action.payload;
        state.loading = false;
      })
      .addCase(loadPosts.rejected, (state, action) => {
        state.loading = false;
        state.error = action.error.message || 'Failed to load posts';
      })

      .addCase(addNewPost.fulfilled, (state, action) => {
        state.items.unshift(action.payload);
      })

      .addCase(editPost.fulfilled, (state, action) => {
        const index = state.items.findIndex(p => p.id === action.payload.id);
        if (index >= 0) state.items[index] = action.payload;
      })

      .addCase(removePost.fulfilled, (state, action) => {
        state.items = state.items.filter(p => p.id !== action.payload);
      });
  },
});

export const selectPosts = (state: RootState) => state.posts.items;
export default postsSlice.reducer;