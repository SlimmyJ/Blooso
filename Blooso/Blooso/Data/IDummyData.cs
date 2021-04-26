#region

using System.Collections.Generic;

using Blooso.Models;

#endregion

namespace Blooso.Data
{
    public interface IDummyData
    {
        List<User> GenerateUserList();

        List<Activity> GenerateActivities();

        List<Tag> GenerateRandomUserTags(int amount);

        List<Activity> GetRandomActivities(int amount);

        List<Tag> GenerateTags();
    }
}