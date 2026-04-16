using BusinessLayer.Implementations;
using BusinessLayer.Interfaces;
using DataLayer.Implementations;
using DataLayer.Interfaces;
using Mapper.Implemenations;
using Mapper.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Register Business Layer services
builder.Services.AddScoped<IUserService, UserService>();

// Register Data Layer services
builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<ITokenGenerate, Token>();
builder.Services.AddSingleton<IConnection, Connection>();

// Register Mapper services
builder.Services.AddScoped<IBusinessLayerMapper, BusinessLayerMapper>();
builder.Services.AddScoped<IDataLayerMapper, DataLayerMapper>();

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
