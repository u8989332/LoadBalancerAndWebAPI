using FileWebApi.LoadBalancer;
using FileWebApi.Tests.Settings;
using NUnit.Framework;
using System.Linq;

namespace FileWebApi.Tests
{
    public class LoadBalancerFactoryTests
    {
        [Test]
        public void GetInstance_WithRandomAlgorithm_TargetServerInList()
        {
            var options = LoadBalacingSettings.GetRamdomBalancingAlgorithmSettings();
            ILoadBalancerFactory factory = new LoadBalancerFactory(options);
            var result = factory.GetInstance();
            var matchInstance = options.Value.Instances.Where(x => x.Ip == result.Ip && x.Port == result.Port).FirstOrDefault();
            Assert.IsNotNull(matchInstance);
        }

        [Test]
        public void GetInstance_WithWeightedRobinAlgorithm_4TargetServersAnd4thReset()
        {
            var instances = LoadBalacingSettings.GetWeightedRobinBalancingAlgorithmSettings().Value.Instances;
            WeightedRobinAlgorithm algorithm = new WeightedRobinAlgorithm(instances, true);
            var result1 = algorithm.GetInstance();
            var result2 = algorithm.GetInstance();
            var result3 = algorithm.GetInstance();
            var result4 = algorithm.GetInstance();
            Assert.AreEqual("localhost", result1.Ip);
            Assert.AreEqual(1000, result1.Port);

            Assert.AreEqual("192.168.1.10", result2.Ip);
            Assert.AreEqual(1002, result2.Port);

            Assert.AreEqual("192.168.1.10", result3.Ip);
            Assert.AreEqual(1002, result3.Port);

            Assert.AreEqual("localhost", result4.Ip);
            Assert.AreEqual(1000, result4.Port);
        }

        [Test]
        public void GetInstance_WithUnknownAlgorithm_StillRunRandomAndGetTargetServerInList()
        {
            var options = LoadBalacingSettings.GetUnknownAlgorithmSettings();
            ILoadBalancerFactory factory = new LoadBalancerFactory(options);
            var result = factory.GetInstance();
            var optionsInstances = options.Value.Instances.Where(x => x.Ip == result.Ip && x.Port == result.Port).FirstOrDefault();
            Assert.IsNotNull(optionsInstances);
        }
    }

}