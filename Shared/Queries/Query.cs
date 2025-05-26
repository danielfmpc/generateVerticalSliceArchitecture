using Shared.Entities;

namespace Shared.Queries;

public static class Query
{
    public static void GenerateQueries(QueryEntity queryEntity)
    {
        Parallel.Invoke(
            () =>
            {
                Console.WriteLine("Begin Query List task...");
                queryEntity.SetType("List");
                GenereteCommand(queryEntity);
            },
            () =>{
                Console.WriteLine("Begin Query ById task...");
                queryEntity.SetType("ById");
                GenereteCommand(queryEntity);
            }
        );
    }

    private static void GenereteCommand(QueryEntity queryEntity)
    {
        string pathQueries = Path.Combine(queryEntity.PathMain, "Queries");
            
        Directory.CreateDirectory(pathQueries);
        
        string queries = "Queries";
        string conteudo = 
$@"using System;
using System.Threading;
using System.Threading.Tasks;
using MinDiator;
@fluentresultUsing
 
namespace {queryEntity.NamespaceBase}.Features.{queryEntity.Name}.{queries};

// Adcione sua propriedade e seu retorno        
public record struct Get{queryEntity.Name}{queryEntity.Type}Query(string props) : IRequest<Guid>;

// Injete seu reposistório
public class  Get{queryEntity.Name}{queryEntity.Type}QueryHandler(object repository) : IRequestHandler<Get{queryEntity.Name}{queryEntity.Type}Query, Guid>
{{
    // Implemente sua lógica
    public async Task<Guid> Handle(Get{queryEntity.Name}{queryEntity.Type}Query request, CancellationToken cancellationToken)
    {{
        @fluentresultHandle
    }}
}}
".Trim();
        conteudo = conteudo.Replace("@fluentresultUsing", queryEntity.FluentResult ? "using FluentResult;" : "");
        conteudo = conteudo.Replace("@fluentresultHandle", queryEntity.FluentResult ? "return Result.Ok();" : "throw new NotImplementedException();");


        string filePath = Path.Combine(queryEntity.PathMain, queries, $"Get{queryEntity.Name}{queryEntity.Type}QueryHandler.cs");
        File.WriteAllText(filePath, conteudo);
        
        Console.WriteLine($"Feature '{queryEntity.Name}' criada em {filePath}");
    }
}