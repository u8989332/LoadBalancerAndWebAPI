using FileWebApi.Models;
using System;
using System.Collections.Generic;

namespace FileWebApi.LoadBalancer
{
    public class RandomAlgorithm : LoadBalancerBaseAlgorithm
    {
        public RandomAlgorithm(Instance[] instances) : base(instances)
        {
        }

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
