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

        public List<BreadcrumbEntity> Breadcrumbs { get; set; }

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
            else if (type.ToLower().Equals("claims"))
            {
                return JobSectionType.Claims;
            }

            return JobSectionType.Generic;
        }
    }

    public class Take5BreadcrumbEntity : EntityBase
    {
        private BreadcrumbEntity breadcrumbControl;
        public BreadcrumbEntity BreadcrumbControl { 
            get { return breadcrumbControl; } 
            set { SetProperty(ref breadcrumbControl, value); } 
        }

        private BreadcrumbEntity breadcrumbHazard;
        public BreadcrumbEntity BreadcrumbHazard {
            get { return breadcrumbHazard; }
            set { SetProperty(ref breadcrumbHazard, value); }
        }

        private BreadcrumbEntity breadcrumbSubmit;

        public BreadcrumbEntity BreadcrumbSubmit
        {
            get { return breadcrumbSubmit; }
            set { SetProperty(ref breadcrumbSubmit, value); }
        }
    }

    public class BreadcrumbEntity : EntityBase
    {
        public string Title { get; set; }
        public bool isChecked;
        public bool IsChecked
        {
            get { return isChecked; }
            set { SetProperty(ref isChecked, value); }
        }

        private BreadcrumbType breadcrumbType;
        public BreadcrumbType BreadcrumbType
        {
            get { return breadcrumbType; }
            set { SetProperty(ref breadcrumbType, value); }
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

        public List<ResponseDataItemEntity> ResponseData { get; set; }

        public List<ActionItemEntity> SubActionList { get; set; }

        private SorEformsResponseType GetResponseType(string type)
        {
            if (type.ToLower().Equals("text"))
            {
                return SorEformsResponseType.Text;
            }
            else if (type.ToLower().Equals("list"))
            {
                return SorEformsResponseType.List;
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
    }

    public class ConditionEntity : EntityBase
    {
        public string ResponseData { get; set; }
    }
}
