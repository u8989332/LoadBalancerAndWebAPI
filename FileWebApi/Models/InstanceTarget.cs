namespace FileWebApi.Models
{
    /// <summary>
    /// After balancing algorithm calculation, return this to controller which server to serve.
    /// </summary>
    public class InstanceTarget
    {
        /// <summary>
        /// Gets or sets the targer server IP.
        /// </summary>
        public string Ip { get; set; }

        /// <summary>
        /// Gets or sets the target server port.
        /// </summary>
        public int Port { get; set; }
    }
}
