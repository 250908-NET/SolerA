using Railways.Services;

namespace Railways.API.Endpoints;

public static class CompanyEndpoints
{
    public static RouteGroupBuilder MapCompanyEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/companies");

        // ----------------- Company CRUD -----------------

        group.MapGet("/get-all", async (ICompanyService service) =>
            await service.GetAllAsync());

        group.MapGet("/{id:int}", async (int id, ICompanyService service) =>
        {
            var company = await service.GetByIdAsync(id);
            return company is not null ? Results.Ok(company) : Results.NotFound();
        });

        group.MapPost("/create", async (string name, int startingMoney, int startingStockPrice, ICompanyService service) =>
        {
            var company = await service.CreateAsync(name, startingMoney, startingStockPrice);
            return Results.Created($"/companies/{company.Id}", company);
        });

        group.MapDelete("/{id:int}/delete", async (int id, ICompanyService service) =>
        {
            var deleted = await service.DeleteAsync(id);
            return deleted ? Results.Ok() : Results.NotFound();
        });


        // ----------------- Game Actions -----------------

        group.MapPost("/{id:int}/payout", async (int id, int totalRevenue, ICompanyService service) =>
        {
            var result = await service.PayoutAsync(id, totalRevenue);
            return result ? Results.Ok() : Results.BadRequest("Unable to payout");
        });

        group.MapPost("/{id:int}/withhold", async (int id, int totalRevenue, ICompanyService service) =>
        {
            var result = await service.WithholdAsync(id, totalRevenue);
            return result ? Results.Ok() : Results.BadRequest("Unable to withhold revenue");
        });

        group.MapPost("/{id:int}/adjust-money", async (int id, int amount, ICompanyService service) =>
        {
            var result = await service.AdjustMoneyAsync(id, amount);
            return result ? Results.Ok() : Results.BadRequest("Unable to adjust company money");
        });

        return group;
    }
}
