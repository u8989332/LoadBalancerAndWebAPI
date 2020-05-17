using FileWebApi.Models;
using System.Collections.Generic;

namespace FileWebApi.LoadBalancer
{
    public abstract class LoadBalancerBaseAlgorithm
    {
        protected Instance[] _instances;
        public LoadBalancerBaseAlgorithm(Instance[] instances)
        {
            _instances = instances;
        }
        public abstract InstanceTarget GetInstance();
    }
}
