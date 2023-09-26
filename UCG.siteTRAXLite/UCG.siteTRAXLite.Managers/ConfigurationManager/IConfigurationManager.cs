using UCG.siteTRAXLite.Entities.Configuration;

namespace UCG.siteTRAXLite.Managers.ConfigurationManager
{
    public interface IConfigurationManager
    {
        Task<bool> SubmitGenericSampleSections(ConfigEntity config);
        Task<List<StepperEntity>> GetGenericSectionSteppers(Guid jobFK);
    }
}
