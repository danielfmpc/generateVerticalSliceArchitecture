using System.Xml.Linq;
using Shared.Commands;
using Shared.Endpoints;
using Shared.Entities;
using Shared.Queries;

namespace GenerateVerticalSliceArchitecture.FluentResult
{
    class Program
    {
        static async Task Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Uso: gvsa-fluent-results <NomeDaFeature>");
                return;
            }

            string name = args[0];
            
            var currentDir = Directory.GetCurrentDirectory();

            string pathMain = Path.Combine(currentDir, "Features", name);
            
            Directory.CreateDirectory(pathMain);

            // Tenta pegar o namespace base do projeto atual
            string namespaceBase = GetRootNamespace(currentDir);

            // string fullNamespace = $"{namespaceBase}.{name}";
            await Task.WhenAll(
                Task.Run(() => Command.GenerateCommand(new CommandEntity(namespaceBase, name, pathMain, true))),
                Task.Run(() => Endpoint.GenerateEndpoint(new EndpointEntity(namespaceBase, name, pathMain, true))),
                Task.Run(() => Query.GenerateQueries(new QueryEntity(namespaceBase, name, pathMain, true)))
            );

        }

        static string GetRootNamespace(string projectDir)
        {
            // var teste = Path.GetDirectoryName();
            var csprojFile = Directory.GetFiles(projectDir, "*.csproj").FirstOrDefault();

            if (csprojFile == null)
                return null;

            try
            {
                var xml = XDocument.Load(csprojFile);
                var rootNamespace = xml.Descendants("RootNamespace").FirstOrDefault()?.Value;
                
                if (!string.IsNullOrEmpty(rootNamespace))
                    return rootNamespace;

                // Se não achar, retorna o nome do arquivo .csproj como fallback
                return Path.GetFileNameWithoutExtension(csprojFile);
            }
            catch
            {
                return null;
            }
        }
        
        static string Capitalizar(string texto)
        {
            if (string.IsNullOrEmpty(texto))
                return texto;

            return char.ToUpper(texto[0]) + texto.Substring(1);
        }
    }
}