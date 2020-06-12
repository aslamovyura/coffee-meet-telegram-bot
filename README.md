# CoffeeMeetBot [Telegram Bot]

![.NET Core](https://github.com/aslamovyura/coffee-meet-telegram-bot/workflows/.NET%20Core/badge.svg)

The main idea of the application is to develop a telegram bot, which helps you to find a company for coffee. You can select a specific user (by typing @username or sharing a contact) or a random registered user ("/random" command).

## Getting Started

To start interacting with Telegram Bot, enter "/start" command and follow the instructions.

## Application settings

For the correct functioning of Telegram Bot, it is necessary to update the [appsettings.json](https://github.com/aslamovyura/coffee-meet-telegram-bot/tree/master/src/Bot/appsettings.json) in the project root directory according to the template below:

```
{
  "ConnectionStrings": {
      "DefaultConnection": "Server=databaseServer;Port=5432;Database=databaseName;User Id=userName; Password=userPassword;"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "AllowedHosts": "*",
  "Url": "https://your-url-app.herokuapp.com",
  "Token": "your-telegram-token"
}
```

## Add Heroku PorgreSQL database 

For the bot correct work, a database is required. To add PortgreQSL database on [Heroku](https://heroku.com/), run the following command:

```
heroku addons:create heroku-postgresql:hobby-dev
```

## Deployment Docker container on Heroku

To start the entire infrastructure, you should run the following commands from the project folder:

```
docker build -t coffee-meet-bot .
docker tag coffee-meet-bot registry.heroku.com/coffee-meet-bot/web
heroku container:push web -a coffee-meet-bot
heroku container:release web -a coffee-meet-bot
```

## Built with

- [ASP.NET Core 3.1](https://docs.microsoft.com/en-us/aspnet/core/)
- [Telegram Bot](https://www.nuget.org/packages/Telegram.Bot/)
- [Docker](https://www.docker.com/)
- [Heroku](https://heroku.com/)
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)

## Author

[Yury Aslamov](https://aslamovyura.github.io/)

## License

This project is under the MIT License - see the [LICENSE.md](https://github.com/aslamovyura/coffee-meet-telegram-bot/blob/master/LICENSE) file for details.
