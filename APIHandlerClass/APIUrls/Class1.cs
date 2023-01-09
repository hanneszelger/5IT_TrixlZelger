using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace APIUrls
{
    public static class URLs
    {
        public static string BaseURL { get; } = "https://localhost:7000/api/";

        public static string CreateUser { get; } = BaseURL + "User";

        public static string GetAllUsers { get; } = BaseURL + "User";

        public static string GetByUserID() =>
            BaseURL + "User/";

        public static string GetByUsername() =>
            BaseURL + "User/GetByName?username=";

        public static string GetByUsername(string username) => 
            BaseURL + "User/GetByName?username=" + username;

        public static string GetByEmail(string email) =>
            BaseURL + "User/GetByEmail?username=" + email;

        public static string Register(string username, string password, string email, string pubKey) =>
            BaseURL + "User/Register?username=" + username + "&plainPassword=" + password + "&email=" + email + "&pubKey=" + pubKey;

        public static string Login(string username, string password) =>
            BaseURL + "User/Login?username=" + username + "&password=" + password;

        public static string Logout(string username, string token) =>
            BaseURL + "User/Logout?username=" + username + "&token=" + token;

        public static string VerifyToken(string username, string token) =>
            BaseURL + "User/VerifyToken?username=" + username + "&token=" + token;

        public static string RequestUnreadMessages(string id) =>
            BaseURL + "Messages/RequestUnread?id=" + id;
    }
}
