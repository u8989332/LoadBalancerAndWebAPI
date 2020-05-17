using FileWebApi.Models;
using System.Collections.Generic;

namespace FileWebApi.LoadBalancer
{
    public class WeightedRobinAlgorithm : LoadBalancerBaseAlgorithm
    {
        private static int pos;
        private static object lockKey = new object();

        public WeightedRobinAlgorithm(Instance[] instances) : base(instances)
        {
        }

        public override InstanceTarget GetInstance()
        {
            List<Instance> serverList = new List<Instance>();
            foreach(var instance in _instances)
            {
                int weight = instance.Weight;
                for (int i = 0; i < weight; ++i)
                {
                    serverList.Add(instance);
                }
            }

            Instance server;
            lock(lockKey)
            {
                if (pos >= serverList.Count)
                {
                    pos = 0;
                }
                server = serverList[pos];
                pos++;
            }

            return new InstanceTarget
            {
                Ip = server.Ip,
                Port = server.Port
            };
        }
    }
}
