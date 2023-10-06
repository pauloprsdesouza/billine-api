using AutoMapper;
using Billine.Admin.Contracts.Users;
using Billine.Admin.Domain.Users;
using Billine.Admin.Infrastructure.Database.DataModel.Users;


namespace Billine.Admin.Infrastructure.Mappers
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserModel, User>().ReverseMap();
            CreateMap<User, UserResponse>();
            _ = CreateMap<PostSignInRequest, User>();
            _ = CreateMap<PostSignUpRequest, User>();
            _ = CreateMap<PutUserRequest, User>();
        }
    }
}
