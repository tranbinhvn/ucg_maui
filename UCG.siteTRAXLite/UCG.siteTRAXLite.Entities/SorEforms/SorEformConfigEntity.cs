using System.Collections.ObjectModel;

namespace UCG.siteTRAXLite.Entities.SorEforms
{
    public class SorEformConfigEntity: EntityBase
    {
        public ConfigInfoEntity ConfigInfo { get; set; }
        public SettingsEntity Settings { get; set; }
    }

    public class ConfigInfoEntity : EntityBase
    {
        public int ConfigVersion { get; set; }
    }

    public class SettingsEntity : EntityBase
    {
        public List<OutcomeOptionEntity> OutcomeOptions { get; set; }
    }

    public class OutcomeOptionEntity : EntityBase
    {
        public string Name { get; set; }
        public List<ActionItemEntity> ActionList { get; set; }
    }

    public class ActionItemEntity : EntityBase
    {
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
        private string responses;
        public string Responses
        {
            get { return responses; }
            set
            {
                SetProperty(ref responses, value);
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
        public List<string> ResponseData { get; set; }

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
            
            return SorEformsResponseType.Text;
        }
    }

    public class ConditionEntity : EntityBase
    {
        public string ResponseData { get; set; }
    }
}
