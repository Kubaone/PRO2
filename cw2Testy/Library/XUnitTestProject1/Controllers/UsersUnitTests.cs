using Library.Controllers;
using Library.Entities;
using Library.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using System.Linq;
using Library.Models.DTO;
using Microsoft.AspNet.Identity;
using Library.Helpers;

namespace XUnitTestProject1.Controllers
{
    public class UsersUnitTests
    {
        [Fact]
        public async Task GetUsers_200Ok()
        {
            //AAA
            //Arrange
            var m = new Mock<IUserRepository>();
            ICollection<User> users = new List<User>
            {
                new User{IdUser=1, Name="kowalski", Email="kowalski@wp.pl"},
                new User{IdUser=2, Name="kowalski2", Email="kowalski2@wp.pl"},
                new User{IdUser=3, Name="kowalski3", Email="kowalski3@wp.pl"}
            };

            m.Setup(c => c.GetUsers()).Returns(Task.FromResult(users));

            var controller = new UsersController(m.Object);

            //Act
            var result = await controller.GetUsers();

            //Assert
            Assert.True(result is OkObjectResult);
            var r = result as OkObjectResult;
            Assert.True((r.Value as ICollection<User>).Count == 3);
            Assert.True((r.Value as ICollection<User>).ElementAt(0).Name == "kowalski");
        }

        [Fact]
        public async Task AddUser_201CreatedAtRoute()
        {
            //AAA
            //Arrange
            var m = new Mock<IUserRepository>();
            UserDto userDto = new UserDto { Login = "Kowal", Name = "kowalski", Email = "kowalski@wp.pl", Password = "Legia" };
            User user = new User
            {
                Name = userDto.Name,
                Email = userDto.Email,
                Login = userDto.Login,
                Surname = userDto.Surname,
                Password = new PasswordHasher().HashPassword(userDto.Password),
                IdUserRoleDict = (int)UserRoleHelper.UserRolesEnum.Reader
            };

            m.Setup(c => c.AddUser(userDto)).Returns(Task.FromResult(user));
            var controller = new UsersController(m.Object);

            //Act
            var result = await controller.AddUser(userDto);

            //Assert
            Assert.True(result is CreatedAtRouteResult);
            var r = result as CreatedAtRouteResult;
            Assert.True((r.Value as User).Email == "kowalski@wp.pl");
        }

        [Fact]
        public async Task GetUser_200Ok()
        {
            //AAA
            //Arrange
            var m = new Mock<IUserRepository>();

            var user = new User { IdUser = 1, Name = "kowalski", Email = "kowalski@wp.pl" };

            m.Setup(c => c.GetUser(user.IdUser)).Returns(Task.FromResult(user));
            var controller = new UsersController(m.Object);

            //Act
            var result = await controller.GetUser(user.IdUser);

            //Assert
            Assert.True(result is OkObjectResult);
            var r = result as OkObjectResult;
            Assert.True((r.Value as User).Name == "kowalski");
        }
    }
}
