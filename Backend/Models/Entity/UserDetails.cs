namespace Models.Entity
{
    public  class UserDetails
    {
        public Guid UID { get; set; }   
        public string? UserName {  get; set; }
        public byte[]? PasswordHash { get; set; }
        public byte[]? PasswordKey { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; } 
    }
}
