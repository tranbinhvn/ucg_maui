using System.Windows.Input;
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
        public ConcurrentObservableCollection<ActionItemEntity> SecondarySORs { get; set; }

        public ClaimSorsTab(StepperEntity entity)
        {
            StepperEntity = entity;

            PrimarySORSData = new ConcurrentObservableCollection<ResponseDataItemEntity>();
            SecondarySORs = new ConcurrentObservableCollection<ActionItemEntity>();

            LoadData();
        }

        private void LoadData()
        {
            if (StepperEntity != null)
            {
                PrimarySOR = StepperEntity.ActionList.FirstOrDefault(a => a.Title.Equals("Primary SOR", StringComparison.OrdinalIgnoreCase) && a.EResponseType == SorEformsResponseType.SelectSingle);

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
                a.Title.Equals("Secondary SORs", StringComparison.OrdinalIgnoreCase) &&
                entity.Value.Equals(a.Condition.ResponseData, StringComparison.OrdinalIgnoreCase)
                );

            if (SecondarySOR != null)
            {
                IsShowSecondarySORs = true;

                ClearData();
                foreach (var item in SecondarySOR.SubActionList)
                {
                    if (!string.IsNullOrEmpty(SecondarySOR.Logic)
                        && SecondarySOR.Logic.Equals("LogicPriceCode551")
                        && item.Title.Equals("Travel"))
                    {
                        foreach (var data in item.ResponseData)
                        {
                            if (data.Value.Equals("100km to 150km"))
                            {
                                data.Validation = "*requires FM authorisation";
                            }
                            if (data.Value.Equals("More than 150km"))
                            {
                                data.Validation = "*requires RM authorisation";
                            }

                            data.HasValidation = !string.IsNullOrEmpty(data.Validation);
                        }
                    }

                    SecondarySORs.Add(item);
                }
            }
        }

        private void ClearData()
        {
            foreach (var item in SecondarySORs)
            {
                item.Response = new ResponseDataItemEntity();
                item.Responses?.Clear();
                item.FilesUpload?.Clear();
                item.IsSaved = false;
                item.ResponseName = string.Empty;
                item.IsShowActionButton = true;
            }

            SecondarySORs.Clear();
        }

        private void EditSor(ActionItemEntity action)
        {
            foreach (var sor in SecondarySORs)
            {
                sor.IsEditing = sor == action;
                sor.IsShowActionButton = !sor.IsEditing;
                sor.IsDisabled = !sor.IsEditing;
            }
        }

        private void RemoveSor(ActionItemEntity action)
        {
            action.IsSaved = false;
            action.Response = new ResponseDataItemEntity();
            action.ResponseName = string.Empty;
            action.IsShowActionButton = true;
            SecondarySORs[SecondarySORs.IndexOf(action)] = action;
            foreach (var sor in SecondarySORs)
            {
                sor.IsDisabled = false;
            }
        }

        public void Confirm(ActionItemEntity action)
        {
            action.IsSaved = true;
            action.IsEditing = !action.IsEditing;
            action.ResponseName = action.Response.Value;
            action.IsShowActionButton = true;
            SecondarySORs[SecondarySORs.IndexOf(action)] = action;
            foreach (var sor in SecondarySORs)
            {
                sor.IsDisabled = false;
            }
        }

        public void Cancel(ActionItemEntity action)
        {
            action.IsEditing = !action.IsEditing;
            action.IsShowActionButton = true;
            foreach (var sor in SecondarySORs)
            {
                sor.IsDisabled = false;
            }
        }
    }
}
