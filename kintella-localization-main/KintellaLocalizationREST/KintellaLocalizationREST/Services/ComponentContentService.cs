using KintellaLocalizationREST.Interfaces;
using KintellaLocalizationREST.Managers;
using KintellaLocalizationREST.Model;

namespace KintellaLocalizationREST.Services
{
    public class ComponentContentService : IComponentContentService
    {
        private readonly ComponentContentDataManager _manager;

        public ComponentContentService(ComponentContentDataManager manager)
        {
            _manager = manager;
        }

        public Task<ComponentContent> GetComponentContentAsync(int componentContentID)
        {
            return _manager.GetComponentContentAsync(componentContentID);
        }

        public Task<List<ComponentContent>> GetAllComponentContentsAsync()
        {
            return _manager.GetAllComponentContentsAsync();
        }

        public bool AddComponentContent(ComponentContent componentContent)
        {
            return _manager.AddComponentContent(componentContent);
        }

        public bool UpdateComponentContent(ComponentContent componentContent)
        {
            return _manager.UpdateComponentContent(componentContent);
        }

        public bool DeleteComponentContent(int componentContentID)
        {
            return _manager.DeleteComponentContent(componentContentID);
        }
    }
}
