using Microsoft.EntityFrameworkCore;
using System.Reflection;
using YetAnotherContactApi.DTOs.Data;
using YetAnotherContactApp.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
// Replace this line:
// builder.Services.AddAutoMapper();
// With the following, specifying the assembly containing your mapping profiles:
// Or
builder.Services.AddAutoMapper(cfg => { },
    Assembly.GetExecutingAssembly());
builder.Services.AddScoped<IContactRepository, ContactRepository>();

builder.Services.AddHttpClient();
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
