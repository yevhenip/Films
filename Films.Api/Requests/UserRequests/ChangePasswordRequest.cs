namespace Films.Api.Requests.UserRequests
{
    public class ChangePasswordRequest
    {
        public string Id { get; set; }
        
        public string Email { get; set; }
        public string NewPassword { get; set; }
        public string OldPassword { get; set; }
    }
}