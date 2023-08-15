using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UCG.siteTRAXLite.DataObjects.DataObject;

namespace UCG.siteTRAXLite.Repositories
{
    public interface ILocalRepository<T> where T : class, IDataObjectBase, new()
    {
        int Save(T item);
        bool SaveList(IEnumerable<T> list);
        bool UpdateAll(IEnumerable<T> list);
        bool UpdateItem(T item);

        IEnumerable<T> GetList(Expression<Func<T, bool>> predicate = null);
        void DeleteAll();
        bool Delete(T item);
        bool Delete(int Id);
        void DeleteAllIds(IEnumerable<object> ids);
      //  ListPagingItemResult<T> GetPaging(PagingRequest page);
      //  ListPagingItemResult<T> GetPaging(TableQuery<T> query, PagingRequest page);
        TableQuery<T> All(Expression<Func<T, bool>> predicate = null, bool includeDeleteItem = false);
        void BeginTransaction();
        void CommitTransaction();
    }

    public interface IRepository<T, Y> : ILocalRepository<T> where T : class, IDataObjectBase, IServerDataObject<Y>, new()
    {
        bool SaveOrUpdate(IEnumerable<T> list, bool ignoreUpdate = false, Action<T, T> updataAction = null, IEnumerable<T> existItems = null, bool inTransaction = true, bool shouldDeleteNonExistServerK = false);
        bool SaveAndDelete(IEnumerable<T> insertItem, IEnumerable<int> deleteItems);
        bool DeleteVariableExisted(IEnumerable<int> deleteItems);
    }
}
