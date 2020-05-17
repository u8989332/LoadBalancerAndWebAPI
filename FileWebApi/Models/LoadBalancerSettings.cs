namespace FileWebApi.Models
{
    public class LoadBalancerSettings
    {
        public Instance[] Instances { get; set; }

        public string BalancingAlgorithm { get; set; }
    }
}
