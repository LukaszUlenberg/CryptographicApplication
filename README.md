# Cryptographic Application - Aplikacja Kryptograficzna

## Testowanie aplikacji

Po skompilowaniu aplikacji, wykonaj nastêpuj¹ce scenariusze testowania.

# Aby utworzyæ klucze, zaszyfrowaæ i odszyfrowaæ
1. Kliknij przycisk `Tworzenie klucza asymetrycznego`. Etykieta wyœwietla nazwê klucza i pokazuje, ¿e jest to pe³na para kluczy.
2. Kliknij przycisk `Eksportowanie klucza publicznego`. Nale¿y pamiêtaæ, ¿e eksportowanie parametrów klucza publicznego nie zmienia bie¿¹cego klucza.
3. `Szyfrowanie pliku` Kliknij przycisk i wybierz plik.
4. `Odszyfrowywanie pliku` Kliknij przycisk i wybierz plik po prostu zaszyfrowany.
5. SprawdŸ plik w³aœnie odszyfrowany.
6. Zamknij aplikacjê i uruchom j¹ ponownie, aby przetestowaæ pobieranie utrwalonego kontenera kluczy w nastêpnym scenariuszu.

# Aby zaszyfrowaæ przy u¿yciu klucza publicznego
1. Kliknij przycisk `Importowanie klucza publicznego`. Etykieta wyœwietla nazwê klucza i pokazuje, ¿e jest ona publiczna.
2. `Szyfrowanie pliku` Kliknij przycisk i wybierz plik.
3. `Odszyfrowywanie pliku` Kliknij przycisk i wybierz plik po prostu zaszyfrowany. Nie powiedzie siê to, poniewa¿ konieczne jest odszyfrowanie klucza prywatnego.

W tym scenariuszu pokazano, ¿e klucz publiczny s³u¿y tylko do szyfrowania pliku dla innej osoby. Zazwyczaj ta osoba da ci tylko klucz publiczny i wstrzyma klucz prywatny do odszyfrowywania.

# Aby odszyfrowaæ przy u¿yciu klucza prywatnego
1. Kliknij przycisk `Uzyskiwanie klucza prywatnego`. Etykieta wyœwietla nazwê klucza i pokazuje, czy jest to pe³na para kluczy.
2. `Odszyfrowywanie pliku` Kliknij przycisk i wybierz plik po prostu zaszyfrowany. To powiedzie siê, poniewa¿ masz pe³n¹ parê kluczy do odszyfrowywania.