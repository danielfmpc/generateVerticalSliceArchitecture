using System;
using System.IO;
using System.Threading.Tasks;

namespace GenerateVerticalSliceArchitecture.Commands;

public static class Command
{
    public static void GenerateCommand(string namespaceBase, string name, string pathMain)
    {
        string pathCommand = Path.Combine(pathMain, "Commands");
            
        Directory.CreateDirectory(pathCommand);
        Parallel.Invoke(
            () =>
            {
                Console.WriteLine("Begin first task...");
                GenereteCommand(namespaceBase, name, pathMain, "Create");
            },
            () =>{
                Console.WriteLine("Begin first task...");
                GenereteCommand(namespaceBase, name, pathMain, "Update");
            },
            () =>{
                Console.WriteLine("Begin first task...");
                GenereteCommand(namespaceBase, name, pathMain, "Delete");
            }
        );
    }

    private static void GenereteCommand(string namespaceBase, string name, string pathMain, string type)
    {
        string pathCommand = "Commands";
        string conteudo = 
$@"using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace {namespaceBase}.Features.{name}.{pathCommand};

// Adcione sua propriedade e seu retorno        
public record struct {type}{name}Command(string props) : IRequest<Guid>;

// Injete seu reposistório
public class  {type}{name}CommandHandler(object repository) : IRequestHandler<{type}{name}Command, Guid>
{{
    // Implemente sua lógica
    public async Task<Guid> Handle({type}{name}Command request, CancellationToken cancellationToken)
    {{
        throw new NotImplementedException();
    }}
}}
".Trim();
        
        string filePath = Path.Combine(pathMain, pathCommand, $"{type}{name}CommandHandler.cs");
        File.WriteAllText(filePath, conteudo);
        
        Console.WriteLine($"Feature '{name}' criada em {filePath}");
    }
}