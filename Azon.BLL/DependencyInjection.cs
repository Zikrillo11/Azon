using Azon.BLL.Interfaces;
using Azon.BLL.Mappings;
using Azon.BLL.Services;
using Azon.DAL.Repositories.Interfaces;
using Azon.DAL.Repositories.Implementations;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Azon.BLL;

public static class DependencyInjection
{
    public static IServiceCollection AddBusinessLogic(
        this IServiceCollection services)
    {
        // Repositories
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICityRepository, CityRepository>();
        services.AddScoped<IMosqueRepository, MosqueRepository>();
        services.AddScoped<IPrayerTimeRepository, PrayerTimeRepository>();
        services.AddScoped<IUserPreferenceRepository, UserPreferenceRepository>();
        services.AddScoped<INotificationLogRepository, NotificationLogRepository>();

        // Services
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ICityService, CityService>();
        services.AddScoped<IMosqueService, MosqueService>();
        services.AddScoped<IPrayerTimeService, PrayerTimeService>();
        services.AddScoped<IUserPreferenceService, UserPreferenceService>();
        services.AddScoped<INotificationLogService, NotificationLogService>();

        // AutoMapper
        services.AddAutoMapper(
            cfg => { },
            typeof(MappingProfile).Assembly);

        // FluentValidation
        services.AddValidatorsFromAssembly(
            typeof(DependencyInjection).Assembly);

        return services;
    }
}