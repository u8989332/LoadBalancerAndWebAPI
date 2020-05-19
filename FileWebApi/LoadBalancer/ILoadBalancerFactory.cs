using FileWebApi.Models;

namespace FileWebApi.LoadBalancer
{
    public interface ILoadBalancerFactory
    {
        InstanceTarget GetInstance();
    }
}