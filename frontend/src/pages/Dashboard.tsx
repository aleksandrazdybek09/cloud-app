import { useState, useEffect } from 'react';
import api from '../services/api'; // Skonfigurowany Axios z zajęć 3

export const Dashboard = () => {
    const [tasks, setTasks] = useState([]);
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        fetchTasks();
    }, []);

    const fetchTasks = async () => {
        try {
            setError(null);
            // Wywołanie GET z poprawną obsługą błędów API
            const response = await api.get('/api/tasks');
            setTasks(response.data);
        } catch (err: any) {
            // Walidacja i łapanie statusów [cite: 290]
            if (err.response) {
                setError(`Błąd API: ${err.response.status} - ${err.response.data.message || 'Nieznany błąd'}`);
            } else {
                setError("Nie można połączyć się z serwerem. Upewnij się, że backend na 8081 działa.");
            }
        }
    };

    return (
        <div>
            <h1>Dashboard Zadań</h1>
            {error && <div style={{ color: 'red' }}>{error}</div>}
            <ul>
                {tasks.map((task: any) => (
                    <li key={task.id}>{task.title}</li>
                ))}
            </ul>
        </div>
    );
};