using EducationContentService.Web.EndpointsSettings;
using EducationContentService.Web.Middlewares;
using Serilog;

namespace EducationContentService.Web.Configuration;

public static class AppExtensions
{
    public static IApplicationBuilder Configure(
        this WebApplication app)
    {
        app.UseRequestCorrelationId();

        app.UseSerilogRequestLogging();

        app.MapOpenApi();

        app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json", "EducationContent"));

        RouteGroupBuilder apiGroup = app.MapGroup("/api/lessons").WithOpenApi();

        app.MapEndpoints(apiGroup);

        return app;
    }
}