namespace TitanGate.Website.Api.Handlers.Mappers
{
    public interface IEntityMapper<Request,Entity>
    {
        Entity RequestToEntity(Request request, string filePath, string password);
    }
}
