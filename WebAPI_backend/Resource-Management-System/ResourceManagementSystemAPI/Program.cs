using DataAccess;
using DataAccess.Entities;
using DataAccess.Interfaces;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore.Extensions;
using BysinessServices;
using BysinessServices.Interfaces;
using BysinessServices.Services;
using ResourceManagementSystemAPI.Middleware;
using FluentValidation.AspNetCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddFluentValidation(conf =>
{
    conf.RegisterValidatorsFromAssembly(typeof(Program).Assembly);
    conf.AutomaticValidationEnabled = false;
});

builder.Services.AddDbContext<ApplicationDbContext>(options => {
    options.UseMySQL(builder.Configuration.GetConnectionString("MySQLConnection"));
});


builder.Services.AddScoped<IGenericRepository<AdditionalRole>, Repository<AdditionalRole>>();
builder.Services.AddScoped<IGenericRepository<Request>, Repository<Request>>();
builder.Services.AddScoped<IGenericRepository<Resource>, Repository<Resource>>();
builder.Services.AddScoped<IGenericRepository<ResourceType>, Repository<ResourceType>>();
builder.Services.AddScoped<IGenericRepository<Schedule>, Repository<Schedule>>();
builder.Services.AddScoped<IGenericRepository<User>, Repository<User>>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IResourceService, ResourceService>();
builder.Services.AddScoped<IRequestService, RequestService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddAutoMapper(typeof(AutomapperProfile));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
