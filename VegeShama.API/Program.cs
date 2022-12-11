using VegeShama.Domain.Enums;
using VegeShama.Infrastructure.Utils;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDomainServices();

var databaseProvider = builder.Configuration.GetValue<DatabaseProviderEnum>("DatabaseProvider");
builder.Services.AddDatabaseProvider(databaseProvider);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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