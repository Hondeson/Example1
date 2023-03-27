using Ex1.Model;
using Ex1.Model.Model;

namespace Ex1.API.Services.Users
{
    public class UsersDbContextService : IUsersService
    {
        private readonly Ex1Context db;
        public UsersDbContextService(Ex1Context db)
        {
            this.db = db;
        }

        public List<User> Get(int offset, int limit)
        {
            return 
                db.Users.OrderByDescending(x => x.Id)
                .Skip(offset)
                .Take(limit)
                .ToList();
        }

        public User Get(long id)
        {
            return db.Users.FirstOrDefault(x => x.Id == id);
        }

        public User GetByEmailAdress(string email)
        {
            return db.Users.FirstOrDefault(x => x.Email == email);
        }

        public void Create(User user, out long id)
        {
            db.Users.Add(user);
            db.SaveChanges();

            id = user.Id;
        }

        public void Update(User user)
        {
            db.SaveChanges();
        }

        public void Delete(User user)
        {
            db.Users.Remove(user);
            db.SaveChanges();
        }
    }
}
