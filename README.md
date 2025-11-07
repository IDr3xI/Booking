# Seat Booking application

## Použité technologie
Backend: .NET 9, C#<br>
Frontend: Blazor Server<br>
Databáze: SQL CE<br>
ORM: Entity Framework Core 7<br>
Autentizace: Windows Authentication

## Rozdělení projektů
Web
- Dialogy, tooltipy a vizualizace místností
- Základní layout a navigace
- Doménová autentizace

Application
- Služby pro rezervace, historii, obsazenost a vytíženost
- Logika pro souběžné rezervace a odrezervování
- DTO a mapování entit na data pro UI

Infrastructure
- Implementace DbContext a EF Core konfigurace
- Repozitáře pro CRUD operace
- Migrace a inicializace databáze

Domain
- Definice entit (User, Room, Seat, Reservation)
- Doménová logika a validace

Tests
- Unit testy služeb
- Testy infrastruktury

## Odhad pracnosti
Web - 4MD<br>
Application - 2MD<br>
Infrastructure - 2MD<br>
Domain - 1MD<br>
Tests - 1MD

Celkově - 10MD
