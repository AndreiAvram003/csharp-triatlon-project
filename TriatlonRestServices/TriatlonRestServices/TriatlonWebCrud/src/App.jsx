// src/App.js
import React from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import RefereeList from './components/RefereeList.jsx';

const App = () => {
    return (
        <Router>
            <div>
                <Routes>
                    <Route path="/" element={<RefereeList />} />
                </Routes>
            </div>
        </Router>
    );
};

export default App;
