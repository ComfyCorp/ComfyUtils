using System;
using System.IO;
using System.Net;
using System.Text;
using System.Collections.Generic;

namespace ComfyUtils
{
    public class Methods
    {
        public static string GET = "GET";
        public static string POST = "POST";
        public static string PUT = "PUT";
        public static string HEAD = "HEAD";
        public static string DELETE = "DELETE";
        public static string PATCH = "PATCH";
        public static string OPTIONS = "OPTIONS";
        public static string TRACE = "TRACE";
        public static string CONNECT = "CONNECT";
    }
    public class ContentTypes
    {
        public class Application
        {
            public static string java_archive = "application/java-archive";
            public static string EDI_X12 = "application/EDI-X12";
            public static string EDIFACT = "application/EDIFACT";
            public static string javascript = "application/javascript";
            public static string octet_stream = "application/octet-stream";
            public static string ogg = "application/ogg";
            public static string pdf = "application/pdf";
            public static string xhtml_xml = "application/xhtml+xml";
            public static string x_shockwave_flash = "application/x-shockwave-flash";
            public static string json = "application/json";
            public static string ld_json = "application/ld+json";
            public static string xml = "application/xml";
            public static string zip = "application/zip";
            public static string x_www_form_urlencoded = "application/x-www-form-urlencoded";
        }
        public class Audio
        {
            public static string mpeg = "audio/mpeg";
            public static string x_ms_wma = "audio/x-ms-wma";
            public static string vnd_rn_realaudio = "audio/vnd.rn-realaudio";
            public static string x_wav = "audio/x-wav";
        }
        public class Image
        {
            public static string gif = "image/gif";
            public static string jpeg = "image/jpeg";
            public static string png = "image/png";
            public static string tiff = "image/tiff";
            public static string vnd_microsoft_icon = "image/vnd.microsoft.icon";
            public static string x_icon = "image/x-icon";
            public static string vnd_djvu = "image/vnd.djvu";
            public static string svg_xml = "image/svg+xml";
        }
        public class Multipart
        {
            public static string mixed = "multipart/mixed";
            public static string alternative = "multipart/alternative";
            public static string related = "multipart/related";
            public static string form_data = "multipart/form-data";
        }
        public class Text
        {
            public static string css = "text/css";
            public static string csv = "text/csv";
            public static string html = "text/html";
            public static string javascript = "text/javascript";
            public static string plain = "text/plain";
            public static string xml = "text/xml";
        }
        public class Video
        {
            public static string mpeg = "video/mpeg";
            public static string mp4 = "video/mp4";
            public static string quicktime = "video/quicktime";
            public static string x_ms_wmv = "video/x-ms-wmv";
            public static string x_msvideo = "video/x-msvideo";
            public static string x_flv = "video/x-flv";
            public static string webm = "video/webm";
        }
    }
    public class NetUtils
    {
        static string UserAgent;
        public static void SetUserAgent(string useragent) { UserAgent = useragent; }
        public static HttpWebRequest CreateRequest(string link)
        {
            HttpWebRequest request = WebRequest.CreateHttp(link);
            request.CookieContainer = new CookieContainer();
            if (!string.IsNullOrEmpty(UserAgent)) { request.UserAgent = UserAgent; }
            return request;
        }
        public static string[] CheckProxies(string[] proxylist, string domain = "https://www.google.com/", int timeout = 5000)
        {
            HttpWebRequest request = WebRequest.CreateHttp(domain);
            List<string> valid = new List<string>();
            foreach (string proxyport in proxylist)
            {
                try
                {
                    request.Proxy = proxyport.Contains(":") ? new WebProxy(proxyport.Split(':')[0],
                        Convert.ToInt32(proxyport.Split(':')[1])) : new WebProxy(proxyport);
                    request.Timeout = timeout;
                    request.GetResponse();
                    valid.Add(proxyport);
                } catch { }
            }
            return valid.ToArray();
        }
    }
    public static class NetExtensions
    {
        public static void AddHeader(this HttpWebRequest request, string headerName, string headerValue)
        {
            request.Headers.Add(headerName, headerValue);
        }
        public static void SetHeader(this HttpWebRequest request, HttpRequestHeader header, string headerValue)
        {
            request.Headers[header] = headerValue;
        }
        public static void AddCookie(this HttpWebRequest request, string cookieName, string cookieValue)
        {
            request.CookieContainer.Add(new Cookie(cookieName, cookieValue) { Domain = request.Host });
        }
        public static void AddBody(this HttpWebRequest request, string content)
        {
            Stream requestStream = request.GetRequestStream();
            byte[] data = Encoding.UTF8.GetBytes(content);
            requestStream.Write(data, 0, data.Length);
        }
        public static HttpWebResponse GetHttpResponse(this HttpWebRequest request)
        {
            return (HttpWebResponse)request.GetResponse();
        }
        public static string GetResponseBody(this HttpWebRequest request)
        {
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader readStream = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            char[] read = new char[256];
            int count = readStream.Read(read, 0, 256);
            StringBuilder stringBuilder = new StringBuilder();
            while (count > 0)
            {
                string str = new string(read, 0, count);
                stringBuilder.Append(str);
                count = readStream.Read(read, 0, 256);
            }
            return stringBuilder.ToString();
        }
        public static string GetResponseBody(this HttpWebResponse response)
        {
            StreamReader readStream = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            char[] read = new char[256];
            int count = readStream.Read(read, 0, 256);
            StringBuilder stringBuilder = new StringBuilder();
            while (count > 0)
            {
                string str = new string(read, 0, count);
                stringBuilder.Append(str);
                count = readStream.Read(read, 0, 256);
            }
            return stringBuilder.ToString();
        }
    }
}
