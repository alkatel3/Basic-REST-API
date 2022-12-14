# Basic-REST-API
 Basic REST API of an expense accounting application
 
 Deployed project: https://base-rest-api-20221026203626.azurewebsites.net/
 
 Postmat colection: https://martian-crescent-155697.postman.co/workspace/New-Team-Workspace~17abced1-47a4-41a6-97c4-9e2ae03615c6/collection/23834673-967d534d-eafd-4701-9a36-1d4ab42bc207?action=share&creator=23834673
 
 Postman enviroment: https://martian-crescent-155697.postman.co/workspace/New-Team-Workspace~17abced1-47a4-41a6-97c4-9e2ae03615c6/environment/23834673-570c641e-fb52-413c-aaba-3fa224d126bd
 
## Installing .NET6.0

**Windows**

Download from the link:  https://dotnet.microsoft.com/en-us/download/dotnet

**Linux**

Add the Microsoft package signing key to the list of validated keys and add the repository:

 ```wget https://packages.microsoft.com/config/ubuntu/21.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb``` 
  
  ```sudo dpkg -i packages-microsoft-prod.deb```
  
  ```rm packages-microsoft-prod.deb```
  
Install the SDK for .NET:

  ```sudo apt-get update```
  
  ```sudo apt-get install -y apt-transport-https```
  
  ```sudo apt-get update```
  
  ```sudo apt-get install -y dotnet-sdk-6.0```
  
Install the .NET runtime:
  
  ```sudo apt-get install -y dotnet-runtime-6.0```

## Project launch

Clone the repository:

 ```git clone https://github.com/juliion/CostAccountingAPI.git```

Go to the working directory of the cloned project "CostAccountingAPI"

   ```cd CostAccountingAPI/CostAccountingAPI```

Run the project:

 ```dotnet run```
