# Cryptographic Application - Aplikacja Kryptograficzna

## Testowanie aplikacji

Po skompilowaniu aplikacji, wykonaj nastêpuj¹ce scenariusze testowania.

# Aby utworzyæ klucze, zaszyfrowaæ i odszyfrowaæ
- Kliknij przycisk **Tworzenie klucza asymetrycznego**. Etykieta wyœwietla nazwê klucza i pokazuje, ¿e jest to pe³na para kluczy.
- Kliknij przycisk **Eksportowanie klucza publicznego**. Nale¿y pamiêtaæ, ¿e eksportowanie parametrów klucza publicznego nie zmienia bie¿¹cego klucza.
- **Szyfrowanie pliku** Kliknij przycisk i wybierz plik.
- **Odszyfrowywanie pliku** Kliknij przycisk i wybierz plik po prostu zaszyfrowany.
- SprawdŸ plik w³aœnie odszyfrowany.
- Zamknij aplikacjê i uruchom j¹ ponownie, aby przetestowaæ pobieranie utrwalonego kontenera kluczy w nastêpnym scenariuszu.

# Aby zaszyfrowaæ przy u¿yciu klucza publicznego
- Kliknij przycisk **Importowanie klucza publicznego**. Etykieta wyœwietla nazwê klucza i pokazuje, ¿e jest ona publiczna.
- **Szyfrowanie pliku** Kliknij przycisk i wybierz plik.
- **Odszyfrowywanie pliku** Kliknij przycisk i wybierz plik po prostu zaszyfrowany. Nie powiedzie siê to, poniewa¿ konieczne jest odszyfrowanie klucza prywatnego.

W tym scenariuszu pokazano, ¿e klucz publiczny s³u¿y tylko do szyfrowania pliku dla innej osoby. Zazwyczaj ta osoba da ci tylko klucz publiczny i wstrzyma klucz prywatny do odszyfrowywania.

# Aby odszyfrowaæ przy u¿yciu klucza prywatnego
Kliknij przycisk **Uzyskiwanie klucza prywatnego**. Etykieta wyœwietla nazwê klucza i pokazuje, czy jest to pe³na para kluczy.
**Odszyfrowywanie pliku** Kliknij przycisk i wybierz plik po prostu zaszyfrowany. To powiedzie siê, poniewa¿ masz pe³n¹ parê kluczy do odszyfrowywania.