using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography;
using System.IO;
using System.Text;

public partial class TestEncrypt : System.Web.UI.Page
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {

    }



    /// <summary>
    /// aes256
    /// </summary>
    /// <param name="SourceStr"></param>
    /// <param name="CryptoKey"></param>
    /// <returns></returns>
    public static string aesEncryptBase64(string SourceStr, string CryptoKey)
    {
        string encrypt = "";
        try
        {
            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            SHA256CryptoServiceProvider sha256 = new SHA256CryptoServiceProvider();
            byte[] key = sha256.ComputeHash(Encoding.UTF8.GetBytes(CryptoKey));
            byte[] iv = md5.ComputeHash(Encoding.UTF8.GetBytes(CryptoKey));
            aes.Key = key;
            aes.IV = iv;

            byte[] dataByteArray = Encoding.UTF8.GetBytes(SourceStr);
            using (MemoryStream ms = new MemoryStream())
            using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
            {
                cs.Write(dataByteArray, 0, dataByteArray.Length);
                cs.FlushFinalBlock();
                encrypt = Convert.ToBase64String(ms.ToArray());
            }
        }
        catch (Exception e)
        {
            //System.Windows.Forms.MessageBox.Show(e.Message);
        }
        return encrypt;
    }


    /// <summary>
    /// AES加密
    /// 加密模式: CBC
    /// 填充: PKCS7
    /// 密鑰長度: 128
    /// 密鑰: ASPX設定為 AAES
    /// 偏移量: AAES
    /// </summary>
    /// <param name="content">輸入字串</param>
    /// <param name="key">key</param>
    /// <returns></returns>
    public static string AesEncrypt(string content, string key)
    {
        byte[] bEncArray = System.Text.Encoding.UTF8.GetBytes(content);
        RijndaelManaged AesCipher = new RijndaelManaged();
        AesCipher.KeySize = 128;
        AesCipher.BlockSize = 128;
        AesCipher.Mode = CipherMode.CBC;
        AesCipher.Padding = PaddingMode.PKCS7;
        AesCipher.IV = System.Text.Encoding.UTF8.GetBytes(key);
        AesCipher.Key = System.Text.Encoding.UTF8.GetBytes(key);
        ICryptoTransform crypto = AesCipher.CreateEncryptor();
        byte[] cipherText = crypto.TransformFinalBlock(bEncArray, 0, bEncArray.Length);
        return Convert.ToBase64String(cipherText, 0, cipherText.Length);
    }


    /// <summary>
    /// AES解密
    /// </summary>
    /// <param name="content">輸入字串</param>
    /// <param name="key">key</param>
    /// <returns></returns>
    public static string AESDecrypt(string content, string key)
    {
        try
        {
            byte[] bDecArray = Convert.FromBase64String(content);
            RijndaelManaged AesCipher = new RijndaelManaged();
            AesCipher.KeySize = 128;
            AesCipher.BlockSize = 128;
            AesCipher.Mode = CipherMode.CBC;
            AesCipher.Padding = PaddingMode.PKCS7;
            AesCipher.IV = System.Text.Encoding.UTF8.GetBytes(key);
            AesCipher.Key = System.Text.Encoding.UTF8.GetBytes(key);
            ICryptoTransform crypto = AesCipher.CreateDecryptor();
            byte[] cipherText = crypto.TransformFinalBlock(bDecArray, 0, bDecArray.Length);
            return System.Text.Encoding.UTF8.GetString(cipherText);
        }
        catch (Exception e)
        {
            return e.ToString();
        }
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button1_Click(object sender, EventArgs e)
    {
        string AesEnString_Token = AesEncrypt(TextBox1.Text, "TEST");
        Label1.Text = AesEnString_Token;
    }


    /// <summary>
    /// hello,AES 
    /// 加密後為 2gzpGMsRg2GyFrHAgkG3ig==
    /// 
    /// 線上加解密參考
    /// https://tool.lmeee.com/jiami/aes
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Button2_Click(object sender, EventArgs e)
    {
        string a1 = TextBox2.Text;
        //string a2 = "gNNQtA7F0IX7Cqpz7x8ogw==";
        string AesAESDecrypt = AESDecrypt(a1, "TEST");
        Label2.Text = AesAESDecrypt;
    }
}