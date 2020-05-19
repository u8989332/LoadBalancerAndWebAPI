using FileWebApi.Models;
using Microsoft.Extensions.Options;
using NSubstitute;

namespace FileWebApi.Tests.Settings
{
    class LoadBalacingSettings
    {
        public static IOptionsSnapshot<LoadBalancerSettings> GetRamdomBalancingAlgorithmSettings()
        {
            IOptionsSnapshot<LoadBalancerSettings> settings = Substitute.For<IOptionsSnapshot<LoadBalancerSettings>>();
            var result = new LoadBalancerSettings
            {
                BalancingAlgorithm = "Random",
                Instances = GetInstances()
            };

            settings.Value.Returns(result);

            return settings;
        }

        public static IOptionsSnapshot<LoadBalancerSettings> GetWeightedRobinBalancingAlgorithmSettings()
        {
            IOptionsSnapshot<LoadBalancerSettings> settings = Substitute.For<IOptionsSnapshot<LoadBalancerSettings>>();
            var result = new LoadBalancerSettings
            {
                BalancingAlgorithm = "WeightedRobin",
                Instances = GetInstances()
            };

            settings.Value.Returns(result);

            return settings;
        }

        public static IOptionsSnapshot<LoadBalancerSettings> GetUnknownAlgorithmSettings()
        {
            IOptionsSnapshot<LoadBalancerSettings> settings = Substitute.For<IOptionsSnapshot<LoadBalancerSettings>>();
            var result = new LoadBalancerSettings
            {
                BalancingAlgorithm = "47854174844",
                Instances = GetInstances()
            };

            settings.Value.Returns(result);

            return settings;
        }

        private static Instance[] GetInstances()
        {
            return new Instance[]
            {
                new Instance
                {
                    Ip = "localhost",
                    Port  = 1000,
                    Weight = 1
                },
                new Instance
                {
                     Ip = "192.168.1.10",
                    Port  = 1002,
                    Weight = 2
                }
            };
        }
    }
}
