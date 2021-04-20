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
    public class TestData
    {
        private readonly Faker _faker;

        public TestData()
        {
            _faker = new Faker("en");
        }

        public void MakeTestData()
        {
            var userFaker = new Faker<User>()
                .RuleFor(x => x.Name, x => x.Person.FullName)
                .RuleFor(x => x.Sex, z => z.PickRandom('M', 'F', 'X'))
                .RuleFor(x => x.IsVaccinated, x => x.Random.Bool())
                .RuleFor(x => x.IsInfected, x => x.Random.Bool())
                .RuleFor(x => x.UserLocation, new UserLocation())
                .RuleFor(x => x.DateOfBirth, x => x.Person.DateOfBirth)
                .RuleFor(x => x.UserTags, GetUserTags());

            var user = JsonConvert.SerializeObject(userFaker.Generate());
            Debug.WriteLine($" TEST:: {user}");
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