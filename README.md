# SportStore with database

## Objectives

- Learn how to use entities
- Learn how to define a persistence layer
- Learn how to use Entity Framework
- Learn how to work with migrations
- Learn how to work with seeds

## Goal

In this exercise we are going to add a database to our e-commerce website.

## Changes since chapter 8

This repository contains the solution code from the previous chapter. The classes `OrderItem` and `CartItem` were removed because the project was a little over engineered. These classes only existed for easier comparison of these lines but made the database configuration more difficult.

The `Order` class also got an extra property for the `Customer`.

## Exercise

1. Add the following connection string to the `appsettings.Development.json` of the `Server` project

```{json}
"ConnectionStrings": {
  "SportStore": "Server=(localdb)\\mssqllocaldb;Database=SportStore;Trusted_Connection=True;"
}
```

2. Create a new Class Library in the `src` folder called `Persistence` and add it to the solution, remove the dummy `Class1.cs`
3. Add the following dependencies to the `Persistence` project:
    - Microsoft.EntityFrameworkCore.Design
    - Microsoft.EntityFrameworkCore.SqlServer
4. Add the following dependencies to the `Server` project:
    - Microsoft.EntityFrameworkCore.Tools
5. Add a reference from the `Persistence` to the `Domain` project.
6. Add a reference from the `Services` to the `Persistence` project.
7. Create a `DbContext` named `SportStoreDbContext` in a `Data` folder in the `Persistence` project
    - This class only contains a constructor which calls the base class' constructor
8. Initialize a connection in the `StartUp` of the `Server`
9. Add a private default constructor for every class in the `Domain`. Note not every domain class should be persisted, how do you know?
10. Read through this documentation: [Persist value objects as owned entity types](https://docs.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/implement-value-objects#persist-value-objects-as-owned-entity-types-in-ef-core-20-and-later) and make every `ValueObject` an owned entity type.
    - Create seperate `IEntityTypeConfiguration` subclasses per class that needs modification, see the [documentation](https://www.learnentityframeworkcore.com/configuration/fluent-api#separate-configuration-classes) for more info.
    - For readonly properties, you need to specify the property in the model builder or Entity Framework won't add it to the database. An alternative is to add a private setter but this is bad practice because it's not obvious why the setter is private and it's not clear at first sight which properties are stored in the database.
    - Set a precision of 12 and a scale of 10 for the `Value` of class `Money`
    - Read through the [documentation](https://docs.microsoft.com/en-us/ef/core/what-is-new/ef-core-6.0/whatsnew#changes-to-owned-optional-dependent-handling) on how to make a `ValueObject` required. Make sure you add the required navigation property and also mark the renamed column required.
11. Add a `DbSet` for each entity that can be fetched
12. Create a migration name InitialCreate which contains all data to create the empty database
    - If the CLI tells you that the `ef` command does not exist, install the tool: `dotnet tool install --global dotnet-ef --version 5.0.12`
13. Create a class named `SportStoreDataInitializer` and paste the following code:

```{cs}
public class SportStoreDataInitializer
{
    private readonly SportStoreDbContext _dbContext;

    public SportStoreDataInitializer(SportStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void SeedData()
    {
        _dbContext.Database.EnsureDeleted();
        if (_dbContext.Database.EnsureCreated())
        {
            SeedProducts();
        }
    }

    private void SeedProducts()
    {
        var products = new ProductFaker()
            .RuleFor(p => p.Id, () => 0) // Remove the id, database column is auto generated
            .RuleFor(p => p.Category, new CategoryFaker().RuleFor(c => c.Id, 0))
            .Generate(100);
        _dbContext.Products.AddRange(products);
        _dbContext.SaveChanges();
    }
}
```
14. Make this class a scoped service and inject an instance in the `Configure` method, finally call the `Seed` method
15. Check if Entity Framework mapped every entity correctly. If not, create or update the `IEntityTypeConfiguration` for that specific entity, create a new migration, make sure the server executed the new migration and check again. Repeat this step until the database model is correct.
    - The only optional fields are `Description` (`Product`) and `DeliveryDate` (`Order`)
    - The `Total` in `Order` should not be persisted
16. Implement a new `ProductService` which interacts with the database
17. Use this new service in the `Server` (instead of the `FakeProductService`)
18. Make sure everything works

## Solution

1. Add the following connection string to the `appsettings.Development.json` of the `Server` project

```{json}
"ConnectionStrings": {
  "SportStore": "Server=(localdb)\\mssqllocaldb;Database=SportStore;Trusted_Connection=True;"
}
```

2. Create a new Class Library in the `src` folder called `Persistence` and add it to the solution, remove the dummy `Class1.cs`

```
cd src
dotnet new classlib -o Persistence
cd ..
dotnet sln .\SportStore.sln add .\src\Persistence\Persistence.csproj
rm .\src\Persistence\Class1.cs
```

3. Add the following dependencies to the `Persistence` project:
    - Microsoft.EntityFrameworkCore
    - Microsoft.EntityFrameworkCore.Design
    - Microsoft.EntityFrameworkCore.Relational
    - Microsoft.EntityFrameworkCore.SqlServer

```
cd .\src\Persistence
dotnet add package Microsoft.EntityFrameworkCore --version 5.0.12
dotnet add package Microsoft.EntityFrameworkCore.Design --version 5.0.12
dotnet add package Microsoft.EntityFrameworkCore.Relational --version 5.0.12
dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 5.0.12
```

4. Add the following dependencies to the `Server` project:
    - System.Data.SqlClient
    - Microsoft.EntityFrameworkCore.SqlServer
    - Microsoft.EntityFrameworkCore.Tools

```
cd .\src\Persistence
dotnet add package System.Data.SqlClient
dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 5.0.12
dotnet add package Microsoft.EntityFrameworkCore.Tools --version 5.0.12
```

5. Add a reference from the `Server` to the `Persistence` project.

```
cd ..
dotnet add .\Server\Server.csproj reference .\Persistence\Persistence.csproj
```

6. Add a reference from the `Persistence` to the `Domain` project.

```
dotnet add .\Persistence\Persistence.csproj reference .\Domain\Domain.csproj
```

7. Add a reference from the `Services` to the `Persistence` project.

```
dotnet add .\Services\Services.csproj reference .\Persistence\Persistence.csproj
```

8. Create a `DbContext` named `SportStoreDbContext` in a `Data` folder in the `Persistence` project
    - This class only contains a constructor which calls the base class' constructor

9. Initialize a connection in the `StartUp` of the `Server`

10. Add a private default constructor for every class in the `Domain`. Note not every domain class should be persisted, how do you know?
    - Only classes that inherit from `Entity` are stored in the database
    - Therefor every `ValueObject` that is referenced by an `Entity` needs to stored in the database
    - Thus every class that meets one of these conditions needs a private constructor
11. Read through this documentation: [Persist value objects as owned entity types](https://docs.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/implement-value-objects#persist-value-objects-as-owned-entity-types-in-ef-core-20-and-later) and make every `ValueObject` an owned entity type.
    - Create seperate `IEntityConfiguration` subclasses per class that needs modification, see the [documentation](https://www.learnentityframeworkcore.com/configuration/fluent-api#separate-configuration-classes) for more info.
    - For readonly properties, you need to specify the property in the model builder or Entity Framework won't add it to the database. An alternative is to add a private setter but this is bad practice because it's not obvious why the setter is private and it's not clear at first sight which properties are stored in the database.
    - Set a precision of 12 and a scale of 10 for the `Value` of class `Money`

12. Add a `DbSet` for each entity that can be fetched

13. Create a migration name InitialCreate which contains all data to create the empty database
    - If the CLI tells you that the `ef` command does not exist, install the tool: `dotnet tool install --global dotnet-ef --version 5.0.12`

```
cd ..
dotnet ef migrations add InitialCreate --project .\src\Persistence\Persistence.csproj --startup-project .\src\Server\Server.csproj --context SportStoreDbContext
```

14. Create a class named `SportStoreDataInitializer` and paste the following code:

```{cs}
public class SportStoreDataInitializer
{
    private readonly SportStoreDbContext _dbContext;

    public SportStoreDataInitializer(SportStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void SeedData()
    {
        _dbContext.Database.EnsureDeleted();
        if (_dbContext.Database.EnsureCreated())
        {
            SeedProducts();
        }
    }

    private void SeedProducts()
    {
        var products = new ProductFaker()
            .RuleFor(p => p.Id, () => 0) // Remove the id, database column is auto generated
            .RuleFor(p => p.Category, new CategoryFaker().RuleFor(c => c.Id, 0))
            .Generate(100);
        _dbContext.Products.AddRange(products);
        _dbContext.SaveChanges();
    }
}
```

15. Make this class a scoped service and inject an instance in the `Configure` method, finally call the `Seed` method
16. Check if Entity Framework mapped every entity correctly. If not, create or update the `IEntityTypeConfiguration` for that specific entity, create a new migration, make sure the server executed the new migration and check again. Repeat this step until the database model is correct.
    - The only optional fields are `Description` (`Product`) and `DeliveryDate` (`Order`)
    - The `Total` in `Order` should not be persisted
17. Implement a new `ProductService` which interacts with the database
18. Use this new service in the `Server` (instead of the `FakeProductService`)
19. Make sure everything works
