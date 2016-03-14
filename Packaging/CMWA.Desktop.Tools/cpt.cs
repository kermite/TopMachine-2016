using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;


namespace CMWA.Packager.Tools.Bytes
{

    public class cpt
    {
        private Byte[] GetBytesFromPassword()
        {
            return Convert.FromBase64String("a4ndawOTGYtIm4RniM7u6Q==");
        }

        public byte[] GetRegistrationKeyBytes(string productKey)
        {
            return RegistrationKeys.GetRegistrationKeyBytes(productKey);
        }


        string password = "";

        private static byte[] EncryptExtracted()
        {
            return new byte[]
        		            {
        		                0x49, 0x76, 0x61, 0x6e,
        		                0x20, 0x4d, 0x65, 0x64,
        		                0x76, 0x65, 0x64, 0x65,
        		                0x76, 0xaa, 0x12, 0x13 };
        }

        internal byte[] GetPassword(int type, string key)
        {
            if (type == 0) return GetBytesFromPassword();

            return GetRegistrationKeyBytes(key);
        }

        #region Reading files
        public Stream ReadFromStream(Stream s, string key, int type)
        {
            byte[] b = GetPassword(type, key);
            return DecryptStream(s, b, EncryptExtracted());
        }

        public Stream WriteFromStream(Stream s, string key, int type)
        {
            byte[] b = GetPassword(type, key);
            return DecryptStream(s, b, EncryptExtracted());
        }

        public byte[] ReadFile(string fn, string key, int type)
        {
            byte[] b = GetPassword(type, key);
            return ReadFile(fn, b);
        }

        public byte[] ReadFile(string fn, byte[] bytes)
        {
            int len = (int)new FileInfo(fn).Length;
            Byte[] b = new Byte[len];
            FileStream fs = File.OpenRead(fn);
            fs.Read(b, 0, len);
            Byte[] boutput = new Byte[len];
            boutput = Decrypt(b, bytes, EncryptExtracted());
            return boutput;
        }
        #endregion

        #region Writing Files
        public byte[] WriteFile(string fn, string output, string key, int type)
        {
            byte[] b = GetPassword(type, key);;
            return WriteFile(fn, output, b);
        }

        internal byte[] WriteFile(string fn, string output, byte[] bytes)
        {
            int len = (int)new FileInfo(fn).Length;
          //  int nlen = (int)(Math.Ceiling((double)len / 16)) * 16;
            Byte[] b = new Byte[len];
            Byte[] boutput;

            FileStream fs = File.OpenRead(fn);
            fs.Read(b, 0, len);

            boutput = Encrypt(b, bytes, EncryptExtracted());

            FileStream outp = File.OpenWrite(output);
            outp.Write(boutput, 0, boutput.Length);
            outp.Close();
            return b;
        }

        public byte[] WriteFile(string output, byte[] input)
        {
            byte[] b = GetPassword(0, ""); ;
            return WriteFile(output, input, b);
        }

        internal byte[] WriteFile(string output, byte[] input, byte[] pwd)
        {

            Byte[] boutput;

            boutput = Encrypt(input, pwd, EncryptExtracted());

            FileStream outp = File.OpenWrite(output);
            outp.Write(boutput, 0, boutput.Length);
            outp.Close();
            return boutput;
        }
        #endregion

        #region Reading and Writing Bytes
        private Stream WriteStream(Stream s,
                                byte[] Key, byte[] IV)
        {

            return null;
        }

        private Stream DecryptStream(Stream s,
                                byte[] Key, byte[] IV)
        {

            return null; 
        }

        private byte[] Encrypt(byte[] clearData, byte[] Key, byte[] IV)
        {

            byte[] encryptedData = new byte[clearData.Length];
            for (int x = 0; x < clearData.Length; x++)
            {
                encryptedData[x] = (byte) (255 - clearData[x]);
            }
            return encryptedData;
        }

        private byte[] Decrypt(byte[] cipherData,
                                byte[] Key, byte[] IV)
        {
            byte[] decryptedData = new byte[cipherData.Length];
            for (int x = 0; x < cipherData.Length; x++)
            {
                decryptedData[x] = (byte)(255 - cipherData[x]);
            }
            return decryptedData;
   
        }

        #endregion
    }
}
