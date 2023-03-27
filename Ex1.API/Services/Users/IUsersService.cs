using Ex1.Model.Model;

namespace Ex1.API.Services.Users
{
    public interface IUsersService
    {
        void Create(User user, out long id);
        void Delete(User user);
        List<User> Get(int offset, int limit);
        User Get(long id);
        User GetByEmailAdress(string email);
        void Update(User user);
    }
}