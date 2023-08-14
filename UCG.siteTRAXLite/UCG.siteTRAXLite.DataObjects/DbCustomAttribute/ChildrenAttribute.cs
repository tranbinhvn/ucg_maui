using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UCG.siteTRAXLite.DataObjects.DbCustomAttribute
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false)]
    public class ChildrenAttribute : Attribute
    {
    }
}
