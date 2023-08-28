﻿using CommunityToolkit.Maui.Views;
using System.Windows.Input;
using UCG.siteTRAXLite.CustomControls;
using UCG.siteTRAXLite.Entities.SorEforms;
using UCG.siteTRAXLite.ViewModels;

namespace UCG.siteTRAXLite.Models.Take5
{
    public class Take5TabModel : BindableBase
    {
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

        public Take5TabModel(StepperEntity stepper)
        {
            Stepper = stepper;
            Questions = new ConcurrentObservableCollection<ActionItemEntity>();

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
            try
            {
                var results = await FilePicker.Default.PickMultipleAsync(new PickOptions
                {
                    PickerTitle = question.Title,
                    FileTypes = FilePickerFileType.Images
                });

                if (results == null || !results.Any())
                    return;

                var uploadedFiles = results.Select(item => new QuestionAttachmentEntity
                {
                    FileName = item.FileName,
                    Source = item.FullPath,
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
