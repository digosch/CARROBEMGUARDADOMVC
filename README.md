# Estacionamento - Carro Bem Guardado

Sistema de controle de estacionamento (entrada, saída e cálculo de valor por tempo de permanência).

## Tecnologias

- C#
- ASP.NET Core MVC
- Entity Framework Core
- SQL Server
- Bootstrap 5

## Como rodar

1. Clone o repo e entre na pasta
2. Rode o `script-sql-server-estacionamento.sql` no seu SQL Server (cria o banco e já popula com dados de teste)
3. Ajusta a connection string no `appsettings.json`
4. `dotnet restore`
5. `dotnet run`
6. Testes: `dotnet test`

## .gitignore

[.gitignore](https://www.toptal.com/developers/gitignore)

---

> This is a challenge by [Coodesh](https://coodesh.com/)
