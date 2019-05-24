# Funda Top Real Estate Agents
This project demonstrates how to create a Top 10 Real Estate Agents in Amsterdam with the most objects for sale. A distinction is made between objects with and without a garden. 
It is also meant as a practice to work with .NET Core, hooking up a third party API and how to deal with issues, such as rate limiting.

## Getting Started
To get the project up and running on your local machine, follow these steps:

### Prerequisites
```
yarn
```

### Installing
Clone the project and go to the /Clientapp folder within your terminal.
Install all dependencies for the Vue app, by running:
```
yarn install
```
After a successful install, make sure the Web Project is setup as StartUp Project in Visual Studio, hit F5 or run
```
dotnet run
```
in your terminal and visit `https://localhost:5001`.

### Deployment
For deployment, run the following command to compile and minify the client app for production:
```
yarn run build
```

### Built With
- .NET Core 2.1
- Vue.js
- Yarn

Good luck in finding your dream home!