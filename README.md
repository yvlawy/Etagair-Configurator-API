## What is Etagair?
Etagair is a general purpose Configurator API for modeling and managing products, services, or everything else.

It's developed in .NET/C# and it is provided as a set of several libraries. 
It's a back-office API, there is no UI.
Data are saved in a embedded database.

For more details, see the [wiki](https://github.com/yvlawy/Etagair-Configurator-API/wiki)

## Key features
-Embedded .NET family assembly, platform independent without references to other libraries.

-Management of complex objects : entities with properties (list of key-value).

-Entities are organized hierarchically in folders.

-Creation of complex entities based on template.

-Localized media, only text for now (by using text code). 
 
-Rich search of entities by properties criteria.

## Getting started
Create a C# application. Create the Etagair engine object.

```csharp
    EtagairEngine engine = new EtagairEngine();
```

