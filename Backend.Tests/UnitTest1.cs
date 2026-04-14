using Xunit;
// Tutaj musisz podać namespace swojej klasy CloudTask.
// Prawdopodobnie jest to:
using cloud_app.Models;

namespace Backend.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void NewTask_ShouldNotBeCompleted()
        {
            // 1. Tworzenie obiektu
            var task = new CloudTask();

            // 2. Nadanie nazwy
            task.Name = "Przetestować bezpiecznik";

            // 3. Weryfikacja (Asercja)
            // Sprawdzamy, czy nowe zadanie domyślnie NIE jest zakończone
            Assert.False(task.IsCompleted);
        }
    }
}