using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace APIHandler
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

        [BsonElement("username")]
        public string Username { get; set; }

        [BsonElement("password")]
        public string Password { get; set; }

        [BsonElement("email")]
        public string Email { get; set; }

        [BsonElement("publicKey")]
        public string PublicKey { get; set; }

        [BsonElement("salt")]
        public string Salt { get; set; }

        public User(string id, string username, string password, string email, string publicKey)
        {
            Id = id;
            Username = username;
            Password = password;
            Email = email;
            PublicKey = publicKey;
        }

        public User() { }
    }
}
