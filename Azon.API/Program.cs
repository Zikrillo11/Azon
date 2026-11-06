
using System.Security.Claims;
using System.Text;
using Azon.BLL;
using Azon.DAL.DAL;
using Azon.Shared.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// ========================================  
// 1. JWT SETTINGS
// ========================================

var jwtSettings = builder.Configuration
    .GetSection("JwtSettings")
    .Get<JwtSettings>()
    ?? throw new InvalidOperationException(
        "JWT settings not found.");

if (string.IsNullOrWhiteSpace(jwtSettings.SecretKey) ||
    Encoding.UTF8.GetByteCount(jwtSettings.SecretKey) < 32)
{
    throw new InvalidOperationException(
        "Invalid JWT SecretKey. Minimum 32 bytes required.");
}

if (string.IsNullOrWhiteSpace(jwtSettings.Issuer) ||
    string.IsNullOrWhiteSpace(jwtSettings.Audience))
{
    throw new InvalidOperationException(
        "Invalid JWT Issuer or Audience.");
}

if (jwtSettings.AccessTokenExpirationMinutes is < 1 or > 60)
{
    throw new InvalidOperationException(
        "JWT expiration must be between 1 and 60 minutes.");
}

builder.Services.Configure<JwtSettings>(
    builder.Configuration.GetSection("JwtSettings"));

// ========================================
// 2. DATABASE
// ========================================

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString(
            "DefaultConnection")));

// ========================================
// 3. AUTHENTICATION
// ========================================

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters =
            new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,

                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),

                ValidateIssuer = true,
                ValidIssuer = jwtSettings.Issuer,

                ValidateAudience = true,
                ValidAudience = jwtSettings.Audience,

                ValidateLifetime = true,
                ClockSkew = TimeSpan.FromSeconds(30),

                NameClaimType = ClaimTypes.Name,
                RoleClaimType = ClaimTypes.Role
            };
    });

// ========================================
// 4. AUTHORIZATION
// ========================================

builder.Services.AddAuthorization();

// ========================================
// 5. CONTROLLERS
// ========================================

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// ========================================
// 6. SWAGGER
// ========================================

builder.Services.AddSwaggerGen();

// ========================================
// 7. BUSINESS LOGIC
// ========================================

builder.Services.Configure<TelegramSettings>(
    builder.Configuration.GetSection("TelegramSettings"));

builder.Services.AddBusinessLogic();

// ========================================
// 8. BUILD
// ========================================

var app = builder.Build();

// ========================================
// 9. MIDDLEWARE
// ========================================

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

// ========================================
// 10. CONTROLLERS
// ========================================

app.MapControllers();

// ========================================
// 11. RUN
// ========================================

app.Run();
