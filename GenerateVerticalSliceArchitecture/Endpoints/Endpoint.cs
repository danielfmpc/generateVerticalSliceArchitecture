using System;
using System.IO;

namespace GenerateVerticalSliceArchitecture.Endpoints;

public static class Endpoint
{
    public static void GenerateEndpoint(string namespaceBase, string name, string pathMain)
    {
        string pathEndpoints = Path.Combine(pathMain, "Endpoints");
            
        Directory.CreateDirectory(pathEndpoints);
        Console.WriteLine("Begin first task...");
        GenereteCommand(namespaceBase, name, pathMain);
    }
    private static void GenereteCommand(string namespaceBase, string name, string pathMain)
    {
        string pathCommand = "Endpoints";
        string conteudo =
$@"using MinDiator;
using Microsoft.AspNetCore.Mvc;
using {namespaceBase}.Features.{name}.Queries;
using {namespaceBase}.Features.{name}.Commands;
namespace {namespaceBase}.Features.{name}.{pathCommand};

// Adicione sua implementação nos metodos http
// Impletação feita de forma generica

public static class {name}Endpoint
{{
    public static void MapToDo(this IEndpointRouteBuilder endpoints)
    {{
        endpoints.MapGet(""/{name.ToLower()}"", async ([FromServices]IMediator mediator) =>
        {{
            var query = new Get{name}ListQuery();

            if(result is null) return Results.NotFound(result.Errors);

            return Results.Ok(result.Value);
        }});

        endpoints.MapGet(""/{name.ToLower()}/{{id:guid}}"", async (Guid id, [FromServices]IMediator mediator) =>
        {{
            var query = new Get{name}ByIdQuery(id);
            var result = await mediator.Send(query);

            if(result is null) return Results.NotFound(result.Errors);

            return Results.Ok(result.Value);
        }});

        endpoints.MapPost(""/{name.ToLower()}"", async ([FromBody]Create{name}Command command, [FromServices]IMediator mediator) =>
        {{
            var result = await mediator.Send(command);
            return result.IsSuccess ? Results.Ok(result.Value) : Results.BadRequest(result.Errors);
        }});

        endpoints.MapPut(""/{name.ToLower()}"", async ([FromBody]Update{name}Command command, [FromServices]IMediator mediator) =>
        {{
            var result = await mediator.Send(command);

            if(result is null) return Results.NotFound(result.Errors);

            return Results.Ok(result.Value);
        }});
    }}
}}
".Trim();
        
        string filePath = Path.Combine(pathMain, pathCommand, $"{name}Endpoint.cs");
        File.WriteAllText(filePath, conteudo);
        
        Console.WriteLine($"Feature '{name}' criada em {filePath}");
    }
}