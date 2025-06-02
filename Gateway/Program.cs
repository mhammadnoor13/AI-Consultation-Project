using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);



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


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = builder.Configuration["Jwt:Authority"];
        options.Audience = builder.Configuration["Jwt:Audience"];
        options.RequireHttpsMetadata = true;
    });


// 2. Authorization policies (open for extension)
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("CasePolicy", policy =>
        policy.RequireClaim("scope", "case.read", "case.write"));
    options.AddPolicy("ConsultantPolicy", policy =>
        policy.RequireClaim("scope", "consultant.read", "consultant.write"));
});


builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.UseCors("AllowReactDev");

app.UseAuthentication();
app.UseAuthorization();
app.MapReverseProxy();

app.MapControllers();

app.Run();
