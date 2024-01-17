using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;


namespace SESWeb
{
    public class AESCrypt
    {
        #region Private/Protected Member Variables

        private readonly ICryptoTransform _decryptor;
        private readonly ICryptoTransform _encryptor;
        private static readonly byte[] IV = Encoding.UTF8.GetBytes("EmpResposeKeyPas");
        private readonly byte[] _password;
        private readonly AesManaged _cipher;

        #endregion

        #region Private/Protected Properties

        private ICryptoTransform Decryptor { get { return _decryptor; } }
        private ICryptoTransform Encryptor { get { return _encryptor; } }

        #endregion

        #region Constructor

        public AESCrypt(string password)
        {
            //Encode digest
            var md5 = new MD5CryptoServiceProvider();
            _password = md5.ComputeHash(Encoding.ASCII.GetBytes(password));

            //Initialize objects
            _cipher = new AesManaged();
            _decryptor = _cipher.CreateDecryptor(_password, IV);
            _encryptor = _cipher.CreateEncryptor(_password, IV);
        }

        #endregion

        #region Public Methods

        public string Decrypt(string text)
        {
            try
            {
                byte[] input = Convert.FromBase64String(text);

                var newClearData = Decryptor.TransformFinalBlock(input, 0, input.Length);
                return Encoding.ASCII.GetString(newClearData);
            }
            catch (ArgumentException ae)
            {
                return "inputCount uses an invalid value or inputBuffer has an invalid offset length. " + ae;
            }
            catch (ObjectDisposedException oe)
            {
                return "The object has already been disposed." + oe;
            }
        }


        #endregion
    }
}