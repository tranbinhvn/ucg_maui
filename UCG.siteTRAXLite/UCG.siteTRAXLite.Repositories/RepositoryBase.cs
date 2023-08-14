using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCG.siteTRAXLite.Repositories.Database;

namespace UCG.siteTRAXLite.Repositories
{
    public class RepositoryBase
    {
        protected IMobileDatabase DB;
        public RepositoryBase(IMobileDatabase db)
        {
            DB = db;
        }
    }
}
