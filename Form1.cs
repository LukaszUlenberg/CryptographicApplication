using System.Security.Cryptography;
using System.Text.Unicode;

namespace CryptographicApplication
{
    public partial class Form1 : Form
    {
        // Zadeklaruj obiekty CspParameters i RsaCryptoServiceProvider
        // z globalnym zakresem klasy formularza.
        readonly CspParameters _cspp = new CspParameters();
        RSACryptoServiceProvider _rsa;

        // Zmienne sciezek dla folderu zródlowego, szyfrujacego i
        // folderów deszyfrowania. Musza konczyc sie odwrotnym ukosnikiem.
        const string EncrFolder = @"c:\Encrypt\";
        const string DecrFolder = @"c:\Decrypt\";
        const string SrcFolder = @"c:\docs\";

        // Plik klucza publicznego
        const string PubKeyFile = @"c:\encrypt\rsaPublicKey.txt";

        // Nazwa kontenera kluczy dla
        // pary wartosci klucza prywatnego/publicznego.
        const string KeyName = "Key01";

        public Form1()
        {
            InitializeComponent();
        }

        private void buttonCreateAsmKeys_Click(object sender, EventArgs e)
        {
            // Przechowuje pare kluczy w kontenerze kluczy.
            _cspp.KeyContainerName = KeyName;
            _rsa = new RSACryptoServiceProvider(_cspp)
            {
                PersistKeyInCsp = true
            };

            label1.Text = _rsa.PublicOnly
                ? $"Key: {_cspp.KeyContainerName} - Tylko publiczny"
                : $"Key: {_cspp.KeyContainerName} - Pe³na para kluczy";
        }

        private void buttonEncryptFile_Click(object sender, EventArgs e)
        {
            if (_rsa is null)
            {
                MessageBox.Show("Klucz nie zosta³ ustawiony.");
            }
            else
            {
                // Wyswietl okno dialogowe, aby wybrac plik do zaszyfrowania.
                _encryptOpenFileDialog.InitialDirectory = SrcFolder;
                if (_encryptOpenFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string fName = _encryptOpenFileDialog.FileName;
                    if (fName != null)
                    {
                        // Przekaz nazwe pliku bez sciezki.
                        EncryptFile(new FileInfo(fName));
                    }
                }
            }
        }

        private void EncryptFile(FileInfo file)
        {
            // Tworzenie instancji Aes dla
            // symetrycznego szyfrowania danych.
            Aes aes = Aes.Create();
            ICryptoTransform transform = aes.CreateEncryptor();

            // Uzyj RSACryptoServiceProvider do
            // zaszyfrowania klucza AES.
            // rsa zostal wczesniej utworzony:
            // rsa = new RSACryptoServiceProvider(cspp);
            byte[] keyEncrypted = _rsa.Encrypt(aes.Key, false);

            // Utwórz tablice bajtów zawierajace
            // wartosci dlugosci klucza i IV.
            int lKey = keyEncrypted.Length;
            byte[] LenK = BitConverter.GetBytes(lKey);
            int lIV = aes.IV.Length;
            byte[] LenIV = BitConverter.GetBytes(lIV);

            // Zapis nastepujacych danych do FileStream
            // dla zaszyfrowanego pliku (outFs):
            // - dlugosc klucza
            // - dlugosc IV
            // - zaszyfrowany klucz
            // - IV
            // - zaszyfrowana zawartosc szyfru

            // Zmien rozszerzenie pliku na ".enc"
            string outFile = Path.Combine(EncrFolder, Path.ChangeExtension(file.Name, ".enc"));

            using (var outFs = new FileStream(outFile, FileMode.Create))
            {
                outFs.Write(LenK, 0, 4);
                outFs.Write(LenIV, 0, 4);
                outFs.Write(keyEncrypted, 0, lKey);
                outFs.Write(aes.IV, 0, lIV);

                // Teraz zapisz tekst zaszyfrowany przy uzyciu
                // CryptoStream do szyfrowania.
                using (var outStreamEncrypted = new CryptoStream(outFs, transform, CryptoStreamMode.Write))
                {
                    // Szyfrujac fragment po fragmencie
                    // jednorazowo, mozna zaoszczedzic pamiec
                    // i pomiescic duze pliki.
                    int count = 0;
                    int offset = 0;

                    // blockSizeBytes moze miec dowolny rozmiar.
                    int blockSizeBytes = aes.BlockSize / 8;
                    byte[] data = new byte[blockSizeBytes];
                    int bytesRead = 0;

                    using (var inFs = new FileStream(file.FullName, FileMode.Open))
                    {
                        do
                        {
                            count = inFs.Read(data, 0, blockSizeBytes);
                            offset += count;
                            outStreamEncrypted.Write(data, 0, count);
                            bytesRead += blockSizeBytes;
                        } while (count > 0);
                    }
                    outStreamEncrypted.FlushFinalBlock();
                }
            }
        }

        private void buttonDecryptFile_Click(object sender, EventArgs e)
        {
            if (_rsa is null)
            {
                MessageBox.Show("Klucz nie zosta³ ustawiony.");
            }
            else
            {
                // Wyswietl okno dialogowe, aby wybrac zaszyfrowany plik.
                _decryptOpenFileDialog.InitialDirectory = EncrFolder;
                if (_decryptOpenFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string fName = _decryptOpenFileDialog.FileName;
                    if (fName != null)
                    {
                        DecryptFile(new FileInfo(fName));
                    }
                }
            }
        }

        private void DecryptFile(FileInfo file)
        {
            // Tworzenie instancji Aes dla
            // symetrycznego deszyfrowania danych.
            Aes aes = Aes.Create();

            // Utwórz tablice bajtów, aby uzyskac dlugosc
            // zaszyfrowanego klucza i IV.
            // Wartosci te zostaly zapisane jako 4 bajty kazda
            // na poczatku zaszyfrowanego pakietu.
            byte[] LenK = new byte[4];
            byte[] LenIV = new byte[4];

            // Konstruujemy nazwe odszyfrowanego pliku.
            string outFile = Path.ChangeExtension(file.FullName.Replace("Encrypt", "Decrypt"), ".txt");

            // Uzyj obiektów FileStream do odczytania zaszyfrowanego pliku
            // pliku (inFs) i zapisania odszyfrowanego pliku (outFs).
            using (var inFs = new FileStream(file.FullName, FileMode.Open))
            {
                inFs.Seek(0, SeekOrigin.Begin);
                inFs.Read(LenK, 0, 3);
                inFs.Seek(4, SeekOrigin.Begin);
                inFs.Read(LenIV, 0, 3);

                // Konwersja dlugosci na wartosci calkowite.
                int lenK = BitConverter.ToInt32(LenK, 0);
                int lenIV = BitConverter.ToInt32(LenIV, 0);

                // Okreslenie pozycji poczatkowej
                // tekstu zaszyfrowanego (startC)
                // i jego dlugosc (lenC).
                int startC = lenK + lenIV + 8;
                int lenC = (int)inFs.Length - startC;

                // Tworzenie tablic bajtów dla
                // zaszyfrowanego klucza Aes,
                // IV i tekstu zaszyfrowanego.
                byte[] KeyEncrypted = new byte[lenK];
                byte[] IV = new byte[lenIV];

                // Wyodrdbnienie klucza i IV
                // zaczynajac od indeksu 8
                // po wartosciach dlugosci.
                inFs.Seek(8, SeekOrigin.Begin);
                inFs.Read(KeyEncrypted, 0, lenK);
                inFs.Seek(8 + lenK, SeekOrigin.Begin);
                inFs.Read(IV, 0, lenIV);

                Directory.CreateDirectory(DecrFolder);
                // Uzyj RSACryptoServiceProvider
                // do odszyfrowania klucza AES.
                byte[] KeyDecrypted = _rsa.Decrypt(KeyEncrypted, false);

                // Odszyfrowanie klucza.
                ICryptoTransform transform = aes.CreateDecryptor(KeyDecrypted, IV);

                // Odszyfruj tekst zaszyfrowany z
                // z FileSteam zaszyfrowanego pliku
                // zaszyfrowanego pliku (inFs) do FileStream
                // dla odszyfrowanego pliku (outFs).
                using (var outFs = new FileStream(outFile, FileMode.Create))
                {
                    int count = 0;
                    int offset = 0;

                    // blockSizeBytes moze miec dowolny rozmiar.
                    int blockSizeBytes = aes.BlockSize / 8;
                    byte[] data = new byte[blockSizeBytes];

                    // Odszyfrowujac jeden fragment na raz,
                    // mozna zaoszczedzic pamiec i
                    // pomiescic duze pliki.

                    // Rozpocznij od poczatku
                    // tekstu zaszyfrowanego.
                    inFs.Seek(startC, SeekOrigin.Begin);

                    using (var outStreamDecrypted = new CryptoStream(outFs, transform, CryptoStreamMode.Write))
                    {
                        do
                        {
                            count = inFs.Read(data, 0, blockSizeBytes);
                            offset += count;
                            outStreamDecrypted.Write(data, 0, count);
                        } while (count > 0);

                        outStreamDecrypted.FlushFinalBlock();
                    }
                }
            }
        }

        private void buttonExportPublicKey_Click(object sender, EventArgs e)
        {
            // Zapisz klucz publiczny utworzony przez RSA
            // do pliku. Uwaga, zapisanie klucza
            // do pliku stanowi zagrozenie dla bezpieczenstwa.
            Directory.CreateDirectory(EncrFolder);
            using (var sw = new StreamWriter(PubKeyFile, false))
            {
                sw.Write(_rsa.ToXmlString(false));
            }
        }

        private void buttonImportPublicKe_Click(object sender, EventArgs e)
        {
            using (var sr = new StreamReader(PubKeyFile))
            {
                _cspp.KeyContainerName = KeyName;
                _rsa = new RSACryptoServiceProvider(_cspp);

                string keytxt = sr.ReadToEnd();
                _rsa.FromXmlString(keytxt);
                _rsa.PersistKeyInCsp = true;

                label1.Text = _rsa.PublicOnly
                    ? $"Key: {_cspp.KeyContainerName} - Public Only"
                    : $"Key: {_cspp.KeyContainerName} - Full Key Pair";
            }
        }

        private void buttonGetPrivateKey_Click(object sender, EventArgs e)
        {
            _cspp.KeyContainerName = KeyName;
            _rsa = new RSACryptoServiceProvider(_cspp)
            {
                PersistKeyInCsp = true
            };

            label1.Text = _rsa.PublicOnly
                ? $"Key: {_cspp.KeyContainerName} - Public Only"
                : $"Key: {_cspp.KeyContainerName} - Full Key Pair";
        }
    }
}