using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Win32;


namespace Setup.Util
{
    public class CommonFunction
    {
        public static DateTime ExpiryDate { get; set; }

        public static bool ActivationKeyChecking(string pcInfo = "")
        {
            RegistryKey myKey = Registry.CurrentUser.OpenSubKey(@"Software/QC4", true);
            bool IsPro = true;
            if (myKey != null)
            {
                string key = myKey.GetValue("qc4Key").ToString();
                string info = Cryptography.Decrypt(key, "QC1234");
                string[] spltAry = info.Split('\t');
                string UserDomainName = Environment.UserDomainName;
                string UserName = Environment.UserName;
                string MachineName = Environment.MachineName;
                List<string> macAddresses = NetworkInterface
.GetAllNetworkInterfaces()
.Where(nic => nic.NetworkInterfaceType != NetworkInterfaceType.Loopback && nic.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
.Select(nic => nic.GetPhysicalAddress().ToString()).ToList();
                if (spltAry.Length == 10)
                {
                    if (spltAry[0] != "" && spltAry[0] != UserDomainName)
                        IsPro = false;
                    else if (spltAry[1] != "" && spltAry[1] != UserName)
                        IsPro = false;
                    else if (spltAry[2] != "" && spltAry[2] != MachineName)
                        IsPro = false;
                    else if (spltAry[3] != "" && validateMacAdrs(spltAry[3], macAddresses))
                        IsPro = false;
                    else if (spltAry[4] != "QC4STDPRO")
                        IsPro = false;
                    else if (spltAry[5] == null || spltAry[5] == "")
                        IsPro = false;
                    else if (spltAry[5] != null || spltAry[5] != "")
                    {
                        DateTime expDate;
                        DateTime.TryParseExact(spltAry[5], "dd-MM-yyyy",
                       CultureInfo.InvariantCulture,
                       DateTimeStyles.None, out expDate);
                        ExpiryDate = expDate;
                        if (expDate.ToString() == "01/01/0001 00:00:00")
                            IsPro = false;
                        else
                        {
                            if (expDate < DateTime.Now.Date)
                                IsPro = false;
                        }
                    }
                }
                else
                    IsPro = false;
            }

            return IsPro;
        }

        private static bool validateMacAdrs(string spltAry, List<string> macAddresses)
        {
            for (int i = 0; i < macAddresses.Count; i++)
            {
                if (spltAry == macAddresses[i])
                {
                    return false;
                }
            }
            return true;
        }
    }

    public static class Cryptography
    {
        #region Settings

        private static int _iterations = 2;
        private static int _keySize = 256;

        private static string _hash = "SHA1";
        private static string _salt = "aselrias38490a32"; // Random

        #endregion

        public static string Encrypt(string value, string password)
        {
            return Encrypt<AesManaged>(value, password);
        }
        public static string Encrypt<T>(string value, string password)
                where T : SymmetricAlgorithm, new()
        {
            try
            {
                string _vector = RandomStr();
                byte[] vectorBytes = ASCIIEncoding.ASCII.GetBytes(_vector);
                byte[] saltBytes = ASCIIEncoding.ASCII.GetBytes(_salt);
                byte[] valueBytes = UTF8Encoding.UTF8.GetBytes(value);

                byte[] encrypted;
                using (T cipher = new T())
                {
                    PasswordDeriveBytes _passwordBytes =
                        new PasswordDeriveBytes(password, saltBytes, _hash, _iterations);
                    byte[] keyBytes = _passwordBytes.GetBytes(_keySize / 8);

                    cipher.Mode = CipherMode.CBC;

                    using (ICryptoTransform encryptor = cipher.CreateEncryptor(keyBytes, vectorBytes))
                    {
                        using (MemoryStream to = new MemoryStream())
                        {
                            using (CryptoStream writer = new CryptoStream(to, encryptor, CryptoStreamMode.Write))
                            {
                                writer.Write(valueBytes, 0, valueBytes.Length);
                                writer.FlushFinalBlock();
                                encrypted = to.ToArray();
                            }
                        }
                    }
                    cipher.Clear();
                }
                byte[] valueBytes2 = UTF8Encoding.UTF8.GetBytes(Convert.ToBase64String(encrypted));
                using (T cipher = new T())
                {
                    PasswordDeriveBytes _passwordBytes =
                        new PasswordDeriveBytes(password, saltBytes, _hash, _iterations);
                    byte[] keyBytes = _passwordBytes.GetBytes(_keySize / 8);

                    cipher.Mode = CipherMode.CBC;

                    using (ICryptoTransform encryptor = cipher.CreateEncryptor(keyBytes, vectorBytes))
                    {
                        using (MemoryStream to = new MemoryStream())
                        {
                            using (CryptoStream writer = new CryptoStream(to, encryptor, CryptoStreamMode.Write))
                            {
                                writer.Write(valueBytes2, 0, valueBytes2.Length);
                                writer.FlushFinalBlock();
                                encrypted = to.ToArray();
                            }
                        }
                    }
                    cipher.Clear();
                }
                return Convert.ToBase64String(encrypted).Insert(10, _vector);
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        public static string Decrypt(string value, string password)
        {
            return Decrypt<AesManaged>(value, password);
        }

        public static string Decrypt<T>(string value, string password) where T : SymmetricAlgorithm, new()
        {
            try
            {
                string key = value.Remove(10, 16);
                string _vector = value.Substring(10, 16);
                byte[] vectorBytes = ASCIIEncoding.ASCII.GetBytes(_vector);
                byte[] saltBytes = ASCIIEncoding.ASCII.GetBytes(_salt);
                byte[] valueBytes = Convert.FromBase64String(key);

                byte[] decrypted;
                int decryptedByteCount = 0;
                using (T cipher = new T())
                {
                    PasswordDeriveBytes _passwordBytes = new PasswordDeriveBytes(password, saltBytes, _hash, _iterations);
                    byte[] keyBytes = _passwordBytes.GetBytes(_keySize / 8);

                    cipher.Mode = CipherMode.CBC;

                    try
                    {
                        using (ICryptoTransform decryptor = cipher.CreateDecryptor(keyBytes, vectorBytes))
                        {
                            using (MemoryStream from = new MemoryStream(valueBytes))
                            {
                                using (CryptoStream reader = new CryptoStream(from, decryptor, CryptoStreamMode.Read))
                                {
                                    decrypted = new byte[valueBytes.Length];
                                    decryptedByteCount = reader.Read(decrypted, 0, decrypted.Length);
                                }
                            }
                        }
                    }
                    catch
                    {
                        return String.Empty;
                    }

                    cipher.Clear();
                }
                byte[] valueBytes2 = Convert.FromBase64String(Encoding.UTF8.GetString(decrypted, 0, decryptedByteCount));
                using (T cipher = new T())
                {
                    PasswordDeriveBytes _passwordBytes = new PasswordDeriveBytes(password, saltBytes, _hash, _iterations);
                    byte[] keyBytes = _passwordBytes.GetBytes(_keySize / 8);

                    cipher.Mode = CipherMode.CBC;

                    try
                    {
                        using (ICryptoTransform decryptor = cipher.CreateDecryptor(keyBytes, vectorBytes))
                        {
                            using (MemoryStream from = new MemoryStream(valueBytes2))
                            {
                                using (CryptoStream reader = new CryptoStream(from, decryptor, CryptoStreamMode.Read))
                                {
                                    decrypted = new byte[valueBytes2.Length];
                                    decryptedByteCount = reader.Read(decrypted, 0, decrypted.Length);
                                }
                            }
                        }
                    }
                    catch
                    {
                        return String.Empty;
                    }

                    cipher.Clear();
                }
                return Encoding.UTF8.GetString(decrypted, 0, decryptedByteCount);
            }
            catch (Exception ex)
            {
                return String.Empty;
            }
        }
        public static string RandomStr()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(RandomString(6, true));
            builder.Append(RandomString(3, false));
            builder.Append(RandomString(3, true));
            builder.Append(RandomString(4, false));
            return builder.ToString();
        }
        public static string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }


    }


}
