using Ex1.Model.Model;
using Ex1.Model.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex1.UnitTests.Fixtures
{
    internal class UserModelFixture
    {
        public static UserModel Create()
        {
            return new UserModel
            {
                Id = 1,
                BornDate = new DateOnly(2000, 11, 10),
                EducationMaxReached = EducationEnum.Masters,
                Email = $"mockEmail@gmail.com",
                FullName = $"Name Surname",
                Gender = GenderEnum.Male,
                Interests = "nic"
            };
        }
    }
}
