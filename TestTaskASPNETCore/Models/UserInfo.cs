using System.ComponentModel.DataAnnotations;

namespace TestTaskASPNETCore.Models
{
    public class UserInfo
    {
        [Key]
        [Required]
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }

        [Required]
        public SchoolingActivityModel SchoolingActivityModel { get; set; }

        public UserInfo()
        {
            Id = Guid.NewGuid().ToString();
        }
        public void Edit(UserInfo userInfo)
        {
            FirstName = userInfo.FirstName;
            LastName = userInfo.LastName;
            Age = userInfo.Age;
            Email = userInfo.Email;
        }

        public UserInfo CopyTo()
        {
            return new UserInfo()
            {
                FirstName = FirstName,
                LastName = LastName,
                Age = Age,
                Email = Email,
                SchoolingActivityModel = SchoolingActivityModel
            };
        }
    }
}
