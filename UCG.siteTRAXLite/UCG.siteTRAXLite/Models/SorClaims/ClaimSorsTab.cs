using System.Windows.Input;
using UCG.siteTRAXLite.Common.Constants;
using UCG.siteTRAXLite.Entities.SorEforms;
using UCG.siteTRAXLite.ViewModels;

namespace UCG.siteTRAXLite.Models.SorClaims
{
    public class ClaimSorsTab : BindableBase
    {
        private bool isVisible;
        public bool IsVisible
        {
            get { return isVisible; }
            set { SetProperty(ref isVisible, value); }
        }

        private bool isShowSecondarySORs;
        public bool IsShowSecondarySORs
        {
            get { return isShowSecondarySORs; }
            set { SetProperty(ref isShowSecondarySORs, value); }
        }

        private StepperEntity stepperEntity;
        public StepperEntity StepperEntity
        {
            get { return stepperEntity; }
            set { SetProperty(ref stepperEntity, value); }
        }

        private ActionItemEntity primarySOR;
        public ActionItemEntity PrimarySOR
        {
            get { return primarySOR; }
            set { SetProperty(ref primarySOR, value); }
        }

        private ActionItemEntity secondarySOR;
        public ActionItemEntity SecondarySOR
        {
            get { return secondarySOR; }
            set { SetProperty(ref secondarySOR, value); }
        }

        private ResponseDataItemEntity selectedPrimarySors;
        public ResponseDataItemEntity SelectedPrimarySors
        {
            get { return selectedPrimarySors; }
            set
            {
                if (value != null)
                {
                    LoadSecondarySors(value);
                }

                SetProperty(ref selectedPrimarySors, value);
            }
        }

        private ICommand editSorCommand;

        public ICommand EditSorCommand
        {
            get
            {
                return this.editSorCommand ?? (this.editSorCommand = new Command<ActionItemEntity>((action) => EditSor(action)));
            }
        }

        private ICommand removeSorCommand;

        public ICommand RemoveSorCommand
        {
            get
            {
                return this.removeSorCommand ?? (this.removeSorCommand = new Command<ActionItemEntity>((action) => RemoveSor(action)));
            }
        }

        private ICommand cancelCommand;
        public ICommand CancelCommand
        {
            get
            {
                return this.cancelCommand ?? (this.cancelCommand = new Command<ActionItemEntity>((action) => Cancel(action)));
            }
        }

        private ICommand confirmCommand;

        public ICommand ConfirmCommand
        {
            get
            {
                return this.confirmCommand ?? (this.confirmCommand = new Command<ActionItemEntity>((action) => Confirm(action)));
            }
        }

        public ConcurrentObservableCollection<ResponseDataItemEntity> PrimarySORSData { get; set; }
        public ConcurrentObservableCollection<ActionItemEntity> SubActions { get; set; }

        public ClaimSorsTab(StepperEntity entity)
        {
            StepperEntity = entity;

            PrimarySORSData = new ConcurrentObservableCollection<ResponseDataItemEntity>();
            SubActions = new ConcurrentObservableCollection<ActionItemEntity>();

            LoadData();
        }

        private void LoadData()
        {
            if (StepperEntity != null)
            {
                PrimarySOR = StepperEntity.ActionList.FirstOrDefault(a => a.Title.Equals(LogicConstant.PrimarySOR, StringComparison.OrdinalIgnoreCase) && a.EResponseType == SorEformsResponseType.SelectSingle);

                if (PrimarySOR != null)
                {
                    PrimarySORSData.Clear();
                    foreach (var item in PrimarySOR.ResponseData)
                    {
                        PrimarySORSData.Add(item);
                    }
                }
            }
        }

        private void LoadSecondarySors(ResponseDataItemEntity entity)
        {
            SecondarySOR = PrimarySOR.SubActionList.FirstOrDefault(a => 
                a.Title.Equals(LogicConstant.Secondary_SORs, StringComparison.OrdinalIgnoreCase) &&
                entity.Value.Equals(a.Condition.ResponseData, StringComparison.OrdinalIgnoreCase)
                );

            if (SecondarySOR != null)
            {
                IsShowSecondarySORs = true;
                SubActions.Clear();

                foreach (var item in SecondarySOR.SubActionList)
                {
                    RemoveResponse(item);

                    if (!string.IsNullOrEmpty(SecondarySOR.Logic)
                        && SecondarySOR.Logic.Equals(LogicConstant.Logic_Price_Code_551)
                        && item.Title.Equals(LogicConstant.LPC551_Travel_Title))
                    {
                        foreach (var data in item.ResponseData)
                        {
                            if (data.Value.Equals(LogicConstant.LPC551_FM_Authorisation_Option))
                            {
                                data.Validation = LogicConstant.LPC551_FM_Authorisation_Message;
                            }
                            if (data.Value.Equals(LogicConstant.LPC551_RM_Authorisation_Option))
                            {
                                data.Validation = LogicConstant.LPC551_RM_Authorisation_Message;
                            }

                            data.HasValidation = !string.IsNullOrEmpty(data.Validation);
                        }
                    }

                    SubActions.Add(item);
                }
            }
        }

        private void EditSor(ActionItemEntity action)
        {
            foreach (var act in SubActions)
            {
                act.IsEditing = act == action;
                act.IsShowActionButton = !act.IsEditing;
                act.IsDisabled = !act.IsEditing;
            }
        }

        private void RemoveSor(ActionItemEntity action)
        {
            RemoveResponse(action);
            SubActions[SubActions.IndexOf(action)] = action;
            foreach (var act in SubActions)
            {
                act.IsDisabled = false;
            }
        }

        public void Confirm(ActionItemEntity action)
        {
            if (action.Response == null || string.IsNullOrEmpty(action.Response.Value))
            {
                Cancel(action);
                return;
            }

            action.IsSaved = true;
            action.IsEditing = !action.IsEditing;
            action.ResponseName = action.Response.Value;
            action.IsShowActionButton = true;
            action.FilesUpload?.Clear();
            SubActions[SubActions.IndexOf(action)] = action;
            foreach (var act in SubActions)
            {
                act.IsDisabled = false;
            }
        }

        public void Cancel(ActionItemEntity action)
        {
            action.IsEditing = !action.IsEditing;
            action.IsShowActionButton = true;
            action.Response = action.ResponseData.FirstOrDefault(d => d.Value.Equals(action.ResponseName)) ?? new ResponseDataItemEntity();
            foreach (var act in SubActions)
            {
                act.IsDisabled = false;
            }
        }

        private void RemoveResponse(ActionItemEntity action)
        {
            action.Response = new ResponseDataItemEntity();
            action.ResponseName = string.Empty;
            action.FilesUpload?.Clear();
            action.Responses?.Clear();
            action.IsEditing = false;
            action.IsDisabled = false;
            action.IsSaved = false;
            action.IsShowActionButton = true;
        }
    }
}
