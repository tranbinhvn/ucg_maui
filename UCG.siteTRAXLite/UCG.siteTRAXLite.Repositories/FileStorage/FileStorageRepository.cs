using UCG.siteTRAXLite.DataObjects.FileStorage;
using UCG.siteTRAXLite.Repositories.Database;

namespace UCG.siteTRAXLite.Repositories.FileStorage
{
    public class FileStorageRepository : Repository<FileStorageDataObject, Guid>, IFileStorageRepository
    {
        public FileStorageRepository(IMobileDatabase db) : base(db)
        {
        }
    }
}
