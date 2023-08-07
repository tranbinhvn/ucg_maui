using UCG.siteTRAXLite.Entities.SorEforms;

namespace UCG.siteTRAXLite.CustomSelectors
{
    public class SorResponseDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate ResponseText { get; set; }
        public DataTemplate ResponseSelectSingle { get; set; }
        public DataTemplate ResponseNumber { get; set; }
        public DataTemplate ResponseRadioSingle { get; set; }
        public DataTemplate ResponseTake5SWMsModal { get; set; }
        public DataTemplate ResponseCheckboxSingle { get; set; }
        public DataTemplate ResponseInputTextArea { get; set; }
        public DataTemplate ResponseUploadMultiple { get; set; }
        public DataTemplate ResponseTake5SWMSSummary { get; set; }
        public DataTemplate ResponseTake5HazardsSummary { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var actionItem = item as ActionItemEntity;
            switch (actionItem.EResponseType)
            {
                case SorEformsResponseType.Text:
                    return ResponseText;
                case SorEformsResponseType.SelectSingle:
                    return ResponseSelectSingle;
                case SorEformsResponseType.Number:
                    return ResponseNumber;
                case SorEformsResponseType.RadioSingle:
                    return ResponseRadioSingle;
                case SorEformsResponseType.CheckboxSingle:
                    return ResponseCheckboxSingle;
                case SorEformsResponseType.InputTextArea:
                    return ResponseInputTextArea;
                case SorEformsResponseType.UploadMultiple:
                    return ResponseUploadMultiple;
                case SorEformsResponseType.Take5SWMsModal: 
                    return ResponseTake5SWMsModal;
                case SorEformsResponseType.Take5SWMSSummary:
                    return ResponseTake5SWMSSummary;
                case SorEformsResponseType.Take5HazardsSummary:
                    return ResponseTake5HazardsSummary;
                default:
                    return ResponseText;
            }
        }
    }
}
