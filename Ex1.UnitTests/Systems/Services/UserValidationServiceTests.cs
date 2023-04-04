using Ex1.API.Services.Users;
using Ex1.UnitTests.Fixtures;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex1.UnitTests.Systems.Services
{
    public class UserValidationServiceTests
    {
        [Fact]
        public void IsEmailValid_OnValid_ReturnsTrue()
        {
            //Arrange
            var userModel = UserModelFixture.Create();
            userModel.Email = "validEmail@gmail.com";

            var usrValSvc = new UserValidationService();

            //Act
            var res = usrValSvc.IsEmailValid(userModel);

            //Assert
            res.Should().BeTrue();
        }

        [Fact]
        public void IsEmailValid_OnInvalid_ReturnsFalse()
        {
            //Arrange
            var userModel = UserModelFixture.Create();
            userModel.Email = "invalid";

            var usrValSvc = new UserValidationService();

            //Act
            var res = usrValSvc.IsEmailValid(userModel);

            //Assert
            res.Should().BeFalse();
        }

        [Fact]
        public void IsFullNameValid_OnValid_ReturnsTrue()
        {
            //Arrange
            var userModel = UserModelFixture.Create();
            userModel.Email = "Valid name";

            var usrValSvc = new UserValidationService();

            //Act
            var res = usrValSvc.IsFullNameValid(userModel);

            //Assert
            res.Should().BeTrue();
        }

        [Fact]
        public void IsFullNameValid2_OnValid_ReturnsTrue()
        {
            //Arrange
            var userModel = UserModelFixture.Create();
            userModel.Email = "Valid name namenamendmanwe asdfsdfsdfsdf";

            var usrValSvc = new UserValidationService();

            //Act
            var res = usrValSvc.IsFullNameValid(userModel);

            //Assert
            res.Should().BeTrue();
        }

        [Fact]
        public void IsFullNameValid_OnInvalid_ReturnsFalse()
        {
            //Arrange
            var userModel = UserModelFixture.Create();
            userModel.FullName = "InvalidFullname";

            var usrValSvc = new UserValidationService();

            //Act
            var res = usrValSvc.IsFullNameValid(userModel);

            //Assert
            res.Should().BeFalse();
        }
    }
}
