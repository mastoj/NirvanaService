NirvanaService
==============

An attempt to implement a generic service with TopShelf that will host an exe project.

This is just a spike at the moment but to get it going you update the config to something you like, see: https://github.com/mastoj/NirvanaService/blob/master/src/NirvanaService/conf/NirvanaService.json. 

After that you just run `NirvanaService -servicename:<name of service>`. The `<name of service>` is the name of one service in the config file. You can have multiple services specified in the config and still use this one hosting service to all of them, as long as it is an executable there which you can run.
