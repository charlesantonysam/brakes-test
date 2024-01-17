 using Microsoft.VisualBasic;
    using System;
 using System.Web;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Diagnostics;
    using System.Security;
    using System.Security.Cryptography;
using System.Web.Security;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

using System.Text;
namespace SESWeb
{
    public class BISecurity
    {
        private static string _Key="", _PassPhrase="";
        public static string EncryptString1(string Message)
        {
            return Message;
        }
        public static string DecryptString1(string Message)
        {
            return Message;
        }
        public static string Decrypt(string text)
        {
           // GetSecurityParameters();
            return Decrypt(text, _Key);
        }
        public static string Encrypt(string text)
        {
           // GetSecurityParameters();
            return Encrypt(text, _Key);
        }
        public static string DecryptUrlSafe(string text)
        {
            //GetSecurityParameters();
            return Decrypt(text.Replace("@", "+"), _Key);
        }
        public static string EncryptUrlSafe(string text)
        {
           // GetSecurityParameters();
            return Encrypt(text, _Key).Replace("+", "@");
        }
        public static string EncryptString(string Message)
        {
            byte[] Results = null;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();
            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(_PassPhrase));
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;
            byte[] DataToEncrypt = UTF8.GetBytes(Message);
            try
            {
                ICryptoTransform Encryptor = TDESAlgorithm.CreateEncryptor();
                Results = Encryptor.TransformFinalBlock(DataToEncrypt, 0, DataToEncrypt.Length);
            }
            finally
            {
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }
            return Convert.ToBase64String(Results);
        }
        public static string DecryptString(string Message)
        {
            byte[] Results = null;
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();
            MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
            byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(_PassPhrase));
            TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();
            TDESAlgorithm.Key = TDESKey;
            TDESAlgorithm.Mode = CipherMode.ECB;
            TDESAlgorithm.Padding = PaddingMode.PKCS7;
            byte[] DataToDecrypt = Convert.FromBase64String(Message);
            try
            {
                ICryptoTransform Decryptor = TDESAlgorithm.CreateDecryptor();
                Results = Decryptor.TransformFinalBlock(DataToDecrypt, 0, DataToDecrypt.Length);
            }
            finally
            {
                TDESAlgorithm.Clear();
                HashProvider.Clear();
            }
            return UTF8.GetString(Results);
        }


        public static string Decrypt(string textToDecrypt, string key)
        {
            RijndaelManaged rijndaelCipher = new RijndaelManaged();
            rijndaelCipher.Mode = CipherMode.CBC;
            rijndaelCipher.Padding = PaddingMode.PKCS7;

            rijndaelCipher.KeySize = 0x80;
            rijndaelCipher.BlockSize = 0x80;
            byte[] encryptedData = Convert.FromBase64String(textToDecrypt);
            byte[] pwdBytes = Encoding.UTF8.GetBytes(key);
            byte[] keyBytes = new byte[0x10];
            int len = pwdBytes.Length;
            if (len > keyBytes.Length)
            {
                len = keyBytes.Length;
            }
            Array.Copy(pwdBytes, keyBytes, len);
            rijndaelCipher.Key = keyBytes;
            rijndaelCipher.IV = keyBytes;
            byte[] plainText = rijndaelCipher.CreateDecryptor().TransformFinalBlock(encryptedData, 0, encryptedData.Length);
            return Encoding.UTF8.GetString(plainText);
        }

        public static string Encrypt(string textToEncrypt, string key)
        {
            RijndaelManaged rijndaelCipher = new RijndaelManaged();
            rijndaelCipher.Mode = CipherMode.CBC;
            rijndaelCipher.Padding = PaddingMode.PKCS7;

            rijndaelCipher.KeySize = 0x80;
            rijndaelCipher.BlockSize = 0x80;
            byte[] pwdBytes = Encoding.UTF8.GetBytes(key);
            byte[] keyBytes = new byte[0x10];
            int len = pwdBytes.Length;
            if (len > keyBytes.Length)
            {
                len = keyBytes.Length;
            }
            Array.Copy(pwdBytes, keyBytes, len);
            rijndaelCipher.Key = keyBytes;
            rijndaelCipher.IV = keyBytes;
            ICryptoTransform transform = rijndaelCipher.CreateEncryptor();
            byte[] plainText = Encoding.UTF8.GetBytes(textToEncrypt);
            return Convert.ToBase64String(transform.TransformFinalBlock(plainText, 0, plainText.Length));
        }
        public string ErrorPageCode()
        {
            return "<body style=\"background:url('/Common/Images/erbg.png') no-repeat black 50% 100px  ;overflow:hidden;\"></body>";
        }

        public Dictionary<string, string> QueryStrings = new Dictionary<string, string>();
        public void setQueryStringToDecrypt(string EncryptedQueryString)
        {
            string dqs = DecryptUrlSafe(EncryptedQueryString);
            string[] dqsa = dqs.Split('|');
            QueryStrings.Clear();
            for (int i = 0; i < dqsa.Length; i++)
            {
                string[] qs = dqsa[i].Split('~');
                QueryStrings.Add(qs[0], qs[1]);
            }
        }
        public string getDecryptedQueryString(string QueryString)
        {
            if (QueryStrings.ContainsKey(QueryString))
            {
                return QueryStrings[QueryString];
            }
            else
            {
                return null;
            }
        }
        public clsEncryptedQueryStrings QueryStringsToEncrypt = new clsEncryptedQueryStrings();
        public class clsEncryptedQueryStrings
        {
            private Dictionary<string, string> eqs = new Dictionary<string, string>();
            private string qs = "";
            public void Add(string QueryStringName, string QueryStringValue)
            {
                qs += (qs != "" ? "|" : "") + QueryStringName + "~" + QueryStringValue;
            }
            public void Clear()
            {
                qs = "";
            }
            public string getEncryptedString()
            {
                return EncryptUrlSafe(qs);
            }
        }
        //private static void GetSecurityParameters()
        //{
        //    try
        //    {
        //        SqlParameter[] _Params = new SqlParameter[2];
        //        _Params[0] = new SqlParameter("@PassPhrase", SqlDbType.VarChar, 100);
        //        _Params[0].Direction = ParameterDirection.InputOutput;
        //        _Params[1] = new SqlParameter("@Key", SqlDbType.VarChar, 100);
        //        _Params[1].Direction = ParameterDirection.InputOutput;
        //        DataSet dsData = SqlHelper.ExecuteDataset(SqlHelper.strConnBI, CommandType.StoredProcedure, "sp_UM_GetSecurityParameters", _Params);
        //        _PassPhrase = _Params[0].Value.ToString();
        //        _Key = _Params[1].Value.ToString();
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

    }
}