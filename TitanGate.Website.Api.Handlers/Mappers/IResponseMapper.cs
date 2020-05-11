namespace TitanGate.Website.Api.Handlers.Mappers
{
    public interface IResponseMapper<TSource, TDestination>
    {
        TDestination EntityToResponse(TSource entity, string image);
    }
}
