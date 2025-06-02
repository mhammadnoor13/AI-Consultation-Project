
using CaseService.API.CaseService.Application.Interfaces;
using CaseService.API.CaseService.Infrastructure.Messaging;
using CaseService.API.CaseService.Infrastructure.Repositories;
using MassTransit;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// 1) Add Swagger for API exploration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactDev",
        policy =>
        {
            policy
                .WithOrigins("http://localhost:3000")   // your React app URL
                .AllowAnyMethod()                       // GET, POST, PUT, DELETE…
                .AllowAnyHeader()                       // Content-Type, Authorization…
                .AllowCredentials();                    // if you ever send cookies/auth
        });
});



builder.Services.AddMassTransit(busConfigurator =>
{
    busConfigurator.SetKebabCaseEndpointNameFormatter();
    busConfigurator.UsingRabbitMq((context, configurator) =>
    {
        configurator.Host(new Uri(builder.Configuration["MessageBroker:Host"]!), h =>
        {
            h.Username(builder.Configuration["MessageBroker:Username"]);
            h.Password(builder.Configuration["MessageBroker:Password"]);
        });

        configurator.ConfigureEndpoints(context);

    });
});


// 2) Configure MongoDB
builder.Services.AddSingleton<IMongoClient>(sp =>
    new MongoClient(builder.Configuration.GetConnectionString("Mongo")));
builder.Services.AddScoped(sp =>
    sp.GetRequiredService<IMongoClient>()
      .GetDatabase(builder.Configuration["Mongo:DatabaseName"]));

// 3) Register your ports & services
builder.Services.AddScoped<ICaseRepository, MongoCaseRepository>();
builder.Services.AddScoped<ICaseEventPublisher, RabbitMqCaseEventPublisher>();  // stub for now
builder.Services.AddScoped<ICaseService, CaseService.API.CaseService.Application.Services.CaseService>();

// 4) Add controllers
builder.Services.AddControllers();

var app = builder.Build();

// 5) Enable middleware
app.UseHttpsRedirection();
app.UseCors("AllowReactDev");


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();

