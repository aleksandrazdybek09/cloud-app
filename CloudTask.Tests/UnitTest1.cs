using Xunit;
using Backend.Models; // Zostawiamy tak, chyba że Twoje modele są w innym folderze (np. Backend.Entities)

namespace App.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void NewTask_ShouldNotBeCompleted()
        {
            // 1. Tworzenie obiektu (używamy właściwej nazwy klasy: TaskItem)
            var task = new TaskItem();

            // 2. Nadanie nazwy
            task.Title = "Przetestować bezpiecznik";

            // 3. Weryfikacja (Asercja)
            Assert.False(task.IsCompleted, "Nowo utworzone zadanie nie powinno być z automatu ukończone.");
        }
    }
}