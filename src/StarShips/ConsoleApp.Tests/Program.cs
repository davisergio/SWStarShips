using NUnit.Framework;

namespace ConsoleApp.Tests
{
    public class Program
    {
        [SetUp]
        public void Setup()
        {
        }

        /// <summary>
        /// Test main process with some possible values
        /// </summary>
        [Test]
        public void MainProcess_TrySomeValues()
        {
            ConsoleApp.Program.MainProcess(50000);
            ConsoleApp.Program.MainProcess(1000);
            ConsoleApp.Program.MainProcess(6300);
            ConsoleApp.Program.MainProcess(998);

            Assert.Pass();
        }
    }
}