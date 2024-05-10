namespace Auth.Models.Membership
{
    public class SignupDTO
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Phone { get; set; }
        public required string Password { get; set; }
        
    }
}
