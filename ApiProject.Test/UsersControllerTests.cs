using AnewAPIproject.Controllers;
using AnewAPIproject.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace ApiProject.Test
{
    
    public class UserControllerTests
    {
        [Fact]
        public void GetAllProducts_ShouldReturnAllProducts()
        {
            var testProducts = GetTestUsers();
            var controller = new SimpleUserController(testProducts);

            var result = controller.GetAllUsers() as List<User>;
            Assert.Equal(testProducts.Count, result.Count);
        }
        private List<User> GetTestUsers()
        {
            var testUsers = new List<User>();
            testUsers.Add(new User { Id = 1, Name = "Demo1" });
            testUsers.Add(new User { Id = 2, Name = "Demo2" });
            testUsers.Add(new User { Id = 3, Name = "Demo3" });
            testUsers.Add(new User { Id = 4, Name = "Demo4" });

            return testUsers;
        }
    }
}
