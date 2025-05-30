using Shared.Entities;

namespace Shared.Queries;

public static class Query
{
    public static async Task GenerateQueries(QueryEntity queryEntity)
    {
        Console.WriteLine("Begin Query List task...");
        queryEntity.SetType("List");
        await GenereteCommand(queryEntity);
                    
        Console.WriteLine("Begin Query ById task...");
        queryEntity.SetType("ById");
        await  GenereteCommand(queryEntity);
    }

    private static async Task GenereteCommand(QueryEntity queryEntity)
    {
        string pathQueries = Path.Combine(queryEntity.PathMain, "Queries");
            
        Directory.CreateDirectory(pathQueries);
        
        string queries = "Queries";
        string conteudo = 
$@"using System;
using System.Threading;
using System.Threading.Tasks;
using MinDiator.Interfaces;
@fluentresultUsing
 
namespace {queryEntity.NamespaceBase}.Features.{queryEntity.Name}.{queries};

// Adcione sua propriedade e seu retorno        
public record struct Get{queryEntity.Name}{queryEntity.Type}Query(@listOrById) : IRequest<@fluentresultsReturn>;

// Injete seu reposistório
public class  Get{queryEntity.Name}{queryEntity.Type}QueryHandler(object repository) : IRequestHandler<Get{queryEntity.Name}{queryEntity.Type}Query, @fluentresultsReturn>
{{
    // Implemente sua lógica
    public async Task<@fluentresultsReturn> Handle(Get{queryEntity.Name}{queryEntity.Type}Query request, CancellationToken cancellationToken)
    {{
        @fluentresultHandle
    }}
}}
".Trim();
        conteudo = conteudo.Replace("@fluentresultUsing", queryEntity.FluentResult ? "using FluentResults;" : "");
        conteudo = conteudo.Replace("@fluentresultHandle", queryEntity.FluentResult ? "return Result.Ok();" : "throw new NotImplementedException();");
        conteudo = conteudo.Replace("@fluentresultsReturn", queryEntity.FluentResult ? "Result<List<Guid>>" : "Guid");
        conteudo = conteudo.Replace("@listOrById", queryEntity.Type.Equals("List", StringComparison.CurrentCultureIgnoreCase) ? "Result<List<Guid>>" : "Guid");


        string filePath = Path.Combine(queryEntity.PathMain, queries, $"Get{queryEntity.Name}{queryEntity.Type}QueryHandler.cs");
        await File.WriteAllTextAsync(filePath, conteudo);
        
        Console.WriteLine($"Feature '{queryEntity.Name}' criada em {filePath}");
    }
}