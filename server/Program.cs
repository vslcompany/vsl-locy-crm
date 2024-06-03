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
});

// Add db context
builder.Services.AddDbContext<VslDbContext>(option => option.UseSqlServer(sqlConnectionString));

// Add in-memory caching
builder.Services.AddMemoryCache();

// Add controllers
builder.Services.AddControllers();

// Add services helper
builder.Services.AddScoped<ICacheService, CacheService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IAuthService<TblSysUser>, AuthService>();
builder.Services.AddScoped<IProfileService, ProfileService>();

// Add authorization role
builder.Services.AddAuthorization(option =>
{
    option.AddPolicy("ManageEmployee", policy => policy.RequireAssertion(context =>
        context.User.HasClaim(claim => claim.Type == "Permission" && (claim.Value.Contains("1048576") || claim.Value.Contains("5000")))
    ));
    option.AddPolicy("ManageCategory", policy => policy.RequireAssertion(context =>
        context.User.HasClaim(claim => claim.Type == "Permission" && (claim.Value.Contains("1048576") || claim.Value.Contains("6000")))
    ));
    option.AddPolicy("ManageCustomer", policy => policy.RequireAssertion(context =>
        context.User.HasClaim(claim => claim.Type == "Permission" && (claim.Value.Contains("1048576") || claim.Value.Contains("7000") || claim.Value.Contains("7020")))
    ));
    option.AddPolicy("DeliveryCustomer", policy => policy.RequireAssertion(context =>
        context.User.HasClaim(claim => claim.Type == "Permission" && (claim.Value.Contains("1048576") || claim.Value.Contains("7000") || claim.Value.Contains("7080")))
    ));
    option.AddPolicy("ImportCustomer", policy => policy.RequireAssertion(context =>
        context.User.HasClaim(claim => claim.Type == "Permission" && (claim.Value.Contains("1048576") || claim.Value.Contains("7000") || claim.Value.Contains("7040")))
    ));
    option.AddPolicy("ExportCustomer", policy => policy.RequireAssertion(context =>
        context.User.HasClaim(claim => claim.Type == "Permission" && (claim.Value.Contains("1048576") || claim.Value.Contains("7000") || claim.Value.Contains("7060")))
    ));
});

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

app.UseMiddleware<ErrorHandleMiddleware>();

app.MapControllers();

app.Run();
