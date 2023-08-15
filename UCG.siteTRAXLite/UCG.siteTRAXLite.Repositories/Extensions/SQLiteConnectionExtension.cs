using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UCG.siteTRAXLite.DataObjects.DbCustomAttribute;

namespace UCG.siteTRAXLite.Repositories.Extensions
{
    public static class SQLiteConnectionExtension
    {
        public static void UpdateWithParent(this SQLiteConnection DB, object item)
        {
            var parentProperties = item.GetType().GetProperties().Where(s => s.GetCustomAttributes(typeof(ParentAttribute), true).Any());

            foreach (var parentPro in parentProperties)
            {
                var proType = parentPro.PropertyType;

                var value = parentPro.GetValue(item);
                var primaryKeyPro = proType.GetProperties().Where(s => s.GetCustomAttributes(typeof(PrimaryKeyAttribute), true).Any()).FirstOrDefault();
                if (primaryKeyPro != null && value != null)
                {
                    var primaryKeyValue = primaryKeyPro.GetValue(value);

                    var foreignKeyPro = item.GetType().GetProperties().Where(s => s.GetCustomAttributes(typeof(ForeignKeyAttribute), true).Any());
                    foreach (var fr in foreignKeyPro)
                    {
                        var foreignAtt = fr.GetCustomAttributes(typeof(ForeignKeyAttribute), true).FirstOrDefault() as ForeignKeyAttribute;
                        if (proType == foreignAtt.ForeignType)
                        {
                            fr.SetValue(item, primaryKeyValue);
                        }
                    }
                }

            }
        }

        public static void UpdateKeyChildren(this SQLiteConnection DB, object item)
        {
            var itemType = item.GetType();
            var primaryKeyPro = itemType.GetProperties().Where(s => s.GetCustomAttributes(typeof(PrimaryKeyAttribute), true).Any()).FirstOrDefault();
            if (primaryKeyPro != null)
            {
                var primaryKeyValue = primaryKeyPro.GetValue(item);
                if (primaryKeyValue == null)
                    return;

                var childrenPros = itemType.GetProperties().Where(s => s.GetCustomAttributes(typeof(ChildrenAttribute), true).Any());

                foreach (var pro in childrenPros)
                {
                    var value = pro.GetValue(item);
                    if (value == null)
                        continue;
                    var proType = pro.PropertyType;

                    if (proType.IsGenericType)
                    {
                        proType = proType.GetGenericArguments().FirstOrDefault();
                    }

                    var foreignKeyPro = proType.GetProperties().Where(s => s.GetCustomAttributes(typeof(ForeignKeyAttribute), true).Any());
                    List<PropertyInfo> updatedProperties = new List<PropertyInfo>();
                    foreach (var fr in foreignKeyPro)
                    {
                        var foreignAtt = fr.GetCustomAttributes(typeof(ForeignKeyAttribute), true).FirstOrDefault() as ForeignKeyAttribute;
                       
                        if (itemType == foreignAtt.ForeignType)
                        {
                            updatedProperties.Add(fr);
                        }
                    }

                    if (pro.PropertyType.IsGenericType)
                    {
                        if (typeof(System.Collections.IEnumerable).IsAssignableFrom(pro.PropertyType.GetGenericTypeDefinition()))
                        {
                            var list = value as System.Collections.IList;
                            foreach (var i in list)
                            {
                                if (i == null)
                                    continue;
                                foreach (var p in updatedProperties)
                                {
                                    p.SetValue(i, primaryKeyValue);
                                }
                            }
                        }
                    }
                    else
                    {
                        foreach (var p in updatedProperties)
                        {
                            p.SetValue(value, primaryKeyValue);
                        }
                    }
                }
            }
        }
    }
}
