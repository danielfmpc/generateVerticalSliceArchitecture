using Shared.Entities;

namespace Shared.Commands;

public static class Command
{
    public static async Task GenerateCommand(CommandEntity commandEntity)
    {
        string pathCommand = Path.Combine(commandEntity.PathMain, "Commands");
            
        Directory.CreateDirectory(pathCommand);
        
        commandEntity.SetType("Create");
        Console.WriteLine($"Begin {commandEntity.Type} task...");
        await GenerateFileCommand(commandEntity);
        
        commandEntity.SetType("Update");
        Console.WriteLine($"Begin {commandEntity.Type} task...");
        await GenerateFileCommand(commandEntity);
        
        commandEntity.SetType("Delete");
        Console.WriteLine($"Begin {commandEntity.Type} task...");
        await GenerateFileCommand(commandEntity);
    }

    private static async Task GenerateFileCommand(CommandEntity commandEntity)
    {
        string pathCommand = "Commands";
        string conteudo = 
            $@"using System;
using System.Threading;
using System.Threading.Tasks;
using MinDiator.Interfaces;
@fluentresultsUsing

namespace {commandEntity.NamespaceBase}.Features.{commandEntity.Name}.{pathCommand};

// Adcione sua propriedade e seu retorno        
public record struct {commandEntity.Type}{commandEntity.Name}Command(string props) : IRequest<@fluentresultsReturn>;

// Injete seu reposistório
public class  {commandEntity.Type}{commandEntity.Name}CommandHandler(object repository) : IRequestHandler<{commandEntity.Type}{commandEntity.Name}Command, @fluentresultsReturn>
{{
    // Implemente sua lógica
    public async Task<@fluentresultsReturn> Handle({commandEntity.Type}{commandEntity.Name}Command request, CancellationToken cancellationToken)
    {{
        @fluentresultsHandle
    }}
}}
".Trim();
        
        conteudo = conteudo.Replace("@fluentresultsUsing", commandEntity.FluentResult ? "using FluentResults;" : "");
        conteudo = conteudo.Replace("@fluentresultsHandle", commandEntity.FluentResult ? "return Result.Ok(Guid.NewGuid());" : "throw new NotImplementedException();");
        conteudo = conteudo.Replace("@fluentresultsReturn", commandEntity.FluentResult ? "Result<Guid>" : "Guid");

        
        string filePath = Path.Combine(commandEntity.PathMain, pathCommand, $"{commandEntity.Type}{commandEntity.Name}CommandHandler.cs");
        await File.WriteAllTextAsync(filePath, conteudo);
        
        Console.WriteLine($"Feature '{commandEntity.Name}' criada em {filePath}");
    }
}