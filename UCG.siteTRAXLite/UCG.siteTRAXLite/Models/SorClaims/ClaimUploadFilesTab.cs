using System.Windows.Input;
using UCG.siteTRAXLite.Common.Constants;
using UCG.siteTRAXLite.Entities.SorEforms;
using UCG.siteTRAXLite.ViewModels;

namespace UCG.siteTRAXLite.Models.SorClaims
{
    public class ClaimUploadFilesTab : BindableBase
    {
        private bool isVisible;
        public bool IsVisible
        {
            get { return isVisible; }
            set { SetProperty(ref isVisible, value); }
        }

        private StepperEntity stepperEntity;
        public StepperEntity StepperEntity
        {
            get { return stepperEntity; }
            set { SetProperty(ref stepperEntity, value); }
        }

        private ICommand browseCommand;

        public ICommand BrowseCommand
        {
            get
            {
                return this.browseCommand ?? (this.browseCommand = new Command<ActionItemEntity>(async (q) => await BrowseFile(q)));
            }
        }

        public ConcurrentObservableCollection<ActionItemEntity> SubActions { get; set; }

        private ActionItemEntity secondarySOR;
        public ActionItemEntity SecondarySOR
        {
            get { return secondarySOR; }
            set { SetProperty(ref secondarySOR, value); }
        }

        public ClaimUploadFilesTab(StepperEntity entity)
        {
            StepperEntity = entity;

            SubActions = new ConcurrentObservableCollection<ActionItemEntity>();
        }

        public void LoadSors(ActionItemEntity secondarySor)
        {
            SecondarySOR = secondarySor;
            if (SecondarySOR != null)
            {
                SubActions.Clear();
                foreach (var item in SecondarySOR.SubActionList)
                {
                    if (IsTravelLogicPriceCode551(item) || IsLogic563B(item))
                    {
                        SubActions.Add(item);
                    }

                }
            }
        }

        private bool IsTravelLogicPriceCode551(ActionItemEntity item)
        {
            return !string.IsNullOrEmpty(SecondarySOR.Logic)
                        && SecondarySOR.Logic.Equals(LogicConstant.Logic_Price_Code_551)
                        && item.Title.Equals(LogicConstant.LPC551_Travel_Title)
                        && item.Response != null
                        && !string.IsNullOrEmpty(item.Response.Value)
                        && !string.IsNullOrEmpty(item.ResponseName);
        }

        private bool IsLogic563B(ActionItemEntity item)
        {
            return !string.IsNullOrEmpty(SecondarySOR.Logic)
                        && SecondarySOR.Logic.Equals(LogicConstant.Logic_Price_Code_563B)
                        && item.Title.Equals(LogicConstant.L563B_Travel_Title)
                        && item.Response != null
                        && !string.IsNullOrEmpty(item.Response.Value)
                        && !string.IsNullOrEmpty(item.ResponseName);
        }

        private async Task BrowseFile(ActionItemEntity question)
        {
            try
            {
                var results = await FilePicker.Default.PickMultipleAsync(new PickOptions
                {
                    PickerTitle = question.Title,
                    FileTypes = FilePickerFileType.Images
                });

                if (results == null || !results.Any())
                    return;

                var uploadedFiles = results.Select(item => new QuestionImageEntity
                {
                    FileName = item.FileName,
                    ImageSource = item.FullPath,
                    FileSize = new FileInfo(item.FullPath).Length,
                }).ToList();

                question.FilesUpload = uploadedFiles;
            }
            catch (Exception ex)
            {
                return;
            }
        }
    }
}
