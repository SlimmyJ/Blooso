using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Blooso.Models;
using Bogus;
using Newtonsoft.Json;

namespace Blooso.Data
{
    public class TestData
    {
        public void MakeTestData()
        {
            var userFaker = new Faker<User>()
                .RuleFor(x => x.Name, x => x.Person.FullName)
                .RuleFor(x => x.Sex, z => z.PickRandom('M', 'F'))
                .RuleFor(x => x.IsVaccinated, x => x.Random.Bool())
                .RuleFor(x => x.IsInfected, x => x.Random.Bool())
                .RuleFor(x => x.UserLocationString, x => x.Person.Address.City)
                .RuleFor(x => x.DateOfBirth, x => x.Person.DateOfBirth);

            var user = JsonConvert.SerializeObject(userFaker.Generate());
            Debug.WriteLine($" TEST:: {user}");
        }
    }
}