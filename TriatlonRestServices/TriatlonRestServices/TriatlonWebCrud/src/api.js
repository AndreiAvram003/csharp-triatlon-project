// src/api.js
import axios from 'axios';

const api = axios.create({
    baseURL: 'http://localhost:5082/api/referee',
});

export default api;
