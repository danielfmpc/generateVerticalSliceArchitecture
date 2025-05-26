[![.NET Publish to NuGet](https://github.com/danielfmpc/generateVerticalSliceArchitecture/actions/workflows/main.yml/badge.svg)](https://github.com/danielfmpc/generateVerticalSliceArchitecture/actions/workflows/main.yml)

# 🧠 Ferramenta CLI Generate Vertical Slice Architecture

**Generate Vertical Slice Architecture** é uma biblioteca **leve**, **moderna** e **sem dependências externas** que cria estrutura a Feature da arquitetura **Vertical Slice**.

---

## 🚀 Por que Generate Vertical Slice Architecture?

- ✅ **Zero dependências externas**
- 🧱 **Pensado para CQRS** – separa Endpoints, Commands e Queries
- 🧩 **Ideal para Vertical Slice Architecture** – organização de features por funcionalidade
- 🧰 **Fácil de executar**

---

## 📦 Instalação Global

```bash
dotnet tool install --global GenerateVerticalSliceArchitecture --version 1.0.0
```

## 📦 Instalação Local

```bash
dotnet tool install --local GenerateVerticalSliceArchitecture --version 1.0.0
```

---

## 1. Navegue até a pasta que tem o csproject:

##### exemplo:
```bash
cd c:\..\..\WebApi
```

## 2. Execure o comando

### Como usar:

```bash
gvsa MeuVerticalSliceArchitecture
```


---

### 2. **Exemplo simples**

```csharp
public record struct  MeuVerticalSliceArchitectureCommand(string props) : IRequest<Guid>;


public class MeuVerticalSliceArchitectureCommandHandler : IRequestHandler<MeuVerticalSliceArchitectureCommand, Guid>
{
    public async Task<Guid> Handle(MeuVerticalSliceArchitectureCommand request, CancellationToken cancellationToken = default)
    {
        throw;
    }
}
```

---

### 📁 Exemplo de organização com Vertical Slice

```
Features/
└── Usuarios/
    ├── Commands/
    │   ├── CreateUsuarioCommandHandler.cs
    │   ├── UpdateUsuarioCommandHandler.cs
    │   └── DeleteUsuarioCommandHandler.cs
    ├── Endpoints/
    │   ├── UsuarioEndpoint.cs
    └── Queries/
        ├── GetUsuarioByIdQuery.cs
        ├── GetUsuarioListQuery.cs
```

---


## ✅ Roadmap

- [x] Criação do VSA generica no ASP.NET Core
- [ ] Criação do VSA com FluentResult no ASP.NET Core
- [ ] Criação do VSA com FluentResult com EF
  - [ ] Criar context
  - [ ] Criar repository
  - [ ] Injetar repository no handler
- [ ] Criação do VSA com FluentResult com Dapper
    - [ ] Criar context
    - [ ] Criar repository
    - [ ] Injetar repository no handler
---

## 🤝 Contribuições

Pull requests são super bem-vindos. Se quiser contribuir com uma ideia, melhoria ou bugfix, fique à vontade para abrir uma issue ou PR.

---

## 📄 Licença

MIT © [danielfmpc](https://github.com/danielfmpc)
