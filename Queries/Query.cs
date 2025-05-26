using System;
using System.IO;
using System.Threading.Tasks;

namespace GenerateVerticalSliceArchitecture.Queries;

public static class Query
{
    public static void GenerateQueries(string namespaceBase, string name, string pathMain)
    {
        Parallel.Invoke(
            () =>
            {
                Console.WriteLine("Begin first task...");
                GenereteCommand(namespaceBase, name, pathMain, "List");
            },
            () =>{
                Console.WriteLine("Begin first task...");
                GenereteCommand(namespaceBase, name, pathMain, "ById");
            }
        );
    }

    private static void GenereteCommand(string namespaceBase, string name, string pathMain, string type)
    {
        string pathQueries = Path.Combine(pathMain, "Queries");
            
        Directory.CreateDirectory(pathQueries);
        
        string queries = "Queries";
        string conteudo = 
$@"using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
 
namespace {namespaceBase}.Features.{name}.{queries};

// Adcione sua propriedade e seu retorno        
public record struct Get{name}{type}Query(string props) : IRequest<Guid>;

// Injete seu reposistório
public class  Get{name}{type}QueryHandler(object repository) : IRequestHandler<Get{name}{type}Query, Guid>
{{
    // Implemente sua lógica
    public async Task<Guid> Handle(Get{name}{type}Query request, CancellationToken cancellationToken)
    {{
        throw new NotImplementedException();
    }}
}}
".Trim();
        
        string filePath = Path.Combine(pathMain, queries, $"Get{name}{type}QueryHandler.cs");
        File.WriteAllText(filePath, conteudo);
        
        Console.WriteLine($"Feature '{name}' criada em {filePath}");
    }
}