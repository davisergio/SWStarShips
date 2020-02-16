using NUnit.Framework;
using System;

namespace Domain.Tests
{
    public class StarShip
    {
        [SetUp]
        public void Setup()
        {

        }

        /// <summary>
        /// Test for "unknown" in MGLT
        /// </summary>
        [Test]
        public void MGLTValue_Unknown()
        {
            Domain.StarShip ss = new Domain.StarShip();
            ss.MGLT = "unknown";

            Assert.IsNull(ss.MGLTValue);
        }

        /// <summary>
        /// Test for null value in MGLT
        /// </summary>
        [Test]
        public void MGLTValue_Null()
        {
            Domain.StarShip ss = new Domain.StarShip();
            ss.MGLT = null;

            Assert.IsNull(ss.MGLTValue);
        }

        /// <summary>
        /// Test for empty value in MGLT
        /// </summary>
        [Test]
        public void MGLTValue_Empty()
        {
            Assert.Pass(); Domain.StarShip ss = new Domain.StarShip();
            ss.MGLT = "";

            Assert.IsNull(ss.MGLTValue);
        }

        /// <summary>
        /// Test for "0" in MGLT
        /// </summary>
        [Test]
        public void MGLTValue_Zero()
        {
            Assert.Pass(); Domain.StarShip ss = new Domain.StarShip();
            ss.MGLT = "0";

            Assert.AreEqual(ss.MGLTValue, 0);
        }

        /// <summary>
        /// Test for random value in MGLT
        /// </summary>
        [Test]
        public void MGLTValue_RandomValue()
        {
            Assert.Pass(); Domain.StarShip ss = new Domain.StarShip();

            Random rd = new Random();
            int number = rd.Next();
            ss.MGLT = number.ToString();

            Assert.AreEqual(ss.MGLTValue, number);
        }
    }
}