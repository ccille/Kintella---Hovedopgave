using KintellaLocalizationREST.Data;
using KintellaLocalizationREST.Model;
using Microsoft.EntityFrameworkCore;

namespace KintellaLocalizationREST.Managers
{
    public class ComponentContentDataManager
    {
        private readonly ApplicationDbContext _context;

        public ComponentContentDataManager(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool AddComponentContent(ComponentContent componentContent)
        {
            try
            {
                _context.ComponentContents.Add(componentContent);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteComponentContent(int componentContentID)
        {
            try
            {
                ComponentContent componentContent = _context.ComponentContents.SingleOrDefault(x => x.ComponentContentID == componentContentID);
                _context.ComponentContents.Remove(componentContent);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<List<ComponentContent>> GetAllComponentContentsAsync()
        {
            List<ComponentContent> componentContents = new();
            try
            {
                componentContents = await _context.ComponentContents.ToListAsync();
                return componentContents;
            }
            catch (Exception ex)
            {
                return componentContents;
            }
        }

        public async Task<ComponentContent> GetComponentContentAsync(int componentContentID)
        {
            ComponentContent componentContent = new();
            try
            {
                componentContent = await _context.ComponentContents.SingleOrDefaultAsync(x => x.ComponentContentID == componentContentID);
                return componentContent;
            }
            catch (Exception ex)
            {

            }
            return componentContent;
        }

        public bool UpdateComponentContent(ComponentContent componentContent)
        {
            try
            {
                ComponentContent componentContentDb = _context.ComponentContents.Find(componentContent.ComponentContentID);
                if (componentContentDb != null)
                {
                    componentContentDb.ComponentContentID = componentContent.ComponentContentID;
                    componentContentDb.Content = componentContent.Content;
                    _context.ComponentContents.Update(componentContentDb);
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
