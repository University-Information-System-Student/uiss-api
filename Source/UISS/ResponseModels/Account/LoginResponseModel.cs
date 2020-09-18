namespace UISS.ResponseModels.Account
{
    public class LoginResponseModel
    {
        public int Status { get; set; }

        public string Message { get; set; }

        public string Jwt { get; set; }
    }
}
