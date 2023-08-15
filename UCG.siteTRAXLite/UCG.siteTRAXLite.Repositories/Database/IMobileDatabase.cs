using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UCG.siteTRAXLite.DataObjects.DataObject;

namespace UCG.siteTRAXLite.Repositories.Database
{
    public interface IMobileDatabase
    {
        void InitializeMobileDatabase(string path);

        void DeleteAllOfflineData();

        int DeleteAll<T>() where T : IDataObjectBase, new();

        int DeleteItem<T>(int id) where T : IDataObjectBase, new();

        void DeleteAllIds<T>(IEnumerable<object> ids);

        IEnumerable<T> GetItemList<T>(Expression<Func<T, bool>> predicate = null) where T : class, IDataObjectBase, new();

        T GetItem<T>(int id) where T : class, IDataObjectBase, new();
        TableQuery<T> All<T>() where T : class, IDataObjectBase, new();

        int SaveItem<T>(T item) where T : class, IDataObjectBase, new();

        int InsertItem<T>(T item) where T : class, IDataObjectBase, new();

        void BulkInsert<T>(IEnumerable<T> list) where T : class, IDataObjectBase, new();

        void UpdateItem<T>(T item) where T : IDataObjectBase;

        void UpdateAll<T>(IEnumerable<T> items);

        int UpdateItemScalar<T>(T item) where T : IDataObjectBase;

        void InsertOrUpdateBulk<T, Y>(IEnumerable<T> items, bool ignoreUpdate = false, Action<T, T> updateAction = null, IEnumerable<T> existItems = null, bool inTransaction = true, bool shouldDeleteNonExistServerK = false)
            where T : IServerDataObject<Y>, new();
        void InsertOrUpdateBulkWithGuid<T>(IEnumerable<T> items, bool ignoreUpdate = false, Action<T, T> updateAction = null, IEnumerable<T> existItems = null, bool inTransaction = true, bool shouldDeleteNonExistServerK = false)
            where T : IServerDataObject<Guid>, new();

        void InsertOrDelete<T>(IEnumerable<T> insertItems, IEnumerable<int> deleteItems)
            where T : IServerDataObject<Guid>, new();

        void InsertOrDelete<T, Y>(IEnumerable<T> insertItems, IEnumerable<int> deleteItems)
            where T : IServerDataObject<Y>, new();

        void DeleteVariableExisted<T, Y>(IEnumerable<int> deleteItems)
            where T : IServerDataObject<Y>, new();

        List<T> GetPaging<T>(int skip, int pageSize, out int totalRecord, Expression<Func<T, bool>> predicate = null)
            where T : IDataObjectBase, new();
        void BeginTransaction();
        void CommitTransaction();
    }
}
