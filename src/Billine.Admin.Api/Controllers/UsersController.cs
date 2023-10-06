using AutoMapper;
using Billine.Admin.Api.Authorization;
using Billine.Admin.Contracts;
using Billine.Admin.Contracts.Users;
using Billine.Admin.Domain.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Billine.Admin.Api.Controllers
{
    [Route("api/v1/users")]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly IOptions<JwtOptions> _jwtOptions;
        private readonly IMapper _mapper;

        public UsersController(IUserService userService, IOptions<JwtOptions> jwtOptions, IMapper mapper)
        {
            _userService = userService;
            _jwtOptions = jwtOptions;
            _mapper = mapper;
        }

        /// <summary>
        /// Creates a new user
        /// </summary>
        /// <remarks>
        /// Creates a login from user's credendentials
        /// </remarks>
        [HttpPost, Route("signup"), AllowAnonymous]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult> SignUp([FromBody] PostSignUpRequest signUpRequest)
        {
            User user = _mapper.Map<User>(signUpRequest);
            user = await _userService.SignUp(user);

            ApiToken apiToken = new(_jwtOptions)
            {
                User = user
            };

            UserResponse response = _mapper.Map<UserResponse>(user);

            return Created($"api/v1/users/{user.Id}", new
            {
                Token = apiToken.ToString(),
                User = response
            });
        }

        /// <summary>
        /// List internal registered users
        /// </summary>
        /// <param name="request"></param>
        /// <remarks>Filtered by email</remarks>
        [HttpPut]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult> Update([FromBody] PutUserRequest request)
        {
            User user = _mapper.Map<User>(request);
            user.Id = User.GetId();

            user = await _userService.Update(user);

            UserResponse response = _mapper.Map<UserResponse>(user);

            return Ok(response);
        }

        /// <summary>
        /// List the logged user profile
        /// </summary>
        /// <remarks>Filtered by email</remarks>
        [HttpGet, Route("profile")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status422UnprocessableEntity)]
        public async Task<ActionResult> GetProfile()
        {
            User user = await _userService.GetProfile(User.GetEmail());

            UserResponse response = _mapper.Map<UserResponse>(user);

            return Ok(response);
        }

        /// <summary>
        /// Allows a User to login into the system
        /// </summary>
        /// <param name="signInRequest"></param>
        /// <remarks>
        /// Verifyes the user's credentials
        /// </remarks>
        [HttpPost, Route("signin"), AllowAnonymous]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status422UnprocessableEntity)]
        [ProducesResponseType(typeof(ResponseError), StatusCodes.Status403Forbidden)]
        public async Task<ActionResult> SignIn([FromBody] PostSignInRequest signInRequest)
        {
            User user = _mapper.Map<User>(signInRequest);
            user = await _userService.SignIn(user);

            if (user is null)
                return Unauthorized();

            ApiToken apiToken = new(_jwtOptions)
            {
                User = user
            };

            UserResponse response = _mapper.Map<UserResponse>(user);

            return Ok(new
            {
                Token = apiToken.ToString(),
                User = response
            });
        }
    }
}