using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDBAPI.Classes;

namespace MongoDBAPI.Services
{

    public class UserDataService
    {
        private readonly IMongoCollection<UserData> _userData;

        public UserDataService(
        IOptions<UserTestDB> bookStoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                bookStoreDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                bookStoreDatabaseSettings.Value.DatabaseName);

            _userData = mongoDatabase.GetCollection<UserData>(
                bookStoreDatabaseSettings.Value.BooksCollectionName);
        }

        public async Task<List<UserData>> GetAsync() =>
        await _userData.Find(_ => true).ToListAsync();

        public async Task<UserData?> GetAsync(string id) =>
            await _userData.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task<UserData?> GetAsyncByUser(string username) =>
            await _userData.Find(x => x.username == username).FirstOrDefaultAsync();

        public async Task CreateAsync(UserData newUserData) =>
            await _userData.InsertOneAsync(newUserData);

        public async Task UpdateAsync(string id, UserData updatedUserData) =>
            await _userData.ReplaceOneAsync(x => x.Id == id, updatedUserData);

        public async Task RemoveAsync(string id) =>
            await _userData.DeleteOneAsync(x => x.Id == id);
    }

}
