using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using vsl_crm_api.Models.Domains;
using vsl_crm_api.Data;
using vsl_crm_api.Interfaces;
using vsl_crm_api.Middlewares;
using vsl_crm_api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add configuration server
var origin = new[] { builder.Configuration["Origin"] ?? "", "http://localhost:5173" };
var sqlConnectionString = builder.Configuration.GetConnectionString("CrmApiConnectionString");
var jwtIssuer = builder.Configuration["Jwt:Issuer"] ?? "";
var jwtAudience = builder.Configuration["Jwt:Audience"] ?? "";
var jwtKey = builder.Configuration["Jwt:Key"] ?? "";
var MyAllowSpecificOrigins = "_vslApiOriginCor";

// Add services to the container.

// Add cors
builder.Services.AddCors(option =>
{
    option.AddPolicy(name: MyAllowSpecificOrigins, policy =>
    {
        policy.WithOrigins(origin).AllowAnyHeader().AllowAnyMethod().AllowCredentials();
    });
});

// Add authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = jwtIssuer,
                        ValidAudience = jwtAudience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
                    };
                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            // Get the request path
                            var path = context.HttpContext.Request.Path;

                            // Check if the request path starts with "/auth/" and skip authentication
                            if (path.StartsWithSegments("/auth/"))
                            {
                                context.Token = null; // Clear token to skip authentication
                            }

                            return Task.CompletedTask;
                        }
                    };
                });

// Add db context
builder.Services.AddDbContext<VslDbContext>(option => option.UseSqlServer(sqlConnectionString));

// Add caching system
builder.Services.AddStackExchangeRedisCache(options =>
{
    string connection = builder.Configuration.GetConnectionString("Redis") ?? "";
    options.Configuration = connection;
    options.InstanceName = "crm";
});

// Add controllers
builder.Services.AddControllers();

// Add services helper
builder.Services.AddScoped<ICacheService, CacheService>();
builder.Services.AddScoped<IAuthService<TblSysUser>, AuthService>();
builder.Services.AddScoped<IProfileService, ProfileService>();

// Add global error handling
builder.Services.AddTransient<ErrorHandleMiddleware>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(MyAllowSpecificOrigins);

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

// app.UseMiddleware<ErrorHandleMiddleware>();

app.MapControllers();

app.Run();
