# System Rezerwacji Zasobów (2026)
**Autor:** [Twoje Imię i Nazwisko]  
**Nr albumu:** [Twój Numer]

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

> **Informacja:** Ten plik będzie ewoluował. [cite_start]W kolejnych etapach dodamy tutaj sekcję "Quick Start", opis zmiennych środowiskowych oraz instrukcję wdrożenia (CI/CD)[cite: 280, 549].