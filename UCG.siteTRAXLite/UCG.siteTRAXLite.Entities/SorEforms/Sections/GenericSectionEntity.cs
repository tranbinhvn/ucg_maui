using UCG.siteTRAXLite.Entities.Configuration;
using UCG.siteTRAXLite.Entities.Job;

namespace UCG.siteTRAXLite.Entities.SorEforms.Sections
{
    public class GenericSectionEntity
    {
        public JobEntity JobData { get; set; }
        public ConfigEntity Config { get; set; }
    }
}
