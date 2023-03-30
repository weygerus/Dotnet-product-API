using Microsoft.OpenApi.Models;
using Adimax.Infrastructure.Data;
using Adimax.Infrastructure.Data.Contract.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<DatabaseContext>();
builder.Services.AddMvc();

// Injeção de dependencias.
builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();

//configuração do Swagger, referencia:https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Desafio",
        Description = "slasla...",
        TermsOfService = new Uri("https://www.google.com/"),
        Contact = new OpenApiContact
        {
            Name = "sla",
            Email = "sla",
            Url = new Uri("https://www.google.com/")
        },
        License = new OpenApiLicense
        {
            Name = "sla",
            Url = new Uri("https://www.google.com/")
        }

    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Desafio Adimax API");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();