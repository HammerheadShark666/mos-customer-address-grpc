using Microservice.Customer.Address.Grpc.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Microservice.Customer.Address.Api.Address.Api.Extensions;

public static class JwtExtensions
{  
    public static void AddJwtAuthentication(this IServiceCollection services)
    {
        services.AddAuthentication(optiones =>
        {
            optiones.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            optiones.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            optiones.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(o =>
        {
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidAudience = EnvironmentVariablesHelper.JwtAudience,
                ValidIssuer = EnvironmentVariablesHelper.JwtIssuer,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(EnvironmentVariablesHelper.JwtSymmetricSecurityKey)),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
            };

            o.MapInboundClaims = false;
        });
    }
}