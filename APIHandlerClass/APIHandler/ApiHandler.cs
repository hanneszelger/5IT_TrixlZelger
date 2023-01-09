using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using DnsClient.Internal;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Driver;
using MongoDB.Driver.Core.Operations;
using APIUrls;
using SocialClassCollection;

namespace APIHandler
{
    public static class Handler
    {
        public static async Task<bool> DataExists(string requestUrl)
        {
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage response = await httpClient.GetAsync(requestUrl);
                return response.IsSuccessStatusCode;
            }
        }

        public static async Task<string> GetData(string requestUrl)
        {
            //api String
            //example for username lugghons -> https://localhost:7079/api/UserData/GetUser/lugghons
            //path = "https://localhost:7079/api/UserData";
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage response = await httpClient.GetAsync(requestUrl);
                if (response.IsSuccessStatusCode)
                {
                    var o = await response.Content.ReadAsStringAsync();
                    return o;
                }
            }
            return null;
        }

        public static async Task<T> GetData<T>(string requestUrl)
        {
            string temp = await GetData(requestUrl);
            try
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(temp);
            }
            catch (Exception)
            {
                return default;
            }
        }

        public static async Task<bool> InsertData(string contentJson, string requestUrl)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), requestUrl))
                {
                    request.Headers.TryAddWithoutValidation("Accept", "application/json");
                    request.Content = new StringContent(contentJson, Encoding.UTF8, "application/json");

                    var response = await httpClient.SendAsync(request);
                    return response.IsSuccessStatusCode;
                }
            }
        }

        public static async Task<bool> InsertObject<T>(T InsertObject, string requestUrl)
        {
            string insertionString = Newtonsoft.Json.JsonConvert.SerializeObject(InsertObject);
            return await InsertData(insertionString, requestUrl);
        }

        public static async Task<HttpStatusCode> GetStatusCode(string requestUrl)
        {
            using (var httpClient = new HttpClient())
            {
                HttpResponseMessage response = await httpClient.GetAsync(requestUrl);
                return response.StatusCode;
            }
        }

        public static async Task<string> Login(string username, string password) =>
            await GetData(URLs.Login(username, password));

        public static async Task<bool> Register(string username, string plainPassword, string email, string pubKey) =>
            await GetStatusCode(URLs.Register(username, plainPassword, email, pubKey)) == HttpStatusCode.Created;

        public static async Task<bool> VerifyToken(string username, string token) =>
            await GetStatusCode(URLs.VerifyToken(username, token)) == HttpStatusCode.OK;

        public static async Task<bool> Logout(string username, string token) =>
            await GetStatusCode(URLs.Logout(username, token)) == HttpStatusCode.NoContent;

        public static async Task<List<Message>> RequestMessages(string id) =>
            await Handler.GetData<List<Message>>(URLs.RequestUnreadMessages(id));


        //public static async Task<bool> DataExists(string requestUrl) =>
        //    await GetData(requestUrl) != null ? true : false;


        #region redundant
        public static async Task<bool> CreateNewProduct(byte[] barcode, string name, Dictionary<MacroType, double> macrosPer100g, List<double> servingSizes)
        {
            string id = ObjectId.GenerateNewId().ToString();
            Food food = new Food(id, barcode, name, macrosPer100g, servingSizes);

            // maybe stringbuilder for MacrosPer100g
            var result = await InsertObject<Food>(food, "https://localhost:7000/api/Food");
            return result;
        }

        
        public static async Task<bool> CreateNewUser(User user)
        {
            return await InsertData(Newtonsoft.Json.JsonConvert.SerializeObject(user), "https://localhost:7000/api/User");
        }

        public static async Task<bool> CreateNewUser(string username, string password, string email, string publicKey)
        {
            string id = ObjectId.GenerateNewId().ToString();
            User user = new User(id, username, password, email, publicKey);

            string jsonInsert = Newtonsoft.Json.JsonConvert.SerializeObject(user);
            Console.WriteLine(jsonInsert);

            var result = await CreateNewUser(user);

            return result;
        }

        public static void InsertJson(string username, string password, string email, string publicKey)
        {
            string id = ObjectId.GenerateNewId().ToString();
            User user = new User(id, username, password, email, publicKey);

            string jsonInsert = Newtonsoft.Json.JsonConvert.SerializeObject(user);
            Console.WriteLine(jsonInsert);
        }
        #endregion
    }
}
