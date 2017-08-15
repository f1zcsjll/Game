using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Net.NetworkInformation;

namespace Tools
{
    public class HttpHelper
    {

        /// <summary>
        /// 创建GET方式的HTTP请求
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static HttpWebRequest CreateGetHttpResponse(string url)
        {
            HttpWebRequest request = null;
            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                //对服务端证书进行有效性校验（非第三方权威机构颁发的证书，如自己生成的，不进行验证，这里返回true）
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                request = WebRequest.Create(url) as HttpWebRequest;
                request.ProtocolVersion = HttpVersion.Version10;    //http版本，默认是1.1,这里设置为1.0
                request.Method = "GET";
                return request;
            }
            else
            {
                request = WebRequest.Create(url) as HttpWebRequest;
                request.Method = "GET";
                return request;
            }
        }

        /// <summary>
        /// 创建POST方式的HTTP请求
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static HttpWebRequest CreatePostHttpResponse(string url)
        {
            HttpWebRequest request = null;
            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                //对服务端证书进行有效性校验（非第三方权威机构颁发的证书，如自己生成的，不进行验证，这里返回true）
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                request = WebRequest.Create(url) as HttpWebRequest;
                request.ProtocolVersion = HttpVersion.Version10;    //http版本，默认是1.1,这里设置为1.0
                request.Method = "POST";
                //request.ContentType = "multipart/form-data";
                request.ContentType = "application/x-www-form-urlencoded";
                return request;
            }
            else
            {
                request = WebRequest.Create(url) as HttpWebRequest;
                request.Method = "POST";
                //request.ContentType = "multipart/form-data";
                request.ContentType = "application/x-www-form-urlencoded";
                return request;
            }
        }

        /// <summary>
        /// 获取请求的数据
        /// </summary>
        public static string GetResponseString(HttpWebResponse webresponse)
        {
            using (Stream s = webresponse.GetResponseStream())
            {
                StreamReader reader = new StreamReader(s, Encoding.UTF8);
                return reader.ReadToEnd();
            }
        }

        /// <summary>
        /// 验证证书
        /// </summary>
        private static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true;
        }

        /// <summary>
        /// 建立MD5码
        /// </summary>
        /// <param name="Instr"></param>
        /// <returns></returns>
        public static string GetMD5Code(string Instr)
        {
            MD5 md5Hasher = MD5.Create();
            byte[] data = md5Hasher.ComputeHash(Encoding.UTF8.GetBytes(Instr));
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sb.Append(data[i].ToString("x2"));
            }
            return sb.ToString();
        }

        /// <summary>
        /// 将数据变成URL
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string DataToUrl(string url, IEnumerable<KeyValuePair<string, string>> data)
        {
            if (data == null)
                throw new ArgumentNullException("data");
            bool first = true;
            var sb = new StringBuilder(url);
            foreach (var item in data)
            {
                if (first)
                {
                    sb.Append('?');
                    first = false;
                }
                else
                    sb.Append('&');
                sb.Append(Uri.EscapeDataString(item.Key) + "=" + Uri.EscapeDataString(item.Value));
            }
            return sb.ToString();
        }

        /// <summary>
        /// 创建POST数据
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string CreatePostData(Dictionary<string,string> data)
        {
            if (data == null)
                return "";
            bool first = true;
            var sb = new StringBuilder();
            foreach(var item in data)
            {
                if(first)
                {
                    first = false;
                }
                else
                {
                    sb.Append('&');
                }
                sb.Append(Uri.EscapeDataString(item.Key) + "=" + Uri.EscapeDataString(item.Value));
            }
            return sb.ToString();
        }

        /// <summary>
        /// 获取Mac地址
        /// </summary>
        /// <returns></returns>
        public static string GetMacAddress()
        {
            string physicalAddress = "";
            NetworkInterface[] nice = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface adaper in nice)
            {
                if (adaper.Description == "en0")
                {
                    physicalAddress = adaper.GetPhysicalAddress().ToString();
                    break;
                }
                else
                {
                    physicalAddress = adaper.GetPhysicalAddress().ToString();
                    if (physicalAddress != "")
                    {
                        break;
                    };
                }
            }
            return physicalAddress;
        }
    }
}