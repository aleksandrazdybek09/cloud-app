const [error, setError] = useState<string | null>(null);

const handleDelete = async (id: number) => {
  try {
    await axios.delete(`${API_URL}/tasks/${id}`);
    setTasks(tasks.filter(t => t.id !== id));
  } catch (err: any) {
    // Obsługa błędów i wyświetlenie użytkownikowi
    setError(err.response?.data?.message || "Coś poszło nie tak przy usuwaniu!");
  }
};

return (
  <div>
    {error && <div className="error-banner">{error}</div>}
    {/* Reszta komponentu */}
  </div>
);