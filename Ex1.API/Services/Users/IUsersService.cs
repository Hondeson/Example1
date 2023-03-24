using Ex1.Model.Model;

namespace Ex1.API.Services.Users
{
    public interface IUsersService
    {
        void Create(User user, out long id);
        void Delete(User user);
        List<User> Get();
        User Get(int id);
        User GetByEmailAdress(string email);
        void Update(User user);
    }
}