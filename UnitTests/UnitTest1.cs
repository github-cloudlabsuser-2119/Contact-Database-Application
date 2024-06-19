using Xunit;
using System.Web.Mvc;
using CRUD_application_2.Controllers;
using CRUD_application_2.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace UnitTests.Controllers
{
    public class UserControllerTests
    {
        [Fact]
        public void Index_ReturnsAViewResult_WithAListOfUsers()
        {
            // Arrange
            var controller = new UserController();

            // Act
            var result = controller.Index() as ViewResult;

            // Assert
            var model = Assert.IsAssignableFrom<IEnumerable<User>>(result.Model);
            Assert.NotNull(model);
        }

        [Fact]
        public void Details_WithValidId_ReturnsUser()
        {
            // Arrange
            var controller = new UserController();
            int testUserId = 1; // Assuming this ID exists

            // Act
            var result = controller.Details(testUserId) as ViewResult;

            // Assert
            var model = Assert.IsType<User>(result.Model);
            Assert.Equal(testUserId, model.Id);
        }

        [Fact]
        public void Create_PostAction_AddsUser()
        {
            // Arrange
            var controller = new UserController();
            var newUser = new User { Id = 3, Name = "Test User", Email = "test@example.com" };

            // Act
            var result = controller.Create(newUser) as RedirectToRouteResult;

            // Assert
            Assert.Equal("Index", result.RouteValues["action"]);
            // Additional assertions to verify the user was added could be included here
        }

        [Fact]
        public void Edit_PostAction_UpdatesUser()
        {
            // Arrange
            var controller = new UserController();
            var userToUpdate = new User { Id = 1, Name = "Updated Name", Email = "updated@example.com" };

            // Act
            var result = controller.Edit(userToUpdate.Id, userToUpdate) as RedirectToRouteResult;

            // Assert
            Assert.Equal("Index", result.RouteValues["action"]);
            // Additional assertions to verify the user was updated could be included here
        }

        [Fact]
        public void Delete_WithValidId_DeletesUser()
        {
            // Arrange
            var controller = new UserController();
            int userIdToDelete = 2; // Assuming this ID exists and can be deleted

            // Act
            var result = controller.Delete(userIdToDelete) as RedirectToRouteResult;

            // Assert
            Assert.Equal("Index", result.RouteValues["action"]);
            // Additional assertions to verify the user was deleted could be included here
        }
    }
}
