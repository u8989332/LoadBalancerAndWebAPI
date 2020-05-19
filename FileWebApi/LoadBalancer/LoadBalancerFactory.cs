namespace FileWebApi.LoadBalancer
{
    using FileWebApi.Models;
    using Microsoft.Extensions.Options;

    /// <summary>
    /// A factory to select a target server.
    /// </summary>
    public class LoadBalancerFactory : ILoadBalancerFactory
    {
        /// <summary>
        /// Weighted Round Robin Algorithm Name.
        /// Must match the setting file.
        /// </summary>
        private const string WeightedRobinAlgorithmName = "weightedrobin";

        /// <summary>
        /// Random Algorithm Name.
        /// Must match the setting file.
        /// </summary>
        private const string RandomAlgorithmName = "random";

        /// <summary>
        /// Target server list loaded by setting.
        /// </summary>
        private readonly Instance[] _instances;

        /// <summary>
        /// algorithm name loaded by setting.
        /// </summary>
        private readonly string _algorithmName;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoadBalancerFactory"/> class.
        /// </summary>
        /// <param name="settings">The settings<see cref="IOptionsSnapshot{LoadBalancerSettings}"/>.</param>
        public LoadBalancerFactory(IOptionsSnapshot<LoadBalancerSettings> settings)
        {
            _algorithmName = settings.Value.BalancingAlgorithm.ToLower();
            _instances = settings.Value.Instances;
        }

        /// <summary>
        /// Return selected target server by using a load-balancing algorithm.
        /// </summary>
        /// <returns>Selected target server <see cref="InstanceTarget"/>.</returns>
        public InstanceTarget GetInstance()
        {
            LoadBalancerBaseAlgorithm alg = null;
            if (_algorithmName == WeightedRobinAlgorithmName)
            {
                alg = new WeightedRobinAlgorithm(_instances);
            }
            else if (_algorithmName == RandomAlgorithmName)
            {
                alg = new RandomAlgorithm(_instances);
            }
            else
            {
                // not match config, so use default Random method.
                alg = new RandomAlgorithm(_instances);
            }

            return alg.GetInstance();
        }
    }
}
