using chronovault_api.Infra.Data;
using chronovault_api.Repositories.Interfaces;
using chronovault_api.Repositories;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using System.Reflection;
using chronovault_api.Services.Interfaces;
using chronovault_api.Services;
using Microsoft.OpenApi.Models;
using chronovault_api.Configuration;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ChronoVault API", Version = "v1" });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

builder.Services.AddDbContext<ChronovaultDbContext>(options =>
    options.UseSqlite("Data Source=chronovault.db"));

// Registrar todos os validators
var assembly = Assembly.GetExecutingAssembly();
var validatorTypes = assembly.GetTypes()
    .Where(t => t.IsClass && !t.IsAbstract && t.BaseType != null)
    .Where(t => t.BaseType.IsGenericType && t.BaseType.GetGenericTypeDefinition() == typeof(AbstractValidator<>))
    .ToList();

foreach (var validatorType in validatorTypes)
{
    var dtoType = validatorType.BaseType.GetGenericArguments()[0];
    var interfaceType = typeof(IValidator<>).MakeGenericType(dtoType);
    builder.Services.AddScoped(interfaceType, validatorType);
}

// Registrar todos os services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderItemService, OrderItemService>();
builder.Services.AddScoped<IImageService, ImageService>();

// Registrar todos os repositórios
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserCredentialRepository, UserCredentialRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderItemRepository, OrderItemRepository>();
builder.Services.AddScoped<IImageRepository, ImageRepository>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:4200") // URL do seu front-end
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("AllowFrontend");

app.MapControllers();

app.Run();
