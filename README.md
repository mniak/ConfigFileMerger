ConfigFileMerger 
====================================
[![License: MIT](https://img.shields.io/badge/license-MIT-green.svg)](LICENSE.md)

Tool to merge `.config` files.


How `configSource` works
---------------------------
Quote from Microsoft documentation article [Connection Strings and Configuration Files](https://docs.microsoft.com/en-us/dotnet/framework/data/adonet/connection-strings-and-configuration-files#using-external-configuration-files):
> In the main application configuration file, you use the `configSource` attribute to specify the fully qualified name and location of the external file. This example refers to an external configuration file named **connections.config**.
> ```xml
> <?xml version='1.0' encoding='utf-8'?>  
> <configuration>  
>     <connectionStrings configSource="connections.config"/>  
> </configuration>  
> ```
