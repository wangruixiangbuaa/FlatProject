using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace HPIT.Data.Core
{
    public class HTTPService
    {
        public static string Post(string url, Headers headers, string contentType, string dataStream, int timeout)
        {
            System.GC.Collect();//垃圾回收，回收没有正常关闭的http链接
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            Stream reqStream = null;
            string result = "";
            try
            {
                //设置最大链接数
                ServicePointManager.DefaultConnectionLimit = 200;
                //设置https验证方式
                if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                {
                    ServicePointManager.ServerCertificateValidationCallback =
                            new RemoteCertificateValidationCallback(CertificateValidation);
                }
                request = (HttpWebRequest)WebRequest.Create(url);

                //HttpWebRequest 相关属性
                request.Method = "POST";
                request.Timeout = timeout * 1000;
                request.ContentType = contentType;
                //配置协议标头
                if (headers != null)
                {
                    request.Headers.Set(headers.Name, headers.Value);
                }
                byte[] data = System.Text.Encoding.UTF8.GetBytes(dataStream);
                request.ContentLength = data.Length;

                //写入数据
                reqStream = request.GetRequestStream();
                reqStream.Write(data, 0, data.Length);
                reqStream.Close();

                //返回数据
                response = (HttpWebResponse)request.GetResponse();
                StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                result = sr.ReadToEnd().Trim();
                sr.Close();
            }
            //处理多线程模式下线程中止
            //catch (System.Threading.ThreadAbortException e)
            //{
            //    System.Threading.Thread.ResetAbort();
            //}
            catch (Exception e)
            {
                throw new HttpServiceException(e.ToString());
            }
            finally
            {
                //关闭连接和流
                if (response != null)
                {
                    response.Close();
                }
                if (request != null)
                {
                    request.Abort();
                }
            }
            return result;
        }
        /// <summary>
        /// 处理http GET请求
        /// </summary>
        /// <param name="url">请求的url地址</param>
        /// <returns></returns>
        public static string Get(string url)
        {
            System.GC.Collect();//垃圾回收，回收没有正常关闭的http链接
            string result = "";
            HttpWebRequest request = null;
            HttpWebResponse response = null;

            try
            {
                //设置最大链接数
                ServicePointManager.DefaultConnectionLimit = 200;
                //设置https验证方式
                if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                {
                    ServicePointManager.ServerCertificateValidationCallback =
                        new RemoteCertificateValidationCallback(CertificateValidation);
                }
                request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";

                //返回数据
                response = (HttpWebResponse)request.GetResponse();
                StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                result = sr.ReadToEnd().Trim();
                sr.Close();
            }
            //处理多线程模式下线程中止
            //catch (System.Threading.ThreadAbortException e)
            //{
            //    System.Threading.Thread.ResetAbort();
            //}
            catch (Exception e)
            {
                throw new HttpServiceException(e.ToString());
            }
            finally
            {
                //关闭连接和流
                if (response != null)
                {
                    response.Close();
                }
                if (request != null)
                {
                    request.Abort();
                }
            }
            return result;
        }
        /* 忽略证书认证错误
         * .NET的SSL通信过程中，使用的证书可能存在各种问题
         * 此方法可以跳过服务器证书验证，完成正常通信。*/
        private static bool CertificateValidation(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            // 认证正常，没有错误   
            return true;
        }
    }
}
/// <summary>
/// 协议标头
/// </summary>
public class Headers
{
    public string Name { get; set; }
    public string Value { get; set; }
    public Headers(string name, string value)
    {
        this.Name = name;
        this.Value = value;
    }
}
public class HttpServiceException : Exception
{
    public HttpServiceException(string msg) : base(msg)
    {

    }
}
