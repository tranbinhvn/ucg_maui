using UCG.siteTRAXLite.Entities.SorEforms;

namespace UCG.siteTRAXLite.CustomSelectors
{
    public class SorResponseDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate ResponseText { get; set; }
        public DataTemplate ResponseList { get; set; }
        public DataTemplate ResponseNumber { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var actionItem = item as ActionItemEntity;
            switch (actionItem.EResponseType)
            {
                case SorEformsResponseType.Text:
                    return ResponseText;
                case SorEformsResponseType.List:
                    return ResponseList;
                case SorEformsResponseType.Number:
                    return ResponseNumber;
                default:
                    return ResponseText;
            }
        }
    }
}
