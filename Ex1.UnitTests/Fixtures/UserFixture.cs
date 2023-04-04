using Ex1.Model.Model;
using Ex1.Model.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex1.UnitTests.Fixtures
{
    internal class UserFixture
    {
        public static List<User> GetUsers(int count)
        {
            var list = new List<User>();

            for (int i = 0; i < count; i++)
            {
                var id = i + 1;
                list.Add(new User()
                {
                    Id = id,
                    BornDate = new DateOnly(2000, 11, 10),
                    EducationMaxReached = (int)EducationEnum.Masters,
                    Email = $"mockEmail{id}@gmail.com",
                    FullName = $"Name{id} Surname{id}",
                    Gender = (int)GenderEnum.Male,
                    Interests = "nic"
                });
            }

            return list;
        }

        public static User FromUserModel(UserModel putUser)
        {
            return new User()
            {
                Id = putUser.Id,
                BornDate = putUser.BornDate,
                EducationMaxReached = (int)putUser.EducationMaxReached,
                Email = putUser.Email,
                FullName = putUser.FullName,
                Gender = (int)putUser.Gender,
                Interests = putUser.Interests,
            };
        }
    }
}
