using Acr.UserDialogs;
using System.Windows.Input;
using UCG.siteTRAXLite.Common.Constants;
using UCG.siteTRAXLite.Common.Utils;
using UCG.siteTRAXLite.DependencyServices;
using UCG.siteTRAXLite.Entities.Configuration;
using UCG.siteTRAXLite.Entities.SorEforms;
using UCG.siteTRAXLite.Helpers;
using UCG.siteTRAXLite.Logics;
using UCG.siteTRAXLite.Managers;
using UCG.siteTRAXLite.Managers.ConfigurationManager;
using UCG.siteTRAXLite.Managers.Mappers;
using UCG.siteTRAXLite.Managers.Models;
using UCG.siteTRAXLite.Managers.SorEformManager;
using UCG.siteTRAXLite.Models;
using UCG.siteTRAXLite.Services;

namespace UCG.siteTRAXLite.ViewModels.Sections
{
    public class GenericSamplePageViewModel : ViewModelBase
    {
        private readonly ISorEformManager _sorEformManager;
        private readonly IUploadManager _uploadManager;
        private readonly IFileService _fileService;
        private readonly IMediaService _mediaService;
        private readonly IConfigurationManager _configurationManager;

        private bool IsLoadingQuestion = false;

        public ConcurrentObservableCollection<ActionItemEntity> Questions { get; set; }
        public ConcurrentObservableCollection<ActionItemEntity> SummaryQuestions { get; set; }
        public ConcurrentObservableCollection<Entities.SorEforms.StepperEntity> Steppers { get; set; }
        public ConcurrentObservableCollection<PriceCodeEntity> PriceCodes { get; set; }

        MultiUploadAction<QuestionAttachmentEntity> uploadHelper;

        private bool showQuestions;
        public bool ShowQuestions
        {
            get { return showQuestions; }
            set { SetProperty(ref showQuestions, value); }
        }

        private bool showSummary;
        public bool ShowSummary
        {
            get { return showSummary; }
            set { SetProperty(ref showSummary, value); }
        }

        private Entities.SorEforms.StepperEntity selectedStepper;
        public Entities.SorEforms.StepperEntity SelectedStepper
        {
            get { return selectedStepper; }
            set
            {
                if (value != null)
                {
                    ShowQuestions = true;
                    HandleSelectedStepper(value);
                    SetProperty(ref selectedStepper, value);
                }
            }
        }

        private bool isShowActionButton;
        public bool IsShowActionButton
        {
            get { return isShowActionButton; }
            set { SetProperty(ref isShowActionButton, value); }
        }

        private bool isShowPriceCodeQTY;
        public bool IsShowPriceCodeQTY
        {
            get { return isShowPriceCodeQTY; }
            set { SetProperty(ref isShowPriceCodeQTY, value); }
        }

        private bool isSubmitted;
        public bool IsSubmitted
        {
            get { return isSubmitted; }
            set { SetProperty(ref isSubmitted, value); }
        }

        private ICommand cancelCommand;
        public ICommand CancelCommand
        {
            get
            {
                return this.cancelCommand ?? (this.cancelCommand = new Command(async () => await Cancel()));
            }
        }

        private ICommand confirmCommand;

        public ICommand ConfirmCommand
        {
            get
            {
                return this.confirmCommand ?? (this.confirmCommand = new Command(async () => await Confirm()));
            }
        }

        private ICommand updateActionListCommand;

        public ICommand UpdateActionListCommand
        {
            get
            {
                return updateActionListCommand ?? (updateActionListCommand = new Command<ActionItemEntity>(async (actionItemEntity) => await UpdateActionList(actionItemEntity)));
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

        private ICommand removeImageCommand;
        public ICommand RemoveImageCommand
        {
            get
            {
                return this.removeImageCommand ?? (this.removeImageCommand = new Command<QuestionAttachmentEntity>((image) => RemoveImage(image)));
            }
        }

        public GenericSamplePageViewModel(INavigationService navigationService,
            IAlertService alertService,
            IOpenAppService openAppService,
            ISorEformManager sorEformManager,
            IServiceEntityMapper mapper,
            IUploadManager uploadManager,
            IMediaService mediaService,
            IFileService fileService,
            IConfigurationManager configurationManager,
            IServiceProvider services) : base(navigationService, alertService, openAppService, mapper, services)
        {
            _sorEformManager = sorEformManager;
            uploadHelper = new MultiUploadAction<QuestionAttachmentEntity>();
            Questions = new ConcurrentObservableCollection<ActionItemEntity>();
            SummaryQuestions = new ConcurrentObservableCollection<ActionItemEntity>();
            Steppers = new ConcurrentObservableCollection<Entities.SorEforms.StepperEntity>();
            PriceCodes = new ConcurrentObservableCollection<PriceCodeEntity>();
            _uploadManager = uploadManager;
            _mediaService = mediaService;
            _fileService = fileService;
            _configurationManager = configurationManager;
            IsShowActionButton = true;

            PageTitle = "Jobs";
        }

        public async override Task OnNavigatingTo(object parameter)
        {
            ClearData();
            await LoadSteppers(parameter as Entities.SorEforms.SectionEntity);
        }

        public async Task LoadSteppers(Entities.SorEforms.SectionEntity section)
        {
            if (section == null)
                return;

            var steppersInDb = await _configurationManager.GetGenericSectionSteppers(JobDetail.JobK);

            var steppers = new List<Entities.SorEforms.StepperEntity>();

            if (section.ESectionType == JobSectionType.Generic)
            {
                steppers = await _sorEformManager.GetGenericSectionSteppers();

                steppers.Add(new Entities.SorEforms.StepperEntity
                {
                    Title = "Submit"
                });
            }

            foreach (var stepper in steppers)
            {
                if (steppersInDb != null)
                    SetResponse(stepper, steppersInDb);
                stepper.StepperType = StepperType.Generic;
                Steppers.Add(stepper);
            }

            SelectedStepper = Steppers.FirstOrDefault();
            SelectedStepper.IsChecked = true;
        }

        private void SetResponse(Entities.SorEforms.StepperEntity stepper, List<Entities.Configuration.StepperEntity> steppersInDb)
        {
            var currentStepper = steppersInDb.FirstOrDefault(s => s.Title.Equals(stepper.Title));
            if (currentStepper == null)
                return;

            SetActionResponse(currentStepper.Actions, stepper.ActionList);
        }

        private void SetActionResponse(List<ActionEntity> actions, List<ActionItemEntity> renderActions)
        {
            foreach (var action in actions)
            {
                var matchingRenderAction = renderActions.FirstOrDefault(a => a.Title.Equals(action.Title));
                if (matchingRenderAction == null) continue;

                var response = action.Responses.FirstOrDefault();
                if (response != null)
                {
                    if (matchingRenderAction.ResponseData != null && matchingRenderAction.ResponseData.Any())
                    {
                        var responseData = matchingRenderAction.ResponseData.FirstOrDefault(a => a.Value == response.Value);
                        matchingRenderAction.Response = responseData;
                    }
                    else if (response.Value != null)
                    {
                        matchingRenderAction.Response.Value = response.Value;
                    }
                }

                SetActionResponse(action.ChildActions, matchingRenderAction.SubActionList);
            }
        }

        private void ClearData()
        {
            Questions.Clear();
            Steppers.Clear();
        }

        private async Task UpdateActionList(ActionItemEntity actionItemEntity)
        {
            if (actionItemEntity != null && !IsLoadingQuestion)
            {
                var currentIndex = Questions.IndexOf(actionItemEntity);

                RemoveSubList(actionItemEntity);

                var newQuestions = actionItemEntity.SubActionList
                    .Where(a => a.Condition.ResponseData.Equals(actionItemEntity?.Response?.Value, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                foreach (var action in newQuestions)
                {
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

                    action.Response.Value = null;

                    Questions.Remove(action);
                }
            }
        }

        private void SetLevels(List<ActionItemEntity> Questions, int level = 0)
        {
            foreach (var question in Questions)
            {
                question.Level = level;

                if (question.SubActionList != null)
                {
                    SetLevels(question.SubActionList, level + 1);
                }
            }
        }

        private void HandleSelectedStepper(Entities.SorEforms.StepperEntity stepper)
        {
            if (stepper.Title.Equals("Submit", StringComparison.OrdinalIgnoreCase))
            {
                SummaryQuestions.Clear();
                ShowQuestions = false;
                ShowSummary = true;

                var steppers = Steppers.ToList();
                steppers = steppers.Where(bc => !bc.Title.Equals("Submit", StringComparison.OrdinalIgnoreCase)).ToList();

                foreach (var bc in steppers)
                {
                    AddQuestionsToSummaryTab(bc.ActionList);
                }

                LogicPriceCode();
            }
            else
            {
                ShowQuestions = true;
                ShowSummary = false;
                IsLoadingQuestion = true;

                Questions.Clear();
                var actionList = stepper.ActionList.ToList();
                SetLevels(stepper.ActionList);
                AddQuestions(actionList);

                IsLoadingQuestion = false;
            }

        }

        private void AddQuestions(List<ActionItemEntity> questions)
        {
            if (questions == null || !questions.Any())
                return;

            foreach (var question in questions)
            {
                Questions.Add(question);

                RemoveSubList(question);

                var newQuestions = question.SubActionList
                    .Where(a => a.Condition.ResponseData.Equals(question?.Response?.Value, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                if (newQuestions != null)
                {
                    AddQuestions(newQuestions);
                }
            }
        }

        private void AddQuestionsToSummaryTab(List<ActionItemEntity> questions)
        {
            if (questions == null || !questions.Any())
                return;

            foreach (var question in questions.Where(q => !string.IsNullOrEmpty(q?.Response?.Value) || ShouldUploadQuestionFiles(q)))
            {
                SummaryQuestions.Add(question);

                if (question.SubActionList != null)
                {
                    AddQuestionsToSummaryTab(question.SubActionList);
                }
            }
        }

        private void LogicPriceCode()
        {
            PriceCodes.Clear();
            var actionList = SummaryQuestions.ToList();
            var priceCode777 = LogicPriceCode777.GetPriceCode(actionList);
            if (priceCode777 != null)
            {
                IsShowPriceCodeQTY = true;
                PriceCodes.Add(priceCode777);
            }
        }

        private async Task Cancel()
        {
            await NavigationService.NavigateBackAsync();
        }

        private async Task Confirm()
        {
            IsShowActionButton = false;

            await UpdateConfigToSubmit();

            if (SummaryQuestions.Any(ShouldUploadQuestionFiles))
                await UploadFiles();

            await AlertService.ShowAlertAsync(MessageStrings.Submitted_Successfully);

            IsShowActionButton = true;
            IsSubmitted = true;
        }

        private void RemoveImage(QuestionAttachmentEntity image)
        {
            if (image == null)
                return;

            foreach (var action in Questions.Where(ShouldUploadQuestionFiles))
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
                if (files == null || !files.Any())
                    return;

                var currentFiles = question.FilesUpload?.ToList() ?? new List<QuestionAttachmentEntity>();
                var currentFilePaths = currentFiles.Select(f => f.Source).ToList();

                foreach (var uploadedFile in files)
                {
                    var isDuplicated = FileUploadHelper.IsDuplicate(uploadedFile.ImageUrl, currentFilePaths);
                    if (isDuplicated)
                    {
                        await AlertService.ShowAlertAsync(MessageStrings.Duplicated_File_Warning);
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

        private async Task UploadFiles()
        {
            try
            {
                if (!SummaryQuestions.Any(ShouldUploadQuestionFiles))
                {
                    await AlertService.ShowAlertAsync($"{MessageStrings.Select_Files_Warning}");
                    return;
                }

                var uploadedFiles = SummaryQuestions
                    .Where(ShouldUploadQuestionFiles)
                    .SelectMany(s => s.FilesUpload)
                    .Where(f => !f.IsComplete);

                var fileNames = uploadedFiles.Select(f => f.FileName).ToList();
                if (!FileUploadHelper.ValidateExtention(fileNames))
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

                await AlertService.ShowAlertAsync($"{MessageStrings.Uploaded_Files_Successfully}");
            }
            catch (Exception ex)
            {
                await AlertService.ShowAlertAsync(ex.Message);
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

        private bool ShouldUploadQuestionFiles(ActionItemEntity question)
        {
            return question.EResponseType == SorEformsResponseType.UploadMultiple &&
                   question.FilesUpload != null &&
                   question.FilesUpload.Any();
        }

        private async Task UpdateConfigToSubmit()
        {
            var configActions = new List<ActionEntity>();
            var configSteppers = new List<Entities.Configuration.StepperEntity>();
            var steppers = Steppers.Where(s => s.StepperType == StepperType.Generic && s.Title != "Submit");
            var sectionStepper = new SectionStepperEntity
            {
                SectionStepperK = Guid.NewGuid(),
                StepperType = StepperType.None,
            };
            foreach (var s in steppers)
            {
                var stepperK = Guid.NewGuid();
                var stepper = new Entities.Configuration.StepperEntity
                {
                    StepperK = stepperK,
                    SectionStepperFK = sectionStepper.SectionStepperK,
                    Title = s.Title,
                    Actions = GetActionTree(s.ActionList, stepperK)
                };

                configSteppers.Add(stepper);
            }
            sectionStepper.Steppers = configSteppers;
            var jobTab = new Entities.Configuration.JobTabEntity
            {
                JobTabK = Guid.NewGuid(),
            };
            var sectionEntity = new Entities.Configuration.SectionEntity
            {
                SectionK = Guid.NewGuid(),
                Title = "Generic",
                JobTabFK = jobTab.JobTabK,
                SectionType = JobSectionType.Generic,
                SectionStepperFK = sectionStepper.SectionStepperK,
                SectionStepper = sectionStepper
            };
            jobTab.Sections = new List<Entities.Configuration.SectionEntity>
            {
                sectionEntity
            };
            var configInfo = new Entities.Configuration.ConfigInfoEntity
            {
                ConfigInfoK = Guid.NewGuid(),
                ConfigVersion = 0
            };
            var config = new ConfigEntity()
            {
                JobFK = JobDetail.JobK,
                ConfigK = Guid.NewGuid(),
                ConfigInfoFK = configInfo.ConfigInfoK,
                JobTabFK = jobTab.JobTabK,
                ConfigInfo = configInfo,
                JobTab = jobTab
            };

            await _configurationManager.SubmitGenericSampleSections(config);
        }

        private List<ActionEntity> GetActionTree(List<ActionItemEntity> list, Guid stepperFK, Guid parentFK = default(Guid))
        {
            return list.Select(a =>
            {
                var actionK = Guid.NewGuid();
                if (a.FilesUpload != null && a.FilesUpload.Any())
                    a.FilesUpload.ForEach(f => f.ActionFK = actionK);
                var resDatas = a.ResponseData != null ? a.ResponseData.Select(r => new ResponseDataEntity
                {
                    ResponseDataK = Guid.NewGuid(),
                    Value = r.Value,
                    ActionFK = actionK,
                }).ToList() : null;

                var responses = new List<ResponseEntity> { new ResponseEntity
                    {
                        ResponseK = Guid.NewGuid(),
                        Value = a.Response?.Value,
                        ActionFK = actionK,
                    } };

                var condition = a.Condition?.ResponseData == null ? null : new PreConditionEntity
                {
                    PreConditionK = Guid.NewGuid(),
                    Value = a.Condition?.ResponseData,
                    ActionFK = actionK,
                };

                return new ActionEntity
                {
                    StepperFK = stepperFK,
                    ParentActionFK = parentFK,
                    ActionK = actionK,
                    Title = a.Title,
                    Description = a.Description,
                    ResponseType = a.EResponseType,
                    ResponseDatas = resDatas,
                    Responses = responses,
                    PreCondition = condition,
                    ChildActions = GetActionTree(a.SubActionList, stepperFK, actionK)
                };
            }).ToList();
        }
    }
}
