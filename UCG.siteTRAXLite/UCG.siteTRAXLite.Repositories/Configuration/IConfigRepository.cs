using UCG.siteTRAXLite.DataObjects.Configuration;

namespace UCG.siteTRAXLite.Repositories.Configuration
{
    public interface IConfigRepository : IRepository<ConfigDataObject, Guid>
    {
    }
    public interface IConfigInfoRepository : IRepository<ConfigInfoDataObject, Guid>
    {
    }
    public interface IJobTabRepository : IRepository<JobTabDataObject, Guid>
    {
    }
    public interface ISectionRepository : IRepository<SectionDataObject, Guid>
    {
    }
    public interface ISectionStepperRepository : IRepository<SectionStepperDataObject, Guid>
    {
    }
    public interface IStepperRepository : IRepository<StepperDataObject, Guid>
    {
    }
    public interface IActionRepository : IRepository<ActionDataObject, Guid>
    {
    }
    public interface IPreConditionRepository : IRepository<PreConditionDataObject, Guid>
    {
    }
    public interface IResponseDataRepository : IRepository<ResponseDataDataObject, Guid>
    {
    }
    public interface IResponseRepository : IRepository<ResponseDataObject, Guid>
    {
    }
}
