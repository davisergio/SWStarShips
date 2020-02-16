using System.Collections.Generic;

namespace Domain
{
    /// <summary>
    /// Pagination Class
    /// As the Web API communicates
    /// </summary>
    public class PagingAccess
    {
        public int count { get; set; }
        public string next { get; set; }
        public string previous { get; set; }

        public IList<StarShip> results { get; set; }
    }
}
