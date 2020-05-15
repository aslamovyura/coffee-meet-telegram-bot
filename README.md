# CoffeeMeetBot [Telegram Bot]

![.NET Core]()

The main idea of the application is to develop a telegram bot, which helps you to find a company for coffee. You can select a specific user (by typing @username or sharing a contact) or a random registered user ("/ random" command).

## Getting Started

To start interacting with the Telegram Bot, user must enter "/ start" command and follow the instructions.

## Application settings

For the correct functioning of Telegram Bot, it is necessary to update the [appsettnigs.json](https://github.com/aslamovyura/coffee-meet-telegram-bot/tree/master/src/Bot/appsettings.json) file in the root directory of the web project, filled in according to the template below.

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

For the bot to work correctly, a database is required. To add PortgreQSL database, you should run the following command:

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

- [ASP.NET Core 3.1](https://docs.microsoft.com/en-us/aspnet/core/);
- [Telegram Bot](https://www.nuget.org/packages/Telegram.Bot/);
- [Docker](https://www.docker.com/);
- [Heroku](https://heroku.com/)

## Author

[Yury Aslamov]();

## License

This project is under the MIT License - see the [LICENSE.md](https://github.com/aslamovyura/coffee-meet-telegram-bot/blob/master/LICENSE) file for details.
