using Domain;
using Helpers;
using InterfaceLayer;
using System;

namespace ConsoleApp
{
    public class Program
    {
        /// <summary>
        /// Main Entry
        /// </summary>
        public static void Main(string[] args)
        {
            MainProcess(GetValueMGLT());

            Console.Write(Message.WAIT_END_APPLICATION);
            Console.ReadKey();
        }

        /// <summary>
        /// Ask to user for a valid input number grander than zero
        /// And validates this value
        /// </summary>
        /// <returns>The value inputed by the user</returns>
        public static decimal GetValueMGLT()
        {
            decimal valueMGLT = 0;

            do
            {
                Console.Write(Message.ASK_FOR_INPUT);
                string inputValue = Console.ReadLine();

                if (Decimal.TryParse(inputValue, out valueMGLT) && valueMGLT > 0)
                    break;

                Console.WriteLine(Message.INPUT_INVALID);
            } while (true);

            return valueMGLT;
        }

        /// <summary>
        /// Main Process
        /// Call reading API
        /// Call routine to output data
        /// </summary>
        /// <param name="valueMGLT">Value MGLT gave by the user</param>
        public static void MainProcess(decimal valueMGLT)
        {
            try
            {
                IStarShipsOperations sw = new Application.StarWarsAPI();
                var listStarShips = sw.GetListStarShipsAsync().Result;

                Console.WriteLine(Message.LIST_MESSAGE);
                foreach (string outputItem in sw.GetOutputData(listStarShips, valueMGLT))
                {
                    Console.WriteLine(outputItem);
                }
            }
            catch (StarShipException sse)
            {
                Console.WriteLine(sse.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(Message.ERR_GENERAL);
                Console.WriteLine(ex.Message);
            }
        }
    }
}
