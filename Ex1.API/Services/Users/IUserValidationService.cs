using Ex1.Model.Request;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace Ex1.API.Services.Users
{
    public interface IUserValidationService
    {
        public bool IsEmailValid(UserModel user)
        {
            return MailAddress.TryCreate(user.Email, out _);
        }

        static readonly Regex _nameRegex = new Regex(@"^\w+(\s+\w+)+$", RegexOptions.Compiled);
        public bool IsFullNameValid(UserModel user)
        {
            return _nameRegex.IsMatch(user.FullName);
        }
    }
}
