# Cryptographic Application - Aplikacja Kryptograficzna

## Testowanie aplikacji

Po skompilowaniu aplikacji, wykonaj nast�puj�ce scenariusze testowania.

# Aby utworzy� klucze, zaszyfrowa� i odszyfrowa�
- Kliknij przycisk **Tworzenie klucza asymetrycznego**. Etykieta wy�wietla nazw� klucza i pokazuje, �e jest to pe�na para kluczy.
- Kliknij przycisk **Eksportowanie klucza publicznego**. Nale�y pami�ta�, �e eksportowanie parametr�w klucza publicznego nie zmienia bie��cego klucza.
- **Szyfrowanie pliku** Kliknij przycisk i wybierz plik.
- **Odszyfrowywanie pliku** Kliknij przycisk i wybierz plik po prostu zaszyfrowany.
- Sprawd� plik w�a�nie odszyfrowany.
- Zamknij aplikacj� i uruchom j� ponownie, aby przetestowa� pobieranie utrwalonego kontenera kluczy w nast�pnym scenariuszu.

# Aby zaszyfrowa� przy u�yciu klucza publicznego
- Kliknij przycisk **Importowanie klucza publicznego**. Etykieta wy�wietla nazw� klucza i pokazuje, �e jest ona publiczna.
- **Szyfrowanie pliku** Kliknij przycisk i wybierz plik.
- **Odszyfrowywanie pliku** Kliknij przycisk i wybierz plik po prostu zaszyfrowany. Nie powiedzie si� to, poniewa� konieczne jest odszyfrowanie klucza prywatnego.

W tym scenariuszu pokazano, �e klucz publiczny s�u�y tylko do szyfrowania pliku dla innej osoby. Zazwyczaj ta osoba da ci tylko klucz publiczny i wstrzyma klucz prywatny do odszyfrowywania.

# Aby odszyfrowa� przy u�yciu klucza prywatnego
Kliknij przycisk **Uzyskiwanie klucza prywatnego**. Etykieta wy�wietla nazw� klucza i pokazuje, czy jest to pe�na para kluczy.
**Odszyfrowywanie pliku** Kliknij przycisk i wybierz plik po prostu zaszyfrowany. To powiedzie si�, poniewa� masz pe�n� par� kluczy do odszyfrowywania.