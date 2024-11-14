using EcoWatt.API.Configuration;
using EcoWatt.API.Extensions;
using EcoWatt.Model;
using EcoWatt.Service.Recommendation;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
IConfiguration configuration = builder.Configuration;
AppConfiguration appConfiguration = new AppConfiguration();
configuration.Bind(appConfiguration);

appConfiguration.ConnectionStrings.OracleFIAP = Environment.GetEnvironmentVariable("ORACLE_CONNECTION_STRING")?? configuration.GetSection("ConnectionStrings:OracleFIAP").Value;

builder.Services.Configure<AppConfiguration>(configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDBContexts(appConfiguration);
builder.Services.AddSwagger(appConfiguration);
builder.Services.AddServices();
builder.Services.AddRepositories();
builder.Services.AddHealthChecks(appConfiguration);
builder.Services.AddAuthenticationAndAutorization();
builder.Services.AddSingleton<RecommendationEngine>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapIdentityApi<AccessUser>();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHealthChecks("/health-check", new HealthCheckOptions()
    {
        Predicate = _ => true,
        ResponseWriter = HealthCheckExtensions.WriteResponse
    });
});

app.MapPost("/logout", async (SignInManager<AccessUser> signInManager, [FromBody] object empty) =>
{
    if (empty != null)
    {
        await signInManager.SignOutAsync();
        return Results.Ok("Logout realizado com sucesso.");
    }
    return Results.Unauthorized();
});


app.Run();
