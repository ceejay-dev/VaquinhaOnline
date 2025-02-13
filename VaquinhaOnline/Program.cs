using VaquinhaOnline.Api.Extensions;
using VaquinhaOnline.Application;
using VaquinhaOnline.Infrastructure;
using VaquinhaOnline.Infrastructure.Seeds;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddApplication(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);

// Adding swagger configuration.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();

//Adding authentication & authorization configuration.
builder.Services.AddAuth(builder.Configuration);

builder.Services.AddCors(options =>
{

    options.AddPolicy("CorsPolicy", builder =>
        builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

var app = builder.Build();

// Chamar o SeedService para popular a base de dados
using (var scope = app.Services.CreateScope())
{
    var seedService = scope.ServiceProvider.GetRequiredService<ISeedService>();
    await seedService.SeedDatabaseAsync(scope.ServiceProvider);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
