using UCG.siteTRAXLite.Entities;
using UCG.siteTRAXLite.Entities.SorEforms;

namespace UCG.siteTRAXLite.Models.SummaryModels
{
    public class SummaryModel
    {
        public JobDetailEntity JobDetail { get; set; }
        public List<ActionItemEntity> Actions { get; set; }
        public string SelectedOutcomeOption { get; set; }
    }
}
