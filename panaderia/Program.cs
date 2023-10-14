using Panaderia.Context;
using Panaderia.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IPanaderiaDbContext, PanaderiaDbContext>();
builder.Services.AddScoped<IPanService, PanService>();

// Add services to the container.
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

app.UseAuthorization();

app.MapControllers();

app.Run();
