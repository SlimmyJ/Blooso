using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Blooso.Models;
using Bogus;
using Newtonsoft.Json;

namespace Blooso.Data
{
    public class DummyData
    {
        public List<User> GenerateDummyData()
        {
            var dummyList = new List<User>();
            var userFaker = new Faker<User>()
                .RuleFor(x => x.Name, x => x.Person.FullName)
                .RuleFor(x => x.Sex, z => z.PickRandom('M', 'F', 'X'))
                .RuleFor(x => x.IsVaccinated, x => x.Random.Bool())
                .RuleFor(x => x.IsInfected, x => x.Random.Bool())
                .RuleFor(x => x.UserLocation, new UserLocation())
                .RuleFor(x => x.DateOfBirth, x => x.Person.DateOfBirth)
                .RuleFor(x => x.ActivityList, GetFifteenRandomActivities())
                .RuleFor(x => x.UserTags, GetTwentyRandomUserTags())
                .RuleFor(x => x.UserPicture, x => x.Image.LoremPixelUrl("People"));

            //var user = JsonConvert.SerializeObject(userFaker.Generate());
            //Debug.WriteLine($" TEST:: {user}");

            for (var i = 0; i < 10; i++)
            {
                var temp = userFaker.Generate();
                temp.Id = i + 1;
                dummyList.Add(temp);
            }

            return dummyList;
        }

        public List<Activities> GetFifteenRandomActivities()
        {
            var rand = new Random();
            var newList = Enum.GetValues(typeof(Activities)).Cast<Activities>().ToList();
            var fifteenRandomActivitieses = newList.OrderBy(x => rand.Next()).Take(15).ToList();

            return fifteenRandomActivitieses;
        }

        public List<Tags> GetTwentyRandomUserTags()
        {
            var rand = new Random();
            var newList = Enum.GetValues(typeof(Tags)).Cast<Tags>().ToList();
            var twentyRandomUserTags = newList.OrderBy(x => rand.Next()).Take(20).ToList();

            return twentyRandomUserTags;
        }
    }
}