using Microsoft.Maui.Networking;
using UCG.siteTRAXLite.DataObjects.Configuration;
using UCG.siteTRAXLite.Entities.Configuration;
using UCG.siteTRAXLite.Entities.SorEforms;
using UCG.siteTRAXLite.Managers.Mappers;
using UCG.siteTRAXLite.Repositories.Configuration;

namespace UCG.siteTRAXLite.Managers.ConfigurationManager
{
    public class ConfigurationManager : ManagerBase, IConfigurationManager
    {
        private readonly IConfigRepository _configRepo;
        private readonly IConfigInfoRepository _configInfoRepo;
        private readonly IJobTabRepository _jobTabRepo;
        private readonly ISectionRepository _sectionRepo;
        private readonly ISectionStepperRepository _sectionStepperRepo;
        private readonly IStepperRepository _stepperRepo;
        private readonly IActionRepository _actionRepo;
        private readonly IPreConditionRepository _preConditionRepo;
        private readonly IResponseDataRepository _responseDataRepo;
        private readonly IResponseRepository _responseRepo;

        public ConfigurationManager(
            IConnectivity connectivity,
            IServiceEntityMapper mapper,
            IConfigRepository configRepo,
            IConfigInfoRepository configInfoRepo,
            IJobTabRepository jobTabRepo,
            ISectionRepository sectionRepo,
            ISectionStepperRepository sectionStepperRepo,
            IStepperRepository stepperRepo,
            IActionRepository actionRepo,
            IPreConditionRepository preConditionRepo,
            IResponseDataRepository responseDataRepo,
            IResponseRepository responseRepo) : base(connectivity, mapper)
        {
            _configRepo = configRepo;
            _configInfoRepo = configInfoRepo;
            _jobTabRepo = jobTabRepo;
            _sectionRepo = sectionRepo;
            _sectionStepperRepo = sectionStepperRepo;
            _stepperRepo = stepperRepo;
            _actionRepo = actionRepo;
            _preConditionRepo = preConditionRepo;
            _responseDataRepo = responseDataRepo;
            _responseRepo = responseRepo;
        }

        public async Task<List<Entities.Configuration.StepperEntity>> GetGenericSectionSteppers(Guid jobFK)
        {
            var section = await GetLastestSubmitSectionInDb(jobFK, JobSectionType.Generic);
            if (section == null)
                return null;

            var steppers = (from st in _stepperRepo.All()
                            where st.SectionStepperFK == section.SectionStepperFK
                            let acts = (from a in _actionRepo.All(a => a.StepperFK == st.ServerK)
                                        let con = _preConditionRepo.All(pc => pc.ActionFK == a.ServerK).FirstOrDefault()
                                        let datas = _responseDataRepo.All(rd => rd.ActionFK == a.ServerK).ToList()
                                        let res = _responseRepo.All(r => r.ActionFK == a.ServerK).ToList()
                                        select new ActionDataObject
                                        {
                                            ServerK = a.ServerK,
                                            ID = a.ID,
                                            Title = a.Title,
                                            StepperFK = a.StepperFK,
                                            Description = a.Description,
                                            PreCondition = con,
                                            ParentActionFK = a.ParentActionFK,
                                            ResponseDatas = datas,
                                            Responses = res,
                                            ResponseType = a.ResponseType,
                                            ChildActions = GetActionTree(a.ServerK)
                                        }).ToList()
                            select new StepperDataObject
                            {
                                ServerK = st.ServerK,
                                SectionStepperFK = st.SectionStepperFK,
                                ID = st.ID,
                                Title = st.Title,
                                Actions = acts
                            });

            return Mapper.Map<List<Entities.Configuration.StepperEntity>>(steppers);
        }

        public async Task<bool> SubmitGenericSampleSections(ConfigEntity config)
        {
            try
            {
                var configObj = Mapper.Map<ConfigDataObject>(config);
                _configRepo.Save(configObj);
                var lastestConfig = await GetVersionInDb();
                configObj.ConfigInfo.ConfigVersion = lastestConfig != null ? lastestConfig.ConfigVersion + 1 : 0;
                _configInfoRepo.Save(configObj.ConfigInfo);
                _jobTabRepo.Save(configObj.JobTab);
                _sectionRepo.SaveOrUpdate(configObj.JobTab.Sections);
                foreach (var section in configObj.JobTab.Sections)
                {
                    _sectionStepperRepo.Save(section.SectionStepper);

                    _stepperRepo.SaveOrUpdate(section.SectionStepper.Steppers);

                    foreach (var stepper in section.SectionStepper.Steppers)
                        await SaveActions(stepper.Actions);
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        #region Support Functions
        public List<ActionDataObject> GetActionTree(Guid parentActionFK)
        {
            return _actionRepo.All(a => a.ParentActionFK == parentActionFK)
                .Select(a => new ActionDataObject
                {
                    ServerK = a.ServerK,
                    ID = a.ID,
                    Title = a.Title,
                    StepperFK = a.StepperFK,
                    Description = a.Description,
                    PreCondition = _preConditionRepo.All(pc => pc.ActionFK == a.ServerK).FirstOrDefault(),
                    ParentActionFK = a.ParentActionFK,
                    ResponseDatas = _responseDataRepo.All(rd => rd.ActionFK == a.ServerK).ToList(),
                    Responses = _responseRepo.All(r => r.ActionFK == a.ServerK).ToList(),
                    ResponseType = a.ResponseType,
                    ChildActions = GetActionTree(a.ServerK)
                }).ToList();
        }

        public async Task<SectionDataObject> GetLastestSubmitSectionInDb(Guid jobFK, JobSectionType jobSectionType)
        {
            var version = await GetVersionInDb();
            if (version == null)
                return null;

            var config = _configRepo.All()
                .FirstOrDefault(c => c.ConfigInfoFK == version.ServerK && c.JobFK == jobFK);
            if (config == null)
                return null;

            var section = _sectionRepo.All()
                .FirstOrDefault(s => s.JobTabFK == config.JobTabFK && s.SectionType == (int)jobSectionType);
            if (section == null)
                return null;

            return section;
        }

        public async Task<ConfigInfoDataObject> GetVersionInDb(int? version = null)
        {
            var query = _configInfoRepo.All();

            if (version.HasValue)
                query = query.Where(ci => ci.ConfigVersion == version);
            else
                query = query.OrderByDescending(ci => ci.ConfigVersion);

            return query.FirstOrDefault();
        }

        public async Task SaveActions(List<ActionDataObject> actions)
        {
            _actionRepo.SaveOrUpdate(actions);

            foreach (var action in actions)
            {
                if (action.PreCondition != null)
                    _preConditionRepo.Save(action.PreCondition);
                _responseDataRepo.SaveOrUpdate(action.ResponseDatas);
                _responseRepo.SaveOrUpdate(action.Responses);
                await SaveActions(action.ChildActions);
            }
        }
        #endregion
    }
}
