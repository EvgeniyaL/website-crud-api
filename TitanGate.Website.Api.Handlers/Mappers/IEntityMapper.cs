namespace TitanGate.Website.Api.Handlers.Mappers
{
    public interface IEntityMapper<TSource, TDestination>
    {
        TDestination RequestToEntity(TSource request, string filePath, string password);
    }
}
