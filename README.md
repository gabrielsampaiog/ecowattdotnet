
Disciplina: 
ADVANCED BUSINESS DEVELOPMENT WITH .NET

Professor: 
THIAGO KELLER

# EcoWattdotnet
API desenvolvida para a entrega da disciplina ADVANCED BUSINESS DEVELOPMENT WITH .NET para a Global Solution

UPDATE BANCO
dotnet ef database update --context FIAPDBContext --project "caminho/projeto/EcoWatt.Database" --startup-project "caminho/projeto/EcoWatt.API"

Instruções de uso: Trocar ConnectionStrings no arquivo appsettings.json ou abrir o cmd e rodar os seguintes comandos: <br>
setx ORACLE_CONNECTION_STRING "Data Source=oracle.fiap.com.br:1521/orcl;User ID=XXXXXXXX;Password=XXXXXX;" <br>


```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "Swagger": {
    "Title": "EcoWatt API",
    "Description": "API para cadastrar usuários e baterias",
    "Email": "rm552342@fiap.com.br",
    "Name": "Gabriel Sampaio"
  },
  "ConnectionStrings": {
    "OracleFIAP": ""
  }
}

```
---

