using Authentication.Application.Features.Register.Queries.GetUser;
using Authentication.Domain.Entities;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Authentication.Application.Mappers
{
    public class AuthProfile:Profile
    {
        public AuthProfile()
        {
            CreateMap<RegisterUser, GetUserResponse>().ReverseMap();
        }
    }
}
