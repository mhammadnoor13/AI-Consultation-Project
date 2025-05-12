using ConsultantService.ConsultantService.Application.Interfaces;
using ConsultantService.ConsultantService.Domain.Interfaces;
using ConsultantService.ConsultantService.Domain.Services;
using ConsultantService.ConsultantService.Infrastructure.Repositories;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();





builder.Services.AddSingleton<IMongoClient>(sp =>
    new MongoClient(builder.Configuration.GetConnectionString("Mongo")));
builder.Services.AddScoped(sp =>
    sp.GetRequiredService<IMongoClient>()
      .GetDatabase(builder.Configuration["Mongo:DatabaseName"]));

builder.Services.AddScoped<IConsultantService, MongoConsultantRepository>();
builder.Services.AddScoped<IConsultantAssignmentPolicy, MinimumLoadAssignmentPolicy>();  // stub for now

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
