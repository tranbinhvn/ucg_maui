using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCG.siteTRAXLite.Repositories.Database
{
    public interface ISQLiteConnectionFactory
    {
        SQLiteConnection CreateConnection();
        SQLiteAsyncConnection CreateAsyncConnection();
        string GetDatabasePath();
    }
}
