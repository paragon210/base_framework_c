using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using FluentAssertions;

namespace AutomationFramework.Helpers
{
    public static class MongoDbHelper
    {

        private static IMongoClient _client;
        private static IMongoDatabase _database;

        /// <summary>
        /// Searches for a practitioner using their first name. Returns true if 1 is found.
        /// </summary>
        /// <param name="firstName"></param>
        /// <returns>bool</returns>
        public static bool ApplicationExists(string firstName)
        {

            IMongoCollection<BsonDocument> collection = _database.GetCollection<BsonDocument>("temp_storage");
            var filter = Builders<BsonDocument>.Filter.Eq("Object.data.person.firstName", firstName);
            var result = collection.Find(filter).Count();
            if (result != 1)
                return false;
            return true;
        }

        /// <summary>
        /// Asynchronous method to retrieve a list of documents based on first name.
        /// </summary>
        /// <param name="firstName"></param>
        /// <returns>Result list of documents.</returns>
        public static async Task<List<BsonDocument>> GetListDocumentsFirstName(string firstName)
        {
            IMongoCollection<BsonDocument> collection = _database.GetCollection<BsonDocument>("temp_storage");
            var filter = Builders<BsonDocument>.Filter.Eq("Object.data.person.firstName", firstName);
            var result = await collection.Find(filter).ToListAsync();
            return result;
        }

        /// <summary>
        /// Deletes an application using a first name. Since the name in test is unique, the deleted count should be 1.
        /// </summary>
        /// <param name="firstName"></param>
        public static void DeleteApplication(string firstName)
        {
            IMongoCollection<BsonDocument> collection = _database.GetCollection<BsonDocument>("temp_storage");
            var filter = Builders<BsonDocument>.Filter.Eq("Object.data.person.firstName", firstName);
            var result = collection.DeleteOne(filter);
            result.DeletedCount.Should().Be(1);
        }

        /// <summary>
        /// Returns a Mongo connection.
        /// </summary>
        /// <returns>IMongoClient connection</returns>
        public static IMongoClient CreateMongoClientObject()
        {
            MongoClientSettings setting = new MongoClientSettings()
            {
                Credentials = new List<MongoCredential> { MongoCredential.CreateCredential("admin", "test", "test@user") },
                Server = new MongoServerAddress("testserver")
            };
            IMongoClient m = new MongoClient(setting);
            return m;
        }

        /// <summary>
        /// Creates and sets the MongoDb to set values.
        /// </summary>
        public static void CreateMongoClient()
        {
            MongoClientSettings setting = new MongoClientSettings()
            {
                Credentials = new List<MongoCredential> { MongoCredential.CreateCredential("admin", "test", "test@user") },
                Server = new MongoServerAddress("testserver")
            };
          _client = new MongoClient(setting);
          _database = _client.GetDatabase("db");
        }
    }
}