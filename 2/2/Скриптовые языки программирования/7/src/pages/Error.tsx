import React from 'react';
import { useNavigate } from 'react-router-dom';

const Error: React.FC = () => {
    const navigate = useNavigate();

    const handleGoBack = () => {
        navigate(-1);
    };

    return (
        <div className="error-container">
            <h1 className="error-title">404 - Page Not Found</h1>
            <p className="error-message">
                The page you are looking for does not exist or has been moved.
            </p>
            <button className="error-button" onClick={handleGoBack}>
                Go Back
            </button>
        </div>
    );
};

export default Error;