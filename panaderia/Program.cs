using Panaderia.Context;
using Panaderia.Services;

var builder = WebApplication.CreateBuilder(args);

var corsRules = "corsRules";

//ENABLING CORS
builder.Services.AddCors(option =>
   option.AddPolicy(name: corsRules,
       builder =>
       {
           builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
       }
   )
);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddSingleton<IPanaderiaDbContext, PanaderiaDbContext>();
builder.Services.AddScoped<IPanService, PanService>();
builder.Services.AddScoped<IServiceIngrediente, IngredienteService>();
builder.Services.AddScoped<ISucursalService, SucursalService>();
builder.Services.AddScoped<IRecetaService, RecetaService>();
builder.Services.AddScoped<IPedidoService, PedidoService>();
builder.Services.AddScoped<IStockService, StockService>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

bool isDevelopment = app.Environment.IsDevelopment();

// Configure the HTTP request pipeline.
if (isDevelopment)
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(corsRules);
app.UseAuthorization();


app.MapControllers();

app.Run();
