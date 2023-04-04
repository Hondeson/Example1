using Ex1.Model.Request;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace Ex1.API.Services.Users
{
    public interface IUserValidationService
    {
        public bool IsEmailValid(UserModel user);
        public bool IsFullNameValid(UserModel user);
    }
}
