using Microsoft.OpenApi.Models;

namespace WebApi.Extensions;

public static class OpenApiExt
{
    public static void AddSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(setup =>
        {
            setup.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Modular Monolith API",
                Version = "v1",
            });

            // TechDebt: Enable swagger documentation enrichment from XML Docs
            // Set the comments path for the Swagger JSON and UI.
            //var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            //var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            //setup.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);

            // Needed to support nested types in the schema
            setup.CustomSchemaIds(x => x.FullName?.Replace("+", ".", StringComparison.Ordinal));

            // setup.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            // {
            //     Type = SecuritySchemeType.Http,
            //     Name = JwtBearerDefaults.AuthenticationScheme,
            //     Scheme = JwtBearerDefaults.AuthenticationScheme,
            //     Reference = new()
            //     {
            //         Type = ReferenceType.SecurityScheme,
            //         Id = JwtBearerDefaults.AuthenticationScheme
            //     }
            // });
        });
    }
}
