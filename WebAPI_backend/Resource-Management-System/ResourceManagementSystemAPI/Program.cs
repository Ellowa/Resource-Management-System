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
using BysinessServices.ModelsValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using static Org.BouncyCastle.Math.EC.ECCurve;
using System.Text;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "JSON Web Acces Token" +
                      "<br>!!!ATTENTION!!!" +
                      "<br>The Token should be passed in header the next way:\n" +
                      "<br>\"Authorization: bearer <i>token</i>\"" +
                      "<br><br>Put the word <b>bearer</b> and <b>at least one space</b> before token in Value input!<br>",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JwtSettings:AccessTokenKey").Value)),
            ValidateLifetime = true,
            LifetimeValidator = JwtVerificationMiddleware.JwtLifetimeValidator,
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddFluentValidation(conf =>
{
    conf.RegisterValidatorsFromAssembly(typeof(ResourceValidator).Assembly);
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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }
