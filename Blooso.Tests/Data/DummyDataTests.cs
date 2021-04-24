using Microsoft.VisualStudio.TestTools.UnitTesting;
using Blooso.Data;
using Blooso.Repositories;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blooso.Models;

namespace Blooso.Data.Tests
{
    [TestClass()]
    public class DummyDataTests
    {
        private DummyData testData = new DummyData();
        private User testUser = new User();
        private List<User> testUsers = new List<User>();

        [TestMethod()]
        public void GenerateDummyDataTestDoesNotReturnEmpty()
        {
            testUsers = testData.GenerateDummyData();
            Assert.IsNotNull(testUser);
        }

        public void ActivityTester()
        {
            testUsers = testData.GenerateDummyData();
        }

        [TestMethod()]
        public void Repository_Activities_ReturnAllActivitiesFromDataBase()
        {
            // Arrange            
            var repo = UserRepository.GetRepository();
            var activities = new List<Activity>();

            // Act
            activities = repo.GetAllActivities();

            // Assert
            
            Assert.IsNotNull(activities);
        }
    }
}