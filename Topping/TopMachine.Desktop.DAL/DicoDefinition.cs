using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;

namespace TopMachine.Topping.DAL
{
    public class DicoDefinition
    {
        public static void GetDefinition(Dico d)
        {
            if (d.DefinitionCrypted != null)
            {
                byte[] cpt = Decrypt(d.DefinitionCrypted, "T0pM@ch1n€");
                string def = System.Text.UTF32Encoding.Default.GetString(cpt);
                d.Definition = def;
            }
        }
        private static  byte[] Decrypt(byte[] cipherData,
                                byte[] Key, byte[] IV)
        {
            MemoryStream ms = new MemoryStream();


            Rijndael alg = Rijndael.Create();
            alg.Key = Key;
            alg.IV = IV;
            alg.Padding = PaddingMode.ISO10126;


            CryptoStream cs = new CryptoStream(ms,
                alg.CreateDecryptor(), CryptoStreamMode.Write);

            // Write the data and make it do the decryption 
            cs.Write(cipherData, 0, cipherData.Length);


            cs.Close();

            byte[] decryptedData = ms.ToArray();

            return decryptedData;
        }

        private static  byte[] Decrypt(byte[] cipherData, string Password)
        {
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(Password,
                new byte[] {0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 
            0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76});


            return Decrypt(cipherData, pdb.GetBytes(32), pdb.GetBytes(16));
        }
    }
}
