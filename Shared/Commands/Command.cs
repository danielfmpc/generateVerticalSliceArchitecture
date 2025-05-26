using Shared.Entities;

namespace Shared.Commands;

public static class Command
{
    public static void GenerateCommand(CommandEntity commandEntity)
    {
        string pathCommand = Path.Combine(commandEntity.PathMain, "Commands");
            
        Directory.CreateDirectory(pathCommand);
        Parallel.Invoke(
            () =>
            {
                Console.WriteLine("Begin Create task...");
                commandEntity.SetType("Create");
                GenereteCommand(commandEntity);
            },
            () =>{
                Console.WriteLine("Begin Update task...");
                commandEntity.SetType("Update");
                GenereteCommand(commandEntity);
            },
            () =>{
                Console.WriteLine("Begin Delete task...");
                commandEntity.SetType("Delete");
                GenereteCommand(commandEntity);
            }
        );
    }

    private static void GenereteCommand(CommandEntity commandEntity)
    {
        string pathCommand = "Commands";
        string conteudo = 
$@"using System;
using System.Threading;
using System.Threading.Tasks;
using MinDiator;
@fluentresultUsing

namespace {commandEntity.NamespaceBase}.Features.{commandEntity.Name}.{pathCommand};

// Adcione sua propriedade e seu retorno        
public record struct {commandEntity.Type}{commandEntity.Name}Command(string props) : IRequest<Guid>;

// Injete seu reposistório
public class  {commandEntity.Type}{commandEntity.Name}CommandHandler(object repository) : IRequestHandler<{commandEntity.Type}{commandEntity.Name}Command, Guid>
{{
    // Implemente sua lógica
    public async Task<Guid> Handle({commandEntity.Type}{commandEntity.Name}Command request, CancellationToken cancellationToken)
    {{
        @fluentresultHandle
    }}
}}
".Trim();
        
        conteudo = conteudo.Replace("@fluentresultUsing", commandEntity.FluentResult ? "using FluentResult;" : "");
        conteudo = conteudo.Replace("@fluentresultHandle", commandEntity.FluentResult ? "return Result.Ok();" : "throw new NotImplementedException();");

        
        string filePath = Path.Combine(commandEntity.PathMain, pathCommand, $"{commandEntity.Type}{commandEntity.Name}CommandHandler.cs");
        File.WriteAllText(filePath, conteudo);
        
        Console.WriteLine($"Feature '{commandEntity.Name}' criada em {filePath}");
    }
}