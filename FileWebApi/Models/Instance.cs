namespace FileWebApi.Models
{
    /// <summary>
    /// Load-balancer target server.
    /// </summary>
    public class Instance
    {
        /// <summary>
        /// Gets or sets the target server IP.
        /// </summary>
        public string Ip { get; set; }

        /// <summary>
        /// Gets or sets the target server port.
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// Gets or sets the target server weight.
        /// This is for some balancing algorithm.
        /// More weight is more important.
        /// </summary>
        public int Weight { get; set; }
    }
}
