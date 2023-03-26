using Ex1.Model.Model;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace Ex1.Model.Request
{
    public class UserModel
    {
        public UserModel()
        {

        }

        public UserModel(User user)
        {
            this.Id = user.Id;
            this.FullName = user.FullName;
            this.Email = user.Email;
            this.BornDate = user.BornDate;
            this.Gender = (GenderEnum)user.Gender;
            this.EducationMaxReached = (EducationEnum)user.EducationMaxReached;
            this.Interests = user.Interests;
        }

        public long Id { get; set; }

        public string FullName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public DateOnly BornDate { get; set; }

        public GenderEnum Gender { get; set; }

        public EducationEnum EducationMaxReached { get; set; }

        public string? Interests { get; set; }
    }
}
