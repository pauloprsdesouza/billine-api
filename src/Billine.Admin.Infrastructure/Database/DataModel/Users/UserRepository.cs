using AutoMapper;
using Billine.Admin.Domain.Users;
using EfficientDynamoDb;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace Billine.Admin.Infrastructure.Database.DataModel.Users
{
    public class UserRepository : IUserRepository
    {
        public readonly IDynamoDbContext _dbContext;
        public readonly IMapper _mapper;

        public UserRepository(IDynamoDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<User> CreateAsync(User user)
        {
            user.Id = Guid.NewGuid();
            user.CreatedAt = DateTimeOffset.UtcNow;

            var model = _mapper.Map<UserModel>(user);

            UserKey userKey = new(user.Email, user.Id);
            userKey.AssignTo(model);

            await _dbContext.PutItemAsync(model);

            return user;
        }

        public async Task<User> GetProfile(string email)
        {
            UserKey userKey = new(email, default);
            var users = await _dbContext.Query<UserModel>()
                    .FromIndex("GSIIndex")
                    .WithKeyExpression(cond => cond.On(item => item.GSIPK).EqualTo(userKey.GSIPK))
                    .ToListAsync();

            var model = users.SingleOrDefault();

            return _mapper.Map<User>(model);
        }

        public async Task<User> UpdateAsync(User user)
        {
            user.UpdatedAt = DateTimeOffset.UtcNow;

            var model = _mapper.Map<UserModel>(user);

            UserKey userKey = new(default, user.Id);
            userKey.AssignTo(model);

            await _dbContext.UpdateItem<UserModel>()
                            .WithPrimaryKey(userKey.PK, userKey.SK)
                            .On(x => x.Name).Assign(model.Name)
                            .On(x => x.Password).Assign(model.Password)
                            .ExecuteAsync();

            return user;
        }
    }
}