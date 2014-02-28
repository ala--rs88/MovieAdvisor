namespace MovieAdvisor.BulkDataLoader.Core.Interfaces
{
    public interface IEntityParser<out TEntity>
        where TEntity : class
    {
        TEntity ParseFromString(string stringRepresentation);
    }
}