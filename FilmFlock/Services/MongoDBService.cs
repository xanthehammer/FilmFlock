using FilmFlock.Mongo;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

public static class MongoDBService
{
    public static void AddMongoDB(this IServiceCollection services)
    {
        var dbUri = Environment.GetEnvironmentVariable(Constants.Storage.Mongo.URI_ENV_KEY);
        if (dbUri == null)
        {
            Console.WriteLine("You must set your <{Constants.Storage.Mongo.URI_ENV_KEY}> environment variable to access the remote DB.");
            Environment.Exit(0);
        }

        var client = new MongoClient(dbUri);
        var database = client.GetDatabase(Constants.Storage.Mongo.FLOCK_DB_NAME);

        services.AddSingleton<IMongoClient>(client);
        services.AddScoped<IMongoDatabase>(sp => client.GetDatabase(Constants.Storage.Mongo.FLOCK_DB_NAME));
    }
}
