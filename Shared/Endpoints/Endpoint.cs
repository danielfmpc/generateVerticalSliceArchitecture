using Shared.Entities;

namespace Shared.Endpoints;

public static class Endpoint
{
    public static async Task GenerateEndpoint(EndpointEntity endpointEntity)
    {
        string pathEndpoints = Path.Combine(endpointEntity.PathMain, "Endpoints");
            
        Directory.CreateDirectory(pathEndpoints);
        Console.WriteLine("Begin first task...");
        GenereteCommand(endpointEntity);
    }
    private static async Task GenereteCommand(EndpointEntity endpointEntity)
    {
        string pathCommand = "Endpoints";
        string conteudo =
$@"using MinDiator;
using Microsoft.AspNetCore.Mvc;
using {endpointEntity.NamespaceBase}.Features.{endpointEntity.Name}.Queries;
using {endpointEntity.NamespaceBase}.Features.{endpointEntity.Name}.Commands;
namespace {endpointEntity.NamespaceBase}.Features.{endpointEntity.Name}.{pathCommand};

// Adicione sua implementação nos metodos http
// Impletação feita de forma generica

public static class {endpointEntity.Name}Endpoint
{{
    public static void Map{endpointEntity.Name}(this IEndpointRouteBuilder endpoints)
    {{
        endpoints.MapGet(""/{endpointEntity.Name.ToLower()}"", async ([FromServices]IMediator mediator) =>
        {{
            var query = new Get{endpointEntity.Name}ListQuery();
            
            var result = await mediator.Send(query);

            if(@fluentresultIf) return Results.NotFound(@fluentresultError);

            return Results.Ok(@fluentresultValue);
        }});

        endpoints.MapGet(""/{endpointEntity.Name.ToLower()}/{{id:guid}}"", async (Guid id, [FromServices]IMediator mediator) =>
        {{
            var query = new Get{endpointEntity.Name}ByIdQuery(id);
            var result = await mediator.Send(query);

            if(@fluentresultIf) return Results.NotFound(@fluentresultError);

            return Results.Ok(@fluentresultValue);
        }});

        endpoints.MapPost(""/{endpointEntity.Name.ToLower()}"", async ([FromBody]Create{endpointEntity.Name}Command command, [FromServices]IMediator mediator) =>
        {{
            var result = await mediator.Send(command);

            if(@fluentresultIf) return Results.NotFound(@fluentresultError);

            return Results.Ok(@fluentresultValue);        }});

        endpoints.MapPut(""/{endpointEntity.Name.ToLower()}"", async ([FromBody]Update{endpointEntity.Name}Command command, [FromServices]IMediator mediator) =>
        {{
            var result = await mediator.Send(command);

            if(@fluentresultIf) return Results.NotFound(@fluentresultError);

            return Results.Ok(@fluentresultValue);
        }});
    }}
}}
".Trim();
        conteudo = conteudo.Replace("@fluentresultIf", endpointEntity.FluentResult ? "result.IsFailed" : "result is null");
        conteudo = conteudo.Replace("@fluentresultValue", endpointEntity.FluentResult ? "result.Value" : "");
        conteudo = conteudo.Replace("@fluentresultError", endpointEntity.FluentResult ? "result.Errors" : "");


        string filePath = Path.Combine(endpointEntity.PathMain, pathCommand, $"{endpointEntity.Name}Endpoint.cs");
        File.WriteAllText(filePath, conteudo);
        
        Console.WriteLine($"Feature '{endpointEntity.Name}' criada em {filePath}");
    }
}