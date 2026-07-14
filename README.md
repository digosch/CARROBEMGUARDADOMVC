# Estacionamento - Carro Bem Guardado

Sistema de controle de estacionamento (entrada, saída e cálculo de valor por tempo de permanência).

## Tecnologias

- C#
- ASP.NET MVC
- ADO
- SQL Server
- Bootstrap 3

## Como rodar

1. Clone o repo e entre na pasta
2. Rode o `script-sql-server-estacionamento.sql` no seu SQL Server
3. Ajusta a connection string no `appsettings.json`
4. `dotnet restore`
5. `dotnet run`
6. Testes: `dotnet test`

## .gitignore
## .NET
bin/
obj/
*.user

## Visual Studio
.vs/

## Rider / VS Code
.idea/
*.suo

## appsettings com segredos locais
appsettings.Development.json

> This is a challenge by [Coodesh](https://coodesh.com/)>
> 
https://github.com/digosch/CARROBEMGUARDADOMVC
