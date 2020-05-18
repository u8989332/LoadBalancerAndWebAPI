namespace FileWebApi.LoadBalancer
{
    using FileWebApi.Models;

    /// <summary>
    /// Base algorithm for load-balacing.
    /// </summary>
    public abstract class LoadBalancerBaseAlgorithm
    {
        /// <summary>
        /// Current target server list.
        /// </summary>
        protected Instance[] _instances;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoadBalancerBaseAlgorithm"/> class.
        /// </summary>
        /// <param name="instances">Target server list<see cref="Instance[]"/>.</param>
        public LoadBalancerBaseAlgorithm(Instance[] instances)
        {
            _instances = instances;
        }

        /// <summary>
        /// All sub class to implement this method to return a target server.
        /// </summary>
        /// <returns>The <see cref="InstanceTarget"/>.</returns>
        public abstract InstanceTarget GetInstance();
    }
}
