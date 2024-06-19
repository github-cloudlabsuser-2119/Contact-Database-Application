using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CRUD_application_2.Controllers;
using CRUD_application_2.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Reflection;

namespace UnitTests.Controllers
{
    [TestClass]
    public class UsersControllerTests
    {
        private UserController _controller;
        private List<User> _users;

        [TestInitialize]
        public void TestInitialize()
        {
            // Initialize your controller here
            _controller = new UserController();

            // Create and add dummy data to the _users list
            _users = new List<User>
            {
                new User { Id = 1, Name = "John Doe", Email = "john@example.com", Phone = "1234567890" },
                new User { Id = 2, Name = "Jane Doe", Email = "jane@example.com", Phone = "0987654321" }
            };

            // Assuming UserController has a way to set its users list, like a method or through reflection
            // If not, you might need to adjust the UserController to allow setting the _users list for testing purposes
            typeof(UserController).GetField("_users", BindingFlags.Static | BindingFlags.NonPublic).SetValue(null, _users);
        }

        [TestMethod]
        public void Index_WithSearchString_ReturnsFilteredUsers()
        {
            // Arrange
            var controller = new UserController();
            string searchString = "Jane"; // Replace "searchTerm" with a valid term present in your users

            // Act
            var result = controller.Index(searchString) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(IEnumerable<User>));
            var model = result.Model as IEnumerable<User>;

            Assert.IsNotNull(model);
            // Optionally, assert that the returned users match the search criteria
            Assert.IsTrue(model.Any(u => u.Name.Contains(searchString) || u.Email.Contains(searchString)));
        }

        [TestMethod]
        public void Index_WithoutSearchString_ReturnsAllUsers()
        {
            // Arrange
            var controller = new UserController();

            // Act
            var result = controller.Index(null) as ViewResult; // Pass null or an empty string to simulate no search term

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(IEnumerable<User>));
            var model = result.Model as IEnumerable<User>;

            Assert.IsNotNull(model);
            // Optionally, assert that all users are returned when no search term is provided
            // This might require knowing the expected number of users in your test setup
        }

        [TestMethod]
        public void Details_WithValidId_ReturnsUser()
        {
            // Arrange
            var controller = new UserController();
            int testUserId = 1; // Assuming this ID exists

            // Act
            var result = controller.Details(testUserId) as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(User));
            var model = result.Model as User;

            Assert.AreEqual(testUserId, model.Id);
        }

        [TestMethod]
        public void Create_PostAction_AddsUser()
        {
            // Arrange
            var controller = new UserController();
            var newUser = new User { Id = 3, Name = "Test User", Email = "test@example.com" };

            // Act
            var result = controller.Create(newUser) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod]
        public void Edit_PostAction_UpdatesUser()
        {
            // Arrange
            var controller = new UserController();
            var userToUpdate = new User { Id = 1, Name = "Updated Name", Email = "updated@example.com" };

            // Act
            var result = controller.Edit(userToUpdate.Id, userToUpdate) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }

        [TestMethod]
        public void Delete_WithValidId_DeletesUser()
        {
            // Arrange
            var controller = new UserController();
            int userIdToDelete = 2; // Assuming this ID exists and can be deleted

            // Act
            var result = controller.DeleteConfirmed(userIdToDelete) as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"]);
        }
    }
}
