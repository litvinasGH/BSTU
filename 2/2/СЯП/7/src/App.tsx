import React, { useEffect } from 'react';
import { Routes, Route, Navigate } from 'react-router-dom';
import SignUp from './pages/SignUp';
import SignIn from './pages/SignIn';
import Error from './pages/Error';
import ResetPassword from './pages/ResetPassword';

const App: React.FC = () => {
  useEffect(() => {
    console.log('App component mounted');
  }, []);

  return (
    <div className="app-container">
      <Routes>
        <Route path="/" element={<Navigate to="/sign-in" replace />} />
        <Route path="/sign-up" element={<SignUp />} />
        <Route path="/sign-in" element={<SignIn />} />
        <Route path="/reset-password" element={<ResetPassword />} />
        <Route path="/error" element={<Error/>} />
        <Route path="*" element={<Navigate to="/error" replace />} />
      </Routes>
    </div>
  );
};

export default App;