using System.Collections.Generic;

using Blooso.Models;

namespace Blooso.Data
{
    public interface IDummyData
    {
        List<Tag> GenerateTags();

        List<User> GenerateUserList();

        List<Activity> GenerateActivities();

        List<Tag> GenerateRandomUserTags(int amount);

        List<Activity> GetRandomActivities(int amount);
    }
}