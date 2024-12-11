# Golden Raspberry Awards API

Esta é uma API RESTful para leitura da lista de indicados e vencedores da categoria Pior Filme do Golden Raspberry Awards.

## Requisitos do Sistema

1. Ler o arquivo CSV dos filmes e inserir os dados em uma base de dados ao iniciar a aplicação.

## Tecnologias Utilizadas

- .NET Core 8
- Entity Framework Core
- Banco de dados Sqlite
- CsvHelper

## Estrutura do Projeto

O projeto está estruturado da seguinte forma:

- **MapEndPoints**: Contém os endpoints da API.
- **Application**: Contém a lógica de negócio da aplicação.
- **Domains**: Contém as entidades do domínio.
- **Infrastructure**: Contém o contexto do banco de dados e o contexto do CSV.
- **Tests**: Contém os testes de integração.

## Instruções para execução

1. Clone o repositório para sua máquina local.
   bash
   git clone https://github.com/fabiofeti/goldenRaspberryAwards
   cd goldenRaspberryAwards\src\FFeti.GoldenRaspberryAwards.Api
   dotnet run
2. Acesse o Swagger em http://localhost:5294/swagger/index.html
 ou execute diretamente o Curl abaixo:
   bash
   curl -X 'GET' \
  'http://localhost:5294/api/v1/movies/awards-winners' \
  -H 'accept: */*