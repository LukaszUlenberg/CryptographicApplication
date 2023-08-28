﻿namespace CryptographicApplication
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            buttonCreateAsmKeys = new Button();
            label1 = new Label();
            buttonEncryptFile = new Button();
            buttonDecryptFile = new Button();
            buttonExportPublicKey = new Button();
            _encryptOpenFileDialog = new OpenFileDialog();
            _decryptOpenFileDialog = new OpenFileDialog();
            buttonGetPrivateKey = new Button();
            buttonImportPublicKe = new Button();
            SuspendLayout();
            // 
            // buttonCreateAsmKeys
            // 
            buttonCreateAsmKeys.Location = new Point(65, 60);
            buttonCreateAsmKeys.Name = "buttonCreateAsmKeys";
            buttonCreateAsmKeys.Size = new Size(219, 23);
            buttonCreateAsmKeys.TabIndex = 0;
            buttonCreateAsmKeys.Text = "Tworzenie klucza asymetrycznego";
            buttonCreateAsmKeys.UseVisualStyleBackColor = true;
            buttonCreateAsmKeys.Click += buttonCreateAsmKeys_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(65, 23);
            label1.Name = "label1";
            label1.Size = new Size(38, 15);
            label1.TabIndex = 1;
            label1.Text = "label1";
            // 
            // buttonEncryptFile
            // 
            buttonEncryptFile.Location = new Point(65, 104);
            buttonEncryptFile.Name = "buttonEncryptFile";
            buttonEncryptFile.Size = new Size(219, 23);
            buttonEncryptFile.TabIndex = 2;
            buttonEncryptFile.Text = "Szyfrowanie pliku";
            buttonEncryptFile.UseVisualStyleBackColor = true;
            buttonEncryptFile.Click += buttonEncryptFile_Click;
            // 
            // buttonDecryptFile
            // 
            buttonDecryptFile.Location = new Point(65, 150);
            buttonDecryptFile.Name = "buttonDecryptFile";
            buttonDecryptFile.Size = new Size(219, 23);
            buttonDecryptFile.TabIndex = 3;
            buttonDecryptFile.Text = "Odszyfrowywanie pliku";
            buttonDecryptFile.UseVisualStyleBackColor = true;
            buttonDecryptFile.Click += buttonDecryptFile_Click;
            // 
            // buttonExportPublicKey
            // 
            buttonExportPublicKey.Location = new Point(65, 200);
            buttonExportPublicKey.Name = "buttonExportPublicKey";
            buttonExportPublicKey.Size = new Size(219, 23);
            buttonExportPublicKey.TabIndex = 4;
            buttonExportPublicKey.Text = "Eksportowanie klucza publicznego";
            buttonExportPublicKey.UseVisualStyleBackColor = true;
            buttonExportPublicKey.Click += buttonExportPublicKey_Click;
            // 
            // _encryptOpenFileDialog
            // 
            _encryptOpenFileDialog.FileName = "_encryptOpenFileDialog";
            // 
            // _decryptOpenFileDialog
            // 
            _decryptOpenFileDialog.FileName = "_decryptOpenFileDialog";
            // 
            // buttonGetPrivateKey
            // 
            buttonGetPrivateKey.Location = new Point(65, 275);
            buttonGetPrivateKey.Name = "buttonGetPrivateKey";
            buttonGetPrivateKey.Size = new Size(219, 23);
            buttonGetPrivateKey.TabIndex = 5;
            buttonGetPrivateKey.Text = "Uzyskiwanie klucza prywatnego";
            buttonGetPrivateKey.UseVisualStyleBackColor = true;
            buttonGetPrivateKey.Click += buttonGetPrivateKey_Click;
            // 
            // buttonImportPublicKe
            // 
            buttonImportPublicKe.Location = new Point(65, 239);
            buttonImportPublicKe.Name = "buttonImportPublicKe";
            buttonImportPublicKe.Size = new Size(219, 23);
            buttonImportPublicKe.TabIndex = 6;
            buttonImportPublicKe.Text = "Importowanie klucza publicznego";
            buttonImportPublicKe.UseVisualStyleBackColor = true;
            buttonImportPublicKe.Click += buttonImportPublicKe_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(buttonImportPublicKe);
            Controls.Add(buttonGetPrivateKey);
            Controls.Add(buttonExportPublicKey);
            Controls.Add(buttonDecryptFile);
            Controls.Add(buttonEncryptFile);
            Controls.Add(label1);
            Controls.Add(buttonCreateAsmKeys);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonCreateAsmKeys;
        private Label label1;
        private Button buttonEncryptFile;
        private Button buttonDecryptFile;
        private Button buttonExportPublicKey;
        private OpenFileDialog _encryptOpenFileDialog;
        private OpenFileDialog _decryptOpenFileDialog;
        private Button buttonGetPrivateKey;
        private Button buttonImportPublicKe;
    }
}