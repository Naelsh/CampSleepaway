using CampSleepaway.Application.Cabins;
using CampSleepaway.Domain.Data;
using CampSleepaway.Persistence;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampSleepaway.Test.Units.Application
{
    class CabinTest
    {

        [Test]
        public void AddCabinByName_EmptyString_NoChange()
        {
            int expected = 0;

            using var context = TestAddons.GetTestContext("CanInsertCabin");
            var cabinCreate = new CabinManager(context);

            string name = null;
            int result = cabinCreate.AddCabinByName(name);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void CanInsertCabinIntoDatabase()
        {
            var expected = 1;

            using var context = TestAddons.GetTestContext("CanInsertCabin");
            var cabinCreate = new CabinManager(context);

            string name = "Cabin 1";
            int result = cabinCreate.AddCabinByName(name);

            Assert.AreEqual(expected, result);
        }

    }
}
