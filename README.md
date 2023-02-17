# :closed_lock_with_key: Auth API

Back-end crianda em net 6 responsavel por autenticar usuarios. Front no repo (Auth_Front)[https://github.com/MatheusPalinkas/Auth_Front].

---

## :memo: Pré-requisitos

- [.Net 6](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)
- [Visual Studio](https://visualstudio.microsoft.com/pt-br/vs/)
- [Sql server](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads)

---

## :hammer_and_wrench: Tecnologias utilizadas

- .Net 6
- Identity
- EntityFramework
- FluentValidation
- Modelagem DDD
- Migrations

---

## :walking: Passo a passo para executar o projeto

#### 1. clonar o repositório

```
git clone https://github.com/MatheusPalinkas/Auth_API
```

#### 2. Abrir solução no visual studio

#### 3. Atualizar as configs dentro do appsettings

Atualizar os parametros: DefaultConnection, UserName, Password

```json
{
  "ConnectionStrings": {
    "DefaultConnection": ""
  },
  ...
  "Email": {
    ...
    "UserName": "",
    "Password": ""
  },
  ...
}

```

#### 4. Executar as migrations

Executar o comando abaixo no Package Manager Console

```bash
Update-Database
```

### 5. Iniciar aplicação

### 6. Abrir o navegador

Abrir o navegador no endereço http://localhost:5250/swagger/index.html

---

### :thumbsup: Projeto publicado

**Observação: A api está publicada no microsoft azure, e o mesmo não libera envio de smtp.**
**Por isso a criação de conta e recupearação de senha não estão disparando email. Local funciona normalmente.**

[Link para o swagger do projeto no microsoft azure](https://webappoauthnet.azurewebsites.net/swagger/index.html)
