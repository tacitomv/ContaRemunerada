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

# Desafio

Implementar um sistema de controle de conta corrente bancária, processando solicitações de depósito, resgates e pagamentos. Um ponto extra seria rentabilizar o dinheiro parado em conta de um dia para o outro como uma conta corrente remunerada.

## Como

Criar um app de uma única página contendo informações do extrato da conta e os botões das ações esperadas (depósitos, retiradas e pagamentos). Mostrar também o histórico da conta.

Não precisa ser nada mega complexo e tampouco se preocupar com design e layout.

## Tecnologias e arquitetura

A tecnologia e arquitetura utilizadas podem ser escolhidas pelo participante, sendo recomendadas as possibilidades abaixo:

Usando C# + ASP.Net Core + MySQL
Arquitetura orientada a domínio: DDD

## O que será avaliado

Capacidade de “se virar” e “Get shit done”;
Lógica, organização e clareza de código;
ReadMe;
Arquitetura da solução;
Cobertura de testes;
Correção das informações financeiras.

## Interessante, mas não fundamental
Guardar os dados em um banco de dados usando um ORM.
Preocupação com segurança (apenas um teste);

## Entrega 
Mandar o link do repositório com o código.

## Links úteis:

https://www.devmedia.com.br/introducao-ao-ddd-em-net/32724

https://docs.microsoft.com/pt-br/aspnet/core/?view=aspnetcore-3.0

https://www.blbbrasil.com.br/blog/matematica-financeira-contabilidade/
