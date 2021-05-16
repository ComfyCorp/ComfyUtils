using System;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;

#pragma warning disable IDE1006

namespace ComfyUtils.VRC
{
    public class AvatarInfo
    {
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string authorId { get; set; }
        public string authorName { get; set; }
        public string[] tags { get; set; }
        public string assetUrl { get; set; }
        public object assetUrlObject { get; set; }
        public string imageUrl { get; set; }
        public string thumbnailImageUrl { get; set; }
        public string releaseStatus { get; set; }
        public int version { get; set; }
        public bool featured { get; set; }
        public Unitypackage[] unityPackages { get; set; }
        public string unityPackageUrl { get; set; }
        public object unityPackageUrlObject { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public class Unitypackage
        {
            public string id { get; set; }
            public string assetUrl { get; set; }
            public object assetUrlObject { get; set; }
            public string unityVersion { get; set; }
            public long unitySortNumber { get; set; }
            public int assetVersion { get; set; }
            public string platform { get; set; }
            public DateTime created_at { get; set; }
        }
    }
    public class WorldInfo
    {
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public bool featured { get; set; }
        public string authorId { get; set; }
        public string authorName { get; set; }
        public int capacity { get; set; }
        public string[] tags { get; set; }
        public string releaseStatus { get; set; }
        public string imageUrl { get; set; }
        public string thumbnailImageUrl { get; set; }
        public string assetUrl { get; set; }
        public object assetUrlObject { get; set; }
        public object pluginUrlObject { get; set; }
        public object unityPackageUrlObject { get; set; }
        public string _namespace { get; set; }
        public Unitypackage[] unityPackages { get; set; }
        public int version { get; set; }
        public string organization { get; set; }
        //public object previewYoutubeId { get; set; }
        public int favorites { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public string publicationDate { get; set; }
        public string labsPublicationDate { get; set; }
        public int visits { get; set; }
        public int popularity { get; set; }
        public int heat { get; set; }
        public int publicOccupants { get; set; }
        public int privateOccupants { get; set; }
        public int occupants { get; set; }
        public object[] instances { get; set; }
        public class Unitypackage
        {
            public string id { get; set; }
            public string assetUrl { get; set; }
            public object assetUrlObject { get; set; }
            public string pluginUrl { get; set; }
            public object pluginUrlObject { get; set; }
            public string unityVersion { get; set; }
            public long unitySortNumber { get; set; }
            public int assetVersion { get; set; }
            public string platform { get; set; }
            public DateTime created_at { get; set; }
        }
    }
    public class UserInfo
    {
        public string id { get; set; }
        public string username { get; set; }
        public string displayName { get; set; }
        public string userIcon { get; set; }
        public string bio { get; set; }
        public string[] bioLinks { get; set; }
        public string profilePicOverride { get; set; }
        public string currentAvatarImageUrl { get; set; }
        public string currentAvatarThumbnailImageUrl { get; set; }
        public string fallbackAvatar { get; set; }
        public string status { get; set; }
        public string statusDescription { get; set; }
        public string state { get; set; }
        public string[] tags { get; set; }
        public string developerType { get; set; }
        public DateTime last_login { get; set; }
        public string last_platform { get; set; }
        public bool allowAvatarCopying { get; set; }
        public string date_joined { get; set; }
        public bool isFriend { get; set; }
        public string friendKey { get; set; }
        public string worldId { get; set; }
        public string instanceId { get; set; }
        public string location { get; set; }
    }
    public class VRCNetUtils
    {
        public static string GetAPIKey()
        {
            HttpWebRequest request = WebRequest.CreateHttp("https://api.vrchat.cloud/api/1/config");
            request.CookieContainer = new CookieContainer();
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            return response.Cookies["apiKey"].Value;
        }
        public static string GetAuthCookie(string useremail, string password, string apikey, string useragent = "UserAgentA")
        {
            HttpWebRequest request = WebRequest.CreateHttp($"https://api.vrchat.cloud/api/1/auth/user");
            request.Headers[HttpRequestHeader.Authorization] = $"Basic {Convert.ToBase64String(Encoding.UTF8.GetBytes($"{Uri.EscapeDataString(useremail)}:{Uri.EscapeDataString(password)}"))}";
            request.UserAgent = useragent;
            request.CookieContainer = new CookieContainer();
            request.CookieContainer.Add(new Cookie("apiKey", apikey) { Domain = request.Host });
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            return response.Cookies["auth"].Value;
        }
        public static HttpWebResponse GetAvatar(string avatarID, string apikey, string auth, string useragent = "UserAgentA")
        {
            HttpWebRequest request = WebRequest.CreateHttp($"https://api.vrchat.cloud/api/1/avatars/{avatarID}");
            request.UserAgent = useragent;
            request.CookieContainer = new CookieContainer();
            request.CookieContainer.Add(new Cookie("apiKey", apikey) { Domain = request.Host });
            request.CookieContainer.Add(new Cookie("auth", auth) { Domain = request.Host });
            return (HttpWebResponse)request.GetResponse();
        }
        public static HttpWebResponse GetWorld(string worldID, string apikey, string auth = null, string useragent = "UserAgentA")
        {
            HttpWebRequest request = WebRequest.CreateHttp($"https://api.vrchat.cloud/api/1/worlds/{worldID}");
            request.UserAgent = useragent;
            request.CookieContainer = new CookieContainer();
            request.CookieContainer.Add(new Cookie("apiKey", apikey) { Domain = request.Host });
            if (auth != null) { request.CookieContainer.Add(new Cookie("auth", auth) { Domain = request.Host }); }
            return (HttpWebResponse)request.GetResponse();
        }
        public static HttpWebResponse GetUser(string userID, string apikey, string auth, string useragent = "UserAgentA")
        {
            HttpWebRequest request = WebRequest.CreateHttp($"https://api.vrchat.cloud/api/1/users/{userID}");
            request.UserAgent = useragent;
            request.CookieContainer = new CookieContainer();
            request.CookieContainer.Add(new Cookie("apiKey", apikey) { Domain = request.Host });
            request.CookieContainer.Add(new Cookie("auth", auth) { Domain = request.Host });
            return (HttpWebResponse)request.GetResponse();
        }
        public static AvatarInfo GetAvatarInfo(string avatarID, string apikey, string auth, string useragent = "UserAgentA")
        {
            HttpWebRequest request = WebRequest.CreateHttp($"https://api.vrchat.cloud/api/1/avatars/{avatarID}");
            request.UserAgent = useragent;
            request.CookieContainer = new CookieContainer();
            request.CookieContainer.Add(new Cookie("apiKey", apikey) { Domain = request.Host });
            request.CookieContainer.Add(new Cookie("auth", auth) { Domain = request.Host });
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
            return JsonConvert.DeserializeObject<AvatarInfo>(stringBuilder.ToString());
        }
        public static WorldInfo GetWorldInfo(string worldID, string apikey, string auth = null, string useragent = "UserAgentA")
        {
            HttpWebRequest request = WebRequest.CreateHttp($"https://api.vrchat.cloud/api/1/worlds/{worldID}");
            request.UserAgent = useragent;
            request.CookieContainer = new CookieContainer();
            request.CookieContainer.Add(new Cookie("apiKey", apikey) { Domain = request.Host });
            if (auth != null) { request.CookieContainer.Add(new Cookie("auth", auth) { Domain = request.Host }); }
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
            return JsonConvert.DeserializeObject<WorldInfo>(stringBuilder.ToString());
        }
        public static UserInfo GetUserInfo(string userID, string apikey, string auth, string useragent = "UserAgentA")
        {
            HttpWebRequest request = WebRequest.CreateHttp($"https://api.vrchat.cloud/api/1/users/{userID}");
            request.UserAgent = useragent;
            request.CookieContainer = new CookieContainer();
            request.CookieContainer.Add(new Cookie("apiKey", apikey) { Domain = request.Host });
            if (auth != null) { request.CookieContainer.Add(new Cookie("auth", auth) { Domain = request.Host }); }
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
            return JsonConvert.DeserializeObject<UserInfo>(stringBuilder.ToString());
        }
    }
}
