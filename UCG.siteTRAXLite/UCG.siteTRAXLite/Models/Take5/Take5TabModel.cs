using CommunityToolkit.Maui.Views;
using System.Windows.Input;
using UCG.siteTRAXLite.Common.Constants;
using UCG.siteTRAXLite.Common.Utils;
using UCG.siteTRAXLite.CustomControls;
using UCG.siteTRAXLite.Entities.SorEforms;
using UCG.siteTRAXLite.Helpers;
using UCG.siteTRAXLite.Managers;
using UCG.siteTRAXLite.Managers.Models;
using UCG.siteTRAXLite.Services;
using UCG.siteTRAXLite.ViewModels;
using CommunityToolkit.Mvvm.Messaging;
using UCG.siteTRAXLite.Messages;
using Acr.UserDialogs;
using UCG.siteTRAXLite.DependencyServices;

#if IOS
using MobileCoreServices;
#endif

namespace UCG.siteTRAXLite.Models.Take5
{
    public class Take5TabModel : BindableBase
    {
        private readonly IAlertService _alertService;

        MultiUploadAction<QuestionAttachmentEntity> uploadHelper;
        private readonly IUploadManager _uploadManager;
        private readonly IFileService _fileService;
        private readonly IMediaService _mediaService;

        private bool isVisible;
        public bool IsVisible
        {
            get { return isVisible; }
            set
            {
                SetProperty(ref isVisible, value);
            }
        }

        private StepperEntity stepper;
        public StepperEntity Stepper
        {
            get { return stepper; }
            set
            {
                SetProperty(ref stepper, value);
            }
        }

        private ICommand browseCommand;

        public ICommand BrowseCommand
        {
            get
            {
                return this.browseCommand ?? (this.browseCommand = new Command<ActionItemEntity>(async (q) => await BrowseFile(q)));
            }
        }

        private ICommand updateActionListCommand;

        public ICommand UpdateActionListCommand
        {
            get
            {
                return this.updateActionListCommand ?? (this.updateActionListCommand = new Command<ActionItemEntity>(async (actionItemEntity) => await UpdateActionList(actionItemEntity)));
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

        public ConcurrentObservableCollection<ActionItemEntity> Questions { get; set; }

        public Take5TabModel(StepperEntity stepper, 
            IAlertService alertService,
            IUploadManager uploadManager,
            IMediaService mediaService,
            IFileService fileService)
        {
            Stepper = stepper;
            _alertService = alertService;
            _uploadManager = uploadManager;
            _mediaService = mediaService;
            _fileService = fileService;
            Questions = new ConcurrentObservableCollection<ActionItemEntity>();
            uploadHelper = new MultiUploadAction<QuestionAttachmentEntity>();

            LoadQuestions();
        }

        public void SetResponseRadioSingle(ActionItemEntity action, ResponseDataItemEntity value)
        {
            action.Response = value;
            if (action != null)
                UpdateActionList(action);
        }

        private void LoadQuestions()
        {
            var i = 0;
            foreach (var item in Stepper.ActionList)
            {
                item.Index = i++;
                Questions.Add(item);
            }

            SetLevels(Stepper.ActionList);
        }

        private void SetLevels(List<ActionItemEntity> actions, int level = 0)
        {
            foreach (var action in actions)
            {
                action.Level = level;

                if (action.SubActionList != null)
                {
                    SetLevels(action.SubActionList, level + 1);
                }
            }
        }

        private async Task UpdateActionList(ActionItemEntity actionItemEntity)
        {
            if (actionItemEntity != null)
            {
                var isCheckboxSingle = actionItemEntity.EResponseType == SorEformsResponseType.CheckboxSingle;
                var responseName = actionItemEntity.Response.Value;
                var isChecked = actionItemEntity.Response.IsChecked;

                var currentIndex = Questions.IndexOf(actionItemEntity);

                RemoveSubList(actionItemEntity);

                var newQuestions = actionItemEntity.SubActionList
                     .Where(a =>
                         (isCheckboxSingle && a.Condition.ResponseData.Equals(responseName, StringComparison.OrdinalIgnoreCase) && isChecked)
                         || (!isCheckboxSingle && a.Condition.ResponseData.Equals(responseName, StringComparison.OrdinalIgnoreCase))
                     )
                     .ToList();

                foreach (var action in newQuestions)
                {
                    action.Index = currentIndex;
                    Questions.Insert(currentIndex + 1, action);
                }
            }
        }

        public void RemoveSubList(ActionItemEntity actionItemEntity)
        {
            if (actionItemEntity.SubActionList != null && actionItemEntity.SubActionList.Any())
            {
                foreach (var action in actionItemEntity.SubActionList)
                {
                    if (action.EResponseType == SorEformsResponseType.SelectSingle)
                        RemoveSubList(action);

                    if (!Questions.Contains(action))
                        continue;

                    if (action.EResponseType != SorEformsResponseType.InputTextArea)
                        action.Response.Value = string.Empty;

                    action.Responses.Clear();

                    Questions.Remove(action);
                }
            }
        }

        private void RemoveImage(QuestionAttachmentEntity image)
        {
            if (image == null)
                return;

            var uploadQuesitons = Questions.Where(q => q.EResponseType == SorEformsResponseType.UploadMultiple);
            foreach (var action in uploadQuesitons)
            {
                if (!action.FilesUpload.Contains(image))
                    continue;

                action.FilesUpload = action.FilesUpload.Where(i => i != image).ToList();
                break;
            }
        }


        private async Task BrowseFile(ActionItemEntity question)
        {
            var results = new List<ImageModel>();
#if IOS
            var actionSheetConfig = new ActionSheetConfig()
            {
                Options = new List<ActionSheetOption>()
                {
                    new ActionSheetOption("Photos") {
                        Action = async () => {
                            var photo = await this._mediaService.OpenGallery();
                            results.Add(photo);
                            await UpdateFilesUploaded(question, results); 
                        }
                    },

                    new ActionSheetOption("Files") {
                        Action = async () => { 
                            results = await _fileService.SelectMultiFile();
                            await UpdateFilesUploaded(question, results); 
                        } 
                    }
                }
            };

            UserDialogs.Instance.ActionSheet(actionSheetConfig);
#else
            results =  await this._fileService.SelectMultiFile();
            await UpdateFilesUploaded(question, results);
#endif
        }

        public async Task UpdateFilesUploaded(ActionItemEntity question, List<ImageModel> files)
        {
            try
            {
                var currentFiles = question.FilesUpload?.ToList() ?? new List<QuestionAttachmentEntity>();

                if (files == null || !files.Any())
                    return;

                var currentFilePaths = currentFiles.Select(f => f.Source).ToList();

                foreach (var uploadedFile in files)
                {
                    var isDuplicated = FileUploadHelper.IsDuplicate(uploadedFile.ImageUrl, currentFilePaths);
                    if (isDuplicated)
                    {
                        await _alertService.ShowAlertAsync(MessageStrings.Duplicated_File_Warning);

                        return;
                    }
                }

                var uploadedFiles = files.Select(item => new QuestionAttachmentEntity
                {
                    FileName = item.FileName,
                    Source = item.ImageUrl,
                    FileSize = item.FileSize,
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

        public async Task UploadFiles()
        {
            try
            {
                var isSelectedFiles = Questions.Any(a => a.FilesUpload != null && a.FilesUpload.Any());

                if (!isSelectedFiles)
                {
                    await _alertService.ShowAlertAsync($"{MessageStrings.Select_Files_Warning}");
                }

                var uploadedFiles = Questions.Where(q => q.EResponseType == SorEformsResponseType.UploadMultiple).SelectMany(s => s.FilesUpload)
                    .Where(f => !f.IsComplete);

                var files = uploadedFiles.Select(f => f.FileName).ToList();
                if (!FileUploadHelper.ValidateExtention(files))
                    return;

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
            }
            catch (Exception ex)
            {
                await _alertService.ShowAlertAsync(ex.Message);
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
