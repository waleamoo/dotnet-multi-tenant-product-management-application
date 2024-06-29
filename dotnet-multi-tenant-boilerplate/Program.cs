using dotnet_multi_tenant_boilerplate.Middleware;
using dotnet_multi_tenant_boilerplate.Models;
using dotnet_multi_tenant_boilerplate.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => {
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer Scheme (\"Bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();

    options.OperationFilter<AddRequiredHeaderParameter>();
});

// add the database context 
builder.Services.AddDbContext<ApplicationDbContext>(option => option.UseSqlite("Data Source=mta_db.db"));
builder.Services.AddDbContext<TenantDbContext>(option => option.UseSqlite("Data Source=mta_db.db"));
// services 
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddScoped<ICurrentTenantService, CurrentTenantService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
// add the middleware created 
app.UseMiddleware<TenantResolver>();

app.MapControllers();

app.Run();
