using SQLite;
using SQLiteNetExtensions.Attributes;
using UCG.siteTRAXLite.DataObjects.DataObject;
using UCG.siteTRAXLite.DataObjects.Job;

namespace UCG.siteTRAXLite.DataObjects.Site
{
    public class SiteDataObject : DataObjectBase<Guid>
    {
        public string CRN { get; set; }
        public string SiteName { get; set; }
        public string StreetNumber { get; set; }
        public string StreetName { get; set; }
        public string Suburb { get; set; }
        public string PostalCode { get; set; }
        public bool IsDeleted { get; set; }
        public string SiteAddress { get; set; }
    }
}
