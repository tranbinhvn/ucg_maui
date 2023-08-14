using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UCG.siteTRAXLite.DataObjects.DataObject;
using UCG.siteTRAXLite.Repositories.Database;

namespace UCG.siteTRAXLite.Repositories
{
    public class LocalRepository<T> : RepositoryBase, ILocalRepository<T> where T : class, IDataObjectBase, new()
    {
        public LocalRepository(IMobileDatabase db) : base(db)
        {
        }
        public virtual void DeleteAll()
        {
            DB.DeleteAll<T>();
        }

        public void DeleteAllIds(IEnumerable<object> ids)
        {
            DB.DeleteAllIds<T>(ids);
        }

        public virtual IEnumerable<T> GetList(Expression<Func<T, bool>> predicate = null)
        {
            var list = DB.GetItemList<T>(predicate);
            return list;
        }

        public virtual int Save(T item)
        {
            return DB.SaveItem(item);
        }

        public virtual bool SaveList(IEnumerable<T> list)
        {

            if (list == null) return false;

            DB.BulkInsert(list);

            return true;
        }

        public virtual bool UpdateAll(IEnumerable<T> list)
        {
            DB.UpdateAll(list);
            return true;
        }

        public virtual bool UpdateItem(T item)
        {
            DB.UpdateItem(item);
            return true;
        }

        public virtual bool Delete(T item)
        {
            DB.DeleteItem<T>(item.ID);
            return true;
        }

        public virtual bool Delete(int Id)
        {
            DB.DeleteItem<T>(Id);
            return true;
        }

      /*  public ListPagingItemResult<T> GetPaging(PagingRequest page)
        {
            int total = 0;
            var items = DB.GetPaging<T>(page.Skip, page.PageSize, out total);
            return new ListPagingItemResult<T>()
            {
                Items = items,
                Count = total
            };
        }

        public ListPagingItemResult<T> GetPaging(TableQuery<T> query, PagingRequest page)
        {
            int total = query.Count();
            var items = query.TakePage(page).ToList();
            return new ListPagingItemResult<T>()
            {
                Items = items,
                Count = total
            };
        }*/

        public TableQuery<T> All(Expression<Func<T, bool>> predicate = null, bool includeDeleteItem = false)
        {
            var query = DB.All<T>();

            // set for IsDelete here
            if (!includeDeleteItem)
            {
                var hasDeletedProperty = typeof(T).GetProperties().Any(s => s.Name == "IsDeleted");
                if (hasDeletedProperty)
                {
                    ParameterExpression param = Expression.Parameter(typeof(T), "t");
                    MemberExpression member = Expression.Property(param, "IsDeleted");
                    ConstantExpression constant = Expression.Constant(false);
                    var eqEx = Expression.Equal(member, constant);
                    var expression = Expression.Lambda<Func<T, bool>>(eqEx, param);
                    query = query.Where(expression);
                }
            }
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            return query;
        }

        public void BeginTransaction()
        {
            DB.BeginTransaction();
        }
        public void CommitTransaction()
        {
            DB.CommitTransaction();
        }
    }

    public class Repository<T, Y> : LocalRepository<T>, IRepository<T, Y> where T : class, IDataObjectBase, IServerDataObject<Y>, new()
    {
        public Repository(IMobileDatabase db) : base(db)
        {
        }

        public bool SaveOrUpdate(IEnumerable<T> list, bool ignoreUpdate = false, Action<T, T> updateAction = null, IEnumerable<T> existItems = null, bool inTransaction = true, bool shouldDeleteNonExistServerK = false)
        {
            DB.InsertOrUpdateBulk<T, Y>(list, ignoreUpdate, updateAction, existItems, inTransaction, shouldDeleteNonExistServerK);
            return true;
        }

        public bool SaveAndDelete(IEnumerable<T> insertItem, IEnumerable<int> deleteItems)
        {
            DB.InsertOrDelete<T, Y>(insertItem, deleteItems);
            return true;
        }

        public bool DeleteVariableExisted(IEnumerable<int> deleteItems)
        {
            DB.DeleteVariableExisted<T, Y>(deleteItems);
            return true;
        }
    }
}
