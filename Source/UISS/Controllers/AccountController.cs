namespace UISS.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    using RequestModels.Account;
    using ResponseModels.Account;

    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        [HttpPost("Login")]
        public async Task<ActionResult<LoginResponseModel>> LoginAsync(LoginRequestModel request)
        {
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
