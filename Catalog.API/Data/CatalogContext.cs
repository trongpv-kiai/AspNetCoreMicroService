using System.Drawing;
using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Data
{
    public class CatalogContext : ICatalogContext
    {
        public CatalogContext(IConfiguration _configuration)
        {
            var client = new MongoClient(_configuration.GetSection("DatabaseSetting").GetSection("ConnectionString").Value);
            var database = client.GetDatabase(_configuration.GetSection("DatabaseSetting").GetSection("DatabaseName").Value);

            Products = database.GetCollection<Product>(_configuration.GetSection("DatabaseSetting").GetSection("CollectionName").Value);
        }

        public IMongoCollection<Product> Products { get; }
    }
}
