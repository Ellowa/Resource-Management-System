using DataAccess;
using DataAccess.Entities;
using DataAccess.Interfaces;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddEntityFrameworkMySQL().AddDbContext<ApplicationDbContext>(options => {
    options.UseMySQL(builder.Configuration.GetConnectionString("MySQLConnection"));
});

builder.Services.AddScoped<IGenericRepository<AdditionalRole>, Repository<AdditionalRole>>();
builder.Services.AddScoped<IGenericRepository<Request>, Repository<Request>>();
builder.Services.AddScoped<IGenericRepository<Resource>, Repository<Resource>>();
builder.Services.AddScoped<IGenericRepository<ResourceType>, Repository<ResourceType>>();
builder.Services.AddScoped<IGenericRepository<Schedule>, Repository<Schedule>>();
builder.Services.AddScoped<IGenericRepository<User>, Repository<User>>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
