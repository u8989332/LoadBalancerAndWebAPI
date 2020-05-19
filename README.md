

# LoadBalancerAndWebAPI
> Web API supports load-balancing to access file services.

[![Build Status](https://travis-ci.org/u8989332/LoadBalancerAndWebAPI.svg?branch=master)](https://travis-ci.org/u8989332/LoadBalancerAndWebAPI)


## Docker
Use docker to run it and open browser with URL : [localhost:5000/api?uri=XXXXXXXXX](localhost:5000/api?uri=XXXXXXXXX)
```sh
git clone https://github.com/u8989332/LoadBalancerAndWebAPI.git

docker build -t loadbalancer-webapi .

docker run -it --rm -p 5000:80 --name my-webapi loadbalancer-webapi
```
## Change Load-Balancing Algorithms
Support 2 algorithms:
* "WeightedRobin"
* "Random"

In the config file Settings/loadbalancersettings.json, a attribute `BalancerSettings`'s value is "WeightedRobin". Can edit this value to "Random" at runtime.