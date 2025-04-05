using ContractManager.Infrastructure;
using ContractManager.Application;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Adiciona camada Application e Infrastructure
builder.Services.AddApplication(); // criaremos já já
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();