using System.Security.Cryptography;
using System.Text;

namespace HPIT.StudentEvaluate.Core
{
    /// <summary>
    /// MD5加密
    /// </summary>
    public class Md5Encrypt
    {
        private static readonly string _Salt = "HPIT_2019";

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="str">加密字符</param>
        /// <returns></returns>
        public static string Md5(string str)
        {
            using (MD5 md5Obj = MD5.Create())
            {
                var md5Bytes = md5Obj.ComputeHash(Encoding.UTF8.GetBytes(str + _Salt));
                //md5Obj.Dispose();
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < md5Bytes.Length; i++)
                {

                    builder.Append(md5Bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
