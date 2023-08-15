using UCG.siteTRAXLite.Entities.SorEforms;

namespace UCG.siteTRAXLite.CustomSelectors
{
    public class SecondarySorDataTemplateSelector : DataTemplateSelector
    {
        public DataTemplate SorUnsavedTemplate { get; set; }
        public DataTemplate SorSavedTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var actionItem = item as ActionItemEntity;

            return actionItem.IsSaved ? SorSavedTemplate : SorUnsavedTemplate;
        }
    }
}
