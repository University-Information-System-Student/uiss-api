namespace UISS.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    using RequestModels.Account;
    using ResponseModels.Account;
    using Services.Contracts;

    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IUserService userService;

        public AccountController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost("Login")]
        public async Task<ActionResult<LoginResponseModel>> LoginAsync(LoginRequestModel request)
        {
            // TODO: Implement LoginAsync Action

            throw new System.NotImplementedException();
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

            var hashedPassword = "passss124333343434";

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
