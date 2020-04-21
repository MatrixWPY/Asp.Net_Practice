using Newtonsoft.Json;
using System;
using System.Security.Cryptography;
using System.Text;

namespace WebAPI.Util
{
    public class Utility
    {
        public static byte[] GetSalt(int maxSaltLength)
        {
            var salt = new byte[maxSaltLength];
            using (var random = new RNGCryptoServiceProvider())
            {
                random.GetNonZeroBytes(salt);
            }
            return salt;
        }

        public static string GetMD5(string strParams)
        {
            MD5 md5 = MD5.Create();
            return BitConverter.ToString(md5.ComputeHash(Encoding.UTF8.GetBytes(strParams))).Replace("-", "");
        }

        public static string GetSHA(string strParams)
        {
            SHA256 objProvider = new SHA256CryptoServiceProvider();
            string strCipherStr = ByteToHex(objProvider.ComputeHash(Encoding.Default.GetBytes(strParams)));
            return strCipherStr;
        }

        public static bool CheckSHA(string strRequestSign, string strCalcSign)
        {
            bool bolResult = false;

            if (String.Compare(strRequestSign, strCalcSign, true) == 0)
            {
                bolResult = true;
            }
            else
            {
                //log.Error(string.Format("[RequestSign : {0}] != [CalcSign : {1}]", strRequestSign, strCalcSign));
            }

            return bolResult;
        }

        public static string HexToAscii(String hexString)
        {
            try
            {
                string ascii = string.Empty;
                for (int i = 0; i < hexString.Length; i += 2)
                {
                    String hs = string.Empty;
                    hs = hexString.Substring(i, 2);
                    uint decval = System.Convert.ToUInt32(hs, 16);
                    char character = System.Convert.ToChar(decval);
                    ascii += character;
                }
                return ascii;
            }
            catch (Exception ex)
            {
                //log.Error(ex.Message);
                throw;
            }
        }

        public static string AsciiToHex(string Value)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                foreach (byte b in Value)
                {
                    sb.Append(string.Format("{0:x2}", b));
                }
                return sb.ToString();
            }
            catch (Exception ex)
            {
                //log.Error(ex.Message);
                throw;
            }
        }

        public static byte[] HexToByte(string hexString)
        {
            try
            {
                //運算後的位元組長度:16進位數字字串長/2
                byte[] byteOUT = new byte[hexString.Length / 2];
                for (int i = 0; i < hexString.Length; i = i + 2)
                {
                    //每2位16進位數字轉換為一個10進位整數
                    byteOUT[i / 2] = Convert.ToByte(hexString.Substring(i, 2), 16);
                }
                return byteOUT;
            }
            catch (Exception ex)
            {
                //log.Error(ex.Message);
                throw;
            }
        }

        public static string ByteToHex(byte[] bytes)
        {
            try
            {
                string hexString = string.Empty;
                if (bytes != null)
                {
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < bytes.Length; i++)
                    {
                        sb.Append(bytes[i].ToString("X2"));
                    }
                    hexString = sb.ToString();
                }
                return hexString;
            }
            catch (Exception ex)
            {
                //log.Error(ex.Message);
                throw;
            }
        }

        /// <summary>
        /// object 轉 JSON (自動移除值為 null 的欄位)
        /// </summary>
        /// <param name="objData"></param>
        /// <returns></returns>
        public static string GetJSON(object objData)
        {
            return JsonConvert.SerializeObject(objData, Newtonsoft.Json.Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
        }

        /// <summary>
        /// 格式化 JSON 輸出資料
        /// </summary>
        /// <param name="strJson"></param>
        /// <returns></returns>
        public static string FormatJSON(string strJson)
        {
            try
            {
                dynamic parsedJson = JsonConvert.DeserializeObject(strJson);
                return JsonConvert.SerializeObject(parsedJson, Newtonsoft.Json.Formatting.Indented);
            }
            catch (Exception)
            {
                return strJson;
            }
        }
    }
}