using GiftShopOnline.Data;
using GiftShopOnline.Helpers;
using GiftShopOnline.Interfaces;
using GiftShopOnline.Services;
using GiftShopOnline.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<UnitOfWork>(
    optionsBuilder =>
        optionsBuilder.UseNpgsql(builder.Configuration.GetConnectionString("GiftShopDb"))
        );

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

var config = builder.Configuration;

builder.Services.AddScoped<CurrentUser>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
    x =>
    {
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = config[Constants.Token.JwtIssuer],
            ValidateIssuer = true,
            ValidAudience = config[Constants.Token.JwtAudience],
            ValidateAudience = true,
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config[Constants.Token.JwtKey])),
            ValidateIssuerSigningKey = true,
        };
    });

builder.Services.AddAutoMapper(typeof(Program));

//JSON Serializer
builder.Services.AddControllers().AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore).AddNewtonsoftJson(
        options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());

var app = builder.Build();

//Enable CORS
app.UseCors(c => c.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());

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
