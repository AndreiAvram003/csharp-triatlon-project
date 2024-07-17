// src/components/RefereeList.jsx
import React, { useState, useEffect } from 'react';
import api from '../api';
import RefereeForm from './RefereeForm.jsx';

const RefereeList = () => {
    const [referees, setReferees] = useState([]);
    const [selectedReferee, setSelectedReferee] = useState(null);

    useEffect(() => {
        fetchReferees();
    }, []);

    const fetchReferees = async () => {
        const response = await api.get('/');
        setReferees(response.data);
    };

    const handleDelete = async (id) => {
        await api.delete(`/${id}`);
        fetchReferees();
    };

    return (
        <div>
            <h1>Referees</h1>
            <RefereeForm onSave={fetchReferees} selectedReferee={selectedReferee} />
            <ul>
                {referees.map((referee) => (
                    <li key={referee.id}>
                        {referee.name}
                        <button onClick={() => setSelectedReferee(referee)}>Edit</button>
                        <button onClick={() => handleDelete(referee.id)}>Delete</button>
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default RefereeList;
