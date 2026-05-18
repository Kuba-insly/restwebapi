using restwebapi.Data;
using Microsoft.EntityFrameworkCore;
using restwebapi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddScoped<IDbService, DbService>();
builder.Services.AddDbContext<AppDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

app.MapOpenApi();
app.UseSwaggerUI(opt =>
{
    opt.SwaggerEndpoint("/openapi/v1.json", "v1");
});

app.UseAuthorization();

app.MapControllers();

app.Run();