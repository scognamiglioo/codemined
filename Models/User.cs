namespace learncode.Models
{
    public class User
    {
        public int Id { get; set; } = 0;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty; // "Admin" ou "User"
       

        public User Clone()
        {
            return new User
            {
                Id = this.Id,
                Email = this.Email,
                Password = this.Password,
                Role = this.Role
            };
        }
    }


}
