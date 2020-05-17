using FileWebApi.Models;
using Microsoft.Extensions.Options;

namespace FileWebApi.LoadBalancer
{
    public class LoadBalancerFactory
    {
        private const string WeightedRobinAlgorithmName = "weightedrobin";
        private const string RandomAlgorithmName = "random";
        private readonly Instance[] _instances;
        private readonly string _algorithmName;

        public LoadBalancerFactory(IOptionsSnapshot<LoadBalancerSettings> settings)
        {
            _algorithmName = settings.Value.BalancingAlgorithm.ToLower();
            _instances = settings.Value.Instances;
        }

        public InstanceTarget GetInstance()
        {
            LoadBalancerBaseAlgorithm alg = null;
            if(_algorithmName == WeightedRobinAlgorithmName)
            {
                alg = new WeightedRobinAlgorithm(_instances);
            }
            else if(_algorithmName == RandomAlgorithmName)
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
