using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetHdd
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(HardwareInfo.GetHDDSignature());
            Console.Read();
        }


        //public static String Encrypt(String plainText) //, String key)
        //{
        //    var plainBytes = Encoding.UTF8.GetBytes(plainText);
        //    return Convert.ToBase64String(Encrypt(plainBytes, GetRijndaelManaged("Hyweb@POS#E00T00")));
        //    //return Convert.ToBase64String(Encrypt(plainBytes, GetRijndaelManaged(key)));
        //}

        //private static RijndaelManaged GetRijndaelManaged(String secretKey)
        //{
        //    var keyBytes = new byte[16];
        //    var secretKeyBytes = Encoding.UTF8.GetBytes(secretKey);
        //    Array.Copy(secretKeyBytes, keyBytes, Math.Min(keyBytes.Length, secretKeyBytes.Length));
        //    return new RijndaelManaged
        //    {
        //        Mode = CipherMode.CBC,
        //        Padding = PaddingMode.PKCS7,
        //        KeySize = 128,
        //        BlockSize = 128,
        //        Key = keyBytes,
        //        IV = keyBytes
        //    };
        //}

    }
}
