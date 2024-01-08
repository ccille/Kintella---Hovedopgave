using KintellaLocalizationREST.Model;

namespace KintellaLocalizationREST.Interfaces
{
    public interface IComponentContentService
    {
        public Task<ComponentContent> GetComponentContentAsync(int componentContentID);
        public Task<List<ComponentContent>> GetAllComponentContentsAsync();
        public bool AddComponentContent(ComponentContent componentContent);
        public bool UpdateComponentContent(ComponentContent componentContent);
        public bool DeleteComponentContent(int componentContentID);
    }
}
