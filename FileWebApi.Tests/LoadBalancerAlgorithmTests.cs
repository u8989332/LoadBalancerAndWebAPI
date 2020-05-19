using FileWebApi.LoadBalancer;
using FileWebApi.Models;
using FileWebApi.Tests.Settings;
using NUnit.Framework;
using System.Linq;

namespace FileWebApi.Tests
{
    public class LoadBalancerAlgorithmTests
    {
        [Test]
        public void RandomAlgorithm_GetInstance_TargetServerInList()
        {
            var instances = LoadBalacingSettings.GetRamdomBalancingAlgorithmSettings().Value.Instances;
            RandomAlgorithm algorithm = new TestRandomAlgorithm(instances);
            var result = algorithm.GetInstance();

            var matchInstance = instances.Where(x => x.Ip == result.Ip && x.Port == result.Port).FirstOrDefault();
            Assert.IsNotNull(matchInstance);
        }

        [Test]
        public void WeightRobinAlgorithm_GetInstance_4TargetServersAnd4thReset()
        {
            var instances = LoadBalacingSettings.GetRamdomBalancingAlgorithmSettings().Value.Instances;
            WeightedRobinAlgorithm algorithm = new WeightedRobinAlgorithm(instances);
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
    }

    public class TestRandomAlgorithm : RandomAlgorithm
    {
        public TestRandomAlgorithm(Instance[] instances) : base(instances)
        {
        }

        // mock random index by sequential number
        private static int pos = 0;
        protected override int GetInstanceRandomIndex(int instanceLength)
        {
            int curIndex = pos;
            pos++;
            if(pos >= instanceLength)
            {
                pos = 0;
            }
            return curIndex;
        }
    }
}