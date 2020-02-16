using NUnit.Framework;

namespace Helpers.Tests
{
    public class Config
    {
        [SetUp]
        public void Setup()
        {

        }

        /// <summary>
        /// Test if will be added a slash as suffix in a URL without slash in the end
        /// </summary>
        [Test]
        public void EndsWithSlash_NoSlash()
        {
            string value = "http://teste.com";
            string expectedValue = $"{value}/";

            Assert.AreEqual(Helpers.Config.EndsWithSlash(value), expectedValue);            
        }

        /// <summary>
        /// Test the return in case the URL ends with slash
        /// </summary>
        [Test]
        public void EndsWithSlash_WithSlash()
        {
            string value = "http://teste.com/";

            Assert.AreEqual(Helpers.Config.EndsWithSlash(value), value);
        }
    }
}