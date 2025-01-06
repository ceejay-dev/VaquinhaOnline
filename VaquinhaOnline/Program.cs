using VaquinhaOnline.Api.Extensions;
using VaquinhaOnline.Application;
using VaquinhaOnline.Infrastructure;

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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
