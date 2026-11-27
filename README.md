# System Zarządzania Stacją Benzynową — Stacja Skolima (C# / .NET)

Kompleksowy system obsługi stacji benzynowej napisany w języku **C# (.NET)**, składający się z dwóch niezależnych warstw prezentacji opartych na wspólnym silniku biznesowym (`Models` oraz `Services`):
- **Aplikacji Webowej (GUI)** — nowoczesny interfejs graficzny wykonany w technologii **Blazor Web App**.
- **Aplikacji Konsolowej (TUI)** — oryginalna wersja tekstowa zrealizowana we wzorcu **MVC (Model-View-Controller)**.

---

## Interfejsy Użytkownika (UI/UX)

### 1. Wersja Webowa — Stacja Skolima (`StacjaSkolimaWeb`)
- **Stylistyka i responsywność:** Autorski ciemny motyw (*Stacja Skolima*) z neonowymi akcentami cyjanu i magenty (`wwwroot/app.css`), oparty na CSS Grid i Flexbox.
- **Reaktywność na żywo:** Komunikacja w trybie `InteractiveServer` (SignalR) — przeliczanie kosztów tankowania w czasie rzeczywistym, dynamiczne paski stanu zbiorników, okna modalne (logowanie, konfiguracja karty, kasa, podgląd paragonu) oraz powiadomienia typu *toast*.
- **Zero ingerencji w logikę domenową:** Projekt webowy współdzieli klasy modeli i serwisów bezpośrednio z aplikacją konsolową, łącząc warstwę widoku i kontrolera w komponentach `.razor` (`Home`, `PanelKlienta`, `PanelPracownika`, `PanelKierownika`).

### 2. Wersja Konsolowa (TUI — Text-based User Interface)
- **Interaktywna nawigacja:** Obsługa menu odbywa się płynnie za pomocą **strzałek (góra/dół)** oraz zatwierdzania klawiszem **Enter**.
- **Wydajne odświeżanie:** Częściowe odświeżanie ekranu (nadpisywanie pozycji kursora) całkowicie eliminuje efekt „migającej konsoli”.

---

## Funkcjonalności według ról

System dzieli się na 3 główne panele dostępowe:

1. **Klient:**
   - Tankowanie pojazdu z dystrybutorów (podgląd stanu zbiorników i wyliczanie kosztu na żywo).
   - Sklep stacji (dodawanie produktów do koszyka z kontrolą dostępności na półce).
   - Obsługa koszyka i płatności: **Gotówka** (limit 15 000 zł), **Karta płatnicza** (weryfikacja 4-cyfrowego PIN-u z blokadą po 3 błędnych próbach) oraz **BLIK** (generowanie 6-cyfrowego kodu zapisywanego do `Data/blik.txt`).
   - Generowanie i podgląd paragonu fiskalnego (zapis do pliku `.txt`).

2. **Pracownik (Wymaga logowania):**
   - Wgląd w aktualne stany magazynowe paliw i produktów sklepowych wraz z alertami niskiego stanu.
   - Rejestracja i przyjmowanie dostaw (uzupełnianie zbiorników i półek z natychmiastowym zapisem).
   - Zmiana własnego hasła dostępowego.

3. **Kierownik (Wymaga logowania):**
   - Dynamiczna zmiana cen paliw oraz produktów sklepowych.
   - Raport sprzedaży z podsumowaniem przychodów, liczby transakcji, sprzedanych litrów oraz pełną tabelą historii paragonów.
   - Zmiana własnego hasła dostępowego.

---

## Architektura i Technologie

- **Język i platforma:** C# / .NET
- **Warstwa Web (GUI):** Blazor Web App (`InteractiveServer`), komponenty `.razor`, autorski arkusz stylów `app.css`, współdzielony stan magazynów w pamięci (`WspolneMagazyny`).
- **Warstwa Konsolowa (TUI):** Wzorzec MVC (`Models`, `Views`, `Controllers`, `Services`).
- **Przechowywanie danych:** Trwały zapis w plikach `.json` (`paliwa.json`, `produkty.json`, `transakcje.json`, `hasla.json`) oraz eksport paragonów i kodów BLIK do plików `.txt`.
- **Autoryzacja:** Hasła użytkowników są kodowane w **Base64** w pliku `hasla.json` i maskowane podczas wpisywania.

---

## Instrukcja uruchomienia

Upewnij się, że masz zainstalowane środowisko **.NET SDK**, a następnie sklonuj repozytorium i przejdź do jego katalogu głównego.

### Uruchomienie aplikacji webowej (GUI — Blazor)
1. Przejdź do folderu projektu webowego:
   ```bash
   cd StacjaSkolimaWeb
   ```
2. Uruchom serwer aplikacji:
   ```bash
   dotnet run
   ```
3. Otwórz w przeglądarce adres wyświetlony w terminalu (np. `http://localhost:5000`).

### Uruchomienie aplikacji konsolowej (TUI)
1. W głównym katalogu repozytorium uruchom projekt konsolowy:
   ```bash
   dotnet run
   ```

---

## Domyślne dane dostępowe

- **Pracownik (hasło):** `1111`
- **Kierownik (hasło):** `9999`
- **Karta klienta w GUI (domyślny PIN):** `1234`