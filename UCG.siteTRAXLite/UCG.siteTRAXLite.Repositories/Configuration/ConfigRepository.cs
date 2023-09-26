using UCG.siteTRAXLite.DataObjects.Configuration;
using UCG.siteTRAXLite.Repositories.Database;

namespace UCG.siteTRAXLite.Repositories.Configuration
{
    public class ConfigRepository : Repository<ConfigDataObject, Guid>, IConfigRepository
    {
        public ConfigRepository(IMobileDatabase db) : base(db)
        {
        }
    }

    public class ConfigInfoRepository : Repository<ConfigInfoDataObject, Guid>, IConfigInfoRepository
    {
        public ConfigInfoRepository(IMobileDatabase db) : base(db)
        {
        }
    }

    public class JobTabRepository : Repository<JobTabDataObject, Guid>, IJobTabRepository
    {
        public JobTabRepository(IMobileDatabase db) : base(db)
        {
        }
    }

    public class SectionRepository : Repository<SectionDataObject, Guid>, ISectionRepository
    {
        public SectionRepository(IMobileDatabase db) : base(db)
        {
        }
    }

    public class SectionStepperRepository : Repository<SectionStepperDataObject, Guid>, ISectionStepperRepository
    {
        public SectionStepperRepository(IMobileDatabase db) : base(db)
        {
        }
    }

    public class StepperRepository : Repository<StepperDataObject, Guid>, IStepperRepository
    {
        public StepperRepository(IMobileDatabase db) : base(db)
        {
        }
    }

    public class ActionRepository : Repository<ActionDataObject, Guid>, IActionRepository
    {
        public ActionRepository(IMobileDatabase db) : base(db)
        {
        }
    }

    public class PreConditionRepository : Repository<PreConditionDataObject, Guid>, IPreConditionRepository
    {
        public PreConditionRepository(IMobileDatabase db) : base(db)
        {
        }
    }
    public class ResponseDataRepository : Repository<ResponseDataDataObject, Guid>, IResponseDataRepository
    {
        public ResponseDataRepository(IMobileDatabase db) : base(db)
        {
        }
    }

    public class ResponseRepository : Repository<ResponseDataObject, Guid>, IResponseRepository
    {
        public ResponseRepository(IMobileDatabase db) : base(db)
        {
        }
    }
}
