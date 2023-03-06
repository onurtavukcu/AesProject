    
using System;
using System.Collections;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace AesForKFProject
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[]? IVector = null;

            var key = 1234567891234567;

            var converted = ByteOperations(key.ToString());

            if (converted.Length < 0)
            {
                throw new Exception("Data Cannot Converted The String!");
            }

            var encriptKey = Encoding.Unicode.GetBytes(key.ToString());

            try
            {
                string plainText = "Onur Bahadır Tavukçu";

                Console.WriteLine(plainText);

                var bytePlainText = Encoding.Unicode.GetBytes(plainText);

                var crypted = EncryptAes(bytePlainText, encriptKey, IVector);

                if (crypted is null) // isFailure eklenecek
                {
                    throw new Exception("Data Cannot Encrypted!");
                }

                //Console.WriteLine(Encoding.Unicode.GetString(crypted));
                Console.WriteLine(Convert.ToBase64String(crypted));

                var decrypted = DecryptAes(crypted, encriptKey, bytePlainText, IVector);

                if (decrypted is null) // isFailure eklenecek
                {
                    throw new Exception("Data Cannot Derypted!");
                }


                Console.WriteLine(decrypted);
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }


        public static byte[] EncryptAes(byte[] plainText, byte[] key, byte[]? IVector = null)
        {
            SymmetricAlgorithm cryptData = Aes.Create();

            byte[] encrypted;

            cryptData.KeySize = 128;

            cryptData.BlockSize = 128;

            cryptData.Key = key;

            if (IVector is not null)
                cryptData.IV = IVector;

            ICryptoTransform encryptor = cryptData.CreateEncryptor(cryptData.Key, cryptData.IV);

            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(plainText);
                    }
                    encrypted = msEncrypt.ToArray();
                }
            }
            return encrypted;
        }

        public static string DecryptAes(byte[] cipherText, byte[] key, byte[] plaintext, byte[]? IVector = null)
        {
            SymmetricAlgorithm cryptData = Aes.Create();

            //string plaintext = null;

            cryptData.KeySize = 128;

            cryptData.BlockSize = 128;

            cryptData.Key = key;

            if (IVector is not null)
                cryptData.IV = IVector;

            ICryptoTransform decryptor = cryptData.CreateDecryptor(cryptData.Key, cryptData.IV);

            using (MemoryStream msDecrypt = new MemoryStream(cipherText))
            {
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                    {
                        return srDecrypt.ReadLine();
                      
                    }
                }                
            }
        }
















        //public static byte[] EncryptAes(byte[] plainText, byte[] key, byte[]? IVector = null)
        //{
        //SymmetricAlgorithm cryptData = Aes.Create();

        //cryptData.KeySize = 128;

        //    cryptData.BlockSize = 128;

        //    cryptData.Key = key;

        //    if (IVector is not null)
        //        cryptData.IV = IVector;

        //    try
        //    {
        //        return cryptData.EncryptEcb(plainText, PaddingMode.PKCS7);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //public static byte[] DecryptAes(byte[] ciphertext, byte[] key, byte[]? IVector = null)
        //{
        //    SymmetricAlgorithm decryptData = Aes.Create();

        //    decryptData.KeySize = 128;

        //    decryptData.BlockSize = 128;

        //    decryptData.Key = key;

        //    if (IVector is not null)
        //        decryptData.IV = IVector;

        //    try
        //    {
        //        return decryptData.DecryptEcb(ciphertext, PaddingMode.PKCS7);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        public static byte[] ByteOperations(string item)
        {
            var byteArray = Encoding.ASCII.GetBytes(item);

            var length = byteArray.Length;


            if (length < 15)
            {
                var willAddByte = 15 - length;

                byte[] willAddByteArray = new byte[willAddByte];


                for (int i = 0; i < willAddByte; i++)
                {
                    willAddByteArray[i] = 0;
                }

                return (byte[])byteArray.Concat(willAddByteArray);
            }

            if (length > 15)
            {
                byte[] tempByteArray = new byte[15];

                for (int i = 0; i < 15; i++)
                {
                    tempByteArray[i] = byteArray[i];
                }

                return tempByteArray;
            }

            return byteArray;

        }



        //}        //public static byte[] ByteOperations<T>(T[] item) 
        //{
        //    var convertedString = Encoding.Unicode.GetBytes(item.ToString());

        //    return convertedString;
        //}

    }
}