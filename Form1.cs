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

        // Zmienne �cie�ek dla folderu �r�d�owego, szyfruj�cego i
        // folder�w deszyfrowania. Musz� ko�czy� si� odwrotnym uko�nikiem.
        const string EncrFolder = @"c:\Encrypt\";
        const string DecrFolder = @"c:\Decrypt\";
        const string SrcFolder = @"c:\docs\";

        // Plik klucza publicznego
        const string PubKeyFile = @"c:\encrypt\rsaPublicKey.txt";

        // Nazwa kontenera kluczy dla
        // pary warto�ci klucza prywatnego/publicznego.
        const string KeyName = "Key01";

        public Form1()
        {
            InitializeComponent();
        }

        private void buttonCreateAsmKeys_Click(object sender, EventArgs e)
        {
            // Przechowuje par� kluczy w kontenerze kluczy.
            _cspp.KeyContainerName = KeyName;
            _rsa = new RSACryptoServiceProvider(_cspp)
            {
                PersistKeyInCsp = true
            };

            label1.Text = _rsa.PublicOnly
                ? $"Key: {_cspp.KeyContainerName} - Tylko publiczny"
                : $"Key: {_cspp.KeyContainerName} - Pe�na para kluczy";
        }

        private void buttonEncryptFile_Click(object sender, EventArgs e)
        {
            if (_rsa is null)
            {
                MessageBox.Show("Klucz nie zosta� ustawiony.");
            }
            else
            {
                // Wy�wietl okno dialogowe, aby wybra� plik do zaszyfrowania.
                _encryptOpenFileDialog.InitialDirectory = SrcFolder;
                if (_encryptOpenFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string fName = _encryptOpenFileDialog.FileName;
                    if (fName != null)
                    {
                        // Przeka� nazw� pliku bez �cie�ki.
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

            // U�yj RSACryptoServiceProvider do
            // zaszyfrowania klucza AES.
            // rsa zosta� wcze�niej utworzony:
            // rsa = new RSACryptoServiceProvider(cspp);
            byte[] keyEncrypted = _rsa.Encrypt(aes.Key, false);

            // Utw�rz tablice bajt�w zawieraj�ce
            // warto�ci d�ugo�ci klucza i IV.
            int lKey = keyEncrypted.Length;
            byte[] LenK = BitConverter.GetBytes(lKey);
            int lIV = aes.IV.Length;
            byte[] LenIV = BitConverter.GetBytes(lIV);

            // Zapis nast�puj�cych danych do FileStream
            // dla zaszyfrowanego pliku (outFs):
            // - d�ugo�� klucza
            // - d�ugo�� IV
            // - zaszyfrowany klucz
            // - IV
            // - zaszyfrowana zawarto�� szyfru

            // Zmie� rozszerzenie pliku na ".enc"
            string outFile = Path.Combine(EncrFolder, Path.ChangeExtension(file.Name, ".enc"));

            using (var outFs = new FileStream(outFile, FileMode.Create))
            {
                outFs.Write(LenK, 0, 4);
                outFs.Write(LenIV, 0, 4);
                outFs.Write(keyEncrypted, 0, lKey);
                outFs.Write(aes.IV, 0, lIV);

                // Teraz zapisz tekst zaszyfrowany przy u�yciu
                // CryptoStream do szyfrowania.
                using (var outStreamEncrypted = new CryptoStream(outFs, transform, CryptoStreamMode.Write))
                {
                    // Szyfruj�c fragment po fragmencie
                    // jednorazowo, mo�na zaoszcz�dzi� pami��
                    // i pomie�ci� du�e pliki.
                    int count = 0;
                    int offset = 0;

                    // blockSizeBytes mo�e mie� dowolny rozmiar.
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
                MessageBox.Show("Klucz nie zosta� ustawiony.");
            }
            else
            {
                // Wy�wietl okno dialogowe, aby wybra� zaszyfrowany plik.
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

            // Utw�rz tablice bajt�w, aby uzyska� d�ugo��
            // zaszyfrowanego klucza i IV.
            // Warto�ci te zosta�y zapisane jako 4 bajty ka�da
            // na pocz�tku zaszyfrowanego pakietu.
            byte[] LenK = new byte[4];
            byte[] LenIV = new byte[4];

            // Konstruujemy nazw� odszyfrowanego pliku.
            string outFile = Path.ChangeExtension(file.FullName.Replace("Encrypt", "Decrypt"), ".txt");

            // U�yj obiekt�w FileStream do odczytania zaszyfrowanego pliku
            // pliku (inFs) i zapisania odszyfrowanego pliku (outFs).
            using (var inFs = new FileStream(file.FullName, FileMode.Open))
            {
                inFs.Seek(0, SeekOrigin.Begin);
                inFs.Read(LenK, 0, 3);
                inFs.Seek(4, SeekOrigin.Begin);
                inFs.Read(LenIV, 0, 3);

                // Konwersja d�ugo�ci na warto�ci ca�kowite.
                int lenK = BitConverter.ToInt32(LenK, 0);
                int lenIV = BitConverter.ToInt32(LenIV, 0);

                // Okre�lenie pozycji pocz�tkowej
                // tekstu zaszyfrowanego (startC)
                // i jego d�ugo�� (lenC).
                int startC = lenK + lenIV + 8;
                int lenC = (int)inFs.Length - startC;

                // Tworzenie tablic bajt�w dla
                // zaszyfrowanego klucza Aes,
                // IV i tekstu zaszyfrowanego.
                byte[] KeyEncrypted = new byte[lenK];
                byte[] IV = new byte[lenIV];

                // Wyodr�bnienie klucza i IV
                // zaczynaj�c od indeksu 8
                // po warto�ciach d�ugo�ci.
                inFs.Seek(8, SeekOrigin.Begin);
                inFs.Read(KeyEncrypted, 0, lenK);
                inFs.Seek(8 + lenK, SeekOrigin.Begin);
                inFs.Read(IV, 0, lenIV);

                Directory.CreateDirectory(DecrFolder);
                // U�yj RSACryptoServiceProvider
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

                    // blockSizeBytes mo�e mie� dowolny rozmiar.
                    int blockSizeBytes = aes.BlockSize / 8;
                    byte[] data = new byte[blockSizeBytes];

                    // Odszyfrowuj�c jeden fragment na raz,
                    // mo�na zaoszcz�dzi� pami�� i
                    // pomie�ci� du�e pliki.

                    // Rozpocznij od pocz�tku
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
            // klucza do pliku stanowi zagro�enie dla bezpiecze�stwa.
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