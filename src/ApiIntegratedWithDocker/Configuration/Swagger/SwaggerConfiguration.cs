using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace ApiIntegratedWithDocker.Configuration.Swagger;

public static class SwaggerConfiguration
{
    public static void AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen();

        services.AddApiVersioning(opt =>
        {
            opt.DefaultApiVersion = new ApiVersion(1, 0);
            opt.AssumeDefaultVersionWhenUnspecified = true;
            opt.ReportApiVersions = true;
            opt.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader());
        });

        services.AddVersionedApiExplorer(setup =>
        {
            setup.GroupNameFormat = "VVVV";
            setup.SubstitutionFormat = "VVVV";
            setup.AssumeDefaultVersionWhenUnspecified = true;
            setup.DefaultApiVersion = new ApiVersion(1, 0);
            setup.SubstituteApiVersionInUrl = true;
        });

        services.ConfigureOptions<ConfigureSwaggerOptions>();
        services.AddEndpointsApiExplorer();
    }

    public static void UseSwaggerConfiguration(this IApplicationBuilder app, string pathBase)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            foreach (var groupName in app.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>()
                                        .ApiVersionDescriptions
                                        .Reverse()
                                        .Select(description => description.GroupName))
            {
                var swaggerJson = $"{pathBase}/swagger/{groupName}/swagger.json";
                options.SwaggerEndpoint(swaggerJson, groupName);
            }
        });
    }
}
