using Domain;
using InterfaceLayer;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Application.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            
        }        

        /// <summary>
        /// Try to connect to Web API correctly
        /// </summary>
        [Test]
        public void GetListStarShipsAsync_ValidAccess()
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            
            if (config.AppSettings.Settings["baseURL"] != null)
                config.AppSettings.Settings.Remove("baseURL");
            config.AppSettings.Settings.Add("baseURL", "https://swapi.co/api/");

            if (config.AppSettings.Settings["getMethodName"] != null)
                config.AppSettings.Settings.Remove("getMethodName");
            config.AppSettings.Settings.Add("getMethodName", "starships/");
            
            config.Save(ConfigurationSaveMode.Modified, true);
            ConfigurationManager.RefreshSection("appSettings");

            IStarShipsOperations swAPI = GetNewFactoryStarWarsAPI();
            IList<StarShip> listStarShip = swAPI.GetListStarShipsAsync().Result;
                       
            Assert.Pass();
        }

        /// <summary>
        /// Try to connect to Web API with wrong URL
        /// </summary>
        [Test]
        public void GetListStarShipsAsync_WrongURL()
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            if (config.AppSettings.Settings["baseURL"] != null)
                config.AppSettings.Settings.Remove("baseURL");
            config.AppSettings.Settings.Add("baseURL", "ht22://wrongurl123abc");

            if (config.AppSettings.Settings["getMethodName"] != null)
                config.AppSettings.Settings.Remove("getMethodName");
            config.AppSettings.Settings.Add("getMethodName", "starships/");

            config.Save(ConfigurationSaveMode.Modified, true);
            ConfigurationManager.RefreshSection("appSettings");

            IStarShipsOperations swAPI = GetNewFactoryStarWarsAPI();
            Assert.That(() => { 
                IList<StarShip> listStarShip = swAPI.GetListStarShipsAsync().Result;
                              }, Throws.Exception);
        }

        /// <summary>
        /// Try to connect to Web API with wrong segment URL
        /// </summary>
        [Test]
        public void GetListStarShipsAsync_WrongMethod()
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            if (config.AppSettings.Settings["baseURL"] != null)
                config.AppSettings.Settings.Remove("baseURL");
            config.AppSettings.Settings.Add("baseURL", "https://swapi.co/api/");

            if (config.AppSettings.Settings["getMethodName"] != null)
                config.AppSettings.Settings.Remove("getMethodName");
            config.AppSettings.Settings.Add("getMethodName", "noexist1/");

            config.Save(ConfigurationSaveMode.Modified, true);
            ConfigurationManager.RefreshSection("appSettings");

            IStarShipsOperations swAPI = GetNewFactoryStarWarsAPI();
            Assert.That(() => {
                IList<StarShip> listStarShip = swAPI.GetListStarShipsAsync().Result;
                              }, Throws.Exception);
        }

        /// <summary>
        /// Call output information data with an empty list
        /// </summary>
        [Test]
        public void GetOutputData_EmptyListStarShips()
        {
            IStarShipsOperations swAPI = GetNewFactoryStarWarsAPI();
            Assert.That(() => { 
                swAPI.GetOutputData(new List<StarShip>(), 10).ToList(); }, 
                Throws.InstanceOf<StarShipException>());
        }

        /// <summary>
        /// Call output information data with null value list
        /// </summary>
        [Test]
        public void GetOutputData_NullListStarShips()
        {
            IStarShipsOperations swAPI = GetNewFactoryStarWarsAPI();
            Assert.Throws<StarShipException>(() => swAPI.GetOutputData(null, 10).ToList());
        }

        /// <summary>
        /// Call output information data with 0 as value for distance in Amount Stop MGLT Calculation
        /// </summary>
        [Test]
        public void GetOutputData_ZeroMGLT()
        {
            IList<StarShip> listStarShip = new List<StarShip>()
            {
                new StarShip() { name = "1", model = "M1", MGLT = "15" }
            };

            IStarShipsOperations swAPI = GetNewFactoryStarWarsAPI();
            Assert.Throws<StarShipException>(() => swAPI.GetOutputData(listStarShip, 0).ToList());
        }

        /// <summary>
        /// Test 2 possible StarShips and check the expected output information
        /// </summary>
        [Test]
        public void GetOutputData_ExpectedOutput()
        {
            IList<StarShip> listStarShips = new List<StarShip>()
            {
                new StarShip() { name = "1", model = "M1", MGLT = "15" },
                new StarShip() { name = "2", model = "M2", MGLT = "10" }
            };

            IStarShipsOperations swAPI = GetNewFactoryStarWarsAPI();
            IEnumerable<string> outputValues = swAPI.GetOutputData(listStarShips, 15);

            Assert.AreEqual(outputValues.First(), "1: 1");
            Assert.AreEqual(outputValues.Last(), "2: 2");
        }

        /// <summary>
        /// Check some values for Amount Stop Calculation
        /// </summary>
        [Test]
        public void GetAmountStopValue_ValidCalc()
        {
            IStarShipsOperations swAPI = GetNewFactoryStarWarsAPI();
            
            Assert.AreEqual(swAPI.GetAmountStopValue(10, 10), 1);
            Assert.AreEqual(swAPI.GetAmountStopValue(20, 10), 2);
            Assert.AreEqual(swAPI.GetAmountStopValue(22, 10), 3);
            Assert.AreEqual(swAPI.GetAmountStopValue(25, 10), 3);
            Assert.AreEqual(swAPI.GetAmountStopValue(2, 10), 1);
            Assert.AreEqual(swAPI.GetAmountStopValue(69, 5), 14);
            Assert.AreEqual(swAPI.GetAmountStopValue(0, 10), 0);
        }

        /// <summary>
        /// Method used as Factory Object
        /// If the project grows, a dependency injection tool can be used
        /// </summary>
        /// <returns>New Instance of StarWarsAPI</returns>
        private StarWarsAPI GetNewFactoryStarWarsAPI()
        {
            return new StarWarsAPI();
        }
    }
}