using System.Collections.Generic;

using Blooso.Models;

using Microsoft.VisualStudio.TestTools.UnitTesting;

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
    }
}