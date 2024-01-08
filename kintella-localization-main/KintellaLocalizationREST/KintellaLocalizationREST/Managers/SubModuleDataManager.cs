using KintellaLocalizationREST.Data;
using KintellaLocalizationREST.Model;
using Microsoft.EntityFrameworkCore;

namespace KintellaLocalizationREST.Managers
{
    public class SubModuleDataManager
    {
        private readonly ApplicationDbContext _context;

        public SubModuleDataManager(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool AddSubModule(SubModule subModule)
        {
            // TODO: Fix this block, for adding properly with variables
            try
            {
                _context.SubModules.Add(subModule);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteSubModuleAsync(int subModuleID)
        {
            try
            {
                SubModule subModule = _context.SubModules.SingleOrDefault(x => x.SubModuleID == subModuleID);
                _context.SubModules.Remove(subModule);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<List<SubModule>> GetAllSubModulesAsync()
        {
            List<SubModule> subModules = new();
            try
            {
                subModules = await _context.SubModules.ToListAsync();
                return subModules;
            }
            catch (Exception ex)
            {
                return subModules;
            }
        }

        public async Task<SubModule> GetSubModuleAsync(int subModuleID)
        {
            SubModule subModule = new();
            try
            {
                subModule = await _context.SubModules.SingleOrDefaultAsync(x => x.SubModuleID == subModuleID);
                return subModule;
            }
            catch (Exception ex)
            {
                return subModule;
            }
        }

        public bool UpdateSubModule(SubModule subModule)
        {
            try
            {
                SubModule subModuleInfoDb = _context.SubModules.Find(subModule.SubModuleID);
                if (subModuleInfoDb != null)
                {
                    subModuleInfoDb.SubModuleName = subModule.SubModuleName;
                    _context.SubModules.Update(subModuleInfoDb);
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
