namespace Blooso.Data.Tests
{
    #region

    using System.Collections.Generic;

    using Blooso.Models;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    #endregion

    [TestClass()]
    public class DummyDataTests
    {
        private DummyData testData = new();

        private User testUser = new();

        private List<User> testUsers = new();

        [TestMethod()]
        public void GenerateDummyDataTestDoesNotReturnEmpty()
        {
            this.testUsers = this.testData.GenerateDummyData();
            Assert.IsNotNull(this.testUser);
        }
    }
}