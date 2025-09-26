using Railways.Services;

namespace Railways.API.Endpoints;

public static class StockEndpoints
{
    public static RouteGroupBuilder MapStockEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/stocks");

        // ----------------- Stock Queries -----------------

        group.MapGet("/get-all", async (IStockService service) =>
            await service.GetAllAsync());

        group.MapGet("/player/{playerId:int}", async (int playerId, IStockService service) =>
            await service.GetByPlayerIdAsync(playerId));

        group.MapGet("/company/{companyId:int}", async (int companyId, IStockService service) =>
            await service.GetByCompanyIdAsync(companyId));

        group.MapGet("/{playerId:int}/{companyId:int}", async (int playerId, int companyId, IStockService service) =>
        {
            var stock = await service.GetByIdsAsync(playerId, companyId);
            return stock is not null ? Results.Ok(stock) : Results.NotFound();
        });

        // ----------------- Stock Mutations -----------------

        group.MapPost("/add-or-update", async (int playerId, int companyId, int sharesOwned, IStockService service) =>
        {
            var result = await service.AddOrUpdateAsync(playerId, companyId, sharesOwned);
            return result ? Results.Ok() : Results.BadRequest("Unable to add or update stock");
        });

        group.MapDelete("{playerId:int}/{companyId:int}/delete", async (int playerId, int companyId, IStockService service) =>
        {
            var deleted = await service.DeleteAsync(playerId, companyId);
            return deleted ? Results.Ok() : Results.NotFound();
        });

        return group;
    }
}
