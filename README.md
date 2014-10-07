NirvanaService
==============

An attempt to implement a generic service with TopShelf that will host an exe project.

This is just a spike at the moment but to get it going you create a config file for the service you want to run and put it in this folder: https://github.com/mastoj/NirvanaService/tree/master/src/NirvanaService/conf. In the folder you can see two sampleconfigurations. 

The reason to why there is one file per service and not one for all the services, which I started with, is that it is much easier to use a configuration manager, like puppet, to replace one file instead of relying on some transformation of a file.

After that you just run `NirvanaService -servicename:<name of service>`. The `<name of service>` is the name of one service in the config file. You can have multiple services specified in the config and still use this one hosting service to all of them, as long as it is an executable there which you can run.
