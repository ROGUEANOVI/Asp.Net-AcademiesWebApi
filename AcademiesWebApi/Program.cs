using AcademiesWebApi.Abstractions;
using AcademiesWebApi.Application;
using AcademiesWebApi.Configuration;
using AcademiesWebApi.DataAccess;
using AcademiesWebApi.Repository;
using AcademiesWebApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Configure Connection Database 
builder.Services.AddDbContext<ApiDbContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConnectionStringAcademiesWebApi"));
    options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
    }
) ;

builder.Services.Configure<JwtConfig>(builder.Configuration.GetSection("JwtConfig"));

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }
)   .AddJwtBearer(jwt =>
        {
            var key = Encoding.ASCII.GetBytes(builder.Configuration["JwtConfig:Secret"]);
            
            jwt.SaveToken = true;
            jwt.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                RequireExpirationTime = false,
                ValidateLifetime = true
            };
        }
    );

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApiDbContext>();

builder.Services.AddScoped(typeof(IApplication<>), typeof(Application<>));
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped(typeof(IDbContext<>), typeof(DbContext<>));
builder.Services.AddScoped(typeof(ITokenHandlerService), typeof(TokenHandlerService));
builder.Services.AddAutoMapper(typeof(MappingConfig));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.UseAuthentication();

app.MapControllers();

app.Run();
