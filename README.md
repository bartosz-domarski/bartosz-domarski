# Contacts Application
## RESTful Web API .NET 6.0 + EF 7 + Postgresql 15 + Angular 15 SPA
### Description:

#### The ContactController is responsible for managing contact data. It contains five methods:

- **GetAll** - returns a list of all contacts as a list of ContactDto.
- **GetById** - returns a contact with the specified id as a ContactDto.
- **Create** - creates a new contact based on the passed CreateContactDto object and returns a 201 code with the location of the created resource.
- **Update** - updates an existing contact with the specified id based on the passed UpdateContactDto object and returns a 200 code.
- **Delete** - deletes an existing contact with the specified id.

#### The AccountController is responsible for user login and registration. It contains two methods:

- **Register** - creates a new user based on the passed RegisterUserDto object and returns a 200 code.
- **Login** - logs in a user based on the passed LoginDto object and returns a JWT token as a JSON object.

## Libraries used:

- FluentValidation
- FluentValidation.AspNetCore
- Microsoft.AspNetCore.Identity
- Microsoft.EntityFrameworkCore
- Microsoft.IdentityModel.Tokens
- Microsoft.AspNetCore.Authentication.JwtBearer
- Microsoft.AspNetCore.Mvc.NewtonsoftJson
- Microsoft.EntityFrameworkCore.Design
- Microsoft.EntityFrameworkCore.Tools
- Microsoft.Extensions.Configuration.Binder
- Microsoft.Extensions.Configuration.FileExtensions
- Microsoft.Extensions.Configuration.Json
- Microsoft.VisualStudio.Web.CodeGeneration.Design
- Npgsql.EntityFrameworkCore.PostgreSQL
- Swashbuckle.AspNetCore

#### The API runs at the address: https://localhost:7209. The SPA runs at the address: http://localhost:4200.



-------------------------------------------------------------------------------------------------------------------------------------------------------



##### The vBetFinder.com project is private and the source code is not available for viewing. Below is a description of how the software works and the libraries used in it.

## vBetFinder.com - Scanner for valuebets, surebets, and system bets for over 40 bookmakers worldwide on Discord.
### This scanner searches for:

- **Valuebets** - bets for which the expected value of the winnings is greater than 1, which means that they are profitable in the long run.
- **Surebets** - bets that are always profitable regardless of the outcome.
- **System bets** - bets based on specific betting systems.

#### The scanner has been programmed to meet strict mathematical requirements so that every given bet is profitable. The software is constantly being improved and new features are being added.


<img src="https://github.com/bartosz-domarski/vbetfinder.com/blob/main/img/vbf1.jpg" alt= “vbf1” width="500" height="333">            <img src="https://github.com/bartosz-domarski/vbetfinder.com/blob/main/img/vbf2.jpg" alt= “vbf2” width="500" height="333">
<img src="https://github.com/bartosz-domarski/vbetfinder.com/blob/main/img/vbf3.jpg" alt= “vbf3” width="500" height="333">            <img src="https://github.com/bartosz-domarski/vbetfinder.com/blob/main/img/vbf4.jpg" alt= “vbf4” width="500" height="333">

#### You can find the Discord server of the scanner at this link, but remember that you need the appropriate role to access it and view the bets: https://discord.gg/BqACsEtPxh

#### Technologies and libraries used in the project:
- C# / .NET
- Python
- Selenium
- Scrapy
- Watchdog
- Schedule
- Concurrent Futures
- JSON
- LINQ
- Discord

#### There are plans to create a website under the titular domain and a dedicated mobile application.
