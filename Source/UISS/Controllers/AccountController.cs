namespace UISS.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    using Security;
    using RequestModels.Account;
    using ResponseModels.Account;
    using Services.Contracts;
    using GlobalConstants;

    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IJwtService jwtService;

        public AccountController(IUserService userService, IJwtService jwtService)
        {
            this.userService = userService;
            this.jwtService = jwtService;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<LoginResponseModel>> LoginAsync(LoginRequestModel request)
        {
            var response = new LoginResponseModel();

            var user = await this.userService
                .GetUserByUsernameAsync(request.Username);

            if (Object.Equals(user, null))
            {
                response.Status = -1;
                response.Message = "Invalid Username Or Password.";
                return Ok(response);
            }

            var isValidPassword = PasswordHasher
                .VerifyHashedPassword(user.HashedPassword, request.Password);

            if (!isValidPassword)
            {
                response.Status = -1;
                response.Message = "Invalid Username Or Password.";
                return Ok(response);
            }

            var id = user.Id.ToString();
            var username = request.Username;
            var userRole = ((UserRole)user.UserRole).ToString();

            var jwt = this.jwtService
                .GenerateJwt(id, username, userRole);

            response.Status = 1;
            response.Message = "Success.";
            response.Jwt = jwt;

            return Ok(response);
        }

        [HttpPost("Register")]
        public async Task<ActionResult<RegisterResponseModel>> RegisterAsync(RegisterRequestModel request)
        {
            var response = new RegisterResponseModel();

            var user = await this.userService
                .GetUserByRegistrationCodeAsync(request.RegistrationCode);

            if (Object.Equals(user, null))
            {
                response.Status = -1;
                response.Message = "Invalid Registration Code.";
                return Ok(response);
            }

            if (user.IsActivatedCode)
            {
                response.Status = -2;
                response.Message = "The Registration Code Is Taken.";
                return Ok(response);
            }

            var hashedPassword = PasswordHasher
                .HashPassword(request.Password);

            await this.userService
                .UpdateUserToRegistered(user.Id, request.Username, hashedPassword, request.Email);

            response.Status = 1;
            response.Message = "Success.";
            return Ok(response);
        }

        [HttpPost("ForgotPassword")]
        public async Task<ActionResult<ForgotPasswordResponseModel>> ForgotPasswordAsync(ForgotPasswordRequestModel request)
        {
            // TODO: Implement ForgotPasswordAsync Action

            throw new System.NotImplementedException();
        }
    }
}
