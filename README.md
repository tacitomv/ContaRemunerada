# ContaRemunerada

## Project setup
In `/clientapp`, run:
```
npm install OR yarn
```
Run the dotnet app as you like.

In `Visual Studio`, click `Run`.

In `VS Code` or in the command line, run `dotnet run`.

## Deploy

Deployed to an Elastic Beanstalk (ELB) instance using AWS Toolkit on Visual Studio.

For deploying in different places, as barebones configurations, use `dotnet publish`.

## Database
An Amazon AWS limited RDS is running for this application, with a limited user. You can configure both at appsettings.json.

### Migrations
This project use Entity Framework Migrations to keep consistency between the domain objects and the database. To work with it you should install the `dotnet ef` tooling.

## Mentions
Vue JS + Asp.net Core 3.1 template by [Alexandre Malavasi](https://www.linkedin.com/in/alexandremalavasi/).

## Autor
[Tácito Viero](mailto:tacito.viero@gmail.com).