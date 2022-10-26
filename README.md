# Basic-REST-API
 Basic REST API of an expense accounting application
 
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
