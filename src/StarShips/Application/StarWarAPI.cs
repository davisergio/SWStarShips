using Domain;
using Helpers;
using InterfaceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace Application
{
    /// <summary>
    /// Star Swars API - Application Layer
    /// </summary>
    public class StarWarsAPI : IStarShipsOperations
    {
        /// <summary>
        /// Read All StarShips Data from  Web API
        /// </summary>
        /// <returns>Star Ships</returns>
        public async Task<IList<StarShip>> GetListStarShipsAsync()
        {
            List<StarShip> listStarWarsAPI = new List<StarShip>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new System.Uri(Helpers.Config.BaseURL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string methodName = Helpers.Config.GetMethodName;
                bool endLoop = false;
                while (!endLoop)
                {
                    HttpResponseMessage response = await client.GetAsync(methodName);
                    if (response.IsSuccessStatusCode)
                    {
                        PagingAccess mainList = await JsonSerializer.DeserializeAsync<PagingAccess>(await response.Content.ReadAsStreamAsync());

                        if (mainList != null)
                        {
                            if (mainList.results != null && mainList.results.Count > 0)
                                listStarWarsAPI.AddRange(mainList.results);

                            if (!string.IsNullOrWhiteSpace(mainList.next))
                                methodName = mainList.next.Replace(Helpers.Config.BaseURL, "");
                            else
                                endLoop = true;
                        }
                    }
                    else
                        throw new StarShipException(Message.ERR_ACCESS_API);
                }

            }

            return listStarWarsAPI;
        }

        /// <summary>
        /// Output information about starships and amount stop
        /// </summary>
        /// <param name="listStarShips">List of Star Ships</param>
        /// <param name="value">Value for distance in Amount Stop routine</param>
        /// <returns>Output information list</returns>
        public IEnumerable<string> GetOutputData(IList<StarShip> listStarShips, decimal value)
        {
            if (listStarShips == null || listStarShips.Count == 0)
                throw new StarShipException(Message.ERR_NO_STARSHIPS_FOR_COMPARISON);

            if (value <= 0)
                throw new StarShipException(Message.ERR_VALUE_INVALID);

            IList<StarShip> filteredStarShips = listStarShips.Where(p => p.MGLTValue.HasValue).ToList();

            foreach (var starShip in filteredStarShips)
            {
                decimal amountStop = GetAmountStopValue(value, starShip.MGLTValue.Value);

                yield return $"{starShip.name}: {amountStop:,0}";
            }
        }

        /// <summary>
        /// Amount Stop Calc
        /// </summary>
        /// <param name="value">Value Total in MGLT</param>
        /// <param name="starShipValue">Value of Star Ship in MGLT</param>
        /// <returns></returns>
        public decimal GetAmountStopValue(decimal value, decimal starShipValue)
        {
            return Math.Ceiling(value / starShipValue);
        }
    }
}
