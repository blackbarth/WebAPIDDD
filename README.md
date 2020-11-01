# WebAPIDDD

```
dotnet new sln -n nomeprojeto
```

```
dotnet new webapi -n Application -f netcoreapp2.1 -o API.Application --no-https
```

```
dotnet sln add API.Application
```

```
dotnet new webapi -n Application -f netcoreapp2.1 -o API.Application --no-https
```

```
dotnet sln add API.Application
```

## Criar domain

```
dotnet new classlib -n Domain -f netcoreapp2.1 -o API.Domain
```

```
dotnet sln add API.Domain
```

## Criar CrossCutting

```
dotnet new classlib -n CrossCutting -f netcoreapp2.1 -o API.CrossCutting
```

```
dotnet sln add API.CrossCutting
```

## Criar Data

```
dotnet new classlib -n Data -f netcoreapp2.1 -o API.Data
```


dotnet sln add API.Data


dotnet new classlib -n Service -f netcoreapp2.1 -o API.Service
dotnet sln add API.Service


instalar entityframeworkcore / Na camada Data
dotnet add package Microsoft.EntityFrameworkCore.Tools --version 2.1.14
dotnet add package Microsoft.EntityFrameworkCore.Design --version 2.1.14

dotnet add package Microsoft.EntityFrameworkCore --version 2.1.14

driver mysql
dotnet add package Pomelo.EntityFrameworkCore.MySql --version 2.1.4


criar referencia de um projeto no outro
 dotnet add .\API.Data\ reference .\API.Domain\


 dotnet tool install --global dotnet-ef --version 3.1.0

 exibe a versao do framework
 dotnet ef --version
 dotnet ef migrations

dotnet ef migrations  add Fist_migration
dotnet ef database update


 dotnet add .\API.Service\ reference .\API.Domain\
 dotnet add .\API.Service\ reference .\API.Data\  

 

 dotnet add .\API.Application\ reference .\API.Domain\
 dotnet add .\API.Application\ reference .\API.Service\
 dotnet add .\API.Application\ reference .\API.CrossCutting\

 dotnet add .\API.CrossCutting\ reference .\API.Domain\
  dotnet add .\API.CrossCutting\ reference .\API.Service\
   dotnet add .\API.CrossCutting\ reference .\API.Data\


cd api.crosscutting
dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection --version 7.0.0




  <PackageReference Include="Microsoft.EntityFrameworkCore" Version="2.1.14" />
- Target netstandard2.1



instalar swagger
cd api.application
dotnet add package Swashbuckle.AspNetCore --version 4.0.1

dotnet add package Swashbuckle.AspNetCore.Swagger --version 4.0.1


instalar entityframeworkcore para sqlserver
dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 2.2.6
dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 2.2.6

trocado as configuracoes de conexao
dotnet ef database update

implementando jwt
nuget Identity.Model.Tokens.Jwt
cd domain
dotnet add package System.IdentityModel.Tokens.Jwt --version 5.6.0


Instalar automapper 
croscutting
service
dotnet add package AutoMapper --version 9.0.0

outra assinatura
        //public async Task<ActionResult> GetAll([FromServices] IUserService service)
		
lista os sdks instalados		
dotnet --list-sdks

restaura todas as bibliotecas
dotnet restore


migrar framework
dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer --version 3.1.0		


instalar json
dotnet add package Microsoft.AspNetCore.Mvc.NewtonsoftJson --version 3.1.0