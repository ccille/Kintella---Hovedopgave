using KintellaLocalizationREST.Interfaces;
using KintellaLocalizationREST.Managers;
using KintellaLocalizationREST.Model;

namespace KintellaLocalizationREST.Services
{
    public class SubModuleService : ISubModuleService
    {
        private readonly SubModuleDataManager _manager;

        public SubModuleService(SubModuleDataManager manager)
        {
            _manager = manager;
        }

        public bool AddSubModule(SubModule subModule)
        {
            return _manager.AddSubModule(subModule);
        }

        public bool DeleteSubModule(int subModuleID)
        {
            return _manager.DeleteSubModuleAsync(subModuleID);
        }

        public Task<List<SubModule>> GetAllSubModulesAsync()
        {
            return _manager.GetAllSubModulesAsync();
        }

        public Task<SubModule> GetSubModuleAsync(int subModuleID)
        {
            return _manager.GetSubModuleAsync(subModuleID);
        }

        public bool UpdateSubModule(SubModule subModule)
        {
            return _manager.UpdateSubModule(subModule);
        }
    }
}
