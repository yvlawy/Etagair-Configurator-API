## What is Etagair?
![The Etagair Application](_Resx/Logo/Etagair_logo128.jpg)

Etagair is a general purpose Configurator API for modeling and managing products, services, or everything else.

It's developed in C# language with .NEt Standard 2.0 and it is provided as a set of several libraries. 
It's a back-office API, there is no UI.
Data are saved in a embedded database.

For more details, see the [wiki](https://github.com/yvlawy/Etagair-Configurator-API/wiki)

## Key features
-Embedded .NET family assembly, platform independent with reference to one library: [LiteDB](https://www.nuget.org/packages/LiteDB/), 
A lightweight embedded .NET NoSQL document store in a single datafile.

-Management of complex objects : entities with properties (list of key-value).

-Entities are organized hierarchically in folders.

-Creation of complex entities based on template.

-Localized media, only text for now (by using text code). 
 
-Rich search of entities by properties criteria.

## Getting started
### Using clauses
Create a C# application. Import the Nuget package (see the link below).
In the program, include these using:

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

### Create a folder within an entity 
Create a folder named "computers", add under it an entity having two properties.
An entity haven't a name. Property of an entity is key-value pair. 

The data:
```csharp
F:computers\
  E:   
  "Name"= "Toshiba Satellite Core I7"
  "Trademark"= "Toshiba"
```
The Code:
```csharp
// create a folder, under the root
Folder foldComputers = engine.Editor.CreateFolder(null, "computers");

// create an entity, under the computers folder
Entity toshibaCoreI7 = engine.Editor.CreateEntity(foldComputers);

// Add 2 properties to the entity (key - value)
engine.Editor.CreateProperty(toshibaCoreI7, "Name", "Toshiba Satellite Core I7");
engine.Editor.CreateProperty(toshibaCoreI7, "Trademark", "Toshiba");
```

### Package on Nuget
The application is ready to use and available on Nuget.
The last version is 0.0.5-alpha.

The web site: https://www.nuget.org/packages/Etagair/

The last release: https://www.nuget.org/packages/Etagair/0.0.5-alpha


