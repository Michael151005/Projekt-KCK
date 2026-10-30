# System Zarządzania Stacją Benzynową (C# Console App)

Aplikacja konsolowa napisana w języku C# (.NET), symulująca pełny system obsługi stacji benzynowej. Projekt został zrealizowany z zachowaniem wzorca projektowego **MVC (Model-View-Controller)** oraz podziałem na warstwy biznesowe (Services).

## Funkcjonalności według ról

System dzieli się na 3 główne panele dostępowe:

1. **Klient:**
   - Tankowanie pojazdu (sprawdzanie stanów z dystrybutora).
   - Sklep (zakup produktów, dodawanie do koszyka).
   - Obsługa koszyka i płatności (Gotówka, Karta z PIN-em, generowany kod BLIK).
   - Drukowanie paragonu z podsumowaniem (zapis do formatu `.txt`).

2. **Pracownik (Wymaga logowania):**
   - Wgląd w aktualne stany magazynowe paliw i produktów.
   - Zamawianie dostaw (uzupełnianie dystrybutorów i półek).
   - Zmiana własnego hasła dostępowego.

3. **Kierownik (Wymaga logowania):**
   - Pełny monitoring magazynów.
   - Dynamiczna zmiana cen paliw i produktów sklepowych.
   - Zarządzanie asortymentem (domawianie towaru).
   - Generowanie raportów sprzedaży.

## Architektura i Technologie

- **Język:** C# (Platforma .NET)
- **Architektura:** MVC (Models, Views, Controllers, Services).
- **Przechowywanie danych:** Trwały zapis za pomocą plików `.json` (np. `paliwa.json`, `produkty.json`, `transakcje.json`).
- **Autoryzacja:** Hasła użytkowników nie są przetrzymywane jawnym tekstem, lecz jako skróty **Base64** w pliku `hasla.json`. Wpisywanie haseł jest maskowane gwiazdkami (`*`).

## Instrukcja uruchomienia

Aby uruchomić projekt na swoim komputerze, upewnij się, że posiadasz zainstalowane środowisko **.NET SDK**.

1. Sklonuj repozytorium na swój dysk.
2. Otwórz terminal / wiersz poleceń i przejdź do folderu głównego projektu (tam, gdzie znajduje się plik `.csproj`).
3. Uruchom aplikację komendą:
   ```bash
   dotnet run

## Domyślne hasła 

- Pracownik: 1111
- Kierownik: 9999