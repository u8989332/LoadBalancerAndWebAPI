namespace FileWebApi.LoadBalancer
{
    using FileWebApi.Models;
    using System;

    /// <summary>
    /// Use random algorithm to select a target server.
    /// </summary>
    public class RandomAlgorithm : LoadBalancerBaseAlgorithm
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RandomAlgorithm"/> class.
        /// </summary>
        /// <param name="instances">Target server list<see cref="Instance[]"/>.</param>
        public RandomAlgorithm(Instance[] instances) : base(instances)
        {
        }

        /// <summary>
        /// The GetInstance.
        /// </summary>
        /// <returns>The <see cref="InstanceTarget"/>.</returns>
        public override InstanceTarget GetInstance()
        {
            int randomIndex = new Random().Next(0, _instances.Length);
            var randomInstance = _instances[randomIndex];
            return new InstanceTarget
            {
                Ip = randomInstance.Ip,
                Port = randomInstance.Port
            };
        }
    }
}
