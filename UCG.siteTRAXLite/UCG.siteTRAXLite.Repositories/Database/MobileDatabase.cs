using SQLite;
using SQLiteNetExtensions.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using UCG.siteTRAXLite.DataObjects;
using UCG.siteTRAXLite.DataObjects.DataObject;
using UCG.siteTRAXLite.DataObjects.DbCustomAttribute;
using UCG.siteTRAXLite.Repositories.Extensions;

namespace UCG.siteTRAXLite.Repositories.Database
{
    public class MobileDatabase : IMobileDatabase
    {
        #region Fields

        private static object _locker = new object();
        private SQLiteConnection DB;

        #endregion

        #region Initialization

        public MobileDatabase(ISQLiteConnectionFactory connectionFactory)
        {
            lock (_locker)
            {
                if (DB == null)
                    DB = connectionFactory.CreateConnection();
                SQLite3.BusyTimeout(DB.Handle, 5000);
                DB.CreateTable<HazardDataObject>();
            }
        }

        public void InitializeMobileDatabase(string path)
        {
            lock (_locker)
            {
                if (DB == null)
                    DB = new SQLiteConnection(path);
                SQLite3.BusyTimeout(DB.Handle, 5000);
                DB.CreateTable<HazardDataObject>();
            }
        }

        #endregion

        #region Public Methods

        public void DeleteAllOfflineData()
        {
            lock (_locker)
            {
                SQLite3.BusyTimeout(DB.Handle, 5000);
                DB.DeleteAll<HazardDataObject>();
            }
        }

        public int DeleteAll<T>() where T : IDataObjectBase, new()
        {
            lock (_locker)
            {
                return DB.DeleteAll<T>();
            }
        }

        public int DeleteItem<T>(int id) where T : IDataObjectBase, new()
        {
            lock (_locker)
            {
                return DB.Delete<T>(id);
            }
        }

        public void DeleteAllIds<T>(IEnumerable<object> ids)
        {
            lock (_locker)
            {
                DB.DeleteAllIds<T>(ids);
            }
        }

        public IEnumerable<T> GetItemList<T>(Expression<Func<T, bool>> predicate = null) where T : class, IDataObjectBase, new()
        {
            lock (_locker)
            {
                var query = DB.Table<T>();
                if (predicate != null)
                {
                    query = query.Where(predicate);
                }

                IEnumerable<T> list = query.ToList();
                return list;
            }
        }

        public T GetItem<T>(int id) where T : class, IDataObjectBase, new()
        {
            lock (_locker)
            {
                var item = DB.Table<T>().FirstOrDefault(x => x.ID == id);

                return item;
            }
        }

        public TableQuery<T> All<T>() where T : class, IDataObjectBase, new()
        {
            return DB.Table<T>();
        }

        public int SaveItem<T>(T item) where T : class, IDataObjectBase, new()
        {
            lock (_locker)
            {
                if (item.ID != 0)
                {
                    DB.Update(item);
                    return item.ID;
                }
                else
                {
                    DB.Insert(item);
                    var newID = DB.ExecuteScalar<int>("select last_insert_rowid();");
                    return newID;
                }
            }
        }

        public int InsertItem<T>(T item) where T : class, IDataObjectBase, new()
        {
            lock (_locker)
            {
                DB.Insert(item);
                var newID = DB.ExecuteScalar<int>("select last_insert_rowid();");
                return newID;
            }
        }

        public void BulkInsert<T>(IEnumerable<T> list) where T : class, IDataObjectBase, new()
        {
            lock (_locker)
            {
                DB.InsertAll(list, typeof(T), false);
            }
        }

        public void UpdateItem<T>(T item) where T : IDataObjectBase
        {
            lock (_locker)
            {
                DB.Update(item);
            }
        }

        public void UpdateAll<T>(IEnumerable<T> items)
        {
            lock (_locker)
            {
                DB.UpdateAll(items, false);
            }
        }

        public int UpdateItemScalar<T>(T item) where T : IDataObjectBase
        {
            lock (_locker)
            {
                return DB.Update(item);
            }
        }

        public void InsertOrDelete<T, Y>(IEnumerable<T> insertItems, IEnumerable<int> deleteItems)
            where T : IServerDataObject<Y>, new()
        {
            lock (_locker)
            {
                if (insertItems != null && insertItems.Count() > 0)
                {
                    DB.InsertAll(insertItems, false);
                }

                DB.Table<T>().Delete(s => deleteItems.Contains(s.ID));

            }
        }

        public void InsertOrDelete<T>(IEnumerable<T> insertItems, IEnumerable<int> deleteItems)
            where T : IServerDataObject<Guid>, new()
        {
            lock (_locker)
            {
                InsertOrDelete<T, Guid>(insertItems, deleteItems);
            }
        }

        public void InsertOrUpdateBulk<T, Y>(IEnumerable<T> items, bool ignoreUpdate = false, Action<T, T> updateAction = null, IEnumerable<T> existItems = null, bool inTransaction = true, bool shouldDeleteNonExistServerK = false)
            where T : IServerDataObject<Y>, new()
        {
            lock (_locker)
            {
                var defaultId = default(Y);
                var groupItems = items.GroupBy(s => s.ServerK);
                if (groupItems.Any(s => s.Count() > 0 && !s.Key.Equals(defaultId)))
                {
                    var newList = new List<T>();

                    groupItems.ToList().ForEach(s =>
                    {
                        if (s.Key.Equals(defaultId))
                        {
                            newList.AddRange(s.ToList());
                        }
                        else
                        {
                            newList.Add(s.FirstOrDefault());
                        }
                    });
                    items = newList;
                }

                if (items != null && items.Count() > 0)
                {

                    var insertUpdateMethod = typeof(MobileDatabase)
                             .GetMethods()
                             .Where(m => m.Name == "InsertOrUpdateBulk" && m.IsGenericMethod && m.GetGenericArguments().Count() == 2).FirstOrDefault();
                    var listServerIds = items.Select(s => s.ServerK);
                    items = items.Where(s => s != null);
                    var existItemIds = existItems != null ? existItems.Where(s => s != null).Select(s => new { s.ServerK, s.ID }).Where(s => !s.ServerK.Equals(defaultId)) : DB.Table<T>().Select(s => new { s.ServerK, s.ID }).ToList().Where(s => !s.ServerK.Equals(defaultId));
                  
                    var newItems = items.Where(s => !existItemIds.Select(e => e.ServerK).Contains(s.ServerK) && s.ID == 0).ToList();
                    var existItemDictionary = existItemIds.ToLookup(s => s.ServerK, s => s.ID);

                    if (existItems != null || shouldDeleteNonExistServerK)
                    {
                        lock (_locker)
                        {
                            var deleteItemsId = existItemIds.Where(s => !listServerIds.Contains(s.ServerK) && !s.ServerK.Equals(defaultId)).Select(s => s.ID);
                            DB.Table<T>().Delete(s => deleteItemsId.Contains(s.ID));
                        }
                    }

                    var updateItems = items.Where(s => existItemIds.Select(m => m.ServerK).Contains(s.ServerK) || s.ID > 0);
                    foreach (var i in updateItems)
                    {
                        if (i.ID == 0)
                            i.ID = existItemDictionary[i.ServerK].FirstOrDefault();
                    }
                    if (!ignoreUpdate)
                    {
                        if (updateAction == null)
                        {
                            if (updateItems.Count() > 0)
                            {
                                lock (_locker)
                                {
                                    DB.UpdateAll(updateItems);
                                }
                            }
                        }
                        else
                        {
                            var dbIds = existItemIds.Select(m => m.ID);
                            var dbItems = DB.Table<T>().Where(s => dbIds.Contains(s.ID)).ToList();
                            foreach (var dbItem in dbItems)
                            {
                                var serverItem = updateItems.Where(s => s.ServerK.Equals(dbItem.ServerK)).FirstOrDefault();
                                updateAction.Invoke(dbItem, serverItem);
                            }
                            if (dbItems.Count() > 0)
                            {
                                lock (_locker)
                                {
                                    DB.UpdateAll(dbItems);
                                }
                            }
                        }
                    }

                    lock (_locker)
                    {
                        if (newItems != null && newItems.Count > 0)
                        {
                            DB.InsertAll(newItems);

                        }
                    }

                    var needUpdateProperties = typeof(T).GetProperties().Where(s => s.GetCustomAttributes(typeof(ChildrenAttribute), true).Any()
                        || s.GetCustomAttributes(typeof(ParentAttribute), true).Any());

                    foreach (var i in items)
                    {
                        DB.UpdateKeyChildren(i);
                    }

                    foreach (var pro in needUpdateProperties)
                    {
                        var proType = pro.PropertyType;
                        object value = null;
                        if (typeof(IDataObjectBase).IsAssignableFrom(proType)
                           || (proType.IsGenericType && typeof(System.Collections.IEnumerable).IsAssignableFrom(proType.GetGenericTypeDefinition()) && typeof(IDataObjectBase).IsAssignableFrom(proType.GetGenericArguments().FirstOrDefault()))
                           )
                        {
                            var itemType = proType.IsGenericType ? proType.GetGenericArguments().FirstOrDefault() : proType;

                            var serverDataObjectType = itemType.GetInterfaces().Where(s => s.IsGenericType && s.GetGenericTypeDefinition() == typeof(IServerDataObject<>)).FirstOrDefault();

                            var genericeMethod = insertUpdateMethod.MakeGenericMethod(new Type[] { itemType, serverDataObjectType.GetGenericArguments().FirstOrDefault() });
                          
                            foreach (var i in items)
                            {
                                value = pro.GetValue(i);
                                if (value == null)
                                    continue;

                                var type = typeof(List<>).MakeGenericType(itemType);
                                var objectCreated = Activator.CreateInstance(type);
                                var list = objectCreated as System.Collections.IList;
                                if (typeof(IDataObjectBase).IsAssignableFrom(proType))
                                {
                                    list.Add(value);
                                }
                                else
                                {
                                    var dataItems = value as System.Collections.IList;
                                    foreach (var data in dataItems)
                                    {
                                        list.Add(data);
                                    }
                                }

                                if (list.Count > 0)
                                {
                                    genericeMethod.Invoke(this, new object[] { list, true, null, null, false, false });

                                    if (pro.GetCustomAttributes(typeof(ParentAttribute), true).Any())
                                    {
                                        DB.UpdateWithParent(i);
                                        lock (_locker)
                                        {
                                            DB.Update(i);
                                        }
                                    }
                                }
                            }
                        }
                    }

                }
            }

        }

        public void InsertOrUpdateBulkWithGuid<T>(IEnumerable<T> items, bool ignoreUpdate = false, Action<T, T> updateAction = null, IEnumerable<T> existItems = null, bool inTransaction = true, bool shouldDeleteNonExistServerK = false)
            where T : IServerDataObject<Guid>, new()
        {
            lock (_locker)
            {

                InsertOrUpdateBulk<T, Guid>(items, ignoreUpdate, updateAction, existItems, inTransaction);
            }
        }

        public List<T> GetPaging<T>(int skip, int pageSize, out int totalRecord, Expression<Func<T, bool>> predicate = null) where T : IDataObjectBase, new()
        {
            lock (_locker)
            {
                var query = DB.Table<T>();
                if (predicate != null)
                {
                    query = DB.Table<T>().Where(predicate);
                }
                var result = query.Skip(skip).Take(pageSize).ToList();
                totalRecord = query.Count();

                return result;
            }
        }

        public void DeleteVariableExisted<T, Y>(IEnumerable<int> deleteItems) where T : IServerDataObject<Y>, new()
        {
            lock (_locker)
            {
                DB.Table<T>().Delete(s => deleteItems.Contains(s.ID));
            }
        }

        public void BeginTransaction()
        {
            DB.BeginTransaction();
        }

        public void CommitTransaction()
        {
            DB.Commit();
        }

        #endregion
    }
}
