using AutoMapper;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Web.BLL.Auth;
using Web.BLL.Mapping;
using Web.Core.Entites;
using Web.DTO.Auth.Setting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<AuthSetting>(builder.Configuration.GetSection("AuthSetting"));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(swagger =>
{

    // To Enable authorization using Swagger (JWT)  
    swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
    });
    swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}

                    }
                });
});

var tokenValidationParams = new TokenValidationParameters
{
    ValidateIssuerSigningKey = true,
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration[$"{nameof(AuthSetting)}:{nameof(AuthSetting.Jwt)}:{nameof(AuthSetting.Jwt.Secret)}"])),
    ValidateAudience = false,
    ValidateLifetime = true,
    ValidIssuer = builder.Configuration[$"{nameof(AuthSetting)}:{nameof(AuthSetting.Jwt)}:{nameof(AuthSetting.Jwt.Issuer)}"],
    RequireExpirationTime = true,
    ClockSkew = TimeSpan.Zero // remove delay of token when expire
};

builder.Services.AddSingleton(tokenValidationParams);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
        .AddJwtBearer(jwt =>
        {
            jwt.SaveToken = true;
            jwt.TokenValidationParameters = tokenValidationParams;
        });


builder.Services.AddDbContext<SystemContext>(e => e.UseSqlServer(builder.Configuration.GetConnectionString("SystemDbContext")));
builder.Services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped<IAuthBLL, AuthBLL>();
builder.Services.AddAutoMapper(ctf => { ctf.AddProfile<MapperProfile>(); });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
