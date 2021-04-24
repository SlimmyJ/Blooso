using System.Collections.ObjectModel;

namespace Blooso.Tests.Data
{
    #region

    using System.Collections.Generic;
    using Blooso.Data;
    using Models;
    using Repositories;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    #endregion

    [TestClass()]
    public class DummyDataTests
    {
        private DummyData testData = new();

        private User testUser = new();

        private ObservableCollection<User> testUsers = new();

        [TestMethod()]
        public void GenerateDummyDataTestDoesNotReturnEmpty()
        {
            testUsers = testData.GenerateUserList();
            Assert.IsNotNull(testUser);
        }

        public void ActivityTester()
        {
            testUsers = testData.GenerateUserList();
        }

        [TestMethod()]
        public void Repository_Activities_ReturnAllActivitiesFromDataBase()
        {
            // Arrange
            var repo = UserRepository.GetRepository();

            // Act
            List<Activity> activities = repo.GetAllActivities();

            // Assert
            Assert.IsNotNull(activities);
        }
    }
}