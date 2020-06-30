# gRPC Setup Steps


* Install VS-2019 Community Edition (https://visualstudio.microsoft.com/vs/community/)
* After you install it successfully, then launch VS-2019
* Create blank sample solution, name it as “gRPCSolution”
* Add a .Net console project by creating new project. Name this new project “Client”
* Add another .Net console project by creating new project. Name this new project “Server”
* Create a folder under “gRPCSoultion”. Name it as “protobuffer”. Under this folder we will create proto files, whose output would be required for Client and Server projects.
* Uptil now this structure will look like this =>

  <image src='../../Images/gRPCFolderStructure.png' title='Solution Structure'/>

* Now we need to include gRPC compile output to Client and Server console applications
* Create a folder names "gRPCclasses" to both the projects i.e. Client and Server projects
* Right click on the Client project and select "Unload Project" action
* Right Click on unloaded Client project and select "Edit Client.csproj"
* In the project edit xml page, search for "ItemGroup" with "Compile" child node
* Add following lines to the above searched "Item Group"
  
