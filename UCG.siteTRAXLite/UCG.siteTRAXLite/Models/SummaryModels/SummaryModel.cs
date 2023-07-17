using UCG.siteTRAXLite.Entities.SorEforms;

namespace UCG.siteTRAXLite.Models.SummaryModels
{
    public class SummaryModel
    {
        public string CRN { get; set; }
        public string SiteName { get; set; }
        public List<ActionItemEntity> Actions { get; set; }
        public string SelectedOutcomeOption { get; set; }
    }
}
