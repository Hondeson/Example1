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
            this.Gender = user.Gender;
            this.EducationMaxReached = user.EducationMaxReached;
            this.Interests = user.Interests;
        }

        public long Id { get; set; }

        public string FullName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public DateOnly BornDate { get; set; }

        public string Gender { get; set; } = null!;

        public string EducationMaxReached { get; set; } = null!;

        public string? Interests { get; set; }
    }

    public static class UserModelValidator
    {
        public static bool IsEmailValid(this UserModel user)
        {
            return MailAddress.TryCreate(user.Email, out _);
        }

        static readonly Regex _nameRegex = new Regex(@"^\w+(\s+\w+)+$", RegexOptions.Compiled);
        public static bool IsFullNameValid(this UserModel user)
        {
            return _nameRegex.IsMatch(user.FullName);
        }
    }
}
