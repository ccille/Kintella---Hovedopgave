using KintellaLocalizationREST.Model;

namespace KintellaLocalizationREST.Interfaces
{
    public interface IModuleService
    {
        public Task<Module> GetModuleAsync(int moduleID);
        public Task<List<Module>> GetAllModulesAsync();
        public bool AddModule(Module module);
        public bool UpdateModule(Module module);
        public bool DeleteModule(int moduleID);
    }
}
