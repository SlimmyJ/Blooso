using System.Collections.Generic;
using System.Collections.ObjectModel;

using Blooso.Models;

namespace Blooso.Data
{
    public interface IDummyData
    {
        ObservableCollection<User> GenerateUserList();

        IEnumerable<Activity> GenerateActivities(int i);

        ObservableCollection<Activity> GenerateActivities();

        IEnumerable<Tag> GenerateTags(int i);

        ObservableCollection<Tag> GenerateTags();

        ObservableCollection<Activity> GetRandomActivities(int amount);

        ObservableCollection<Tag> GenerateRandomUserTags(int amount);
    }
}