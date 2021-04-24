namespace Blooso.Data
{
    #region

    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    using Blooso.Models;

    using Bogus;

    #endregion

    public class DummyData
    {
        public List<User> GenerateDummyData()
        {
            var dummyList = new List<User>();
            Faker<User> userFaker = new Faker<User>().RuleFor(x => x.Name, x => x.Person.FullName)
                .RuleFor(x => x.Sex, x => x.PickRandom("Male", "Female", "Gender Fluid"))
                .RuleFor(x => x.IsVaccinated, x => x.Random.Bool()).RuleFor(x => x.IsInfected, x => x.Random.Bool())
                .RuleFor(x => x.UserLocation, x => x.Person.Address.City)
                .RuleFor(x => x.DateOfBirth, x => x.Person.DateOfBirth).RuleFor(x => x.Activities, new List<Activity>())
                .RuleFor(x => x.Tags, new List<Tag>()).RuleFor(x => x.FriendList, new ObservableCollection<User>())
                .RuleFor(x => x.UserFeedMessages, new ObservableCollection<Message>());

            // var user = JsonConvert.SerializeObject(userFaker.Generate());
            // Debug.WriteLine($" TEST:: {user}");
            for (var i = 0; i < 20; i++)
            {
                User tempuser = userFaker.Generate();
                tempuser.Id = i + 1;
                tempuser.UserPicture = $"a{i}.jpg";

                // tempuser.Activities = this.GetRandomActivities(10);
                // tempuser.Tags = this.GetRandomUserTags(12);
                tempuser.ShortBio = string.Empty;
                tempuser.Password = "log";

                dummyList.Add(tempuser);
            }

            return dummyList;
        }

        // public List<Activity> GetRandomActivities(int amount)
        // {
        // var rand = new Random();
        // //var newList = new List<Activity>();
        // var newList = GetAllActivities();             

        // return newList.OrderBy(x => rand.Next()).Take(amount).ToList();
        // }

        // public List<Tag> GetRandomUserTags(int amount)
        // {
        // var rand = new Random();
        // //var newList = new List<Tag>();
        // var newList = GetAllTags();

        // return newList.OrderBy(x => rand.Next()).Take(amount).ToList();
        // }
    }
}