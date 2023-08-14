using SQLite;
using UCG.siteTRAXLite.Repositories.Database;

namespace UCG.siteTRAXLite.Platforms.Android.Database
{
    public class SQLiteAndroid : ISQLiteConnectionFactory
    {
        public string GetDatabasePath()
        {
            var sqliteFileName = "SiteTRAXLiteDB.db3";
            string libraryPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var path = Path.Combine(libraryPath, sqliteFileName);
            return path;
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
