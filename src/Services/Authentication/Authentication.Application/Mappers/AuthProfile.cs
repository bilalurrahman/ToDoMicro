using Authentication.Application.Features.Register.Queries.GetUser;
using Authentication.Application.Features.Token;
using Authentication.Application.Features.Token.Command.UpdateRefreshToken;
using Authentication.Application.Features.Token.Query.GetRefreshToken;
using Authentication.Common.Helpers.JWTHelper;
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
            CreateMap<UserToken, GetRefreshTokenResponse>().ReverseMap();
            CreateMap<UpdateRefreshTokenRequest, UserToken>().ReverseMap();
            CreateMap<JWTModel, VerifyRefreshTokenResponse>().ReverseMap();
        }
    }
}
