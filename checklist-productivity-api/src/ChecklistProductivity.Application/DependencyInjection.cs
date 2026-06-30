using ChecklistProductivity.Application.Interfaces;
using ChecklistProductivity.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ChecklistProductivity.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IChecklistService, ChecklistService>();
        return services;
    }
}
