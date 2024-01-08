using KintellaLocalizationREST.Data;
using KintellaLocalizationREST.Model;
using Microsoft.EntityFrameworkCore;

namespace KintellaLocalizationREST.Managers
{
    public class ModuleDataManager
    {
        private readonly ApplicationDbContext _context;

        public ModuleDataManager(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool AddModule(Module module)
        {
            // TODO: Fix this block, for adding properly with variables
            try
            {
                _context.Modules.Add(module);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteModuleAsync(int moduleID)
        {
            try
            {
                Module module = _context.Modules.SingleOrDefault(x => x.ModuleID == moduleID);
                _context.Modules.Remove(module);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<List<Module>> GetAllModulesAsync()
        {
            List<Module> modules = new();
            try
            {
                modules = await _context.Modules.ToListAsync();
                return modules;
            }
            catch (Exception ex)
            {
                return modules;
            }
        }

        public async Task<Module> GetModuleAsync(int moduleID)
        {
            Module module = new();
            try
            {
                module = await _context.Modules.SingleOrDefaultAsync(x => x.ModuleID == moduleID);
                return module;
            }
            catch (Exception ex)
            {
                return module;
            }
        }

        public bool UpdateModule(Module module)
        {
            try
            {
                Module moduleInfoDb = _context.Modules.Find(module.ModuleID);
                if (moduleInfoDb != null)
                {
                    moduleInfoDb.ModuleName = module.ModuleName;
                    _context.Modules.Update(moduleInfoDb);
                    _context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
