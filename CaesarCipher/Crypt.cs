using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CaesarCipher
{
    class Crypt
    {
        private SymmetricAlgorithm curCrypto;
        private AesCryptoServiceProvider aesCrypto;
        private TripleDESCryptoServiceProvider tripleCrypto;
        private DESCryptoServiceProvider desCrypto;
        public Crypt()
        {
            aesCrypto = new AesCryptoServiceProvider();
            tripleCrypto = new TripleDESCryptoServiceProvider();
            desCrypto = new DESCryptoServiceProvider();
            //aesCrypto.GenerateKey();
            //aesCrypto.GenerateIV();
            //tripleCrypto.GenerateKey();
            //tripleCrypto.GenerateIV();
            //desCrypto.GenerateIV();
            //desCrypto.GenerateKey();
        }


        private void Selection(ref SymmetricAlgorithm crypto, string mode)
        {
            switch (mode)
            {
                case "CBC":
                    crypto.Mode = CipherMode.CBC;
                    break;
                case "CFB":
                    crypto.Mode = CipherMode.CFB;
                    break;
                case "CTS":
                    crypto.Mode = CipherMode.CTS;
                    break;
                case "ECB":
                    crypto.Mode = CipherMode.ECB;
                    break;
                case "OFB":
                    crypto.Mode = CipherMode.OFB;
                    break;
                default:
                    crypto.Mode = CipherMode.CBC;
                    break;
            }
        }

        public void SelectionCipher(ref SymmetricAlgorithm crypto, string algoritm)
        {
            switch (algoritm)
            {
                case "DES":
                    crypto = desCrypto;
                    break;
                case "TripleDES":
                    crypto = tripleCrypto;
                    break;
                case "AES":
                    crypto = aesCrypto;
                    break;
                default:
                    crypto = desCrypto;
                    break;
            }
        }
        
        public string Encrypt(string key,string IV, string opentext, string mode, string cipher)
        {
            SelectionCipher(ref curCrypto, cipher);
            Selection(ref curCrypto, mode);

            curCrypto.Key = UTF8Encoding.UTF8.GetBytes(key);
            curCrypto.IV = UTF8Encoding.UTF8.GetBytes(IV);

            FileStream fs = new FileStream(@"d:\crypt.txt", FileMode.OpenOrCreate, FileAccess.Write);
            
            CryptoStream cryptoStream = new CryptoStream(fs, curCrypto.CreateEncryptor(), CryptoStreamMode.Write);
            byte[] data = UTF8Encoding.UTF8.GetBytes(opentext);
            cryptoStream.Write(data, 0, data.Length);
            fs.Flush();
            cryptoStream.Flush();
            cryptoStream.Close();
            fs.Close();
            
            return fs.Name;
        }

        public string Decrypt(string key, string IV, string ciphertext, string mode)
        {
            curCrypto.Key = UTF8Encoding.UTF8.GetBytes(key);
            curCrypto.IV = UTF8Encoding.UTF8.GetBytes(IV);
            FileStream fs = new FileStream(@"d:\crypt.txt", FileMode.Open, FileAccess.Read);
            CryptoStream cryptoStream = new CryptoStream(fs,curCrypto.CreateDecryptor(),CryptoStreamMode.Read);
            StreamReader reader = new StreamReader(cryptoStream,Encoding.UTF8);
            string data = reader.ReadToEnd();
            fs.Flush();
            cryptoStream.Flush();
            fs.Close();
            cryptoStream.Close();
            return data;
        }
    }
}
