using EaseTrail.WebApp;
using EaseTrail.WebApp.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var key = Encoding.ASCII.GetBytes("QNIUADFIVNSDIUFBVOIABDVOIUB94FH28F9VNE8942098VRN98H&*H*&WH87N3NG98R");

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Restrict", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireRole("1", "2");
    });

    options.AddPolicy("Admins", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireRole("1", "2", "3", "6");
    });
});

builder.Services.AddAuthorization();

builder.Services.AddDbContext<Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MeuBancoDeDados"));
});

builder.Services.AddProjectServices();

var app = builder.Build();

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
