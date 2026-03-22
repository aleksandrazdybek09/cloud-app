# System Rezerwacji Zasobów (2026)
**Autor:** [Aleksandra Zdybek]  
**Nr albumu:** [95851]

[cite_start]Projekt natywnej aplikacji chmurowej realizowany w architekturze 3-warstwowej w ramach zajęć "Budowa i Administracja Aplikacji w Chmurze"[cite: 2, 63].

## Deklaracja Architektury (Mapowanie Azure)
[cite_start]Ten projekt został zaplanowany z myślą o usługach PaaS (Platform as a Service) w regionie Poland Central[cite: 243, 541].

| Warstwa | Komponent Lokalny | Usługa Azure |
| :--- | :--- | :--- |
| **Presentation** | React 19 (Vite) | Azure Static Web Apps |
| **Application** | API (.NET 9) | Azure App Service |
| **Data** | SQL Server (Dev) | Azure SQL Database (Serverless) |

## Status Projektu
* [cite_start][x] **Artefakt 1:** Zaplanowano strukturę folderów i diagram C4 (dostępny w `/docs`)[cite: 234, 480].
* [cite_start][ ] **Artefakt 2:** Konfiguracja środowiska Docker (w trakcie...)[cite: 481].

# Realizacja Części 3 (Frontend - React & Vite):
* **3.1. Inicjalizacja:** Aplikacja kliencka (React + Vite) została pomyślnie zainicjalizowana w folderze `/frontend`.
* **3.2. Widoki:** Utworzono responsywny widok wyświetlający listę danych pobieranych z serwera.
* **3.3. Komunikacja z API:** Zaimplementowano połączenie z backendem przy użyciu biblioteki Axios (metoda GET). Zadbano o dobre praktyki – adres serwera jest wstrzykiwany dynamicznie za pomocą zmiennej środowiskowej `VITE_API_URL` (brak hardcodowanych adresów URL).
* **3.5. Konteneryzacja:** Skonfigurowano plik `Dockerfile` dla frontendu, co pozwala na bezproblemowe uruchomienie interfejsu w kontenerze na porcie `8080`.

# Realizacja Części 4 (Backend - REST API):
* **4.1. REST API & CRUD:** Backend uruchamia się poprawnie w kontenerze Docker na porcie `8081`. API posiada pełną obsługę CRUD dla głównej encji (minimum 5 endpointów: Pobieranie listy, Pobieranie szczegółów, Dodawanie, Aktualizacja, Usuwanie).
* **4.2. Połączenie z DB:** API jest skonfigurowane do automatycznego łączenia się z kontenerem bazy danych (konfiguracja w `Program.cs`).
* **4.3. Kontroler:** Pomyślnie zaimplementowano logikę sterującą żądaniami HTTP w pliku `TasksController.cs`.
* **4.4. Walidacja i błędy:** Backend dba o spójność danych i zwraca odpowiednie kody statusów HTTP (np. 200, 201, 400, 404). Odpowiedzi te są poprawnie obsługiwane i walidowane po stronie frontendu (m.in. w widoku `Dashboard.tsx`).

# Realizacja Części 5:
* **5.1. Stabilność:** API zwraca wyłącznie obiekty DTO (TaskReadDto, TaskCreateDto).
* **5.2. Trwałość:** Wykorzystano zewnętrzne wolumeny (named volumes). Dane przetrwały komendę `docker compose down -v`.
* **5.3. Migracje:** Projekt wykorzystuje EF Core, historia jest w folderze Migrations.
* **5.4. Frontend:** Użytkownik może dodawać i odczytywać zadania z poziomu aplikacji React.

> **Informacja:** Ten plik będzie ewoluował. [cite_start]W kolejnych etapach dodamy tutaj sekcję "Quick Start", opis zmiennych środowiskowych oraz instrukcję wdrożenia (CI/CD)[cite: 280, 549].