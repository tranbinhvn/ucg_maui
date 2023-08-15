using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCG.siteTRAXLite.Repositories.Database;

namespace UCG.siteTRAXLite.Platforms.iOS.Database
{
    internal class SQLiteIOS : ISQLiteConnectionFactory
    {
        public SQLiteAsyncConnection CreateAsyncConnection()
        {
            return new SQLiteAsyncConnection(GetDatabasePath());
        }

        public SQLiteConnection CreateConnection()
        {
            return new SQLiteConnection(GetDatabasePath());
        }

        public string GetDatabasePath()
        {
            var sqliteFilename = "SiteTRAXLiteDB.db";
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libraryPath = Path.Combine(documentsPath, "..", "Library", "Databases");

            if (!Directory.Exists(libraryPath))
            {
                Directory.CreateDirectory(libraryPath);
            }

            var path = Path.Combine(libraryPath, sqliteFilename);
            return path;
        }
    }
}
