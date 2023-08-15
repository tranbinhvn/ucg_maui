using System.Collections.ObjectModel;

namespace UCG.siteTRAXLite.Entities.SorEforms
{
    public class SorEformConfigEntity: EntityBase
    {
        public ConfigInfoEntity ConfigInfo { get; set; }
        public JobTabEntity JobTab { get; set; }
    }

    public class ConfigInfoEntity : EntityBase
    {
        public int ConfigVersion { get; set; }
    }

    public class JobTabEntity : EntityBase
    {
        public List<SectionEntity> Sections { get; set; }
    }

    public class SectionEntity : EntityBase
    {
        public string Title { get; set; }

        private string sectionType;
        public string SectionType
        {
            get { return sectionType; }
            set
            {
                sectionType = value;
                if (value != null)
                    ESectionType = GetSectionType(value);
            }
        }

        public List<StepperEntity> Steppers { get; set; }

        public JobSectionType ESectionType { get; set; }

        private JobSectionType GetSectionType(string type)
        {
            if (type.ToLower().Equals("generic"))
            {
                return JobSectionType.Generic;
            }
            else if (type.ToLower().Equals("take5"))
            {
                return JobSectionType.Take5;
            }
            else if (type.ToLower().Equals("sorclaims"))
            {
                return JobSectionType.Claims;
            }

            return JobSectionType.Generic;
        }
    }

    public class StepperEntity : EntityBase
    {
        public string Title { get; set; }
        public bool isChecked;
        public bool IsChecked
        {
            get { return isChecked; }
            set { SetProperty(ref isChecked, value); }
        }

        private StepperType stepperType;
        public StepperType StepperType
        {
            get { return stepperType; }
            set { SetProperty(ref stepperType, value); }
        }

        public List<ActionItemEntity> ActionList { get; set; }
    }

    public class ActionItemEntity : EntityBase
    {
        private int index;
        public int Index
        {
            get { return index; }
            set { SetProperty(ref index, value); }
        }

        public ConditionEntity Condition { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public SorEformsResponseType EResponseType { get; set; }
        private string responseType;
        public string ResponseType
        {
            get { return responseType; }
            set 
            { 
                responseType = value; 
                EResponseType = GetResponseType(value); 
            }
        }

        private string responseName;
        public string ResponseName
        {
            get { return responseName; }
            set
            {
                SetProperty(ref responseName, value);
            }
        }

        private ResponseDataItemEntity response = new ResponseDataItemEntity();
        public ResponseDataItemEntity Response
        {
            get { return response; }
            set
            {
                SetProperty(ref response, value);
            }
        }

        private List<ResponseDataItemEntity> responses = new List<ResponseDataItemEntity>();
        public List<ResponseDataItemEntity> Responses
        {
            get { return responses; }
            set
            {
                SetProperty(ref responses, value);
            }
        }

        private List<QuestionImageEntity> filesUpload;
        public List<QuestionImageEntity> FilesUpload
        {
            get { return filesUpload; }
            set
            {
                SetProperty(ref filesUpload, value);
            }
        }

        private string logic;
        public string Logic
        {
            get { return logic; }
            set
            {
                SetProperty(ref logic, value);
            }
        }
        private int level;
        public int Level
        {
            get { return level; }
            set
            {
                SetProperty(ref level, value);
            }
        }

        private bool isShowModal;
        public bool IsShowModal
        {
            get { return isShowModal; }
            set
            {
                SetProperty(ref isShowModal, value);
            }
        }

        private bool isEditing;
        public bool IsEditing
        {
            get { return isEditing; }
            set
            {
                SetProperty(ref isEditing, value);
            }
        }

        private bool isShowActionButton = true;
        public bool IsShowActionButton
        {
            get { return isShowActionButton; }
            set
            {
                SetProperty(ref isShowActionButton, value);
            }
        }

        private bool isDisabled;
        public bool IsDisabled
        {
            get { return isDisabled; }
            set
            {
                SetProperty(ref isDisabled, value);
            }
        }

        private bool isSaved;
        public bool IsSaved
        {
            get { return isSaved; }
            set
            {
                SetProperty(ref isSaved, value);
            }
        }

        public List<ResponseDataItemEntity> ResponseData { get; set; }

        public List<ActionItemEntity> SubActionList { get; set; }

        private SorEformsResponseType GetResponseType(string type)
        {
            if (type.ToLower().Equals("text"))
            {
                return SorEformsResponseType.Text;
            }
            else if (type.ToLower().Equals("select-single"))
            {
                return SorEformsResponseType.SelectSingle;
            }
            else if (type.ToLower().Equals("number"))
            {
                return SorEformsResponseType.Number;
            }
            else if (type.ToLower().Equals("radio-single"))
            {
                return SorEformsResponseType.RadioSingle;
            }
            else if (type.ToLower().Equals("take5-swms-modal"))
            {
                return SorEformsResponseType.Take5SWMsModal;
            }
            else if (type.ToLower().Equals("checkbox-single"))
            {
                return SorEformsResponseType.CheckboxSingle;
            }
            else if (type.ToLower().Equals("input-textarea"))
            {
                return SorEformsResponseType.InputTextArea;
            }
            else if (type.ToLower().Equals("upload-multiple"))
            {
                return SorEformsResponseType.UploadMultiple;
            }
            else if (type.ToLower().Equals("take5-swms-summary"))
            {
                return SorEformsResponseType.Take5SWMSSummary;
            }
            else if (type.ToLower().Equals("take5-hazards-summary"))
            {
                return SorEformsResponseType.Take5HazardsSummary;
            }

            return SorEformsResponseType.Text;
        }
    }

    public class ResponseDataItemEntity: EntityBase
    {
        private string value;
        public string Value
        {
            get { return value; }
            set { SetProperty(ref this.value, value); }
        }

        private bool isChecked;
        public bool IsChecked
        {
            get { return isChecked; }
            set { SetProperty(ref isChecked, value); }
        }

        private bool hasValidation;
        public bool HasValidation
        {
            get { return hasValidation; }
            set { SetProperty(ref hasValidation, value); }
        }

        private string validation;
        public string Validation
        {
            get { return validation; }
            set { SetProperty(ref this.validation, value); }
        }

        private bool hasData;
        public bool HasData
        {
            get { return hasData; }
            set { SetProperty(ref hasData, value); }
        }

        private ResponseDataItemEntity selectedUnit;
        public ResponseDataItemEntity SelectedUnit
        {
            get { return selectedUnit; }
            set
            {
                HasData = value != null;
                SetProperty(ref selectedUnit, value);
            }
        }

        private List<ResponseDataItemEntity> responseData;
        public List<ResponseDataItemEntity> ResponseData
        {
            get { return responseData; }
            set { SetProperty(ref responseData, value); }
        }

        public List<ActionItemEntity> ActionList { get; set; }
    }

    public class ConditionEntity : EntityBase
    {
        public string ResponseData { get; set; }
    }
}
