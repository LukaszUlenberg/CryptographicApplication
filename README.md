# Cryptographic Application - Aplikacja Kryptograficzna

## Testowanie aplikacji

Po skompilowaniu aplikacji, wykonaj nast�puj�ce scenariusze testowania.

# Aby utworzy� klucze, zaszyfrowa� i odszyfrowa�
1. Kliknij przycisk `Tworzenie klucza asymetrycznego`. Etykieta wy�wietla nazw� klucza i pokazuje, �e jest to pe�na para kluczy.
2. Kliknij przycisk `Eksportowanie klucza publicznego`. Nale�y pami�ta�, �e eksportowanie parametr�w klucza publicznego nie zmienia bie��cego klucza.
3. `Szyfrowanie pliku` Kliknij przycisk i wybierz plik.
4. `Odszyfrowywanie pliku` Kliknij przycisk i wybierz plik po prostu zaszyfrowany.
5. Sprawd� plik w�a�nie odszyfrowany.
6. Zamknij aplikacj� i uruchom j� ponownie, aby przetestowa� pobieranie utrwalonego kontenera kluczy w nast�pnym scenariuszu.

# Aby zaszyfrowa� przy u�yciu klucza publicznego
1. Kliknij przycisk `Importowanie klucza publicznego`. Etykieta wy�wietla nazw� klucza i pokazuje, �e jest ona publiczna.
2. `Szyfrowanie pliku` Kliknij przycisk i wybierz plik.
3. `Odszyfrowywanie pliku` Kliknij przycisk i wybierz plik po prostu zaszyfrowany. Nie powiedzie si� to, poniewa� konieczne jest odszyfrowanie klucza prywatnego.

W tym scenariuszu pokazano, �e klucz publiczny s�u�y tylko do szyfrowania pliku dla innej osoby. Zazwyczaj ta osoba da ci tylko klucz publiczny i wstrzyma klucz prywatny do odszyfrowywania.

# Aby odszyfrowa� przy u�yciu klucza prywatnego
1. Kliknij przycisk `Uzyskiwanie klucza prywatnego`. Etykieta wy�wietla nazw� klucza i pokazuje, czy jest to pe�na para kluczy.
2. `Odszyfrowywanie pliku` Kliknij przycisk i wybierz plik po prostu zaszyfrowany. To powiedzie si�, poniewa� masz pe�n� par� kluczy do odszyfrowywania.