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

### Using clauses
Create a C# application. Create the Etagair engine object.
In the program, you have to include these using:
```csharp
  using Etagair.Core.System;
  using Etagair.Engine;
```
### Create the engine and the database
These lines of code create the engine object. The init method create the database or use the existing one.

```csharp
	EtagairEngine engine = new EtagairEngine();

	// the path must exists, location where to put the database file
	string dbPath = @".\Data\";

	// create the database or reuse the existing one
	if (!engine.Init(dbPath))
	{
		Console.WriteLine("Db initialization Failed.");
		return;
	}

	// the database is created or reused and opened, ready to the execution
	Console.WriteLine("Db initialized with success.");
```

