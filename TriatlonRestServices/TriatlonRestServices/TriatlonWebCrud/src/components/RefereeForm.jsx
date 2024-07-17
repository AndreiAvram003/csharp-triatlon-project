// src/components/RefereeForm.jsx
import React, { useState, useEffect } from 'react';
import api from '../api';

const RefereeForm = ({ onSave, selectedReferee }) => {
    const [name, setName] = useState('');
    const [password, setPassword] = useState('');
    const [trialId, setTrialId] = useState('');

    useEffect(() => {
        if (selectedReferee) {
            setName(selectedReferee.name);
            setPassword(selectedReferee.password);
            setTrialId(selectedReferee.trialId);
        }
    }, [selectedReferee]);

    const handleSubmit = async (e) => {
        e.preventDefault();
        const referee = { name, password, trial_id: trialId };

        if (selectedReferee) {
            await api.put(`/${selectedReferee.id}`, referee);
        } else {
            await api.post('/', referee);
        }

        setName('');
        setPassword('');
        setTrialId('');
        onSave();
    };

    return (
        <form onSubmit={handleSubmit}>
            <div>
                <label>Name:</label>
                <input type="text" value={name} onChange={(e) => setName(e.target.value)} />
            </div>
            <div>
                <label>Password:</label>
                <input type="password" value={password} onChange={(e) => setPassword(e.target.value)} />
            </div>
            <div>
                <label>Trial ID:</label>
                <input type="number" value={trialId} onChange={(e) => setTrialId(e.target.value)} />
            </div>
            <button type="submit">Save</button>
        </form>
    );
};

export default RefereeForm;
