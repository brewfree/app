using AutoMapper;
using BrewFree.Data.Models;
using BrewFree.ReadModels;
using Microsoft.AspNetCore.Builder;

namespace BrewFree
{
    public static class AutoMapperConfig
    {
        public static void UseAppAutoMapper(this IApplicationBuilder app)
        {
            Mapper.Initialize(x =>
            {
                x.CreateMap<Brewer, BrewerReadModel>();
                x.CreateMap<ApplicationUser, UserReadModel>();
            });
        }
    }
}
