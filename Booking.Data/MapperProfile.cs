using AutoMapper;
using Booking.Core.Entities;
using Booking.Core.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Data
{
    public class MapperProfile : Profile
    {
        public MapperProfile() 
        {
            CreateMap<GymClass, GymClassViewModel>();
            //CreateMap<GymClass, GymClassViewModel>()
            //    .ForMember(dest => dest.Attending, from => from.MapFrom<AttendingResolver>());
            //    ;
        }
    }
    public class AttendingResolver : IValueResolver<GymClass, GymClassViewModel, bool>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public AttendingResolver(IHttpContextAccessor httpContextAccessor) { 
        }

        public bool Resolve(GymClass source, GymClassViewModel destination, bool destMember, ResolutionContext context)
        {
            return source.AttendingMembers is null ? false :
                 source.AttendingMembers.Any(a => a.ApplicationUserId == _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

        }
    }
}
