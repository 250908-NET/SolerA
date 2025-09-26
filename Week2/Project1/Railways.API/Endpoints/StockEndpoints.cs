using Railways.Services;

namespace Railways.API.Endpoints;

public static class StockEndpoints
{
    public static RouteGroupBuilder MapStockEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/stocks");

        group.MapPost("/buy", async (int playerId, int companyId, int shares, IPlayerService service) =>
        {
            var success = await service.BuySharesAsync(playerId, companyId, shares);
            return success ? Results.Ok("✅ Purchase successful.") : Results.BadRequest("❌ Purchase failed.");
        });

        group.MapPost("/sell", async (int playerId, int companyId, int shares, IPlayerService service) =>
        {
            var success = await service.SellSharesAsync(playerId, companyId, shares);
            return success ? Results.Ok("✅ Sell successful.") : Results.BadRequest("❌ Sell failed.");
        });

        return group;
    }
}