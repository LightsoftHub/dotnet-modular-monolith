# Modular Monolith Solution Template for ASP.NET Core

## Technologies

* .NET 8
* Light Framework
* Entity Framework Core 8
* MediatR
* Mapster
* FluentValidation
* SignalR
* Serilog
* Redis
* RabbitMQ

The easiest way to get started is to install the [.NET template](https://www.nuget.org/packages/ModularMonolith.Solution.Template):
```bash
dotnet new install ModularMonolith.Solution.Template
```

To create a ASP.NET Core Web API solution:
```bash
dotnet new mm-sln -n YourProjectName
```

To create module projects template:
```bash
dotnet new mm -n YourModuleName
```

## Overview

### Shared

This will contain common entities, enums, exceptions, interfaces, types and logic specific to the domain layer.

### Shared.Auth

This layer contains authentication & authorization logic.

### Shared.Infrastructure

This layer contains common classes for accessing external resources such as file systems, web services, smtp, and so on. These classes should be based on interfaces defined within the shared layer.