using Microsoft.OpenApi.Models;
using Desafio.Infrastructure.Data;
using Desafio.Infrastructure.Data.Contract.Interfaces;
using Desafio.Infrastructure.Data.Contract.Repositories;
using Hangfire;
using Desafio.Infrastructure.Data.Jobs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<DatabaseContext>();
builder.Services.AddMvc();

// Injeção de dependencias.
builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();
builder.Services.AddTransient<IProductLogRepository, ProductLogRepository>();
builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<IProductCategoryRepository, ProductCategoryRepository>();

// Configuração do Swagger.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Projeto desafio",
        Description = "API de gerencimento de produtos"
        // TermsOfService = new Uri("https://www.google.com/"),
        // Contact = new OpenApiContact
        // {
        //     Name = "Gabriel",
        //     Email = "gabrileao38@gmail.com",
        //     Url = new Uri("https://www.google.com/")
        // },
        // License = new OpenApiLicense
        // {
        //     Name = "sla",
        //     Url = new Uri("https://www.google.com/")
        // }
    });
});

// Inicialização do Hangfire.
try
{
    builder.Services.AddHangfire(configuration => configuration.UseRecommendedSerializerSettings().UseSqlServerStorage("Server=localhost\\SQLEXPRESS;Database=ADIMAX_API;Trusted_Connection=True;TrustServerCertificate=True;Encrypt=False"));
    builder.Services.AddHangfireServer();
    
    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Desafio Adimax API");
        });

        app.UseRouting();
        app.UseHangfireDashboard();

        RecurringJob.AddOrUpdate<HangfireService>(x => x.PopulateProductLogTableJob(), Cron.Daily);

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
catch (Exception buildErrorException)
{
    throw buildErrorException;
}

