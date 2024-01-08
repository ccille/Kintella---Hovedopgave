using KintellaLocalizationREST.Data;
using KintellaLocalizationREST.Interfaces;
using KintellaLocalizationREST.Managers;
using KintellaLocalizationREST.Model;

namespace KintellaLocalizationREST.Services
{
    public class ModuleService : IModuleService
    {
        private ModuleDataManager _manager;

        public ModuleService(ModuleDataManager manager)
        {
            _manager = manager;
        }

        // Create
        public bool AddModule(Module module)
        {
            return _manager.AddModule(module);
        }

        // Read
        public Task<List<Module>> GetAllModulesAsync()
        {
            return _manager.GetAllModulesAsync();
        }

        public Task<Module> GetModuleAsync(int moduleID)
        {
            return _manager.GetModuleAsync(moduleID);
        }

        // Update
        public bool UpdateModule(Module module)
        {
            return _manager.UpdateModule(module);
        }

        // Delete
        public bool DeleteModule(int moduleID)
        {
            return _manager.DeleteModuleAsync(moduleID);
        }
    }
}
