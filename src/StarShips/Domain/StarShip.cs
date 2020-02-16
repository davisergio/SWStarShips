namespace Domain
{
    /// <summary>
    /// StarShip Class
    /// With necessary information to the program works
    /// </summary>
    public class StarShip
    {
        public string name { get; set; }
        public string model { get; set; }

        //MGLT received by Web API
        public string MGLT { get; set; }

        /// <summary>
        /// MGLT typed value
        /// </summary>
        public decimal? MGLTValue
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this.MGLT))
                    return null;

                try
                {
                    return decimal.Parse(this.MGLT);
                }
                catch
                {
                    return null;
                }
            }
        }
    }
}
