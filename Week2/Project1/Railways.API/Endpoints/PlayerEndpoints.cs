using Railways.Services;

namespace Railways.API.Endpoints;

public static class PlayerEndpoints
{
    public static RouteGroupBuilder MapPlayerEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/players");

        group.MapGet("/", async (IPlayerService service) =>
            await service.GetAllAsync());

        group.MapGet("/{id:int}", async (int id, IPlayerService service) =>
        {
            var player = await service.GetByIdAsync(id);
            return player is not null ? Results.Ok(player) : Results.NotFound();
        });

        group.MapPost("/", async (string username, IPlayerService service) =>
        {
            var player = await service.CreateAsync(username, 1000);
            return Results.Created($"/players/{player.Id}", player);
        });

        group.MapDelete("/{id:int}", async (int id, IPlayerService service) =>
        {
            var deleted = await service.DeleteAsync(id);
            return deleted ? Results.Ok() : Results.NotFound();
        });

        return group;
    }
}