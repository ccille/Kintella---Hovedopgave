using KintellaLocalizationREST.Model;

namespace KintellaLocalizationREST.Interfaces
{
    public interface ISubModuleService
    {
        public Task<SubModule> GetSubModuleAsync(int subModuleID);
        public Task<List<SubModule>> GetAllSubModulesAsync();
        public bool AddSubModule(SubModule subModule);
        public bool UpdateSubModule(SubModule subModule);
        public bool DeleteSubModule(int subModuleID);
    }
}
