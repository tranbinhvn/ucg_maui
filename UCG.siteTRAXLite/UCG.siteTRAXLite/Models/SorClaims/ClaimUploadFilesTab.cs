using System.Windows.Input;
using UCG.siteTRAXLite.Common.Constants;
using UCG.siteTRAXLite.Entities.SorEforms;
using UCG.siteTRAXLite.Logics;
using UCG.siteTRAXLite.Services;
using UCG.siteTRAXLite.ViewModels;

namespace UCG.siteTRAXLite.Models.SorClaims
{
    public class ClaimUploadFilesTab : BindableBase
    {
        private readonly IAlertService _alertService;

        private bool isVisible;
        public bool IsVisible
        {
            get { return isVisible; }
            set { SetProperty(ref isVisible, value); }
        }

        private bool isShowUploadButton;
        public bool IsShowUploadButton
        {
            get { return isShowUploadButton; }
            set { SetProperty(ref isShowUploadButton, value); }
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

        private ICommand removeImageCommand;
        public ICommand RemoveImageCommand
        {
            get
            {
                return this.removeImageCommand ?? (this.removeImageCommand = new Command<QuestionImageEntity>((image) => RemoveImage(image)));
            }
        }

        private ICommand uploadCommand;

        public ICommand UploadCommand
        {
            get
            {
                return this.uploadCommand ?? (this.uploadCommand = new Command(async () => await UploadFiles()));
            }
        }

        public ConcurrentObservableCollection<ActionItemEntity> SubActions { get; set; }

        private ActionItemEntity secondarySOR;
        public ActionItemEntity SecondarySOR
        {
            get { return secondarySOR; }
            set { SetProperty(ref secondarySOR, value); }
        }

        public ClaimUploadFilesTab(StepperEntity entity, IAlertService alertService)
        {
            StepperEntity = entity;
            _alertService = alertService;

            SubActions = new ConcurrentObservableCollection<ActionItemEntity>();
        }

        public void LoadSors(ActionItemEntity secondarySor)
        {
            SecondarySOR = secondarySor;
            SubActions.Clear();
            if (SecondarySOR != null)
            {
                foreach (var item in SecondarySOR.SubActionList)
                {
                    if ( (LogicPriceCode551.CheckLogic(SecondarySOR.Logic, item) && LogicPriceCode551.CheckResponse(item)) 
                        || (LogicPriceCode563B.CheckLogic(SecondarySOR.Logic, item))  && LogicPriceCode563B.CheckResponse(item))
                    {
                        SubActions.Add(item);
                    }
                }

                IsShowUploadButton = SubActions.Any();
                RecalculateAttachmentHeight();
            }
        }

        private void RemoveImage(QuestionImageEntity image)
        {
            if (image == null)
                return;

            foreach (var action in SubActions)
            {
                if (!action.FilesUpload.Contains(image))
                    continue;

                action.FilesUpload = action.FilesUpload.Where(i => i != image).ToList();
                RecalculateAttachmentHeight();
                break;
            }
        }

        private async Task BrowseFile(ActionItemEntity question)
        {
            try
            {
                var currentFiles = question.FilesUpload?.ToList() ?? new List<QuestionImageEntity>();

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

                currentFiles.AddRange(uploadedFiles);

                question.FilesUpload = currentFiles.ToList();
                RecalculateAttachmentHeight();
            }
            catch (Exception ex)
            {
                return;
            }
        }

        private void RecalculateAttachmentHeight()
        {
            foreach (var action in SubActions)
            {
                if (action.FilesUpload == null || !action.FilesUpload.Any())
                    continue;

                action.AttachmentHeightRequest = action.FilesUpload.Count * 80;
            }
        }

        private async Task UploadFiles()
        {
            await _alertService.ShowAlertAsync(MessageStrings.Uploaded_Files_Successfully);
        }
    }
}
