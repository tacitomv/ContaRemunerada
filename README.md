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
[T�cito Viero](mailto:tacito.viero@gmail.com).

#Desafio

Implementar um sistema de controle de conta corrente banc�ria, processando solicita��es de dep�sito, resgates e pagamentos. Um ponto extra seria rentabilizar o dinheiro parado em conta de um dia para o outro como uma conta corrente remunerada.

##Como

Criar um app de uma �nica p�gina contendo informa��es do extrato da conta e os bot�es das a��es esperadas (dep�sitos, retiradas e pagamentos). Mostrar tamb�m o hist�rico da conta.

N�o precisa ser nada mega complexo e tampouco se preocupar com design e layout.

##Tecnologias e arquitetura

A tecnologia e arquitetura utilizadas podem ser escolhidas pelo participante, sendo recomendadas as possibilidades abaixo:

Usando C# + ASP.Net Core + MySQL
Arquitetura orientada a dom�nio: DDD

##O que ser� avaliado

Capacidade de �se virar� e �Get shit done�;
L�gica, organiza��o e clareza de c�digo;
ReadMe;
Arquitetura da solu��o;
Cobertura de testes;
Corre��o das informa��es financeiras.

##Interessante, mas n�o fundamental
Guardar os dados em um banco de dados usando um ORM.
Preocupa��o com seguran�a (apenas um teste);

##Entrega 
Mandar o link do reposit�rio com o c�digo.

##Links �teis:

https://www.devmedia.com.br/introducao-ao-ddd-em-net/32724

https://docs.microsoft.com/pt-br/aspnet/core/?view=aspnetcore-3.0

https://www.blbbrasil.com.br/blog/matematica-financeira-contabilidade/