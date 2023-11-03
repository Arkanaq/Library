using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Collections;
using System.Configuration;
using TestApp.DbModel.Models;
using TestApp.Services;
using static MongoDB.Driver.WriteConcern;

namespace TestApp.DbModel
{
    public class LibraryStoreDbSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string AuthorCollectionName { get; set; } = null!;
        public string UserCollectionName { get; set; } = null!;
        public string BooksCollectionName { get; set; } = null!;
    }

}
