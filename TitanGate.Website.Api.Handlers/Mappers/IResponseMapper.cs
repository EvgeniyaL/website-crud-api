namespace TitanGate.Website.Api.Handlers.Mappers
{
    public interface IResponseMapper<Entity, Response>
    {
        Response EntityToResponse(Entity entity, string image);
    }
}
