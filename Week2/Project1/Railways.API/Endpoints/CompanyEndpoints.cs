using Railways.Services;

namespace Railways.API.Endpoints;

public static class CompanyEndpoints
{
    public static RouteGroupBuilder MapCompanyEndpoints(this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/companies");

        group.MapGet("/", async (ICompanyService service) =>
            await service.GetAllAsync());

        group.MapGet("/{id:int}", async (int id, ICompanyService service) =>
        {
            var company = await service.GetByIdAsync(id);
            return company is not null ? Results.Ok(company) : Results.NotFound();
        });

        group.MapPost("/", async (string name, int totalShares, ICompanyService service) =>
        {
            var company = await service.CreateAsync(name, 1000, totalShares);
            return Results.Created($"/companies/{company.Id}", company);
        });

        return group;
    }
}