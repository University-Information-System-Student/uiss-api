namespace UISS.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

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
            userService.GetUserByUserNameAsync("sd");

            // TODO: Implement LoginAsync Action

            throw new System.NotImplementedException();
        }

        [HttpPost("Register")]
        public async Task<ActionResult<RegisterResponseModel>> RegisterAsync(RegisterRequestModel request)
        {
            // TODO: Implement RegisterAsync Action

            throw new System.NotImplementedException();
        }

        [HttpPost("ForgotPassword")]
        public async Task<ActionResult<ForgotPasswordResponseModel>> ForgotPasswordAsync(ForgotPasswordRequestModel request)
        {
            // TODO: Implement ForgotPasswordAsync Action

            throw new System.NotImplementedException();
        }
    }
}
