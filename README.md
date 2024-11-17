# Resumo sobre a EcoWatt
A EcoWatt é uma solução inovadora de automação e monitoramento energético que
transforma residências em ambientes inteligentes, sustentáveis e econômicos. Com
uma central de controle conectada a sensores distribuídos por toda a casa, a EcoWatt
monitora e gerencia o consumo de energia em tempo real, priorizando o uso de fontes
renováveis como solar e eólica. O sistema armazena a energia gerada, analisa as
condições climáticas para prever a produção e alterna automaticamente para a rede
elétrica quando necessário. Através de um aplicativo intuitivo, o usuário pode visualizar
o consumo detalhado de cada dispositivo, configurar preferências de uso e acessar
relatórios completos que ajudam a economizar e otimizar o uso de energia.

Combinando conveniência e sustentabilidade, a EcoWatt vai além do simples
monitoramento. Nosso sistema inclui um módulo exclusivo para carregamento veicular,
garantindo que veículos elétricos sejam carregados de forma eficiente e sustentável.
Nossa missão é oferecer uma solução prática que reduz os custos e o impacto
ambiental, preparando residências para os desafios energéticos do futuro. A EcoWatt é
mais do que um produto: é uma ferramenta poderosa para quem deseja ter controle
total sobre o consumo de energia, contribuindo para um mundo mais sustentável


# EcoWatt – Transforme sua Casa em um Ambiente

Inteligente e Sustentável
Bem-vindo à EcoWatt! Em um cenário onde o consumo de energia cresce, os custos
aumentam e a sustentabilidade é uma necessidade urgente, a EcoWatt traz uma
solução inovadora para o futuro da energia residencial. Imagine uma casa capaz de
gerar e gerenciar sua própria energia de forma inteligente e econômica – essa é a visão
da EcoWatt.


- O Que é a EcoWatt?
  A EcoWatt é uma solução completa de automação e monitoramento energético que
  transforma sua residência em um ambiente inteligente e eficiente. Com um sistema
  integrado que monitora, gerencia e otimiza o uso de fontes de energia – solar, eólica e
  rede elétrica convencional – a EcoWatt conecta sua casa a uma central de controle que
  prioriza o uso de energias renováveis, reduzindo a dependência da rede elétrica e
  trazendo economia para você.


- Funcionalidades e Diferenciais
  A EcoWatt vai além do monitoramento básico. Nosso sistema armazena a energia
  gerada e usa previsões climáticas para otimizar o uso das fontes renováveis. Quando
  necessário, ele alterna automaticamente para a rede elétrica, garantindo um
  fornecimento contínuo sem necessidade de intervenção do usuário. Tudo isso é
  facilmente acessado através de um aplicativo intuitivo, onde você acompanha o
  consumo em tempo real, configura prioridades de energia e visualiza relatórios
  detalhados. Para proprietários de veículos elétricos, a EcoWatt inclui um módulo de
  carregamento sustentável e eficiente.
  Com a EcoWatt, você assume o controle total sobre o consumo de energia da sua casa,
  economiza e contribui para um futuro mais sustentável.


- Junte-se à Revolução Energética
  Na EcoWatt, nossa missão é promover a inovação, a sustentabilidade e a qualidade de
  vida. Oferecemos uma solução prática para os desafios energéticos atuais, preparando
  sua residência para o futuro. Transforme sua casa em um ambiente inteligente e
  sustentável com a EcoWatt. Acesse nossa landing page para saber mais e descubra
  como podemos ajudar você a economizar e a preservar o meio ambiente.

EcoWatt – juntos, construindo um futuro mais sustentável.


# Documentação ECOWATT API .NET
API desenvolvida para a entrega da disciplina ADVANCED BUSINESS DEVELOPMENT WITH .NET para a Global Solution

Disciplina: 
ADVANCED BUSINESS DEVELOPMENT WITH .NET

Professor: 
THIAGO KELLER

Instruções de uso: Trocar ConnectionStrings no arquivo appsettings.json ou abrir o cmd e rodar os seguintes comandos: <br>
setx ORACLE_CONNECTION_STRING "Data Source=oracle.fiap.com.br:1521/orcl;User ID=XXXXXXXX;Password=XXXXXX;" <br><br>
dotnet ef database update --context FIAPDBContext --project "caminho/projeto/EcoWatt.Database" --startup-project "caminho/projeto/EcoWatt.API"



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

---

## Equipe

Apresentação dos integrantes da equipe de desenvolvimento e suas responsabilidades no projeto:

- 2TDSPM Gabriel Sampaio RM 552342 - Banco de dados / C#
- 2TDSPM Gabriel Neves RM 552244 - React Native (Front End)
- 2TDSPM Livia Freitas RM 99892 - API Java
- 2TDSAV Rafael Mendonça RM 552422 - IA
- 2TDSPM Renato Romeu RM 551325 - DevOps, QA, UX, Business
  
---

1. Repository Pattern
O padrão Repository foi aplicado para abstrair as operações de acesso aos dados.

Implementação:
IRepository<T>: Interface genérica para operações CRUD.
UsuarioRepository e BateriaRepository: Implementações específicas para as entidades.
<br>
2. Dependency Injection (DI)
O padrão de Injeção de Dependência foi utilizado para gerenciar as dependências entre as classes.

Implementação:
Serviços e repositórios são registrados no contêiner de DI e injetados nos controllers e outros serviços.
<br>
3. DTO (Data Transfer Object)
O padrão DTO foi aplicado para transferir dados entre a camada de API e as camadas internas da aplicação.

Implementação:
UsuarioRequest e UsuarioResponse: Modelos para criar ou retornar dados de usuários.
UsuarioUseRequest: Modelo para salvar usos de baterias.
<br>
4. Factory Pattern (parcial)
Utilizado no contexto do RecommendationEngine para criar modelos de recomendação.

Implementação:
A lógica de treinamento e previsão é encapsulada no RecommendationEngine, permitindo a criação de modelos com base nos dados fornecidos.
<br>
5. Clean Architecture (inspirado)
A API segue princípios da Clean Architecture, como separação de responsabilidades entre camadas e baixo acoplamento.

Camadas Principais:
API (Apresentação): Controladores para interação com os clientes.
Serviços (Domínio): Regras de negócio e lógica de aplicação.
Repositórios (Infraestrutura): Comunicação com o banco de dados.

