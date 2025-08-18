using AutoMapper;
using Nexus.Common.Models;
using Nexus.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nexus.Common.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>()
                 .ReverseMap();

         

            CreateMap<User, RegisterDto>().ReverseMap();
            CreateMap<UserOTP, UserOTPDto>().ReverseMap();
        }

    }
}