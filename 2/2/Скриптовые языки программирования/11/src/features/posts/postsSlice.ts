// src/features/posts/postsSlice.ts
import { createSlice, createAsyncThunk } from '@reduxjs/toolkit';
import {
  fetchPosts,
  createPost,
  updatePost,
  deletePost,
  Post,
  NewPost
} from './postsAPI';
import type { RootState } from '../../app/store';

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

// Thunk’ы для CRUD
export const loadPosts = createAsyncThunk<Post[]>('posts/load', fetchPosts);
export const addNewPost = createAsyncThunk<Post, NewPost>(
  'posts/add',
  createPost
);
export const editPost = createAsyncThunk<Post, Post>(
  'posts/edit',
  updatePost
);
export const removePost = createAsyncThunk<number, number>(
  'posts/remove',
  async (id) => {
    await deletePost(id);
    return id;
  }
);

const postsSlice = createSlice({
  name: 'posts',
  initialState,
  reducers: {},
  extraReducers: (builder) => {
    builder
      // Загрузка
      .addCase(loadPosts.pending, (state) => {
        state.loading = true;
      })
      .addCase(loadPosts.fulfilled, (state, action) => {
        state.items = action.payload;
        state.loading = false;
      })
      .addCase(loadPosts.rejected, (state, action) => {
        state.loading = false;
        state.error = action.error.message || 'Failed to load posts';
      })

      // Добавление
      .addCase(addNewPost.fulfilled, (state, action) => {
        const newPost: Post = { ...action.payload }
        state.items.unshift(newPost);
      })

      // Редактирование (всегда через editPost)
      .addCase(editPost.fulfilled, (state, action) => {
        const idx = state.items.findIndex((p) => p.id === action.payload.id);
        if (idx !== -1) {
          state.items[idx] = action.payload;
        }
      })

      // Удаление
      .addCase(removePost.fulfilled, (state, action) => {
        state.items = state.items.filter((p) => p.id !== action.payload);
      });
  },
});

export const selectPosts = (state: RootState) => state.posts.items;
export default postsSlice.reducer;
