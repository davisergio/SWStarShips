using Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InterfaceLayer
{
    /// <summary>
    /// Interface for the routines in the Application Layer
    /// </summary>
    public interface IStarShipsOperations
    {
        /// <summary>
        /// Search for starships and return this list
        /// </summary>
        /// <returns>List of StarShips</returns>
        Task<IList<StarShip>> GetListStarShipsAsync();

        /// <summary>
        /// Get otput information about Star Ships and Amount Stop Calculation
        /// </summary>
        /// <param name="listStarShips">List of Star Ships</param>
        /// <param name="value">Value for distance in Amount Stop Calculation</param>
        /// <returns></returns>
        IEnumerable<string> GetOutputData(IList<StarShip> listStarShips, decimal value);

        /// <summary>
        /// Calculate the amount stop for the distance
        /// </summary>
        /// <param name="value">Total Distance in MGLT</param>
        /// <param name="starShipValue">Star Ship Value in MGLT</param>
        /// <returns>Amount Stop</returns>
        decimal GetAmountStopValue(decimal value, decimal starShipValue);
    }
}
