
namespace Models.Request
{
    public  class UserRegisterRequest
    {
        public Guid UID { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
