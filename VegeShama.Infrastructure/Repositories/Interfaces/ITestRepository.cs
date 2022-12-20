namespace VegeShama.Infrastructure.Repositories.Interfaces
{
    public interface ITestRepository
    {
        int GetEntitiesCount();
        Guid GetFirstSimpleEntityId();
        Guid GetFirstComplexEntityId();
        public void GetSimpleEntity(Guid id);
        public void GetComplexEntity(Guid id);
        public void GetSimpleEntityWithIndex(Guid id);
        public void GetComplexEntityWithIndex(Guid id);
        public void GetAllSimpleEntities();
        public void GetAllComplexEntities();
        public void AddSimpleEntity();
        public void AddComplexEntity();
        public void UpdateSimpleEntity(Guid id);
        public void UpdateComplexEntity(Guid id);
        public void DeleteSimpleEntity(Guid id);
        public void DeleteComplexEntity(Guid id);
    }
}
