using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace CMWA.Packager.Tools
{
    public class RegistrationKeys
    {

        public static string GetVerificationString(string key)
        {
            return string.Format("{0:X8}", key.GetHashCode());
        }

        internal static byte[] GetRegistrationKeyBytes(string productKey)
        {
            /*byte[] b = DeviceInfo.GetDeviceIDBytes();
            byte[] o = new byte[b.Length];

            RandomNumberGenerator rng = RandomNumberGenerator.Create();
            o = new PasswordDeriveBytes(productKey, b).GetBytes(16);

            return o;*/

            return new byte[0];
        }

        public static string GetRegistrationKey(string productKey)
        {
            byte[] b = RegistrationKeys.GetRegistrationKeyBytes(productKey);
            byte[] o = new byte[b.Length];
            
            
            o = new PasswordDeriveBytes(productKey, b).GetBytes(16);

            StringBuilder sb = new StringBuilder();
            
            sb.Append(String.Format("{0:X8}-{1:X4}-{2:X4}-{3:X4}-",
                        BitConverter.ToInt32(o, 0),
                        BitConverter.ToInt16(o, 4),
                        BitConverter.ToInt16(o, 6),
                        BitConverter.ToInt16(o, 8)));

            for (int i = 10;
                 i < o.Length && i < 16;
                 i++)
            {
                sb.Append(String.Format("{0:X2}", o[i]));
            }

            return sb.ToString(); 
             
        }
    }
}
