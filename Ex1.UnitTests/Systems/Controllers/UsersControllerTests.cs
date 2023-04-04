using Ex1.API.Controllers;
using Ex1.API.Services.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex1.Model.Model;
using Ex1.UnitTests.Fixtures;
using Ex1.Model.Request;

namespace Ex1.UnitTests.Systems.Controllers
{
    public class UsersControllerTests
    {
        [Fact]
        public void Get_OnSucces_ReturnsOK()
        {
            //Arrange
            int offset = 0;
            int limit = 2;

            var logMock = new Mock<ILogger<UsersController>>();
            var usrValSvcMock = new Mock<IUserValidationService>();

            var usrSvcMock = new Mock<IUsersService>();
            usrSvcMock
                .Setup(svc => svc.Get(offset, limit))
                .Returns(UserFixture.GetUsers(limit));

            var cont = new UsersController(logMock.Object, usrSvcMock.Object, usrValSvcMock.Object);

            //Act
            var res = cont.Get(offset, limit);

            //Assert
            res.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void Get_OnSucces_ReturnsArrayOfUserModel()
        {
            //Arrange
            int offset = 0;
            int limit = 2;

            var logMock = new Mock<ILogger<UsersController>>();
            var usrValSvcMock = new Mock<IUserValidationService>();

            var usrSvcMock = new Mock<IUsersService>();
            usrSvcMock
                .Setup(svc => svc.Get(offset, limit))
                .Returns(UserFixture.GetUsers(limit));

            var cont = new UsersController(logMock.Object, usrSvcMock.Object, usrValSvcMock.Object);

            //Act
            var res = (OkObjectResult)cont.Get(offset, limit);

            //Assert
            res.Value.Should().BeOfType<UserModel[]>();
        }

        [Fact]
        public void Get_OnEmpty_ReturnsNoContent()
        {
            //Arrange
            int offset = 0;
            int limit = 0;

            var logMock = new Mock<ILogger<UsersController>>();
            var usrValSvcMock = new Mock<IUserValidationService>();

            var usrSvcMock = new Mock<IUsersService>();
            usrSvcMock
                .Setup(svc => svc.Get(offset, limit))
                .Returns(UserFixture.GetUsers(limit));

            var cont = new UsersController(logMock.Object, usrSvcMock.Object, usrValSvcMock.Object);

            //Act
            var res = cont.Get(offset, limit);

            //Assert
            res.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public void Get_ById_OnSucces_ReturnsOK()
        {
            //Arrange
            int id = 1;

            var logMock = new Mock<ILogger<UsersController>>();
            var usrValSvcMock = new Mock<IUserValidationService>();

            var usrSvcMock = new Mock<IUsersService>();
            usrSvcMock
                .Setup(svc => svc.Get(id))
                .Returns(UserFixture.GetUsers(1)[0]);

            var cont = new UsersController(logMock.Object, usrSvcMock.Object, usrValSvcMock.Object);

            //Act
            var res = cont.Get(id);

            //Assert
            res.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void Get_ById_OnNotExisting_ReturnsNotFound()
        {
            //Arrange
            int id = 1;

            var logMock = new Mock<ILogger<UsersController>>();
            var usrValSvcMock = new Mock<IUserValidationService>();

            var usrSvcMock = new Mock<IUsersService>();
            usrSvcMock
                .Setup(svc => svc.Get(id))
                .Returns(() => null);

            var cont = new UsersController(logMock.Object, usrSvcMock.Object, usrValSvcMock.Object);

            //Act
            var res = cont.Get(id);

            //Assert
            res.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void Post_OnSucces_ReturnsCreatedAtRoute()
        {
            //Arrange
            var postUser = UserModelFixture.Create();

            var logMock = new Mock<ILogger<UsersController>>();

            var usrValSvcMock = new Mock<IUserValidationService>();
            usrValSvcMock
                .Setup(svc => svc.IsEmailValid(postUser))
                .Returns(true);

            usrValSvcMock
                .Setup(svc => svc.IsFullNameValid(postUser))
                .Returns(true);

            var usrSvcMock = new Mock<IUsersService>();
            usrSvcMock
                .Setup(svc => svc.GetByEmailAdress(postUser.Email))
                .Returns(() => null);

            var cont = new UsersController(logMock.Object, usrSvcMock.Object, usrValSvcMock.Object);

            //Act
            var res = cont.Post(postUser);

            //Assert
            res.Should().BeOfType<CreatedAtRouteResult>();
        }

        [Fact]
        public void Post_OnInvalidEmail_ReturnsBadRequest()
        {
            //Arrange
            var postUser = UserModelFixture.Create();

            var logMock = new Mock<ILogger<UsersController>>();

            var usrValSvcMock = new Mock<IUserValidationService>();
            usrValSvcMock
                .Setup(svc => svc.IsEmailValid(postUser))
                .Returns(false);

            usrValSvcMock
                .Setup(svc => svc.IsFullNameValid(postUser))
                .Returns(true);

            var usrSvcMock = new Mock<IUsersService>();
            usrSvcMock
                .Setup(svc => svc.GetByEmailAdress(postUser.Email))
                .Returns(() => null);

            var cont = new UsersController(logMock.Object, usrSvcMock.Object, usrValSvcMock.Object);

            //Act
            var res = cont.Post(postUser);

            //Assert
            res.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public void Post_OnInvalidFullName_ReturnsBadRequest()
        {
            //Arrange
            var postUser = UserModelFixture.Create();

            var logMock = new Mock<ILogger<UsersController>>();

            var usrValSvcMock = new Mock<IUserValidationService>();
            usrValSvcMock
                .Setup(svc => svc.IsEmailValid(postUser))
                .Returns(true);

            usrValSvcMock
                .Setup(svc => svc.IsFullNameValid(postUser))
                .Returns(false);

            var usrSvcMock = new Mock<IUsersService>();
            usrSvcMock
                .Setup(svc => svc.GetByEmailAdress(postUser.Email))
                .Returns(() => null);

            var cont = new UsersController(logMock.Object, usrSvcMock.Object, usrValSvcMock.Object);

            //Act
            var res = cont.Post(postUser);

            //Assert
            res.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public void Post_OnAlreadyExisting_ReturnsConflict()
        {
            //Arrange
            var postUser = UserModelFixture.Create();

            var logMock = new Mock<ILogger<UsersController>>();

            var usrValSvcMock = new Mock<IUserValidationService>();
            usrValSvcMock
                .Setup(svc => svc.IsEmailValid(postUser))
                .Returns(true);

            usrValSvcMock
                .Setup(svc => svc.IsFullNameValid(postUser))
                .Returns(true);

            var usrSvcMock = new Mock<IUsersService>();
            usrSvcMock
                .Setup(svc => svc.GetByEmailAdress(postUser.Email))
                .Returns(UserFixture.GetUsers(1)[0]);

            var cont = new UsersController(logMock.Object, usrSvcMock.Object, usrValSvcMock.Object);

            //Act
            var res = cont.Post(postUser);

            //Assert
            res.Should().BeOfType<ConflictObjectResult>();
        }

        [Fact]
        public void Put_OnSucces_ReturnsOk()
        {
            //Arrange
            var putUser = UserModelFixture.Create();

            var logMock = new Mock<ILogger<UsersController>>();

            var usrValSvcMock = new Mock<IUserValidationService>();
            usrValSvcMock
                .Setup(svc => svc.IsEmailValid(putUser))
                .Returns(true);

            usrValSvcMock
                .Setup(svc => svc.IsFullNameValid(putUser))
                .Returns(true);

            var usrSvcMock = new Mock<IUsersService>();
            usrSvcMock
                .Setup(svc => svc.Get(putUser.Id))
                .Returns(UserFixture.FromUserModel(putUser));


            var cont = new UsersController(logMock.Object, usrSvcMock.Object, usrValSvcMock.Object);

            //Act
            var res = cont.Put(putUser.Id, putUser);

            //Assert
            res.Should().BeOfType<OkResult>();
        }

        [Fact]
        public void Put_OnInvalidEmail_ReturnsBadRequest()
        {
            //Arrange
            var putUser = UserModelFixture.Create();

            var logMock = new Mock<ILogger<UsersController>>();

            var usrValSvcMock = new Mock<IUserValidationService>();
            usrValSvcMock
                .Setup(svc => svc.IsEmailValid(putUser))
                .Returns(false);

            usrValSvcMock
                .Setup(svc => svc.IsFullNameValid(putUser))
                .Returns(true);

            var usrSvcMock = new Mock<IUsersService>();
            usrSvcMock
                .Setup(svc => svc.Get(putUser.Id))
                .Returns(UserFixture.FromUserModel(putUser));


            var cont = new UsersController(logMock.Object, usrSvcMock.Object, usrValSvcMock.Object);

            //Act
            var res = cont.Put(putUser.Id, putUser);

            //Assert
            res.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public void Put_OnInvalidFullName_ReturnsBadRequest()
        {
            //Arrange
            var putUser = UserModelFixture.Create();

            var logMock = new Mock<ILogger<UsersController>>();

            var usrValSvcMock = new Mock<IUserValidationService>();
            usrValSvcMock
                .Setup(svc => svc.IsEmailValid(putUser))
                .Returns(true);

            usrValSvcMock
                .Setup(svc => svc.IsFullNameValid(putUser))
                .Returns(false);

            var usrSvcMock = new Mock<IUsersService>();
            usrSvcMock
                .Setup(svc => svc.Get(putUser.Id))
                .Returns(UserFixture.FromUserModel(putUser));


            var cont = new UsersController(logMock.Object, usrSvcMock.Object, usrValSvcMock.Object);

            //Act
            var res = cont.Put(putUser.Id, putUser);

            //Assert
            res.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public void Put_OnNotExisting_ReturnsNotFound()
        {
            //Arrange
            var putUser = UserModelFixture.Create();

            var logMock = new Mock<ILogger<UsersController>>();

            var usrValSvcMock = new Mock<IUserValidationService>();
            usrValSvcMock
                .Setup(svc => svc.IsEmailValid(putUser))
                .Returns(true);

            usrValSvcMock
                .Setup(svc => svc.IsFullNameValid(putUser))
                .Returns(true);

            var usrSvcMock = new Mock<IUsersService>();
            usrSvcMock
                .Setup(svc => svc.Get(putUser.Id))
                .Returns(() => null);


            var cont = new UsersController(logMock.Object, usrSvcMock.Object, usrValSvcMock.Object);

            //Act
            var res = cont.Put(putUser.Id, putUser);

            //Assert
            res.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void Delete_OnSucces_ReturnsOk()
        {
            //Arrange
            int id = 1;

            var logMock = new Mock<ILogger<UsersController>>();
            var usrValSvcMock = new Mock<IUserValidationService>();

            var usrSvcMock = new Mock<IUsersService>();
            usrSvcMock
                .Setup(svc => svc.Get(id))
                .Returns(() => UserFixture.GetUsers(1)[0]);

            var cont = new UsersController(logMock.Object, usrSvcMock.Object, usrValSvcMock.Object);

            //Act
            var res = cont.Delete(id);

            //Assert
            res.Should().BeOfType<OkResult>();
        }

        [Fact]
        public void Delete_OnNotExisting_ReturnsNotFound()
        {
            //Arrange
            int id = 1;

            var logMock = new Mock<ILogger<UsersController>>();
            var usrValSvcMock = new Mock<IUserValidationService>();

            var usrSvcMock = new Mock<IUsersService>();
            usrSvcMock
                .Setup(svc => svc.Get(id))
                .Returns(() => null);

            var cont = new UsersController(logMock.Object, usrSvcMock.Object, usrValSvcMock.Object);

            //Act
            var res = cont.Delete(id);

            //Assert
            res.Should().BeOfType<NotFoundResult>();
        }
    }
}
