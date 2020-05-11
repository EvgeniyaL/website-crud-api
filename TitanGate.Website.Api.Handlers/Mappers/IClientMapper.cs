namespace TitanGate.Website.Api.Handlers.Mappers
{
    public interface IClientMapper<TSource, TDestination>
    {
        TDestination RequestToEntity(TSource request, string clientSecret);
    }
}