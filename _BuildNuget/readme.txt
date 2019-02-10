Etagair nuget publication readme.txt:

need to install the package: Nuget.CommandLine into the solution. 
(Nuget.Build?)

================================================================================
Publications:
last is: 0.0.1.0, 09fev19.

================================================================================
Folders and files descriptions:

BuildNuget\
To build the nuget package of the application Etagair.

Model\
The model of files and folders.

Built\
temporary content. 
where are built the current package.


================================================================================
Process:

-Don't forget to set the new version number to all the libraries in the Appli folder!
   use 3 digits!
   
-Build them.
  

-Update the nuget text file descriptor:
(in Visual Studio)
_BuildNuget\Model\
	Etagair.nuspec

-Create a target folder:
Built\Etagair.x.x.x.x\

-Copy the model folders.

-Copy the Etagair.nuspec
  (from the model folder)
	into the path: on the root (into Built\Etagair.x.x.x.x\)
  
-Copy the 3 dll libraries:
	(from the folder: Dev\DevApp\bin\debug)
	6 dlls

	into the path:
		Model\lib\netcoreapp2.0\
		
-Check the version of all the libraries


-Generate the package:
(inside Visual Studio, in the package manager console)
	>cd _BuildNuget\Built
	>cd .\Etagair.x.x.x.x
	>nuget pack

the result:
	inside: Built\Etagair.x.x.x.x\
		Etagair.x.x.x.x.nupkg

then, publish the generated file on the nuget website.
Sign in, 
Upload the packet:
	Built\Etagair.x.x.x.x\Etagair.x.x.x.x.nupkg
