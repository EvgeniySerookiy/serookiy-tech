using System.Reflection;
using EducationContentService.Core.Features;
using EducationContentService.Core.Features.Lessons;
using EducationContentService.Web.EndpointsSettings;
using Serilog;
using Serilog.Exceptions;

namespace EducationContentService.Web.Configuration;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddConfiguration(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<CreateHandler>();

        return services
            .AddSerilogLogging(configuration)
            .AddOpenApi()
            .AddEndpointsSettings(typeof(IEndpoint).Assembly);
    }

    private static IServiceCollection AddSerilogLogging(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        return services.AddSerilog((sp, lc) => lc
            .ReadFrom.Configuration(configuration)
            .ReadFrom.Services(sp)
            .Enrich.FromLogContext()
            .Enrich.WithExceptionDetails()
            .Enrich.WithProperty("ServiceName", "LessonService"));
    }
}