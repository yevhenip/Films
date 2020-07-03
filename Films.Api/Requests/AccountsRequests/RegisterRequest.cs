namespace Films.Api.Requests.AccountsRequests
{
    public class RegisterRequest
    {
        public string UserName { get; set; }
        
        public string Email { set; get; }
        
        public string Password { get; set; }
        
        public string PasswordConfirm { get; set; }
    }
}