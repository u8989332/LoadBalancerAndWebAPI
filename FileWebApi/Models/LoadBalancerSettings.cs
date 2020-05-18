namespace FileWebApi.Models
{
    /// <summary>
    /// Bind load-balancing setting to this class.
    /// </summary>
    public class LoadBalancerSettings
    {
        /// <summary>
        /// Gets or sets target servers.
        /// </summary>
        public Instance[] Instances { get; set; }

        /// <summary>
        /// Gets or sets Balancing Algorithm.
        /// </summary>
        public string BalancingAlgorithm { get; set; }
    }
}
