
# dotnet-gvsa

Este pacote contém duas formas de gerar arquivos de feature automaticamente usando .NET:

## ✅ 1. Ferramenta CLI (`gvsa MeuVerticalSliceArchitecture`)

### Como instalar:

1. Navegue até a pasta que tem o csproject:

##### exemplo:
```bash
cd c:\..\..\WebApi
```

2. Execure o comando

### Como usar:

```bash
gvsa MeuVerticalSliceArchitecture
```

Isso criará automaticamente:

```
Feature/
|  MeuVerticalSliceArchitecture/
|  |  Commands/
|  |  |  foo.cs
|  |  |  ...
|  |  Endpoints/
|  |  |  foo.cs
|  |  |  ...
|  |  Queries/
|  |  |  foo.cs
|  |  |  ...
```