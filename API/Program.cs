using API.Extensions;
using API.Middleware;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();
app.UseStatusCodePagesWithRedirects("/errors/{0}");
// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseStaticFiles();

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

using var scope=app.Services.CreateScope();
var services= scope.ServiceProvider;
var context = services.GetRequiredService<ShopContext>();
var logger = services.GetRequiredService<ILogger<Program>>();

try{
    await context.Database.MigrateAsync();
    await ShopContextSeed.SeedAsync(context);
}
catch(Exception ex)
{
    logger.LogError(ex,"An Error Occured during Migration!");
}

app.Run();
