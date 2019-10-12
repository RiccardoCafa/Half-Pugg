import axios from 'axios';

const api = axios.create({
    baseURL: 'https://localhost:44338'
});

export default api;