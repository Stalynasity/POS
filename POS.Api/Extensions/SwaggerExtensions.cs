using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
//no es obligatoria referencia a mi api documentacion, asi se documenta nuestro swagger (terminos, cantoctos, licencias)
namespace POS.Api.Extensions
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection addSwagger(this IServiceCollection services)
        {
            var openApi = new OpenApiInfo
            {
                Title = "POS API",
                Version = "v1",
                Description = "punto de venta API 2022",
                TermsOfService = new Uri("https://opensource.org/licences/NIT"),
                Contact = new OpenApiContact
                {
                    Name = "SIR TECH",
                    Email = "sirtech@gmail.com",
                    Url = new Uri("https://opensource.org/licences/NIT")
                },
                License = new OpenApiLicense
                {
                    Name = "Use under License",
                    Url = new Uri("https://opensource.org/licences/NIT")
                }
            };

            services.AddSwaggerGen(x =>
            {
                openApi.Version = "v1";
                x.SwaggerDoc("v1", openApi);

                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "Jwt Authentication",
                    Description = " Jwt Bearer token",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "Jwt",
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };

                x.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
                x.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { securityScheme, new string[]{} }
                });
            });

            return services;
        }
    }
}

