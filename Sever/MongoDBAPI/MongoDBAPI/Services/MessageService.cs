using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDBAPI.Classes;

namespace MongoDBAPI.Services
{
    public class MessageService
    {
        private readonly IMongoCollection<Message> _userData;

        public MessageService(
        IOptions<UserTestDB> bookStoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                bookStoreDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                bookStoreDatabaseSettings.Value.DatabaseName);

            _userData = mongoDatabase.GetCollection<Message>(
                bookStoreDatabaseSettings.Value.BooksCollectionName);
        }

        public async Task<List<Message>> GetAsync() =>
            await _userData.Find(_ => true).ToListAsync();

        public async Task<Message?> GetAsync(string id) =>
            await _userData.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task<Message?> GetAsyncFromUser(string fromUserId) =>
            await _userData.Find(x => x.Userid == fromUserId).FirstOrDefaultAsync();

        public async Task<Message?> GetAsyncToUser(string toUserId) =>
            await _userData.Find(x => x.Userid == toUserId).FirstOrDefaultAsync();

        public async Task CreateAsync(Message newUserData) =>
            await _userData.InsertOneAsync(newUserData);

        public async Task UpdateAsync(string id, Message updatedUserData) =>
            await _userData.ReplaceOneAsync(x => x.Id == id, updatedUserData);

        public async Task RemoveAsync(string id) =>
            await _userData.DeleteOneAsync(x => x.Id == id);
    }
}
