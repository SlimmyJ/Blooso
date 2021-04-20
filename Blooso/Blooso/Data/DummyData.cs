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
        public List<User> MakeTestData()
        {
            var dummyList = new List<User>();
            var userFaker = new Faker<User>()
                .RuleFor(x => x.Name, x => x.Person.FullName)
                .RuleFor(x => x.Sex, z => z.PickRandom('M', 'F', 'X'))
                .RuleFor(x => x.IsVaccinated, x => x.Random.Bool())
                .RuleFor(x => x.IsInfected, x => x.Random.Bool())
                .RuleFor(x => x.UserLocation, new UserLocation())
                .RuleFor(x => x.DateOfBirth, x => x.Person.DateOfBirth)
                .RuleFor(x => x.ActivityList, GetActivities())
                .RuleFor(x => x.UserTags, GetUserTags())
                .RuleFor(x => x.UserPicture, x => x.Image.LoremPixelUrl("People"));

            //var user = JsonConvert.SerializeObject(userFaker.Generate());
            //Debug.WriteLine($" TEST:: {user}");

            for (int i = 0; i < 100; i++)
            {
                dummyList.Add(userFaker.Generate());
            }

            return dummyList;
        }

        public List<Activities> GetActivities()
        {
            var newList = Enum.GetValues(typeof(Activities)).Cast<Activities>().ToList();
            var rand = new Random();
            var sevenRandom = newList.OrderBy(x => rand.Next()).Take(7).ToList();

            return sevenRandom;
        }

        public List<Tags> GetUserTags()
        {
            var newList = Enum.GetValues(typeof(Tags)).Cast<Tags>().ToList();
            var rand = new Random();
            var twentyRandom = newList.OrderBy(x => rand.Next()).Take(20).ToList();
            Console.WriteLine(string.Join(", ", twentyRandom));
            return twentyRandom;
        }
    }
}