## Used technologies and packages ##

    * Entity Framework Core
    * ASP .NET Core 3.1
    * Microsoft.AspNetCore.Authentication.JwtBearer 
    * Microsoft.AspNetCore.Diagnostics.HealthChecks 
    * Microsoft.AspNetCore.Mvc.NewtonsoftJson
    * Microsoft.Extensions.DependencyInjection
    * Microsoft.Extensions.Options.ConfigurationExtensions
    * xunit
    * Moq

## Used design patterns ##

    * Repository pattern
    * Factory / Dependency injection
    * Facade pattern
    * Singleton pattern via Dependency injection

The Mediator pattern is applicable in this case, but it will add complexity to the maintainability of the project.

## Project Setup ##

The Database will be automatically initialised on the `(localdb)\\mssqllocaldb` server. The name of the Database is `TitanGateWebsiteApi`.
The image upload directories should be changed based on your preference, they are located in `appsettings.json` for the web api and in the `tets_settings.json` for the test project.

## Postman ##

You can locate the postman collection on the project root level. It includes all endpoints of the Client Api and Websites Api.
There is a global variable `accessToken` that needs to be created in postman globals. It is used to set the token in the authorization header for Websites Api endpoints.

## Custom Header ##

There is a required custom header `X-Correlation-Token` for each endpoint except the `/health`. It's value is saved in the context TraceIdentifier property, which is represented in the global request/reponse log.

## Login ##

In order to be able to login you should create a client via `Create Client` endpoint, which is configured to be accessible only from localhost. Then you should call the `Get Access Token` endpoint with the newly created client.

## Categories ##

The Categories are predefined in the Database and they are as follows:

    `eCommerce` 
    `Business` 
    `Entertainment`
    `Portfolio` 
    `Media` 
    `Brochure` 
    `Nonprofit` 
    `Educational` 
    `Infopreneur` 
    `Community Forum` 
    `Personal` 

New api should be implemented to be able to modify the records in the Database.

## Homepage Snapshots ##

The Websites Api is a pure JSON Api. The images are send like a Base64 string. Currently the Api is implemented to work only with the image the payload can be changed in order to be able to send the metadata of the image. Alternatively the upload of the image can be done in a separete call accepting multipart form data.

After receiving the image the business layer creates it on the file system and returns the path to it, so that can be saved in the Database.

