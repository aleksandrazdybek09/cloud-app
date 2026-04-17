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

# Realizacja Części 6: Wdrożenie aplikacji do chmury (Microsoft Azure)
W tym etapie projekt został z sukcesem przeniesiony ze środowiska lokalnego (localhost) do chmury publicznej Microsoft Azure, zyskując pełną dostępność w internecie.
**Zrealizowane zadania:**
* **Baza danych (Azure SQL):** Utworzenie chmurowej instancji bazy danych, konfiguracja reguł zapory sieciowej (Firewall) dla bezpiecznego dostępu oraz udana migracja schematu bazy z wykorzystaniem narzędzi Entity Framework Core (`dotnet ef`).
* **Backend (.NET 8):** Publikacja REST API w usłudze Azure App Service (środowisko Linux). Proces obejmował konfigurację zmiennych środowiskowych (m.in. wymuszenie nasłuchiwania na porcie `8080` dla .NET 8 oraz włączenie interfejsu Swagger w środowisku chmurowym) i zdefiniowanie punktu wejścia aplikacji.
* **Frontend (React):** Zintegrowanie aplikacji klienckiej z nowym, produkcyjnym adresem API.
* **Hosting i CI/CD:** Wdrożenie frontendu przy pomocy usługi Azure Static Web Apps wraz z automatyzacją procesu budowania i publikacji (Continuous Deployment) przy użyciu GitHub Actions.

# Realizacja Części 7: Zabezpieczenie danych dostępowych (Azure Key Vault i Managed Identity)
W tym etapie skupiono się na podniesieniu poziomu bezpieczeństwa aplikacji poprzez całkowitą eliminację poufnych danych (takich jak hasła do bazy danych) z kodu źródłowego i przeniesienie ich do dedykowanego, bezpiecznego magazynu chmurowego.
**Zrealizowane zadania:**
* **Magazyn Kluczy (Azure Key Vault):** Utworzenie bezpiecznego zasobu w chmurze (usługa Key Vault) i przeniesienie do niego głównego ciągu połączenia do bazy danych (DbConnectionString) w formie zaszyfrowanego wpisu tajnego.
* **Eliminacja sekretów z kodu (.NET 8):** Usunięcie jawnych haseł z plików konfiguracyjnych (appsettings.json, appsettings.Development.json). Zaktualizowanie konfiguracji w pliku Program.cs przy użyciu bibliotek Azure.Identity oraz DefaultAzureCredential, co pozwala na dynamiczne pobieranie poświadczeń w trakcie działania programu.
* **Tożsamość Zarządzana (Managed Identity):** Włączenie tożsamości przypisanej przez system (System-assigned) dla backendu w usłudze Azure App Service. Skonfigurowanie kontroli dostępu (IAM) i nadanie aplikacji roli Użytkownika wpisów tajnych usługi Key Vault, co pozwala na bezpieczną, bezhasłową komunikację między usługami.
* **Wdrożenie i weryfikacja:** Publikacja zaktualizowanej i zabezpieczonej wersji aplikacji w chmurze oraz weryfikacja poprawnego łączenia się z bazą danych przy wykorzystaniu poświadczeń pobieranych "w locie" z Magazynu Kluczy. Uaktualnienie repozytorium na platformie GitHub.

# Realizacja Części 8: Automatyzacja wdrożeń (CI/CD), testowanie i nowa funkcjonalność
W tym etapie skupiono się na usprawnieniu cyklu życia aplikacji poprzez automatyzację procesu publikacji, zapewnienie jakości kodu za pomocą testów oraz rozbudowę systemu o nowe możliwości zarządzania danymi. Zrealizowane zadania:

* **Testy Jednostkowe (xUnit):** Utworzenie dedykowanego projektu testowego i napisanie "strażnika" logiki biznesowej. Zaimplementowanie metody testowej (NewTask_ShouldNotBeCompleted) weryfikującej, czy nowo tworzone obiekty zadań przyjmują domyślnie poprawny status ukończenia.

* **Automatyzacja CI/CD (GitHub Actions):** Skonfigurowanie połączenia między repozytorium GitHub a usługą Azure App Service poprzez Centrum wdrażania. Uruchomienie automatycznego przepływu pracy (workflow), który samodzielnie kompiluje i wdraża nową wersję aplikacji po każdym wypchnięciu zmian (push) do gałęzi głównej.

* **Rozbudowa funkcjonalności (Full-Stack):** Wprowadzenie funkcji usuwania zadań. Utworzenie nowego punktu końcowego w backendzie (DELETE /api/Tasks/{id}) połączonego z bazą danych SQL, oraz dodanie interaktywnego przycisku „Usuń” w interfejsie użytkownika (Frontend), korzystającego z asynchronicznych żądań JavaScript.

* **Wdrożenie i weryfikacja (Push Test):** Przeprowadzenie testu automatyzacji udowadniającego poprawność działania potoku CI/CD (zmiany wypchnięte na GitHub automatycznie zaktualizowały działającą aplikację w chmurze Azure). Finalna aktualizacja struktury repozytorium i dokumentacji projektu.

> **Informacja:** Ten plik będzie ewoluował. [cite_start]W kolejnych etapach dodamy tutaj sekcję "Quick Start", opis zmiennych środowiskowych oraz instrukcję wdrożenia (CI/CD)[cite: 280, 549].