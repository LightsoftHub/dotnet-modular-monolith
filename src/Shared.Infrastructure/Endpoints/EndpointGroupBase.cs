using Microsoft.AspNetCore.Builder;

namespace ModularMonolith.Infrastructure.Endpoints;

public abstract class EndpointGroupBase
{
    public abstract void Map(WebApplication app);
}
