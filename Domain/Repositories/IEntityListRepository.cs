namespace Domain.Repositories;

public interface IEntityListRepository<T>
{
    public List<T> GetEntityList();
}
