using System.Windows.Input;
using UCG.siteTRAXLite.Common.Constants;
using UCG.siteTRAXLite.Common.Utils;
using UCG.siteTRAXLite.Entities.SorEforms;
using UCG.siteTRAXLite.Helpers;
using UCG.siteTRAXLite.Logics;
using UCG.siteTRAXLite.Managers;
using UCG.siteTRAXLite.Managers.Models;
using UCG.siteTRAXLite.Services;
using UCG.siteTRAXLite.ViewModels;

namespace UCG.siteTRAXLite.Models.SorClaims
{
    public class ClaimUploadFilesTab : BindableBase
    {
        private readonly IAlertService _alertService;
        private readonly IUploadManager _uploadManager;

        MultiUploadAction<QuestionAttachmentEntity> uploadHelper;

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
                return this.removeImageCommand ?? (this.removeImageCommand = new Command<QuestionAttachmentEntity>((image) => RemoveImage(image)));
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

        public ClaimUploadFilesTab(
            StepperEntity entity,
            IAlertService alertService,
            IUploadManager uploadManager)
        {
            StepperEntity = entity;
            _alertService = alertService;
            _uploadManager = uploadManager;

            uploadHelper = new MultiUploadAction<QuestionAttachmentEntity>();
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
            }

            IsShowUploadButton = SubActions.Any();
        }

        private void RemoveImage(QuestionAttachmentEntity image)
        {
            if (image == null)
                return;

            foreach (var action in SubActions)
            {
                if (!action.FilesUpload.Contains(image))
                    continue;

                action.FilesUpload = action.FilesUpload.Where(i => i != image).ToList();
                break;
            }
        }

        private async Task BrowseFile(ActionItemEntity question)
        {
            try
            {
                var currentFiles = question.FilesUpload?.ToList() ?? new List<QuestionAttachmentEntity>();

                var results = await FilePicker.Default.PickMultipleAsync(new PickOptions
                {
                    PickerTitle = question.Title,
                });

                if (results == null || !results.Any())
                    return;

                var currentFilePaths = currentFiles.Select(f => f.Source).ToList();

                foreach (var uploadedFile in results)
                {
                    var isDuplicated = FileUploadHelper.IsDuplicate(uploadedFile.FullPath, currentFilePaths);
                    if (isDuplicated)
                    {
                        await _alertService.ShowAlertAsync(MessageStrings.Duplicated_File_Warning);

                        return;
                    }
                }

                var uploadedFiles = results.Select(item => new QuestionAttachmentEntity
                {
                    FileName = item.FileName,
                    Source = item.FullPath,
                    FileSize = new FileInfo(item.FullPath).Length,
                    ContentType = item.ContentType
                }).ToList();

                currentFiles.AddRange(uploadedFiles);

                question.FilesUpload = currentFiles.ToList();
            }
            catch (Exception ex)
            {
                return;
            }
        }

        private async Task UploadFiles()
        {
            try
            {
                var isSelectedFiles = SubActions.Any(a => a.FilesUpload != null && a.FilesUpload.Any());

                if (!isSelectedFiles)
                {
                    await _alertService.ShowAlertAsync($"{MessageStrings.Select_Files_Warning}");
                }

                var uploadedFiles = SubActions.SelectMany(s => s.FilesUpload).Where(f => !f.IsComplete);

                var files = uploadedFiles.Select(f => f.FileName).ToList();
                if (!FileUploadHelper.ValidateExtention(files))
                    return;

                IsShowUploadButton = false;
                var accessType = Connectivity.Current.NetworkAccess;
                if (accessType == NetworkAccess.Internet)
                {
                    foreach (var item in uploadedFiles)
                    {
                        uploadHelper.Enqueue(new ItemWithAction<QuestionAttachmentEntity> { Item = item, UploadSingleAction = UploadFileSingle });
                    }

                    await uploadHelper.Upload();
                }

                await _alertService.ShowAlertAsync($"{MessageStrings.Uploaded_Files_Successfully}");
                IsShowUploadButton = true;
            }
            catch (Exception ex)
            {
                await _alertService.ShowAlertAsync(ex.Message);
                IsShowUploadButton = true;
            }
        }

        private async Task UploadFileSingle(QuestionAttachmentEntity fileData)
        {
            var files = new List<FileUploaded>();
            var file = new FileUploaded()
            {
                FileName = Path.GetFileName(fileData.FileName),
                Content = FileUtils.GetBytesFromFilePath(fileData.Source),
                FilePath = fileData.Source
            };
            files.Add(file);

            try
            {
                await _uploadManager.UploadFileToAzureAsync(files, fileData);
            }
            catch (FileUploadException ex)
            {
                throw new FileUploadException(ex.Message);
            }
        }
    }
}
