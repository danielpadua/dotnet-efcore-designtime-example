# Objetivo

Este repositório é um exemplo de como implementar uma DesignTimeDbContextFactory genérica e multi-ambiente usando .Net Core e EfCore + Migrations

Para ler o guia completo acesse: https://medium.com/@danielpadua/efcore-implementando-um-designtimedbcontextfactory-multi-ambiente-7f384d62cf2

# Como executar

1. Clonar este projeto: `git clone https://github.com/danielpadua/dotnet-efcore-designtime-example.git`;
2. Navegar até o diretório: `cd dotnet-efcore-designtime-example/src/DesignTimeExample`;
3. Atualizar o banco de dados SQLite com a migration atual: `dotnet ef database update`;
4. Executar o projeto: `dotnet run`.
