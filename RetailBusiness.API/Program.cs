using Microsoft.EntityFrameworkCore;
using RetailBusiness.Infrastructure.Data.Repositories;
using RetailBusiness.Infrastructure.Data;
using RetailBusiness.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ISqlExecutor, SqlExecutor>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddControllers();

// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();