using Railways.Services;

namespace Railways.API.Endpoints;

public static class PlayerEndpoints
{
    public static RouteGroupBuilder MapPlayerEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/players");

        // ----------------- Player CRUD -----------------

        group.MapGet("/get-all", async (IPlayerService service) =>
            await service.GetAllAsync());

        group.MapGet("/{id:int}", async (int id, IPlayerService service) =>
        {
            var player = await service.GetByIdAsync(id);
            return player is not null ? Results.Ok(player) : Results.NotFound();
        });

        group.MapPost("/create", async (string username, int startingMoney, IPlayerService service) =>
        {
            var player = await service.CreateAsync(username, startingMoney);
            return Results.Created($"/players/{player.Id}", player);
        });

        group.MapDelete("/{id:int}/delete", async (int id, IPlayerService service) =>
        {
            var deleted = await service.DeleteAsync(id);
            return deleted ? Results.Ok() : Results.NotFound();
        });


        // ----------------- Game Actions -----------------

        group.MapPost("/{playerId:int}/buy", async (int playerId, int companyId, int shares, IPlayerService service) =>
        {
            var result = await service.BuySharesAsync(playerId, companyId, shares);
            return result ? Results.Ok() : Results.BadRequest("Unable to buy shares");
        });

        group.MapPost("/{playerId:int}/sell", async (int playerId, int companyId, int shares, IPlayerService service) =>
        {
            var result = await service.SellSharesAsync(playerId, companyId, shares);
            return result ? Results.Ok() : Results.BadRequest("Unable to sell shares");
        });

        group.MapPost("/{playerId:int}/ipo", async (int playerId, int companyId, int initialPrice, IPlayerService service) =>
        {
            var result = await service.IPOAsync(playerId, companyId, initialPrice);
            return result ? Results.Ok() : Results.BadRequest("Unable to IPO company");
        });

        group.MapPost("/{playerId:int}/merge", async (int playerId, int companyId1, int companyId2, string newName, IPlayerService service) =>
        {
            var merged = await service.MergeCompaniesAsync(playerId, companyId1, companyId2, newName);
            return merged is not null ? Results.Ok(merged) : Results.BadRequest("Merge failed");
        });

        return group;
    }
}
