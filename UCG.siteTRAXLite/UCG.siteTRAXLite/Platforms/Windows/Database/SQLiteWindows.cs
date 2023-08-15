using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCG.siteTRAXLite.Repositories.Database;
using Windows.Storage;

namespace UCG.siteTRAXLite.Platforms.Windows.Database
{
    public class SQLiteWindows : ISQLiteConnectionFactory
    {
        public string GetDatabasePath()
        {

            ApplicationData.Current.LocalFolder.CreateFileAsync("SiteTRAXLiteDB.db", CreationCollisionOption.OpenIfExists).AsTask().Wait();
            string dbpath = Path.Combine(ApplicationData.Current.LocalFolder.Path, "SiteTRAXLiteDB.db");
        
            return dbpath;
        }

        public SQLiteConnection CreateConnection()
        {
            var conn = new SQLiteConnection(GetDatabasePath());
            return conn;
        }

        public SQLiteAsyncConnection CreateAsyncConnection()
        {
            var conn = new SQLiteAsyncConnection(GetDatabasePath());
            return conn;
        }
    }
}
