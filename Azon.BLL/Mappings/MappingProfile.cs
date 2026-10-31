
using AutoMapper;
using Azon.Domain.Entities;
using Azon.Shared.DTOs.UserDTOs;
using Azon.Shared.DTOs.CityDTOs;
using Azon.Shared.DTOs.MosqueDTOs;
using Azon.Shared.DTOs.PrayerTimeDTOs;
using Azon.Shared.DTOs.UserPreferenceDTOs;
using Azon.Shared.DTOs.NotificationLogDTOs;

namespace Azon.BLL.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // User mappings
        CreateMap<UserForCreateDto, User>();
        CreateMap<UserForUpdateDto, User>()
            .ForMember(dest => dest.TelegramUserId, opt => opt.Ignore())
            .ForMember(dest => dest.Preference, opt => opt.Ignore())
            .ForMember(dest => dest.NotificationLogs, opt => opt.Ignore());

        CreateMap<User, UserForResultDto>();
        CreateMap<User, UserForShortResultDto>();

        // City mappings
        CreateMap<CityForCreateDto, City>();
        CreateMap<CityForUpdateDto, City>()
            .ForMember(dest => dest.Mosques, opt => opt.Ignore())
            .ForMember(dest => dest.PrayerTimes, opt => opt.Ignore())
            .ForMember(dest => dest.UserPreferences, opt => opt.Ignore());

        CreateMap<City, CityForResultDto>();
        CreateMap<City, CityForShortResultDto>();

        // Mosque mappings
        CreateMap<MosqueForCreateDto, Mosque>();
        CreateMap<MosqueForUpdateDto, Mosque>()
            .ForMember(dest => dest.City, opt => opt.Ignore());

        CreateMap<Mosque, MosqueForResultDto>()
            .ForMember(dest => dest.CityName,
                opt => opt.MapFrom(src => src.City != null ? src.City.Name : null));

        CreateMap<Mosque, MosqueForShortResultDto>();

        // PrayerTime mappings
        CreateMap<PrayerTimeForCreateDto, PrayerTime>();
        CreateMap<PrayerTimeForUpdateDto, PrayerTime>()
            .ForMember(dest => dest.City, opt => opt.Ignore());

        CreateMap<PrayerTime, PrayerTimeForResultDto>();

        CreateMap<PrayerTime, PrayerTimeForShortResultDto>();

        // UserPreference mappings
        CreateMap<UserPreferenceForCreateDto, UserPreference>()
            .ForMember(dest => dest.User, opt => opt.Ignore())
            .ForMember(dest => dest.City, opt => opt.Ignore());

        CreateMap<UserPreferenceForUpdateDto, UserPreference>()
            .ForMember(dest => dest.UserId, opt => opt.Ignore())
            .ForMember(dest => dest.User, opt => opt.Ignore())
            .ForMember(dest => dest.City, opt => opt.Ignore());

        CreateMap<UserPreference, UserPreferenceForResultDto>()
            .ForMember(dest => dest.CityName,
                opt => opt.MapFrom(src => src.City.Name));

        CreateMap<UserPreference, UserPreferenceForShortResultDto>();

        // NotificationLog mappings
        CreateMap<NotificationLogForCreateDto, NotificationLog>();

        CreateMap<NotificationLogForUpdateDto, NotificationLog>()
            .ForMember(dest => dest.UserId, opt => opt.Ignore())
            .ForMember(dest => dest.User, opt => opt.Ignore())
            .ForMember(dest => dest.PrayerDate, opt => opt.Ignore())
            .ForMember(dest => dest.PrayerType, opt => opt.Ignore());

        CreateMap<NotificationLog, NotificationLogForResultDto>()
            .ForMember(dest => dest.NotificationStatus,
                opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.UserName,
                opt => opt.MapFrom(src => src.User.FirstName));

        CreateMap<NotificationLog, NotificationLogForShortResultDto>()
            .ForMember(dest => dest.NotificationStatus,
                opt => opt.MapFrom(src => src.Status));
    }
}
