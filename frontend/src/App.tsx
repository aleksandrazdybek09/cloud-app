import { useState, useEffect } from 'react';

// Interfejs dla naszego zadania (dopasowany do Twojego DTO)
interface Task {
  id: number;
  title: string;
  isCompleted: boolean;
}

function App() {
  const [tasks, setTasks] = useState<Task[]>([]);
  const [newTaskTitle, setNewTaskTitle] = useState('');

  // Pobieranie zadań z Twojego backendu (port 8081)
  const fetchTasks = async () => {
    try {
      const response = await fetch('http://localhost:8081/api/Tasks');
      if (response.ok) {
        const data = await response.json();
        setTasks(data);
      }
    } catch (error) {
      console.error("Błąd pobierania zadań:", error);
    }
  };

  useEffect(() => {
    fetchTasks();
  }, []);

  // Dodawanie nowego zadania
  const addTask = async (e: React.FormEvent) => {
    e.preventDefault();
    if (!newTaskTitle) return;

    try {
      const response = await fetch('http://localhost:8081/api/Tasks', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({ title: newTaskTitle }), // Wysyłamy TaskCreateDto
      });

      if (response.ok) {
        setNewTaskTitle(''); // Czyszczenie pola
        fetchTasks(); // Odświeżenie listy po dodaniu
      }
    } catch (error) {
      console.error("Błąd dodawania zadania:", error);
    }
  };

  return (
    <div style={{ padding: '40px', fontFamily: 'Arial, sans-serif', maxWidth: '600px', margin: '0 auto' }}>
      <h2>Cloud App - Lista Zadań</h2>

      <form onSubmit={addTask} style={{ display: 'flex', gap: '10px', marginBottom: '30px' }}>
        <input
          type="text"
          value={newTaskTitle}
          onChange={(e) => setNewTaskTitle(e.target.value)}
          placeholder="Co masz do zrobienia?"
          style={{ flex: 1, padding: '10px', fontSize: '16px' }}
        />
        <button type="submit" style={{ padding: '10px 20px', fontSize: '16px', cursor: 'pointer' }}>
          Dodaj zadanie
        </button>
      </form>

      <ul style={{ listStyleType: 'none', padding: 0 }}>
        {tasks.map(task => (
          <li key={task.id} style={{
            padding: '15px',
            borderBottom: '1px solid #ccc',
            textDecoration: task.isCompleted ? 'line-through' : 'none'
          }}>
            {task.title}
          </li>
        ))}
      </ul>
    </div>
  );
}

export default App;