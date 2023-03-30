# Emerald Chameleon Chat

https://euphonious-malasada-9f25dd.netlify.app/

#
This is a backend starter template for our projects. This is a .NET 6 Web API project that utilizes Entity Framework and the repository pattern for our Data Access layer.

# Understanding The Folder Structure
The application is broken into a few main folders:

-DAL: This is the 'Data Access Layer', aka how we interact with the database. Inside here you will find a folder for DbContexts, which are how Entity Framework connects to the DB & a folder for our Repositories. Each Entity Model will have its own Repository for interacting with the database.
*Each class repository must extend the BaseRepository class & implement the IClassRepository interface
```
public class WeatherForecastRepository : BaseRepository<WeatherForecast>, IWeatherForecastRepository //this class EXTENDS our base repository & implements the repositories interface
```
*Each IClassRepository interface must implement the IRepository interface - which is what actually defines our 'Base Contract' for what our Repositories are supposed to do. The BaseRepository class actually implements this contract & gives us implementations for our basic CRUD operations. If we have an Entity Class that has something 'special' about it that we need to create a special repository method for, we would include it in the IClassRepository interface and then in the ClassRepository implementation itself.

-Controllers: This is where the Controllers will live. Currently can go ahead and implement business logic right in the controllers. This is not best practices, and I will get around to implementing the Mediator pattern to separate the Controller logic from the Business logic - but it all living in the Controllers will work for now.

-BLL: This folder holds the 'Business Logic' of the application. This is not fully implemented yet, stay tuned.

-AutoMapperProfiles: The project utilizes AutoMapper to help us map from our Entity models to our DTO's. When you create a new DTO you must update the AutoMapper profiles here, pretty self explanatory. The IExtension classes just give us nice .MapToDTO/.MapToExtentsion extension methods to use in our app.

-Migrations: This is where Entity Framework will chuck our migration files when generated.

-Model: This folder holds our Models. This is further broken up into two subfolders- one for DTO's and one for Entity classes. Entity classes are the model for our database, and DTO classes are what we pass to/from in the httprequests via the controllers. Should make a new DTO for GET's and CREATE's and UPDATE's for each Class - do not use Entity classes directly in the controllers for the request or response type.

# Working with Entity Framework

Creating Migrations:

A migration is needed every time you change one of the Entity models or add one. This is pretty simple to do via the Package Manager Console. From the Package Manager Console, simply run add-migration {MigrationName}
ex:
```
add-migration AddedNewModels
```

Once the migration has succesfully been created, update your database locally by running this command in the package manager console
```
update-database
```

TODO: Configuration files for dev/prod connection strings.

# Working in Client (Next JS Guidelines)

## Creating Components
components/newComponent - Valid name of folder
components/newComponent/NewComponent.tsx - Valid name of the component.
components/newComponent/NewComponent.module.scss - This is the 'local' styles sheet for this component.

## Creating Pages

app/ This is the directory for the index file.
app/page.tsx This is the 'Page'. In Next JS this is basically a component, that gets passed to the Layout.tsx file.
app/layout.tsx This is the layout of the page that is in the same base folder.
app/layout.tsx This is the Root Layout of the application. Anything you do here will be global across the app
example:
In app/layout.tsx if you import a component for making a cool background, it will persist across the whole application, and all layouts.

app/NewFolder This is a new page with route /NewFolder.
app/NewFolder/page.tsx This is the 'Page' that will populate the route.
app/NewFolder/layout.tsx This is the 'Local' layout for the page.

## Running Next JS

npm run dev

# TODO: Finish 'Working in Client (Next JS Guidelines) and make it not suck


