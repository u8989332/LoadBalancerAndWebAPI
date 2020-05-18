namespace FileWebApi.LoadBalancer
{
    using FileWebApi.Models;
    using System.Collections.Generic;

    /// <summary>
    /// Use weighted round robin algorithm to select a target server.
    /// </summary>
    public class WeightedRobinAlgorithm : LoadBalancerBaseAlgorithm
    {
        /// <summary>
        /// Current round robin index.
        /// </summary>
        private static int pos;

        /// <summary>
        /// Thread-safe lock key.
        /// </summary>
        private static object lockKey = new object();

        /// <summary>
        /// Initializes a new instance of the <see cref="WeightedRobinAlgorithm"/> class.
        /// </summary>
        /// <param name="instances">Target server list<see cref="Instance[]"/>.</param>
        public WeightedRobinAlgorithm(Instance[] instances) : base(instances)
        {
        }

        /// <summary>
        /// Select server.
        /// </summary>
        /// <returns>The selected server <see cref="InstanceTarget"/>.</returns>
        public override InstanceTarget GetInstance()
        {
            List<Instance> serverList = new List<Instance>();
            foreach (var instance in _instances)
            {
                int weight = instance.Weight;
                for (int i = 0; i < weight; ++i)
                {
                    serverList.Add(instance);
                }
            }

            Instance server;
            // thread-safety to update index
            lock (lockKey)
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
